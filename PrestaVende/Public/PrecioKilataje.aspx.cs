using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class PrecioKilataje : System.Web.UI.Page
    {
        private CLASS.cs_kilataje cs_kilataje;
        private static string str_error;

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
                        getDataKilataje();
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

        #region
        private void getDataKilataje()
        {
            try
            {
                cs_kilataje = new CLASS.cs_kilataje();

                gvKilataje.DataSource = cs_kilataje.getKilatajeCambioPrecio(ref str_error);
                gvKilataje.DataBind();
            }
            catch (Exception ex)
            {
                showError(str_error + ex.ToString());
            }
        }

        private bool validaPrecioNuevo()
        {
            try
            {
                int IntContadorErrores = 0;

                foreach (GridViewRow item in gvKilataje.Rows)
                {
                    TextBox txtPrecio = ((TextBox)item.FindControl("txtPrecio"));
                    decimal precio = decimal.Parse(txtPrecio.Text);

                    if (precio <= 0)
                    {
                        IntContadorErrores++;
                        showWarning("El kilataje " + item.Cells[1].ToString() + " no puede tener valor 0 o menor a 0.");
                    }
                }

                if (IntContadorErrores > 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void guardarPreciosNuevos()
        {
            try
            {
                int IntContadorErrores = 0;
                cs_kilataje = new CLASS.cs_kilataje();

                foreach (GridViewRow item in gvKilataje.Rows)
                {
                    TextBox txtPrecio = ((TextBox)item.FindControl("txtPrecio"));
                    decimal precio = Convert.ToDecimal(txtPrecio.Text);

                    if (cs_kilataje.updatePrecioKilataje(ref str_error, item.Cells[0].Text.ToString(), precio) <= 0)
                    {
                        IntContadorErrores++;
                    }
                }

                if (IntContadorErrores > 0)
                {
                    showError("Por favor actualizar de nuevo el precio de cada Kilataje ya que ocurrio mas de 1 error.");
                }
                else
                {
                    showSuccess("Se actualizaron correctamente los precios para los kilatajes.");
                    getDataKilataje();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
        #endregion

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaPrecioNuevo())
                {
                    guardarPreciosNuevos();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
    }
}