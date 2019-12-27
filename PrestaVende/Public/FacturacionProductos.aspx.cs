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
        CLASS.cs_manejo_inventario cs_manejo_inventario = new CLASS.cs_manejo_inventario();
        static string error="";

        private static DataTable dtTablaArticulos;
        DataRow row = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && CLASS.cs_usuario.id_usuario == 0)
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
                        row["id_inventario"] = item[0].ToString();
                        row["numero_linea"] = item[1].ToString();
                        row["producto"] = item[2].ToString();
                        row["marca"] = item[3].ToString();
                        row["valor"] = item[4].ToString();
                        row["caracteristicas"] = item[5].ToString();
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
                dtTablaArticulos.Columns.Add("producto");
                dtTablaArticulos.Columns.Add("marca");
                dtTablaArticulos.Columns.Add("valor");
                dtTablaArticulos.Columns.Add("caracteristicas");
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
            AgregaArticuloAGrid();
        }

        protected void tbnFacturar_Click(object sender, EventArgs e)
        {

        }
    }
}