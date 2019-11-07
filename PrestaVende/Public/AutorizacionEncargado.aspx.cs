using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class AutorizacionEncargado : System.Web.UI.Page
    {
        private CLASS.cs_usuario login = new CLASS.cs_usuario();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            inUser();
        }

        private void inUser()
        {
            try
            {
                if (validateTextBox())
                {
                    string[] respuesta = new string[11];

                    respuesta = login.Login(txtUser.Text, txtPassword.Text);


                    if (!respuesta[0].Equals("true"))
                    {
                        showError(respuesta[0]);
                    }
                    else
                    {
                        if (respuesta[6].ToString().Equals(5))
                        {
                            showError("Usted no puede autorizar.");
                        }
                        else
                        {
                            showWarning("Autorizacion exitosa.");
                            CLASS.cs_usuario.autorizado = true;
                        }
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