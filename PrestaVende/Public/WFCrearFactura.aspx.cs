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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && Convert.ToInt32(Session["id_usuario"]) == 0)
                {
                    Response.Redirect("~/WFWebLogin.aspx");
                }
                else
                {
                    if (IsPostBack)
                    {
                        if (lblWarning.Text == "") { divWarning.Visible = false; }
                        if (lblError.Text == "") { divError.Visible = false; }
                        if (lblSuccess.Text == "") { divSucceful.Visible = false; }
                    }
                    else
                    {
                        cs_prestamo = new CLASS.cs_prestamo();
                        cs_factura = new CLASS.cs_factura();
                        cs_transaccion = new CLASS.cs_transaccion();
                        cs_serie = new CLASS.cs_serie();
                        ds_global = new DataSet();
                        Session["dsGlobal"] = ds_global;

                        getDetalleFactura();
                        getPrestamo();
                        getTransaccion();
                        getSeries();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #region funciones

        private void getPrestamo()
        {
            try
            {
                string id_prestamo = this.Request.QueryString["id_prestamo"];
                cs_prestamo = new CLASS.cs_prestamo();

                foreach (DataRow item in cs_prestamo.ObtenerPrestamoEspecifico(ref error, id_prestamo).Rows)
                {
                    //lblnombre_prestamo.Text = item[1].ToString() + " - Cliente: " + item[2].ToString() + " " + item[3].ToString() + " " + item[4].ToString() + " " + item[5].ToString();
                    this.lblNombrePrestamo.Text = item["numero_prestamo"].ToString();
                    this.lblCodigoCliente.Text = item["id_cliente"].ToString();
                    this.lblNombreCliente.Text = item["primer_nombre"].ToString() + " " + item["segundo_nombre"].ToString() + " " + item["primer_apellido"].ToString() + " " + item["segundo_apellido"].ToString();
                    this.lblValorInteres.Text = item["factor"].ToString() + "%";

                    string tipo_transaccion = getEquivalenteTransaccion(this.Request.QueryString["id_tipo"]);
                    this.lblSaldoPrestamoNumero.Text = item["saldo_prestamo"].ToString();

                    if (tipo_transaccion == "10")
                    {
                        this.txtAbonoCapital.Text = item["saldo_prestamo"].ToString();
                        this.lblAbonoCapital.Text = "MONTO CANCELACION";
                        this.imgBtnBuscaSubSemana.Visible = false;
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
                    this.lblAbonoCapital.Visible = false;
                    this.txtAbonoCapital.Visible = false;
                    break;
                case "2":
                    id_tipo_transaccion = "9";
                    break;
                case "3":
                    id_tipo_transaccion = "10";
                    this.txtAbonoCapital.Enabled = false;
                    break;
            }

            return id_tipo_transaccion;
        }

        private void getTransaccion()
        {
            try
            {
                string id_tipo_transaccion = getEquivalenteTransaccion(this.Request.QueryString["id_tipo"]);

                cs_transaccion = new CLASS.cs_transaccion();
                foreach (DataRow item in cs_transaccion.ObtenerTransaccion(ref error, id_tipo_transaccion).Rows)
                    this.lblTransaccion.Text = item[1].ToString();                                
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
                cs_serie = new CLASS.cs_serie();
                int id_sucursal = Convert.ToInt32(Session["id_sucursal"]);
                this.ddlSerie.DataSource = cs_serie.getSerieDDL(ref error,id_sucursal);
                this.ddlSerie.DataValueField = "id_serie";
                this.ddlSerie.DataTextField = "serie";
                this.ddlSerie.DataBind();

                int id_serie = int.Parse(this.ddlSerie.SelectedValue.ToString());

                if (id_serie > 0)
                {
                    cs_serie = new CLASS.cs_serie();
                    this.lblNumeroPrestamoNumeroFactura.Text = (cs_serie.getCorrelativoSerie(ref error, id_serie) + 1).ToString();
                }
                else
                    this.lblNumeroPrestamoNumeroFactura.Text = "0";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        private void getDetalleFactura()
        {
            try
            {
                DataSet getDataSetActual = (DataSet)this.Session["dsGlobal"];

                string id_prestamo = Request.QueryString["id_prestamo"];
                cs_factura = new CLASS.cs_factura();

                getDataSetActual = cs_factura.ObtenerDetalleFacturas(ref error, id_prestamo);

                if (getDataSetActual.Tables.Count > 0)
                {
                    this.gvDetalleFactura.DataSource = getDataSetActual.Tables[0];
                    this.gvDetalleFactura.DataBind();

                    DataTable dt = new DataTable();

                    dt = getDataSetActual.Tables[1];

                    this.lblSubTotalFactura.Text = dt.Rows[0]["SubTotal"].ToString();
                    this.lblIVAFactura.Text = dt.Rows[0]["IVA"].ToString();
                    this.lblTotalFacturaV.Text = dt.Rows[0]["Total"].ToString();

                    string id_tipo_transaccion = getEquivalenteTransaccion(this.Request.QueryString["id_tipo"]);
                    decimal total_cobro = 0;
                    
                    if (id_tipo_transaccion == "9" || id_tipo_transaccion == "10")
                    {
                        if (this.txtAbonoCapital.Text == "")
                        {
                            this.txtAbonoCapital.Text = "0";
                        }
                        
                        total_cobro = Convert.ToDecimal(dt.Rows[0]["Total"].ToString()) + Convert.ToDecimal(this.txtAbonoCapital.Text.ToString());
                        this.lblTotalCobro.Text = total_cobro.ToString();
                    }
                    else
                    {
                        this.lblTotalCobro.Text = dt.Rows[0]["Total"].ToString();
                    }

                    int semanas = int.Parse(getDataSetActual.Tables[0].Rows[0]["Cantidad"].ToString());
                    DataTable TablaSemanas = new DataTable();
                    TablaSemanas.Columns.Add("id", typeof(int));
                    TablaSemanas.Columns.Add("nombre", typeof(string));

                    for (int i = semanas; i > 0; i--)
                    {
                        TablaSemanas.Rows.Add(i, i.ToString());
                    }

                    this.ddlSemanas.DataSource = TablaSemanas;
                    this.ddlSemanas.DataValueField = "id";
                    this.ddlSemanas.DataTextField = "nombre";
                    this.ddlSemanas.DataBind();
                    this.Session["dsGlobal"] = getDataSetActual;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #endregion

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
                int id_serie = int.Parse(this.ddlSerie.SelectedValue.ToString());

                if (id_serie > 0)
                { 
                    cs_serie = new CLASS.cs_serie();
                    this.lblNumeroPrestamoNumeroFactura.Text = (cs_serie.getCorrelativoSerie(ref error, id_serie) + 1).ToString();
                }
                else
                    this.lblNumeroPrestamoNumeroFactura.Text = "0";
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
                    this.ddlSemanas.Enabled = true;
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
                string id_serie = this.ddlSerie.SelectedValue.ToString();

                if (int.Parse(id_serie) > 0)
                {
                    if(Convert.ToInt32(this.Session["id_caja"]) > 0)
                    {
                        if (Convert.ToInt32(this.Session["id_tipo_caja"]) == 2)
                        {
                            DataSet DataSActual = (DataSet)this.Session["dsGlobal"];
                            decimal abono = 0, saldo_prestamo_actual = 0;
                            int id_recibo = 0;
                            bool abonoB = false;

                            abonoB = decimal.TryParse(this.txtAbonoCapital.Text, out abono);

                            if((this.txtAbonoCapital.Visible == true && abono >= 5) || (this.txtAbonoCapital.Visible == false))
                            {
                                if (this.txtAbonoCapital.Text.ToString() == "" || this.txtAbonoCapital.Text.ToString().Length <= 0)
                                    saldo_prestamo_actual = 0;
                                else
                                    saldo_prestamo_actual = decimal.Parse(this.txtAbonoCapital.Text.ToString());
                                
                                decimal lSaldo_prestamo = Convert.ToDecimal(this.lblSaldoPrestamoNumero.Text.ToString());
                                string id_tipo_transaccion = getEquivalenteTransaccion(this.Request.QueryString["id_tipo"]);

                                if ((this.txtAbonoCapital.Visible == true && abono < lSaldo_prestamo && id_tipo_transaccion == "9") || (this.txtAbonoCapital.Visible == false) 
                                      || (this.txtAbonoCapital.Visible == true && abono == lSaldo_prestamo && id_tipo_transaccion == "10"))
                                {                                    
                                    string numero_prestamo = this.lblNombrePrestamo.Text;
                                    string Resultado = "";
                                    string id_prestamo = Request.QueryString["id_prestamo"];

                                    cs_factura = new CLASS.cs_factura();
                                    error = "";
                                    Resultado = cs_factura.GuardarFactura(ref error, DataSActual, id_serie, this.lblCodigoCliente.Text.ToString(), id_tipo_transaccion, Convert.ToInt32(this.Session["id_caja"]), numero_prestamo, abono.ToString(), ref id_recibo);
                                     
                                    if(Resultado == "")
                                    {
                                        showWarning("Error al generar la factura. " + error);
                                    } else
                                    {
                                        try
                                        {
                                            Session["saldo_caja"] = Convert.ToString(Convert.ToDecimal(this.Session["saldo_caja"].ToString()) + abono + Convert.ToDecimal(DataSActual.Tables[1].Rows[0]["Total"].ToString().Replace(",", ".")));
                                        }
                                        catch (Exception ex)
                                        {
                                            showWarning("Error sumando saldo a caja asignada, salga del sistema y vuelva ingresar." + ex.ToString());
                                        }
                                        
                                        string script = "window.open('WebReport.aspx?tipo_reporte=2" + "&id_factura=" + Resultado + "&id_sucursal=" + this.Session["id_sucursal"].ToString() + "&numero_contrato=" + this.lblNombrePrestamo.Text.ToString() + "');";
                                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "ImpresionFactura", script, true);

                                        if (id_tipo_transaccion == "9" || id_tipo_transaccion == "10")
                                        {
                                            string script2 = "window.open('WebReport.aspx?tipo_reporte=5" + "&id_recibo=" + id_recibo.ToString() + "&id_sucursal=" + this.Session["id_sucursal"].ToString() + "');";
                                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ImpresionRecibo", script2, true);
                                        }                                        

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
            try
            {
                DataSet dataSActual = (DataSet)this.Session["dsGlobal"];
                int semanas = int.Parse(this.ddlSemanas.SelectedValue.ToString());
                int dias = semanas * 7;
                int dias_plazo = int.Parse(dataSActual.Tables[0].Rows[0]["dias_plan"].ToString());

                for (int i = 0; i < dataSActual.Tables[0].Rows.Count; i++)
                {
                    String[] separador = { "/" };
                    String cadena = dataSActual.Tables[0].Rows[i]["fecha_ultimo_pago"].ToString();

                    String[] strlist = cadena.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);

                    int dia = int.Parse(strlist[0]);
                    int mes = int.Parse(strlist[1]);
                    int anio = int.Parse(strlist[2]);

                    DateTime FechaUltimoPago = new DateTime(anio, mes, dia);

                    dataSActual.Tables[0].Rows[i]["calculo_fecha_ultimo_pago"] = FechaUltimoPago.AddDays(dias).ToString("dd/MM/yyyy");
                    dataSActual.Tables[0].Rows[i]["calculo_fecha_proximo_pago"] = FechaUltimoPago.AddDays(dias + dias_plazo).ToString("dd/MM/yyyy");

                    if (dataSActual.Tables[0].Rows[i]["cargo"].ToString().ToLower() != "mora")
                    {
                        dataSActual.Tables[0].Rows[i]["Subtotal"] = (decimal.Parse(dataSActual.Tables[0].Rows[i]["Precio"].ToString()) * semanas).ToString();
                        dataSActual.Tables[0].Rows[i]["IVA"] = (decimal.Parse(dataSActual.Tables[0].Rows[i]["Subtotal"].ToString()) *
                                                                decimal.Parse(dataSActual.Tables[0].Rows[i]["factor_impuesto"].ToString()) / 100).ToString();
                        dataSActual.Tables[0].Rows[i]["Cantidad"] = semanas.ToString();
                    }
                }

                dataSActual.Tables[1].Rows[0]["Total"] = Decimal.Round(dataSActual.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("SubTotal")), 2).ToString();
                dataSActual.Tables[1].Rows[0]["IVA"] = Decimal.Round(dataSActual.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("IVA")), 2).ToString();
                dataSActual.Tables[1].Rows[0]["SubTotal"] = Decimal.Round(decimal.Parse(dataSActual.Tables[1].Rows[0]["Total"].ToString()) - decimal.Parse(dataSActual.Tables[1].Rows[0]["IVA"].ToString()), 2).ToString();

                this.lblSubTotalFactura.Text = dataSActual.Tables[1].Rows[0]["SubTotal"].ToString();
                this.lblIVAFactura.Text = dataSActual.Tables[1].Rows[0]["IVA"].ToString();
                this.lblTotalFacturaV.Text = dataSActual.Tables[1].Rows[0]["Total"].ToString();
                this.gvDetalleFactura.DataSource = dataSActual.Tables[0];
                this.gvDetalleFactura.DataBind();

                string id_tipo_transaccion = getEquivalenteTransaccion(this.Request.QueryString["id_tipo"]);
                decimal total_cobro = 0;

                if (id_tipo_transaccion == "9" || id_tipo_transaccion == "10")
                {
                    if (this.txtAbonoCapital.Text == "")
                    {
                        this.txtAbonoCapital.Text = "0";
                    }
                    total_cobro = Convert.ToDecimal(dataSActual.Tables[1].Rows[0]["Total"].ToString()) + Convert.ToDecimal(this.txtAbonoCapital.Text.ToString());
                    this.lblTotalCobro.Text = total_cobro.ToString();
                }
                else
                {
                    this.lblTotalCobro.Text = dataSActual.Tables[1].Rows[0]["Total"].ToString();
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
            
        }

        protected void txtAbonoCapital_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet getDataSetActual = (DataSet)this.Session["dsGlobal"];

                if (getDataSetActual.Tables.Count > 0)
                {
                    DataTable dt = new DataTable();

                    dt = getDataSetActual.Tables[1];

                    string id_tipo_transaccion = getEquivalenteTransaccion(this.Request.QueryString["id_tipo"]);
                    decimal total_cobro = 0;

                    if (id_tipo_transaccion == "9" || id_tipo_transaccion == "10")
                    {
                        if (this.txtAbonoCapital.Text == "")
                        {
                            this.txtAbonoCapital.Text = "0";
                        }
                        total_cobro = Convert.ToDecimal(dt.Rows[0]["Total"].ToString()) + Convert.ToDecimal(this.txtAbonoCapital.Text.ToString());
                        this.lblTotalCobro.Text = total_cobro.ToString();
                    }
                    else
                    {
                        this.lblTotalCobro.Text = dt.Rows[0]["Total"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
    }
}
