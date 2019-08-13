using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende
{
    public partial class WebLogin : System.Web.UI.Page
    {
        CLASS.cs_usuario login = new CLASS.cs_usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                inUser();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void inUser()
        {
            try
            {
                if (validateTextBox())
                {
                    string[] respuesta;
                    respuesta = login.Login(txtUser.Text, txtPassword.Text);


                    if (!respuesta[0].Equals("true"))
                    {
                        showError(respuesta[0]);
                    }
                    else
                    {
                        //string id = Convert.ToString(getId.getIdUser(txtUser.Text, txtPassword.Text));
                        //string id_ubicacion = Convert.ToString(getId.getIdUbicacion(txtUser.Text, txtPassword.Text));
                        HttpCookie userLogin = new HttpCookie("userLogin");
                        CLASS.cs_usuario.id_usuario     = Convert.ToInt32(respuesta[1]);
                        CLASS.cs_usuario.id_empresa     = Convert.ToInt32(respuesta[2]);
                        CLASS.cs_usuario.id_sucursal    = Convert.ToInt32(respuesta[3]);
                        CLASS.cs_usuario.usuario        = respuesta[4];
                        CLASS.cs_usuario.primer_nombre  = respuesta[5];
                        CLASS.cs_usuario.primer_apellido = respuesta[6];
                        CLASS.cs_usuario.id_rol         = Convert.ToInt32( respuesta[7]);

                        Session["id_rol"] = CLASS.cs_usuario.id_rol;
                        
                        
                        userLogin.Expires = DateTime.Now.AddHours(3);
                        Response.Cookies.Add(userLogin);
                        Response.Redirect("~/Public/WFPrincipal.aspx", false);
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool validateTextBox()
        {
            if (txtUser.Text == "" && txtPassword.Text == "")
            {
                showWarning("Debe ingresar usuario y contraseña.");
                return false;
            }
            else if (txtUser.Text == "")
            {
                showWarning("Debe ingresar usuario.");
                return false;
            }
            else if (txtPassword.Text == "")
            {
                showWarning("Debe ingresar contraseña.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool showWarning(string warning)
        {
            divWarning.Visible = true;
            lblWarning.Controls.Add(new LiteralControl(String.Format("<span style='color:Orange'>{0}</span>", warning)));
            return true;
        }

        private bool showError(string error)
        {
            divError.Visible = true;
            lblError.Controls.Add(new LiteralControl(String.Format("<span style='color:Red'>{0}</span>", error)));
            return true;
        }
    }
}