using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class ReporteCancelaciones : System.Web.UI.Page
    {
        private string error = "";
        private CLASS.cs_sucursal cs_sucursal = new CLASS.cs_sucursal();

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
                        ObtenerSucursales();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void ObtenerSucursales()
        {
            try
            {
                int id_empresa = Convert.ToInt32(this.Session["id_empresa"].ToString());
                cs_sucursal = new CLASS.cs_sucursal();
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

        private void generaReporte()
        {
            try
            {
                string id_sucuarsal = this.ddlSucursal.SelectedValue.ToString();

                if (int.Parse(id_sucuarsal) > 0)
                    if (this.txtFechaInicial.Text.ToString().Length < 1)
                        showWarning("Usted debe ingresar una fecha de inicio para poder generar el reporte.");
                    else if (this.txtFechaFin.Text.ToString().Length < 1)
                        showWarning("Usted debe ingresar una fecha de fin para poder generar el reporte.");
                    else
                    {
                        string scriptEstadoCuenta = "window.open('WebReport.aspx?tipo_reporte=13" + "&id_sucursal=" + id_sucuarsal + "&fecha_inicio=" + this.txtFechaInicial.Text + "&fecha_fin=" + this.txtFechaFin.Text + "&transaccion=10');";
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "NewWindow", scriptEstadoCuenta, true);
                    }
                else
                    showWarning("Seleccione una sucursal para poder generar el reporte.");
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            generaReporte();
        }

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }
}