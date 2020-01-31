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
                        setColumnsArticulo();
                        Session["CurrentTableJoyas"] = dtTablaArticulos;
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
                dtComprobar = cs_manejo_inventario.getArticulos(ref error, this.txtBusqueda.Text.ToString());
                if (dtComprobar.Rows.Count <= 0)
                {
                    showWarning("No se encontro ningun articulo con este numero de prestamo.");
                }
                else
                {
                    this.ddlArticulos.DataSource = dtComprobar;
                    this.ddlArticulos.DataValueField = "id_inventario";
                    this.ddlArticulos.DataTextField = "descripcion_producto";
                    this.ddlArticulos.DataBind();
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
                if (this.txtBusqueda.Text.ToString().Length <= 0)
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
                if ((DataTable)this.Session["CurrentTableJoyas"] != null)
                {
                    DataTable ArticuloCompleto = (DataTable)this.Session["CurrentTableJoyas"];
                    DataTable articuloViene = new DataTable("articuloViene");
                    cs_manejo_inventario = new CLASS.cs_manejo_inventario();
                    articuloViene = cs_manejo_inventario.getArticuloEspecifico(ref error, this.txtBusqueda.Text.ToString(), this.ddlArticulos.SelectedValue.ToString());

                    foreach (DataRow item in articuloViene.Rows)
                    {
                        row = ArticuloCompleto.NewRow();
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
                        row["valor_liquidado"] = item["valor_liquidado"].ToString();
                    }

                    ArticuloCompleto.Rows.Add(row);

                    dtTablaArticulos = ArticuloCompleto;
                    Session["CurrentTableJoyas"] = dtTablaArticulos;

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
                dtTablaArticulos.Columns.Add("valor_liquidado");
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
                if (this.lblIdCliente.Text.ToString().Length <= 0 || this.lblNombreCliente.Text.ToString().Length <= 0 || this.lblNombreCliente.Text.ToString() == "")
                {
                    showWarning("Usted no ha seleccionado un cliente.");
                    return false;
                }
                else if (this.gvProductoFacturar.Rows.Count <= 0)
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
                int id_factura_encabezado = 0, id_recibo = 0;
                DataTable dtSubTotalIVA = (DataTable)this.Session["CurrentTableJoyas"];
                foreach (DataRow item in dtSubTotalIVA.Rows)
                {
                    subTotal += Convert.ToDecimal(item["subTotal"]);
                    IVA += Convert.ToDecimal(item["IVA"]);
                }

                encabezado[0] = this.ddlSerie.SelectedValue;
                encabezado[1] = this.lblNumeroFactura.Text.ToString();
                encabezado[2] = this.lblIdCliente.Text;
                encabezado[3] = this.lblTotalFacturaNumero.Text;
                encabezado[4] = subTotal.ToString();
                encabezado[5] = IVA.ToString();
                encabezado[6] = "13";
                encabezado[7] = this.Session["id_caja"].ToString();
                encabezado[8] = "";
                encabezado[9] = "0";
                encabezado[10] = "0";

                cs_manejo_inventario = new CLASS.cs_manejo_inventario();
                error = "";
                if (cs_manejo_inventario.GuardarFactura(ref error, (DataTable)this.Session["CurrentTableJoyas"], encabezado, ref id_factura_encabezado, ref id_recibo))
                {
                    try
                    { 
                        Session["saldo_caja"] = Convert.ToString(Convert.ToDecimal(Session["saldo_caja"].ToString()) + Convert.ToDecimal(lblTotalFacturaNumero.Text.ToString()));
                    }
                    catch (Exception ex)
                    {
                        showWarning("Error sumando saldo a caja asignada, salga del sistema y vuelva ingresar. " + ex.ToString());
                    }

                    showSuccess("Factura guardada correctamente.");
                    string script = "window.open('WebReport.aspx?tipo_reporte=2&id_factura= " + id_factura_encabezado.ToString() + " &id_sucursal=" + Session["id_sucursal"].ToString() + "&numero_contrato=200000000020');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "ImpresionFactura", script, true);

                    Session["CurrentTableJoyas"] = null;

                    string script2 = "window.open('WebReport.aspx?tipo_reporte=5" + "&id_recibo=" + id_recibo.ToString() + "&id_sucursal=" + Session["id_sucursal"].ToString() + "');";
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
                DataTable dtProducto = (DataTable)this.Session["CurrentTableJoyas"];

                if (this.gvProductoFacturar.Rows.Count > 0 && dtProducto.Rows.Count > 0)
                {
                    
                    foreach (DataRow item in dtProducto.Rows)
                    {
                        totalFactura += Convert.ToDecimal(item["valor"].ToString());
                    }
                    this.lblTotalFacturaNumero.Text = totalFactura.ToString();
                }
                else
                {
                    this.lblTotalFacturaNumero.Text = totalFactura.ToString(); 
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
                int id_sucursal = Convert.ToInt32(Session["id_sucursal"]);
                cs_serie = new CLASS.cs_serie();
                this.ddlSerie.DataSource = cs_serie.getSerieDDL(ref error, id_sucursal);
                this.ddlSerie.DataValueField = "id_serie";
                this.ddlSerie.DataTextField = "serie";
                this.ddlSerie.DataBind();

                int id_serie = int.Parse(this.ddlSerie.SelectedValue.ToString());

                if (id_serie > 0)
                {
                    cs_serie = new CLASS.cs_serie();
                    this.lblNumeroFactura.Text = (cs_serie.getCorrelativoSerie(ref error, id_serie) + 1).ToString();
                }
                else
                    this.lblNumeroFactura.Text = "0";
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
                decimal montoFila = 0, porcentaje = 0, montoDescuento = 0, montoSubTotal = 0, montoDetalleIVA = 0;
                if (this.gvProductoFacturar.Rows.Count > 0)
                {
                    DataTable getProductos = (DataTable)this.Session["CurrentTableJoyas"];
                    foreach (DataRow item in getProductos.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item["valor"].ToString()) / Convert.ToDecimal(this.lblTotalFacturaNumero.Text.ToString()), 4);
                        montoDescuento = porcentaje * Convert.ToDecimal(this.txtMontoARecalcular.Text.ToString());
                        montoFila = Convert.ToDecimal(item["valor"].ToString()) - montoDescuento;
                        montoFila = Math.Round(montoFila, 2);
                        item["valor"] = montoFila.ToString();
                        montoSubTotal = (montoFila / Convert.ToDecimal(1.12));
                        item["subtotal"] = Math.Round(montoSubTotal, 2).ToString();
                        montoDetalleIVA = montoFila - montoSubTotal;
                        item["IVA"] = Math.Round(montoDetalleIVA, 2).ToString();
                    }

                    Session["CurrentTableJoyas"] = getProductos;
                    this.gvProductoFacturar.DataSource = getProductos;
                    this.gvProductoFacturar.DataBind();
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
                string id = this.ddlArticulos.SelectedValue.ToString();
                foreach (GridViewRow item in this.gvProductoFacturar.Rows)
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
                string itemValue = this.ddlArticulos.Text.ToString();
                if (ddlArticulos.Items.FindByValue(itemValue) != null)
                {
                    string itemText = this.ddlArticulos.Items.FindByValue(itemValue).Text;
                    ListItem li = new ListItem();
                    li.Text = itemText;
                    li.Value = itemValue;
                    //Label1.Text = "Item Found and remove: " + itemText;
                    this.ddlArticulos.Items.Remove(li);
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
                this.lblIdCliente.Text = CLASS.cs_cliente.Id_cliente;
                this.lblNombreCliente.Text = CLASS.cs_cliente.Nombre_cliente;
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
                int id_serie = int.Parse(this.ddlSerie.SelectedValue.ToString());

                if (id_serie > 0)
                {
                    cs_serie = new CLASS.cs_serie();
                    this.lblNumeroFactura.Text = (cs_serie.getCorrelativoSerie(ref error, id_serie) + 1).ToString();
                }
                else
                    this.lblNumeroFactura.Text = "0";
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
                    DataTable dtBorrarItem = (DataTable)this.Session["CurrentTableJoyas"];
                    int index = Convert.ToInt32(e.CommandArgument);
                    dtBorrarItem.Rows[index].Delete();
                    Session["CurrentTableJoyas"] = dtBorrarItem;
                    this.gvProductoFacturar.DataSource = dtBorrarItem;
                    this.gvProductoFacturar.DataBind();
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