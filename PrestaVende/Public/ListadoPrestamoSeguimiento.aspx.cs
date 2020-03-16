using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class ListadoPrestamoSeguimiento : System.Web.UI.Page
    {
        private CLASS.cs_prestamo cs_prestamo = new CLASS.cs_prestamo();
        private CLASS.cs_cliente cs_cliente = new CLASS.cs_cliente();
        private string error = "";

        #region funciones

        private void getPrestamo()
        {
            try
            {
                string id_prestamo = Request.QueryString["id_cliente"];

                gvPrestamo.DataSource = cs_prestamo.ObtenerPrestamos(ref error, id_prestamo);
                gvPrestamo.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getClient()
        {
            try
            {
                string id_cliente = Request.QueryString["id_cliente"];
                lblid_cliente.Text = id_cliente;
                foreach (DataRow item in cs_cliente.getSpecificClient(ref error, id_cliente).Rows)
                {
                    lblnombre_cliente.Text = item[3].ToString() + " " + item[4].ToString() + " " + item[5].ToString() + " " + item[6].ToString();
                    lblDireccionTexto.Text = item[9].ToString();
                    lblTelefonoTexto.Text = item[11].ToString();
                    lblFechaCreacionTexto.Text = item[19].ToString();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

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
                        Session["prestamo_seleccionado"] = "";
                        getClient();
                        getPrestamo();
                        getTipoSeguimiento();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }            
        }

        private void getTipoSeguimiento()
        {
            try
            {
                this.ddlTipoSeguimiento.DataSource = cs_prestamo.getTipoSeguimiento(ref error);
                this.ddlTipoSeguimiento.DataValueField = "id_tipo_seguimiento";
                this.ddlTipoSeguimiento.DataTextField = "tipo_seguimiento";
                this.ddlTipoSeguimiento.DataBind();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        protected void gvPrestamo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "crear")
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    GridViewRow selectedRow = gvPrestamo.Rows[index];
                    TableCell numero_prestamo = selectedRow.Cells[2];
                    Session["prestamo_seleccionado"] = numero_prestamo.Text.ToString();
                    btnSeguimiento.Visible = true;

                    gvSeguimientos.DataSource = cs_prestamo.GetSeguimientos(ref error, Session["prestamo_seleccionado"].ToString(), HttpContext.Current.Session["id_sucursal"].ToString());
                    gvSeguimientos.DataBind();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {
            ddlTipoSeguimiento.SelectedIndex = 0;
            txtDescripcion.Text = "";
        }

        protected void btnAceptModal_Click(object sender, EventArgs e)
        {
            try
            { 
                if(ddlTipoSeguimiento.SelectedIndex != 0 && txtDescripcion.Text.Trim() != "")
                {
                    bool respuesta = false;
                    respuesta = cs_prestamo.GuardarSeguimientos(ref error, Session["prestamo_seleccionado"].ToString(), Session["id_sucursal"].ToString(), ddlTipoSeguimiento.SelectedValue.ToString(), txtDescripcion.Text);

                    if (respuesta)
                    {
                        showSuccess("Se guardo el seguimiento.");
                        gvSeguimientos.DataSource = cs_prestamo.GetSeguimientos(ref error, Session["prestamo_seleccionado"].ToString(), HttpContext.Current.Session["id_sucursal"].ToString());
                        gvSeguimientos.DataBind();
                    }
                    else
                        showError("Error al guardar el seguimiento.");

                    ddlTipoSeguimiento.SelectedIndex = 0;
                    txtDescripcion.Text = "";
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
    }
}