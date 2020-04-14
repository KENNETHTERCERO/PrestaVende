using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class TrasladoArticulos : System.Web.UI.Page
    {
        private CLASS.cs_manejo_inventario cs_manejo_inventario = new CLASS.cs_manejo_inventario();
        private CLASS.cs_serie cs_serie = new CLASS.cs_serie();
        private CLASS.cs_sucursal cs_sucursal = new CLASS.cs_sucursal();
        private CLASS.cs_traslado cs_traslado = new CLASS.cs_traslado();

        static string error = "";
        private static DataTable dtTablaArticulos;
        DataRow row = null;

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


                    //Pruebas
                //    this.Session["id_empresa"] = 1;
                //this.Session["id_rol"] = 0;
                //this.Session["id_sucursal"] = 1;


                if (IsPostBack)
                    {
                        if (lblWarning.Text == "") { divWarning.Visible = false; }
                        if (lblError.Text == "") { divError.Visible = false; }
                        if (lblSuccess.Text == "") { divSucceful.Visible = false; }
                    }
                    else
                    {

                    dtTablaArticulos = new DataTable("tablaArticulos");
                    setColumnsArticulo();
                    Session["tablaArticulos"] = dtTablaArticulos;
                    

                    Limpiar();
                       
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #region Metodos

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
                dtTablaArticulos.Columns.Add("observaciones");
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void Limpiar()
        {
            validarSucursales();
            txtBusqueda.Text = "";
            ObtenerSeries();                     
            ddlArticulos.DataSource = null;
            gvProductoTraslado.DataSource = null;
            gvProductoTraslado.DataBind();
            
        }

        private void validarSucursales()
        {
            ObtenerSucursales();
            ObtenerSucursalesRestantes(int.Parse(ddlSucursalOrigen.SelectedValue.ToString()));

            if (int.Parse(this.Session["id_rol"].ToString()) == 0)
            {
                ddlSucursalOrigen.Enabled = true;
                ddlSucursalDestino.Enabled = true;
            }else
            {                
                ddlSucursalOrigen.Enabled = false;                
                ddlSucursalOrigen.SelectedValue =this.Session["id_sucursal"].ToString();              
            }
          
        }

        private void getProductos()
        {
            try
            {
                DataTable dtComprobar = new DataTable();                
                int id_sucursal = int.Parse(ddlSucursalOrigen.SelectedValue.ToString());
                int id_sucursal_seleccionada = (id_sucursal > 0 ? id_sucursal : int.Parse(this.Session["id_sucursal"].ToString()));


                if (id_sucursal_seleccionada > 0)
                {
                    dtComprobar = cs_manejo_inventario.getArticulos(ref error, this.txtBusqueda.Text.ToString(), id_sucursal_seleccionada);
                    if (dtComprobar.Rows.Count <= 0)
                    {
                        showWarning("No se encontro ningun articulo con este numero de prestamo. error: " + error);
                    }
                    else
                    {
                        this.ddlArticulos.DataSource = dtComprobar;
                        this.ddlArticulos.DataValueField = "id_inventario";
                        this.ddlArticulos.DataTextField = "descripcion_producto";
                        this.ddlArticulos.DataBind();
                    }
                }else
                {
                    showError("Debe selecionar una sucursal origen.");
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void ObtenerSucursales()
        {
            try
            {
                int id_empresa = Convert.ToInt32(Session["id_empresa"]);
                //int id_empresa = 1;

                cs_sucursal = new CLASS.cs_sucursal();
                this.ddlSucursalOrigen.DataSource = cs_sucursal.ObtenerSucursalesPorEmpresa(ref error, id_empresa.ToString());
                this.ddlSucursalOrigen.DataValueField = "id_sucursal";
                this.ddlSucursalOrigen.DataTextField = "sucursal";
                this.ddlSucursalOrigen.DataBind();

                if (this.Session["id_rol"].ToString().Equals("3") || this.Session["id_rol"].ToString().Equals("4") || this.Session["id_rol"].ToString().Equals("5"))
                {
                    this.ddlSucursalOrigen.SelectedValue = this.Session["id_sucursal"].ToString();
                    //this.ddlSucursal.SelectedValue = "1";
                    this.ddlSucursalOrigen.Enabled = false;
                }
              
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void ObtenerSucursalesRestantes(int sucursal_origen)
        {
            try
            {
                int id_empresa = Convert.ToInt32(Session["id_empresa"]);
               
                //int id_empresa = 1;

                cs_sucursal = new CLASS.cs_sucursal();
                this.ddlSucursalDestino.DataSource = cs_sucursal.ObtenerSucursalesPorEmpresaRestantes(ref error, id_empresa.ToString(), sucursal_origen);
                this.ddlSucursalDestino.DataValueField = "id_sucursal";
                this.ddlSucursalDestino.DataTextField = "sucursal";
                this.ddlSucursalDestino.DataBind();
               
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void ObtenerSeries()
        {
            try
            {
                int id_sucursal = Convert.ToInt32(ddlSucursalOrigen.SelectedValue.ToString());
                cs_serie = new CLASS.cs_serie();
                this.ddlSerie.DataSource = cs_serie.ObtenerSeriesImpresion(ref error, id_sucursal, 3); //3 Boletas traslados
                this.ddlSerie.DataValueField = "id_serie";
                this.ddlSerie.DataTextField = "serie";
                this.ddlSerie.DataBind();

                validarCorrelativo();

            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        private void validarCorrelativo()
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

        private bool validaArticuloAgregado()
        {
            try
            {
                int contadorDuplicados = 0;
                string id = this.ddlArticulos.SelectedValue.ToString();
                foreach (GridViewRow item in this.gvProductoTraslado.Rows)
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
                    showWarning("No se puede agregar 2 veces el mismo articulo para el traslado.");
                    return false;
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
                if ((DataTable)this.Session["tablaArticulos"] != null)
                {
                    DataTable ArticuloCompleto = (DataTable)this.Session["tablaArticulos"];
                    DataTable articuloViene = new DataTable("articuloViene");
                    cs_manejo_inventario = new CLASS.cs_manejo_inventario();
                    articuloViene = cs_manejo_inventario.getArticuloEspecifico(ref error, this.txtBusqueda.Text.ToString(), this.ddlArticulos.SelectedValue.ToString(), int.Parse(ddlSucursalOrigen.SelectedValue.ToString()));

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
                        row["observaciones"] = "";
                    }

                    ArticuloCompleto.Rows.Add(row);

                    dtTablaArticulos = ArticuloCompleto;
                    Session["tablaArticulos"] = dtTablaArticulos;

                    gvProductoTraslado.DataSource = dtTablaArticulos;
                    gvProductoTraslado.DataBind();
                }
             }
            catch (Exception ex)
            {
                showError(ex.ToString());
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

        private bool validaDatos()
        {
            try
            {
                if (!(int.Parse(ddlSucursalOrigen.SelectedValue.ToString()) > 0))
                {
                    showWarning("Seleccione la sucursal de origen.");
                    return false;
                }
                else if (!(int.Parse(ddlSucursalDestino.SelectedValue.ToString()) > 0))
                {
                    showWarning("Seleccione la sucursal de destino.");
                    return false;
                }
                else if (this.gvProductoTraslado.Rows.Count <= 0)
                {
                    showWarning("Se deben agregar los productos antes de realizar el traslado.");
                    return false;
                }
                else if (!(int.Parse(ddlSerie.SelectedValue.ToString()) > 0))
                {
                    showWarning("Debe seleccionar la serie del recibo a emitir.");
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

        private void actualizarTabla()
        {
            DataTable ArticuloCompleto = (DataTable)this.Session["tablaArticulos"];

            foreach (GridViewRow item in this.gvProductoTraslado.Rows)
            {
                TextBox txtObservaciones = ((TextBox)item.FindControl("txtObservaciones"));

                row = ArticuloCompleto.NewRow();
                row["id_inventario"] = item.Cells[1].Text.ToString();
                row["numero_linea"] = item.Cells[2].Text.ToString(); 
                row["numero_prestamo"] = item.Cells[3].Text.ToString();
                row["producto"] = item.Cells[4].Text.ToString();
                row["marca"] = item.Cells[5].Text.ToString();
                row["monto_prestado"] = item.Cells[6].Text.ToString();
                row["valor"] = item.Cells[7].Text.ToString();
                row["caracteristicas"] = item.Cells[8].Text.ToString();               
                row["observaciones"] = txtObservaciones.Text;
            }

            ArticuloCompleto.Rows.Add(row);

            dtTablaArticulos = ArticuloCompleto;
            Session["tablaArticulos"] = dtTablaArticulos;
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

        #region Eventos

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                
                string id_serie_recibo = ddlSerie.SelectedValue.ToString();
                string numero_recibo = lblNumeroFactura.Text;

                if ((DataTable)this.Session["tablaArticulos"] != null)
                {
                  
                    if (validaDatos())
                    {
                        cs_traslado = new CLASS.cs_traslado();

                        actualizarTabla();

                        bool respuesta = cs_traslado.grabarTraslado(int.Parse(ddlSucursalOrigen.SelectedValue.ToString()), int.Parse(ddlSucursalDestino.SelectedValue.ToString()), int.Parse(id_serie_recibo), int.Parse(lblNumeroFactura.Text), (DataTable)this.Session["tablaArticulos"], ref error);

                        if (respuesta)
                        {
                            showSuccess("Traslado almacenado con éxito.");
                            Limpiar();
                        }
                        else
                        {
                            showError("No se puedo almacenar el traslado, razón: " + error);
                        }
                    
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
           
        }

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

        protected void ddlSucursalOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSucursalDestino.Enabled = true;
            ObtenerSucursalesRestantes(int.Parse(ddlSucursalOrigen.SelectedValue.ToString()));
            ObtenerSeries();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            txtBusqueda.Focus();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaArticuloAgregado())
                {
                    AgregaArticuloAGrid();                    
                    borrarItem();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            validarCorrelativo();
        }

        protected void gvProductoTraslado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "borrar")
                {
                    DataTable dtBorrarItem = (DataTable)this.Session["tablaArticulos"];
                    int index = Convert.ToInt32(e.CommandArgument);
                    dtBorrarItem.Rows[index].Delete();
                    Session["tablaArticulos"] = dtBorrarItem;
                    this.gvProductoTraslado.DataSource = dtBorrarItem;
                    this.gvProductoTraslado.DataBind();
                    
                }
            }
            catch (Exception ex)
            {
                showError("Error al eliminar producto: " + ex.ToString());
            }
        }

        #endregion
    }
}