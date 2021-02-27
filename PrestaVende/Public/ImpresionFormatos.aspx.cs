using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class ImpresionFormatos : System.Web.UI.Page
    {
        private string error = "";
        private static string reporte = "";
        private CLASS.cs_sucursal cs_sucursal = new CLASS.cs_sucursal();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && Convert.ToInt32(HttpContext.Current.Session["id_usuario"]) == 0)
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
                        cs_sucursal = new CLASS.cs_sucursal();
                        ObtenerSucursales();
                        setNombreReimpresion();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void setNombreReimpresion()
        {
            try
            {
                reporte = this.Request.QueryString.Get("reporte");
                if (reporte=="10")
                {
                    this.lblTipoReimpresion.Text = "REIMPRESION ESTADO DE CUENTA PRESTAMO";
                }
                else if (reporte == "11")
                {
                    this.lblTipoReimpresion.Text = "REIMPRESION ETIQUETA";
                }
                else if (reporte == "12")
                {
                    this.lblTipoReimpresion.Text = "REIMPRESION CONTRATO";
                }
                else if (reporte == "13")
                {
                    this.lblTipoReimpresion.Text = "REIMPRESION RECIBO";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ObtenerSucursales()
        {
            try
            {
                int id_empresa = Convert.ToInt32(Session["id_empresa"].ToString());
                this.ddlSucursal.DataSource = cs_sucursal.ObtenerSucursalesPorEmpresa(ref error, id_empresa.ToString());
                this.ddlSucursal.DataValueField = "id_sucursal";
                this.ddlSucursal.DataTextField = "sucursal";
                this.ddlSucursal.DataBind();

                if (this.Session["id_rol"].ToString().Equals("3") || this.Session["id_rol"].ToString().Equals("4") || this.Session["id_rol"].ToString().Equals("5"))
                {
                    this.ddlSucursal.SelectedValue = this.Session["id_sucursal"].ToString();
                    this.ddlSucursal.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void imprimirReporte()
        {
            try
            {
                if (this.txtNumeroPrestamo.Text.ToString() == "" || this.txtNumeroPrestamo.Text.ToString().Length <= 5)
                {
                    showWarning("Debe ingresar un numero de prestamo para poder imprimir.");
                }
                else
                {
                    string scriptReporte = "window.open('WebReport.aspx?tipo_reporte=" + reporte + "&id_sucursal=" + this.ddlSucursal.SelectedValue.ToString() + "&numero_prestamo=" + this.txtNumeroPrestamo.Text.ToString() + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReporte", scriptReporte, true);
                }
            }
            catch (Exception)
            {

                throw;
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

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            imprimirReporte();
        }
    }
}