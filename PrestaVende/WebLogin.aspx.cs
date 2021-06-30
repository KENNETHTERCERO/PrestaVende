using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende
{
    public partial class WebLogin : System.Web.UI.Page
    {
        private CLASS.cs_usuario login = new CLASS.cs_usuario();
        private CLASS.cs_asignacion_caja asignacion_caja = new CLASS.cs_asignacion_caja();
        private static DateTime fecha_asignacion, fecha_hoy;

        private static string id_asignacion = "0", estado_asignacion = "", caja_asignada = "", error = "";
                                                                                                                                                                                 
        protected void Page_Load(object sender, EventArgs e)
        {
            setLogo();
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
                    string[] respuesta = new string[15];
                    
                    respuesta = login.Login(txtUser.Text, txtPassword.Text);

                    if (!respuesta[0].Equals("true"))
                    {
                        showError(respuesta[0]);
                    }
                    else
                    {
                        HttpCookie userLogin = new HttpCookie("userLogin");
                        string id_estado_caja = "";
                        Session["id_usuario"]           = Convert.ToInt32(respuesta[1]);
                        Session["id_empresa"]           = Convert.ToInt32(respuesta[2]);
                        Session["id_sucursal"]          = Convert.ToInt32(respuesta[3]);
                        Session["usuario"]              = respuesta[4];
                        Session["primer_nombre"]        = respuesta[5];
                        Session["primer_apellido"]     = respuesta[6];
                        Session["id_rol"]               = Convert.ToInt32( respuesta[7]);
                        Session["id_caja"]              = Convert.ToInt32(respuesta[9]);
                        id_asignacion                   = respuesta[10];
                        estado_asignacion               = respuesta[11];
                        caja_asignada                   = respuesta[12];
                        Session["id_tipo_caja"]         = Convert.ToInt32(respuesta[13]);
                        id_estado_caja = respuesta[14];
                        userLogin.Expires = DateTime.Now.AddHours(3);
                        Response.Cookies.Add(userLogin);

                        getDatosCaja();
                        ingresarPrincipal(id_estado_caja);
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ingresarPrincipal(string id_estado_caja)
        {
            try
            {
                if ((Convert.ToInt32(Session["id_caja"]) == 0 && (Convert.ToInt32(Session["id_rol"]) != 5) || id_estado_caja == "7"))
                {
                    Response.Redirect("~/Public/WFPrincipal.aspx", false);
                }
                else
                {
                    if (!id_asignacion.Equals("0") && estado_asignacion.Equals("0") && Convert.ToInt32(Session["id_caja"]) != 0)
                    {
                        Response.Redirect("~/Public/RecepcionCaja.aspx?id_asignacion=" + id_asignacion, false);
                    }
                    else if (caja_asignada.Equals("1") && Convert.ToInt32(Session["id_caja"]) != 0)
                    {
                        fecha_hoy = DateTime.Now;

                        if (fecha_asignacion.Date < fecha_hoy.Date && Convert.ToInt32(Session["id_rol"]) == 5)
                        {
                            showWarning("Usted aun tiene la caja asignada, por favor solicite al gerente que la cierre.");
                        }
                        else
                        {
                            Response.Redirect("~/Public/WFPrincipal.aspx", false);
                        }
                    }
                    else
                    {
                        showWarning("Usted no tiene caja asignada.");
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

        private void getDatosCaja()
        {
            try
            {
                foreach (DataRow item in asignacion_caja.getAsignacionCaja(ref error, id_asignacion).Rows)
                {
                    fecha_asignacion = Convert.ToDateTime(item[5].ToString());
                    Session["saldo_caja"] = Convert.ToDecimal((item["monto"].ToString() != "") ? item["monto"].ToString() : "0");
                    Session["puede_vender"] = Convert.ToDecimal((item["puede_vender"].ToString() != "") ? item["puede_vender"].ToString() : "0");
                    Session["puede_vender"] = Convert.ToDecimal((item["puede_vender"].ToString() != "") ? item["puede_vender"].ToString() : "0");
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void setLogo()
        {
            try
            {
                string logo = Request.QueryString.Get("E").ToString();

                if (logo == "null")
                {
                    imageLogo.Visible = false;
                }
                else
                {
                    imageLogo.ImageUrl = "Public/image/" + logo + ".png";
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("System.NullReferenceException"))
                {
                    imageLogo.Visible = false;
                }
                else
                {
                    showError(ex.ToString());
                }
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