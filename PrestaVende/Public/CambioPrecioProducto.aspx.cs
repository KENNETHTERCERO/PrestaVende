using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class CambioPrecioProducto : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && CLASS.cs_usuario.id_usuario == 0)
                {
                  //  Response.Redirect("~/WFWebLogin.aspx");
                }
              
                    if (IsPostBack)
                    {
                        if (lblWarning.Text == "") { divWarning.Visible = false; }
                        if (lblError.Text == "") { divError.Visible = false; }
                        if (lblSuccess.Text == "") { divSucceful.Visible = false; }
                    }
                   
              
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #region metodos

        private void buscarPrestamo(Decimal intPrestamo)
        {
            try
            {
                div_gridView.Visible = true;
               // GrdVLiquidacion.DataSource = clsRecepcion.obtenerLiquidaciones(intPrestamo);
                GrdVLiquidacion.DataBind();

            }
            catch (Exception ex)
            {
                showError("Error al buscar prestamo: " + ex.ToString());
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
            if (TxtPrestamo.Text.Length > 0)
            {
                buscarPrestamo(Decimal.Parse(TxtPrestamo.Text));
            }
            else
            {
                showError("Debe ingresar prestamo a buscar.");
            }
        }

        

        
    }
}