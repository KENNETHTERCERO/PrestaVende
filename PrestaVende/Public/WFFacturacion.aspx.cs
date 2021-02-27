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
                int dia = 0, mes = 0, year = 0;
                string id_prestamo = Request.QueryString["id_prestamo"];
                cs_prestamo = new CLASS.cs_prestamo();
                foreach (DataRow item in cs_prestamo.ObtenerPrestamoEspecifico(ref error, id_prestamo).Rows)
                {
                    this.lblprestamoNumero.Text = item[1].ToString();
                    this.lblNombreCliente.Text = " - Cliente: " + item[2].ToString() + " " + item[3].ToString() + " " + item[4].ToString() + " " + item[5].ToString();
                    dia = Convert.ToInt32(item["dia"].ToString());
                    mes = Convert.ToInt32(item["mes"].ToString());
                    year = Convert.ToInt32(item["year"].ToString());
                }

                DateTime fecha_prestamo = new DateTime(year, mes, dia);
                DateTime fecha_hoy = DateTime.Now.Date;

                if (fecha_prestamo == fecha_hoy)
                {
                    btnAnularPrestamo.Visible = true;
                }
                else
                {
                    btnAnularPrestamo.Visible = false;
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
                cs_factura = new CLASS.cs_factura();
                gvFactura.DataSource = cs_factura.ObtenerFacturas(ref error, id_prestamo);
                gvFactura.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void anularPrestamo()
        {
            try
            {
                cs_prestamo = new CLASS.cs_prestamo();
                error = "";
                if (cs_prestamo.anularPrestamo(ref error, this.Session["id_sucursal"].ToString(), this.lblprestamoNumero.Text.ToString()))
                {
                    showSuccess("Prestamo anulado correctamente.");
                    this.btnAbonoCapital.Enabled = false;
                    this.btnAnularPrestamo.Enabled = false;
                    this.btnCancelacion.Enabled = false;
                    this.btnCobroIntereses.Enabled = false;
                }
                else
                {
                    int startIndex = 48;
                    int lenght = 111;
                    string errorModificado = error.Substring(startIndex, lenght);
                    showError(errorModificado + ".");
                }
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
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && Convert.ToInt32(this.Session["id_usuario"]) == 0)
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
                        getPrestamo();
                        getFactura();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
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

        protected void btnRetiroArticulo_Click(object sender, EventArgs e)
        {
            string id_prestamo = Request.QueryString["id_prestamo"];
            Response.Redirect("WFRetiroArticulo.aspx?id_prestamo=" + id_prestamo);
        }

        protected void btnAnularPrestamo_Click(object sender, EventArgs e)
        {
            anularPrestamo();
        }
    }
}