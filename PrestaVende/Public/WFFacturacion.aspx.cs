using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFFacturacion : System.Web.UI.Page
    {
        private CLASS.cs_prestamo cs_prestamo = new CLASS.cs_prestamo();
        private CLASS.cs_factura cs_factura = new CLASS.cs_factura();
        private string error = "";

        #region funciones

        private void getPrestamo()
        {
            try
            {
                string id_prestamo = Request.QueryString["id_prestamo"];
                foreach (DataRow item in cs_prestamo.ObtenerPrestamoEspecifico(ref error, id_prestamo).Rows)
                {
                    lblnombre_prestamo.Text = item[1].ToString() + " - Cliente: " + item[2].ToString() + " " + item[3].ToString() + " " + item[4].ToString() + " " + item[5].ToString();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getFactura()
        {
            try
            {
                string id_prestamo = Request.QueryString["id_prestamo"];

                gvFactura.DataSource = cs_factura.ObtenerFacturas(ref error, id_prestamo);
                gvFactura.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #endregion

        #region controls
        protected void Page_Load(object sender, EventArgs e)
        {
            getPrestamo();
            getFactura();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFPrincipal.aspx");
        }

        protected void gvPrestamo_RowCommand(object sender, GridViewCommandEventArgs e)
        {

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

        protected void btnCobroIntereses_Click(object sender, EventArgs e)
        {
            string id_prestamo = Request.QueryString["id_prestamo"];
            Response.Redirect("WFCrearFactura.aspx?id_prestamo=" + id_prestamo + "&id_tipo=1");
        }

        protected void btnAbonoCapital_Click(object sender, EventArgs e)
        {
            string id_prestamo = Request.QueryString["id_prestamo"];
            Response.Redirect("WFCrearFactura.aspx?id_prestamo=" + id_prestamo + "&id_tipo=2");
        }

        protected void btnCancelacion_Click(object sender, EventArgs e)
        {
            string id_prestamo = Request.QueryString["id_prestamo"];
            Response.Redirect("WFCrearFactura.aspx?id_prestamo=" + id_prestamo + "&id_tipo=3");
        }
    }
}