using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class RecepcionTrasladoArticulos : System.Web.UI.Page
    {
        private CLASS.cs_sucursal cs_sucursal = new CLASS.cs_sucursal();
        private CLASS.cs_serie cs_serie = new CLASS.cs_serie();
        private CLASS.cs_traslado cs_traslado = new CLASS.cs_traslado();

        private static DataTable dtTablaArticulos;
        DataRow row = null;
        static string error = "";

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

                        validarSucursales();
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

        private void Limpiar()
        {
            
            ObtenerSeries();
            ddlRecibos.DataSource = null;
            ddlRecibos.Enabled = false;
            ddlRecibos.DataBind();
            
            gvProductoTraslado.DataSource = null;
            gvProductoTraslado.DataBind();
        }

        private void validarSucursales()
        {
            ObtenerSucursales();            

            if (int.Parse(this.Session["id_rol"].ToString()) == 0)
            {
                ddlSucursal.Enabled = true;
                ddlSucursal.Enabled = true;
            }
            else
            {
                ddlSucursal.Enabled = false;
                ddlSucursal.SelectedValue = this.Session["id_sucursal"].ToString();
            }

        }

        private void ObtenerSucursales()
        {
            try
            {
                int id_empresa = Convert.ToInt32(Session["id_empresa"]);                

                cs_sucursal = new CLASS.cs_sucursal();
                this.ddlSucursal.DataSource = cs_sucursal.ObtenerSucursalesPorEmpresa(ref error, id_empresa.ToString());
                this.ddlSucursal.DataValueField = "id_sucursal";
                this.ddlSucursal.DataTextField = "sucursal";
                this.ddlSucursal.DataBind();

                if (this.Session["id_rol"].ToString().Equals("3") || this.Session["id_rol"].ToString().Equals("4") || this.Session["id_rol"].ToString().Equals("5"))
                {
                    this.ddlSucursal.SelectedValue = this.Session["id_sucursal"].ToString();                    
                    this.ddlSucursal.Enabled = false;
                }

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
                int id_sucursal = Convert.ToInt32(ddlSucursal.SelectedValue.ToString());
                cs_serie = new CLASS.cs_serie();
                this.ddlSerie.DataSource = cs_serie.ObtenerSeriesRecepcion(ref error, id_sucursal, 3); //3 boletas traslado
                this.ddlSerie.DataValueField = "id_serie";
                this.ddlSerie.DataTextField = "serie";
                this.ddlSerie.DataBind();                

            }
            catch (Exception ex)
            {
                showError(ex.ToString() + " " + error);
            }
        }

        private void ObtenerBoletasPendientesRecibir(int id_serie)
        {
            try
            {
                int id_sucursal = Convert.ToInt32(ddlSucursal.SelectedValue.ToString());
                cs_serie = new CLASS.cs_serie();

                DataSet dsBoletas = cs_traslado.ObtenerTrasladosPendientes(ref error, id_sucursal, id_serie);

                if (dsBoletas.Tables[0].Rows.Count > 0)
                {
                    this.ddlRecibos.DataSource = dsBoletas;
                    this.ddlRecibos.DataValueField = "numero_boleta";
                    this.ddlRecibos.DataTextField = "boleta";
                    this.ddlRecibos.DataBind();
                }

                if(dsBoletas.Tables[0].Rows.Count == 1)
                {
                    showWarning("No existen boletas pendientes de recibir para la serie y sucursal seleccionada.");
                }
                    
                

            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        private void setColumnsArticulo()
        {
            try
            {
                dtTablaArticulos.Columns.Add("id_inventario");
                dtTablaArticulos.Columns.Add("sucursal_origen");
                dtTablaArticulos.Columns.Add("sucursal_destino");
                dtTablaArticulos.Columns.Add("producto");
                dtTablaArticulos.Columns.Add("marca");
                dtTablaArticulos.Columns.Add("monto_prestado");
                dtTablaArticulos.Columns.Add("valor");
                dtTablaArticulos.Columns.Add("caracteristicas");
                dtTablaArticulos.Columns.Add("observaciones");               
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void cargarArticulos()
        {

            try
            {
                if (int.Parse(ddlRecibos.SelectedValue.ToString()) > 0)
                {
                    if ((DataTable)this.Session["tablaArticulos"] != null)
                    {
                        DataTable ArticuloCompleto = (DataTable)this.Session["tablaArticulos"];
                        DataTable articuloViene = new DataTable("articuloViene");

                        int id_serie = int.Parse(ddlSerie.SelectedValue.ToString());
                        int id_sucursal = int.Parse(ddlSucursal.SelectedValue.ToString());
                        int numero_boleta = int.Parse(ddlRecibos.SelectedValue.ToString());

                        articuloViene = cs_traslado.ObtenerDatosTrasladoPorBoleta(id_sucursal, id_serie, numero_boleta);

                        foreach (DataRow item in articuloViene.Rows)
                        {

                            row = ArticuloCompleto.NewRow();
                            row["id_inventario"] = item["id_inventario"].ToString();
                            row["sucursal_origen"] = item["sucursal_origen"].ToString();
                            row["sucursal_destino"] = item["sucursal_destino"].ToString();
                            row["producto"] = item["producto"].ToString();
                            row["marca"] = item["marca"].ToString();
                            row["monto_prestado"] = item["monto_prestado"].ToString();
                            row["valor"] = item["valor"].ToString();
                            row["caracteristicas"] = item["caracteristicas"].ToString();
                            row["observaciones"] = item["observaciones"].ToString();
                            ArticuloCompleto.Rows.Add(row);
                        }

                        dtTablaArticulos = ArticuloCompleto;
                        Session["tablaArticulos"] = dtTablaArticulos;

                        gvProductoTraslado.DataSource = dtTablaArticulos;
                        gvProductoTraslado.DataBind();

                        foreach (GridViewRow item in this.gvProductoTraslado.Rows)
                        {
                            TextBox txtPrecio = ((TextBox)item.FindControl("txtPrecio"));
                            txtPrecio.Text = item.Cells[7].Text.ToString();
                        }

                    }
                }
                else
                {
                    gvProductoTraslado.DataSource = null;
                    gvProductoTraslado.DataBind();
                }
            }
            catch (Exception ex)
            {
                showError("Error al cargar boleta: " + ex.ToString());
            }
                                  
        }

        private bool validaPrecio()
        {
            try
            {
                int contadorErrores = 0;

                foreach (GridViewRow item in this.gvProductoTraslado.Rows)
                {
                    TextBox txtPrecio = ((TextBox)item.FindControl("txtPrecio"));
                    decimal precio = decimal.Parse(txtPrecio.Text);
                    int id_serie = int.Parse(ddlSerie.SelectedValue.ToString());
                    int numero_boleta = int.Parse(ddlRecibos.SelectedValue.ToString());
                    string id_inventario = item.Cells[1].Text.ToString();
                    decimal PrecioSugerido = decimal.Parse(item.Cells[6].Text);

                    bool respuesta = false;
                    string error = "";

                    if (precio > 0)
                    {
                        if (precio < PrecioSugerido)
                        {
                            showWarning("El precio no puede ser menor al precio sugerido del prestamo " + id_inventario);
                            contadorErrores++;
                        }
                    }
                    else
                    {
                        showWarning("El precio seleccionado debe ser mayor a 0.00 para el prestamo " + id_inventario);
                        contadorErrores++;
                    }

                }
                if (contadorErrores > 0)
                {
                    showError("Existieron " + contadorErrores.ToString() + " errores, por favor valide el precio de los articulos.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                showError("Error al recibir boleta: " + ex.ToString());
                return false;
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
            ddlRecibos.Enabled = true;
            if (int.Parse(ddlSerie.SelectedValue.ToString()) > 0)
            {
                ObtenerBoletasPendientesRecibir(int.Parse(ddlSerie.SelectedValue.ToString()));
            }
        }              
       
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                int contadorErrores = 0;
                string error = "";
                bool respuesta = false;

                if (validaPrecio())
                {
                    foreach (GridViewRow item in this.gvProductoTraslado.Rows)
                    {
                        TextBox txtPrecio = ((TextBox)item.FindControl("txtPrecio"));

                        respuesta = cs_traslado.RecibirDatosTrasladoPorBoleta(ref error, Convert.ToInt32(ddlSerie.SelectedValue.ToString()), Convert.ToInt32(ddlRecibos.SelectedValue.ToString()), Convert.ToInt32(item.Cells[1].Text.ToString()), Convert.ToDecimal(txtPrecio.Text.ToString()));

                        if (!respuesta)
                        {
                            contadorErrores++;   
                        }
                    }

                    if (contadorErrores > 0)
                    {
                        throw new Exception("No se pudo recibir la boleta de traslado. " + error);
                    }
                    else
                    {
                        showSuccess("El traslado fue recibido con éxito.");
                        validarSucursales();
                        Limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                showError("Error al recibir boleta: " + ex.ToString());
            }
        }

        protected void ddlRecibos_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarArticulos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            validarSucursales();
            Limpiar();
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

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Limpiar();
        }
    }
}