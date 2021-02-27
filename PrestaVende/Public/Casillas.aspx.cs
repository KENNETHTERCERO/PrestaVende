using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class Casillas : System.Web.UI.Page
    {
        CLASS.cs_categoria cs_categoria = new CLASS.cs_categoria();
        private static string error = "";
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
                        obtenerCategoria();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void obtenerCategoria()
        {
            try
            {
                cs_categoria = new CLASS.cs_categoria();
                error = "";
                this.ddlCategoria.DataSource = cs_categoria.getCategoriaComboBox(ref error);
                this.ddlCategoria.DataValueField = "id_categoria";
                this.ddlCategoria.DataTextField = "categoria";
                this.ddlCategoria.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

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

        private bool showSucces(string error)
        {
            divSucceful.Visible = true;
            lblSuccess.Controls.Add(new LiteralControl(string.Format("<span style='color:Green'>{0}</span>", error)));
            return true;
        }

        protected void btnGuardarCasilla_Click(object sender, EventArgs e)
        {
            guardarCasilla();
        }

        private void guardarCasilla()
        {
            try
            {
                if (validarDatos())
                {
                    CLASS.cs_casilla cs_casilla = new CLASS.cs_casilla();
                    int casillasInsertadas = 0;
                    error = "";
                    casillasInsertadas = cs_casilla.insertCasillas(ref error, ddlCategoria.SelectedValue.ToString(), txtLetras.Text.ToString(), Convert.ToInt32(txtNumeroCasillaInicial.Text.ToString()), Convert.ToInt32(txtNumeroCasillaFinal.Text.ToString()));

                    if (casillasInsertadas > 0)
                    {
                        showSucces("Se agregaron casillas correctamente para la categoria " + ddlCategoria.SelectedItem.ToString());
                        txtLetras.Text = "";
                        txtNumeroCasillaInicial.Text = "";
                        txtNumeroCasillaFinal.Text = "";
                        ddlCategoria.SelectedValue = "0";
                    }
                    else
                    {
                        showError("No se pudieron insertar casillas. " + error);
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validarDatos()
        {
            try
            {
                if (ddlCategoria.SelectedValue.ToString() == "0")
                {
                    showWarning("Usted debe seleccionar una categoria para poder guardar.");
                    return false;
                }
                else if(txtLetras.Text.ToString().Length <= 0)
                {
                    showWarning("Usted debe ingresar letras para poder guardar.");
                    return false;
                }
                else if (txtNumeroCasillaInicial.Text.ToString().Length <= 0)
                {
                    showWarning("Usted debe ingresar numero inicial para poder guardar.");
                    return false;
                }
                else if (txtNumeroCasillaFinal.Text.ToString().Length <= 0)
                {
                    showWarning("Usted debe ingresar numero final para poder guardar.");
                    return false;
                }
                else if (Convert.ToInt32(txtNumeroCasillaFinal.Text.ToString()) < Convert.ToInt32(txtNumeroCasillaInicial.Text.ToString()))
                {
                    showWarning("El numero de casilla final no puede ser mayor al numero inicial.");
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
    }
}