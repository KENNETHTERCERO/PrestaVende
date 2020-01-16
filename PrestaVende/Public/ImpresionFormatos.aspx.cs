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

                if (cookie == null && (int)HttpContext.Current.Session["id_usuario"] == 0)
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
                reporte = Request.QueryString.Get("reporte");
                if (reporte=="10")
                {
                    lblTipoReimpresion.Text = "REIMPRESION ESTADO DE CUENTA PRESTAMO";
                }
                else if (reporte == "11")
                {
                    lblTipoReimpresion.Text = "REIMPRESION ETIQUETA";
                }
                else if (reporte == "12")
                {
                    lblTipoReimpresion.Text = "REIMPRESION CONTRATO";
                }
                else if (reporte == "13")
                {
                    lblTipoReimpresion.Text = "REIMPRESION RECIBO";
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
                int id_empresa = (int)Session["id_empresa"];
                ddlSucursal.DataSource = cs_sucursal.ObtenerSucursalesPorEmpresa(ref error, id_empresa.ToString());
                ddlSucursal.DataValueField = "id_sucursal";
                ddlSucursal.DataTextField = "sucursal";
                ddlSucursal.DataBind();

                ddlSucursal.SelectedValue = Session["id_sucursal"].ToString();

                if ((int)Session["id_rol"] == 3 || (int)Session["id_rol"] == 4 || (int)Session["id_rol"] == 5)
                    ddlSucursal.Enabled = false;
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
                if (txtNumeroPrestamo.Text.ToString() == "" || txtNumeroPrestamo.Text.ToString().Length <= 5)
                {
                    showWarning("Debe ingresar un numero de prestamo para poder imprimir.");
                }
                else
                {
                    string scriptReporte = "window.open('WebReport.aspx?tipo_reporte=" + reporte + "&id_sucursal=" + ddlSucursal.SelectedValue.ToString() + "&numero_prestamo=" + txtNumeroPrestamo.Text.ToString() + "');";
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