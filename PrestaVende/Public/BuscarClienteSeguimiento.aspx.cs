using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class BuscarClienteSeguimiento : System.Web.UI.Page
    {
        private CLASS.cs_cliente cs_cliente = new CLASS.cs_cliente();
        private string error = "";
        private static bool isUpdate = false;

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
                        this.txtBusquedaCliente.Attributes.Add("onkeypress", "button_click(this,'" + this.btnBuscarCliente.ClientID + "', event)");
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
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

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            getClients();
        }

        protected void gvCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "crear")
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    GridViewRow selectedRow = gvCliente.Rows[index];
                    TableCell id_cliente = selectedRow.Cells[2];
                    Session["id_cliente_to_sub_cliente"] = id_cliente.Text.ToString();
                    Response.Redirect("ListadoPrestamoSeguimiento.aspx?id_cliente=" + id_cliente.Text);

                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getClients()
        {
            try
            {
                string condicion = "";
                if (validateTXTFind())
                {
                    condicion = "DPI LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "nit LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "primer_nombre + ' ' + primer_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "primer_nombre + ' ' + segundo_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "primer_nombre + ' ' + segundo_nombre + ' ' + primer_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "primer_nombre + ' ' + segundo_nombre + ' ' + segundo_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "segundo_nombre + ' ' + primer_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "segundo_nombre + ' ' + segundo_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "correo_electronico LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "numero_telefono LIKE '%" + txtBusquedaCliente.Text + "%'";
                    gvCliente.DataSource = cs_cliente.findClient(ref error, condicion);
                    gvCliente.DataBind();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validateTXTFind()
        {
            if (txtBusquedaCliente.Text.ToString().Length == 0)
            {
                showWarning("Debe ingresar criterios de busqueda.");
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void gvCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}