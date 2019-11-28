using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFCrearFactura : System.Web.UI.Page
    {
        private CLASS.cs_prestamo cs_prestamo = new CLASS.cs_prestamo();
        private CLASS.cs_factura cs_factura = new CLASS.cs_factura();
        private CLASS.cs_transaccion cs_transaccion = new CLASS.cs_transaccion();
        private CLASS.cs_serie cs_serie = new CLASS.cs_serie();
        private static DataSet ds_global = new DataSet();
        private string error = "";
        private static string id_cliente = "0";
        private static string saldo_prestamo = "0";

        #region funciones

        private void getPrestamo()
        {
            try
            {
                string id_prestamo = Request.QueryString["id_prestamo"];
                foreach (DataRow item in cs_prestamo.ObtenerPrestamoEspecifico(ref error, id_prestamo).Rows)
                {
                    //lblnombre_prestamo.Text = item[1].ToString() + " - Cliente: " + item[2].ToString() + " " + item[3].ToString() + " " + item[4].ToString() + " " + item[5].ToString();
                    lblNombrePrestamo.Text = item[1].ToString();
                    lblCodigoCliente.Text = item[1].ToString();
                    lblNombreCliente.Text = item[2].ToString() + " " + item[3].ToString() + " " + item[4].ToString() + " " + item[5].ToString();
                    lblValorInteres.Text = item[8].ToString() + "%";
                    id_cliente = item[6].ToString();
                    saldo_prestamo = item[7].ToString();

                    //if (decimal.Parse(saldo_prestamo = item[8].ToString()) > 500)
                    //    imgBtnBuscaSubSemana.Visible = true;
                    //else
                    //    imgBtnBuscaSubSemana.Visible = false;

                    string tipo_transaccion = getEquivalenteTransaccion(Request.QueryString["id_tipo"]);

                    if(tipo_transaccion == "10")
                    {
                        txtAbonoCapital.Text = saldo_prestamo;
                        lblAbonoCapital.Text = "MONTO CANCELACION";
                        imgBtnBuscaSubSemana.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private string getEquivalenteTransaccion(string transaccion)
        {
            string id_tipo_transaccion = "0";

            switch (transaccion)
            {
                case "1":
                    id_tipo_transaccion = "8";
                    lblAbonoCapital.Visible = false;
                    txtAbonoCapital.Visible = false;
                    break;
                case "2":
                    id_tipo_transaccion = "9";
                    break;
                case "3":
                    id_tipo_transaccion = "10";
                    txtAbonoCapital.Enabled = false;
                    break;
            }

            return id_tipo_transaccion;
        }

        private void getTransaccion()
        {
            try
            {
                string id_tipo_transaccion = getEquivalenteTransaccion(Request.QueryString["id_tipo"]);
                
                foreach (DataRow item in cs_transaccion.ObtenerTransaccion(ref error, id_tipo_transaccion).Rows)                
                    lblTransaccion.Text = item[1].ToString();                                
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getSeries()
        {
            try
            {
                int id_sucursal = CLASS.cs_usuario.id_sucursal;
                ddlSerie.DataSource = cs_serie.getSerieDDL(ref error,id_sucursal);
                ddlSerie.DataValueField = "id_serie";
                ddlSerie.DataTextField = "serie";
                ddlSerie.DataBind();

                int id_serie = int.Parse(ddlSerie.SelectedValue.ToString());

                if (id_serie > 0)
                    lblNumeroPrestamoNumeroFactura.Text = (cs_serie.getCorrelativoSerie(ref error, id_serie) + 1).ToString();
                else
                    lblNumeroPrestamoNumeroFactura.Text = "0";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        private void getDetalleFactura()
        {
            string id_prestamo = Request.QueryString["id_prestamo"];                     

            ds_global = cs_factura.ObtenerDetalleFacturas(ref error, id_prestamo);            

            if (ds_global.Tables.Count > 0)
            {
                gvDetalleFactura.DataSource = ds_global.Tables[0];
                gvDetalleFactura.DataBind();

                DataTable dt = new DataTable();

                dt = ds_global.Tables[1];

                lblSubTotalFactura.Text = dt.Rows[0]["SubTotal"].ToString();
                lblIVAFactura.Text = dt.Rows[0]["IVA"].ToString();
                lblTotalFacturaV.Text = dt.Rows[0]["Total"].ToString();
            }           

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

            }
            else
            {
                getDetalleFactura();
                getPrestamo();
                getTransaccion();
                getSeries();
            }
        }

        #region messages
        private bool showWarning(string warning)
        {
            divWarning.Visible = true;
            lblWarning.Controls.Add(new LiteralControl(string.Format("<span style='color:Orange'>{0}</span>", warning)));
            return true;
        }

        private bool showError(string error)
        {
            divError.Visible = true;
            lblError.Controls.Add(new LiteralControl(string.Format("<span style='color:Red'>{0}</span>", error)));
            return true;
        }

        private bool showSuccess(string error)
        {
            divSucceful.Visible = true;
            lblSuccess.Controls.Add(new LiteralControl(string.Format("<span style='color:Green'>{0}</span>", error)));
            return true;
        }
        #endregion

        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id_serie = int.Parse(ddlSerie.SelectedValue.ToString());

                if (id_serie > 0)
                    lblNumeroPrestamoNumeroFactura.Text = (cs_serie.getCorrelativoSerie(ref error, id_serie) + 1).ToString();
                else
                    lblNumeroPrestamoNumeroFactura.Text = "0";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }            
        }

        protected void btnAcept_Click(object sender, EventArgs e)
        {
            try
            {
                if (CLASS.cs_usuario.autorizado)
                {                    
                    CLASS.cs_usuario.autorizado = false;

                    int semanas = int.Parse(ds_global.Tables[0].Rows[0]["Cantidad"].ToString());
                    DataTable TablaSemanas = new DataTable();
                    TablaSemanas.Columns.Add("id", typeof(int));
                    TablaSemanas.Columns.Add("nombre", typeof(string));

                    for (int i = semanas; i > 0; i--)
                    {
                        TablaSemanas.Rows.Add(i, i.ToString());
                    }

                    ddlSemanas.DataSource = TablaSemanas;
                    ddlSemanas.DataValueField = "id";
                    ddlSemanas.DataTextField = "nombre";
                    ddlSemanas.DataBind();
                    ddlSemanas.Visible = true;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                string id_serie = ddlSerie.SelectedValue.ToString();

                if (int.Parse(id_serie) > 0)
                {
                    if(CLASS.cs_usuario.id_caja > 0)
                    {
                        if (CLASS.cs_usuario.id_tipo_caja == 2)
                        {
                            decimal abono = 0;
                            bool abonoB = false;

                            abonoB = decimal.TryParse(txtAbonoCapital.Text, out abono);

                            if((txtAbonoCapital.Visible == true && abono >= 5) || (txtAbonoCapital.Visible == false))
                            {
                                decimal lSaldo_prestamo = decimal.Parse(saldo_prestamo);
                                string id_tipo_transaccion = getEquivalenteTransaccion(Request.QueryString["id_tipo"]);

                                if ((txtAbonoCapital.Visible == true && abono < lSaldo_prestamo && id_tipo_transaccion == "9") || (txtAbonoCapital.Visible == false) 
                                      || (txtAbonoCapital.Visible == true && abono == lSaldo_prestamo && id_tipo_transaccion == "10"))
                                {                                    
                                    string numero_prestamo = lblNombrePrestamo.Text;
                                    string Resultado = "";
                                    string id_prestamo = Request.QueryString["id_prestamo"];

                                    Resultado = cs_factura.GuardarFactura(ref error, ds_global, id_serie, id_cliente, id_tipo_transaccion, CLASS.cs_usuario.id_caja, numero_prestamo, abono.ToString());
                                     
                                    if(Resultado == string.Empty)
                                    {
                                        showWarning("Error al generar la factura.");
                                    } else
                                    {
                                        showSuccess("Se creo prestamo correctamente.");
                                        string script = "window.open('WebReport.aspx?tipo_reporte=2" + "&id_factura=" + Resultado + "');";
                                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);

                                        string scriptText = "alert('my message'); window.location='WFFacturacion.aspx?id_prestamo=" + id_prestamo.ToString() + "'";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
                                    }

                                }
                                else
                                    showWarning("El abono ingresado no puede ser mayor o igual al saldo del prestamo.");
                            }
                            else
                                showWarning("El abono ingresado es incorrecto.");

                        } else
                            showWarning("El tipo de caja asignada no es del tipo correcto para realizar la operación.");

                    } else
                        showWarning("No tiene asignada una caja.");
                    
                } else
                    showWarning("Seleccione una serie.");

            }
            catch (Exception ex)
            {

                showWarning(ex.ToString() + " " + error);
            }
        }

        protected void imgBtnBuscaSubSemana_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        protected void ddlSemanas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int semanas = int.Parse(ddlSemanas.SelectedValue.ToString());
            int dias = semanas * 7;
            int dias_plazo = int.Parse(ds_global.Tables[0].Rows[0]["dias_plan"].ToString());

            for (int i = 0; i < ds_global.Tables[0].Rows.Count; i++)
            {
                DateTime FechaUltimoPago = Convert.ToDateTime(ds_global.Tables[0].Rows[i]["fecha_ultimo_pago"].ToString());
                ds_global.Tables[0].Rows[i]["calculo_fecha_ultimo_pago"] = FechaUltimoPago.AddDays(dias).ToString("dd/MM/yyyy");
                ds_global.Tables[0].Rows[i]["calculo_fecha_proximo_pago"] = FechaUltimoPago.AddDays(dias + dias_plazo).ToString("dd/MM/yyyy");

                if (ds_global.Tables[0].Rows[i]["cargo"].ToString().ToLower() != "mora")
                {
                    ds_global.Tables[0].Rows[i]["Subtotal"] = (decimal.Parse(ds_global.Tables[0].Rows[i]["Precio"].ToString()) * semanas).ToString();
                    ds_global.Tables[0].Rows[i]["IVA"] = (decimal.Parse(ds_global.Tables[0].Rows[i]["Subtotal"].ToString()) * 
                                                            decimal.Parse(ds_global.Tables[0].Rows[i]["factor_impuesto"].ToString()) / 100).ToString();
                    ds_global.Tables[0].Rows[i]["Cantidad"] = semanas.ToString();
                }
            }

            ds_global.Tables[1].Rows[0]["Total"] = Decimal.Round(ds_global.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("SubTotal")),2).ToString();
            ds_global.Tables[1].Rows[0]["IVA"] = Decimal.Round(ds_global.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("IVA")),2).ToString();
            ds_global.Tables[1].Rows[0]["SubTotal"] = Decimal.Round(decimal.Parse(ds_global.Tables[1].Rows[0]["Total"].ToString()) - decimal.Parse(ds_global.Tables[1].Rows[0]["IVA"].ToString()), 2).ToString();

            lblSubTotalFactura.Text = ds_global.Tables[1].Rows[0]["SubTotal"].ToString();
            lblIVAFactura.Text = ds_global.Tables[1].Rows[0]["IVA"].ToString();
            lblTotalFacturaV.Text = ds_global.Tables[1].Rows[0]["Total"].ToString();

            gvDetalleFactura.DataSource = ds_global.Tables[0];
            gvDetalleFactura.DataBind();
        }
    }
}
