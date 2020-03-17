using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class Marcas : System.Web.UI.Page
    {
        private static string error = "";
        private CLASS.cs_marca cs_marca = new CLASS.cs_marca();
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
                        obtenerMarcas();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void obtenerMarcas()
        {
            try
            {
                cs_marca = new CLASS.cs_marca();
                error = "";
                this.gvMarcas.DataSource = cs_marca.getMarcas(ref error);
                this.gvMarcas.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString() + " ***** " + error);
            }
        }

        private bool validaMarca()
        {
            try
            {
                cs_marca = new CLASS.cs_marca();
                int numero = 0;
                error = "";
                numero = cs_marca.validaMarca(ref error, this.txtMarca.Text.ToString());

                if (numero == 999999)
                {
                    showError("Ocurrio un error creando la marca, por favor valide." + error);
                    return false;
                }
                else if (numero > 0)
                {
                    showWarning("La marca " + this.txtMarca.Text.ToString() + " ya existe, por favor busque y valide si existe.");
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

        private void guardarMarca()
        {
            try
            {
                cs_marca = new CLASS.cs_marca();
                error = "";
                if (cs_marca.insertMarca(ref error, this.txtMarca.Text.ToString()) > 0)
                {
                    showSuccess("Se agrego la marca " + this.txtMarca.Text.ToString() + " correctamente.");
                    txtMarca.Text = "";
                    obtenerMarcas();
                }
                else
                {
                    showError("No se pudo guardar la marca + " + this.txtMarca.Text.ToString() + " por el siguiente error " + error);
                }
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
                if (!validaMarca())
                {
                    return false;
                }
                else if (this.txtMarca.Text.ToString() == "" || this.txtMarca.Text.ToString().Length == 0)
                {
                    showWarning("Debe ingresar una marca para poder guardarla.");
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaDatos())
                {
                    guardarMarca();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
    }
}