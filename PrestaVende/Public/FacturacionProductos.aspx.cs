using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class FacturacionProductos : System.Web.UI.Page
    {
        #region variables
        private CLASS.cs_manejo_inventario cs_manejo_inventario = new CLASS.cs_manejo_inventario();
        private CLASS.cs_serie cs_serie = new CLASS.cs_serie();

        static string error="";

        private static DataTable dtTablaArticulos;
        DataRow row = null;
        #endregion

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
                        dtTablaArticulos = new DataTable("tablaJoyas");
                        ViewState["CurrentTableJoyas"] = dtTablaArticulos;
                        setColumnsArticulo();
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
        private void getProductos()
        {
            try
            {
                DataTable dtComprobar = new DataTable();
                dtComprobar = cs_manejo_inventario.getArticulos(ref error, txtBusqueda.Text.ToString());
                if (dtComprobar.Rows.Count <= 0)
                {
                    showWarning("No se encontro ningun articulo con este numero de prestamo.");
                }
                else
                {
                    ddlArticulos.DataSource = dtComprobar;
                    ddlArticulos.DataValueField = "id_inventario";
                    ddlArticulos.DataTextField = "descripcion_producto";
                    ddlArticulos.DataBind();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validaNumeroPrestamo()
        {
            try
            {
                if (txtBusqueda.Text.ToString().Length <= 0)
                {
                    showWarning("Debe ingresar un numero de prestamo para buscar.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void AgregaArticuloAGrid()
        {
            try
            {
                if (ViewState["CurrentTableJoyas"] != null)
                {
                    DataTable ArticuloCompleto = new DataTable("TablaArticuloCompleto");

                    ArticuloCompleto = cs_manejo_inventario.getArticuloEspecifico(ref error, txtBusqueda.Text.ToString(), ddlArticulos.SelectedValue.ToString());

                    foreach (DataRow item in ArticuloCompleto.Rows)
                    {
                        row = dtTablaArticulos.NewRow();
                        row["id_inventario"] = item["id_inventario"].ToString();
                        row["numero_linea"] = item["numero_linea"].ToString();
                        row["numero_prestamo"] = item["numero_prestamo"].ToString();
                        row["producto"] = item["producto"].ToString();
                        row["marca"] = item["marca"].ToString();
                        row["monto_prestado"] = item["monto_prestado"].ToString();
                        row["valor"] = item["valor"].ToString();
                        row["caracteristicas"] = item["caracteristicas"].ToString();
                        row["subTotal"] = item["subTotal"].ToString();
                        row["IVA"] = item["IVA"].ToString();
                    }

                    dtTablaArticulos.Rows.Add(row);
                    ViewState["CurrentTableJoyas"] = dtTablaArticulos;
                    gvProductoFacturar.DataSource = dtTablaArticulos;
                    gvProductoFacturar.DataBind();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void setColumnsArticulo()
        {
            try
            {
                dtTablaArticulos.Columns.Add("id_inventario");
                dtTablaArticulos.Columns.Add("numero_linea");
                dtTablaArticulos.Columns.Add("numero_prestamo");
                dtTablaArticulos.Columns.Add("producto");
                dtTablaArticulos.Columns.Add("marca");
                dtTablaArticulos.Columns.Add("monto_prestado");
                dtTablaArticulos.Columns.Add("valor");
                dtTablaArticulos.Columns.Add("caracteristicas");
                dtTablaArticulos.Columns.Add("subTotal");
                dtTablaArticulos.Columns.Add("IVA");
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void crearFactura()
        {
            try
            {
                if (validaDatos())
                {
                    preparaDatosFacturacion();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool validaDatos()
        {
            try
            {
                if (lblIdCliente.Text.ToString().Length <= 0 || lblNombreCliente.Text.ToString().Length <= 0 || lblNombreCliente.Text.ToString() == "")
                {
                    showWarning("Usted no ha seleccionado un cliente.");
                    return false;
                }
                else if (gvProductoFacturar.Rows.Count <= 0)
                {
                    showWarning("Se deben agregar los productos antes de facturar.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void preparaDatosFacturacion()
        {
            try
            {
                string[] encabezado = new string[11];
                decimal subTotal = 0, IVA = 0;
                int id_factura_encabezado = 0;
                foreach (DataRow item in dtTablaArticulos.Rows)
                {
                    subTotal += Convert.ToDecimal(item["subTotal"]);
                    IVA += Convert.ToDecimal(item["IVA"]);
                }

                encabezado[0] = ddlSerie.SelectedValue;
                encabezado[1] = lblNumeroFactura.Text.ToString();
                encabezado[2] = lblIdCliente.Text;
                encabezado[3] = lblTotalFacturaNumero.Text;
                encabezado[4] = subTotal.ToString();
                encabezado[5] = IVA.ToString();
                encabezado[6] = "13";
                encabezado[7] = Session["id_caja"].ToString();
                encabezado[8] = "";
                encabezado[9] = "0";
                encabezado[10] = "0";

                if (cs_manejo_inventario.GuardarFactura(ref error, dtTablaArticulos, encabezado, ref id_factura_encabezado))
                {
                    showSuccess("Factura guardada correctamente.");
                    string script = "window.open('WebReport.aspx?tipo_reporte=2" + "&id_factura=" + id_factura_encabezado.ToString() + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "ImpresionFactura", script, true);

                    string script2 = "window.open('WebReport.aspx?tipo_reporte=5" + "&id_factura=" + id_factura_encabezado.ToString() + "&id_sucursal=" + Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]) + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "ImpresionRecibo", script2, true);

                    string scriptText = "alert('my message'); window.location='FacturacionProductos.aspx'";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
                }
                else
                {
                    showError("No se pudo guardar la factura correctamente, error en " + error);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void calculaTotalFactura()
        {
            try
            {
                decimal totalFactura = 0;
                if (gvProductoFacturar.Rows.Count > 0 && dtTablaArticulos.Rows.Count > 0)
                {
                    foreach (DataRow item in dtTablaArticulos.Rows)
                    {
                        totalFactura += Convert.ToDecimal(item["valor"].ToString());
                    }
                    lblTotalFacturaNumero.Text = totalFactura.ToString();
                }
                else
                {
                    lblTotalFacturaNumero.Text = totalFactura.ToString(); 
                }
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
                int id_sucursal = Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]);
                ddlSerie.DataSource = cs_serie.getSerieDDL(ref error, id_sucursal);
                ddlSerie.DataValueField = "id_serie";
                ddlSerie.DataTextField = "serie";
                ddlSerie.DataBind();

                int id_serie = int.Parse(ddlSerie.SelectedValue.ToString());

                if (id_serie > 0)
                    lblNumeroFactura.Text = (cs_serie.getCorrelativoSerie(ref error, id_serie) + 1).ToString();
                else
                    lblNumeroFactura.Text = "0";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        private void aplicarDescuento()
        {
            try
            {
                decimal montoFila = 0, porcentaje = 0, montoDescuento = 0;
                if (gvProductoFacturar.Rows.Count > 0)
                {
                    foreach (GridViewRow item in gvProductoFacturar.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item.Cells[7].Text.ToString()) / Convert.ToDecimal(lblTotalFacturaNumero.Text.ToString()), 4);
                        montoDescuento = porcentaje * Convert.ToDecimal(txtMontoARecalcular.Text.ToString());
                        montoFila = Convert.ToDecimal(item.Cells[7].Text.ToString()) - montoDescuento;
                        montoFila = Math.Round(montoFila, 2);
                        item.Cells[7].Text = montoFila.ToString();
                    }
                    montoFila = 0;
                    porcentaje = 0;
                    montoDescuento = 0;

                    foreach (DataRow item in dtTablaArticulos.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item["valor"].ToString()) / Convert.ToDecimal(lblTotalFacturaNumero.Text.ToString()), 4);
                        montoDescuento = porcentaje * Convert.ToDecimal(txtMontoARecalcular.Text.ToString());
                        montoFila = Convert.ToDecimal(item["valor"].ToString()) - montoDescuento;
                        montoFila = Math.Round(montoFila, 2);
                        item["valor"] = montoFila.ToString();
                    }
                    calculaTotalFactura();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validaArticuloAgregado()
        {
            try
            {
                int contadorDuplicados = 0;
                string id = ddlArticulos.SelectedValue.ToString();
                foreach (GridViewRow item in gvProductoFacturar.Rows)
                {
                    if (item.Cells[1].Text.ToString().Equals(id))
                    {
                        contadorDuplicados++;
                    }
                }
                if (contadorDuplicados == 0)
                {
                    
                    return true;
                }
                else
                {
                    showWarning("No se puede agregar 2 veces el mismo articulo para la venta.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void borrarItem()
        {
            try
            {
                string itemValue = ddlArticulos.Text.ToString();
                if (ddlArticulos.Items.FindByValue(itemValue) != null)
                {
                    string itemText = ddlArticulos.Items.FindByValue(itemValue).Text;
                    ListItem li = new ListItem();
                    li.Text = itemText;
                    li.Value = itemValue;
                    //Label1.Text = "Item Found and remove: " + itemText;
                    ddlArticulos.Items.Remove(li);
                }
                else
                {
                    //Label1.Text = "Item not Found, Value: " + itemValue;
                }

                //ddlArticulos.Items.Remove(ddlArticulos.SelectedValue);
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaNumeroPrestamo())
                {
                    getProductos();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }   
        }

        protected void btnAceptCliente_Click(object sender, EventArgs e)
        {
            try
            {
                lblIdCliente.Text = CLASS.cs_cliente.Id_cliente;
                lblNombreCliente.Text = CLASS.cs_cliente.Nombre_cliente;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
        
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaArticuloAgregado())
                {
                    AgregaArticuloAGrid();
                    calculaTotalFactura();
                    borrarItem();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void tbnFacturar_Click(object sender, EventArgs e)
        {
            crearFactura();
        }

        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id_serie = int.Parse(ddlSerie.SelectedValue.ToString());

                if (id_serie > 0)
                    lblNumeroFactura.Text = (cs_serie.getCorrelativoSerie(ref error, id_serie) + 1).ToString();
                else
                    lblNumeroFactura.Text = "0";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        protected void gvProductoFacturar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "borrar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    dtTablaArticulos.Rows[index].Delete();
                    gvProductoFacturar.DataSource = dtTablaArticulos;
                    gvProductoFacturar.DataBind();
                    calculaTotalFactura();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnAplicaDescuento_Click(object sender, EventArgs e)
        {
            aplicarDescuento();
            calculaTotalFactura();
        }
    }
}