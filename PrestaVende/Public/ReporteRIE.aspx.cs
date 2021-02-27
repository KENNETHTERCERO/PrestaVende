using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class ReporteRIE : System.Web.UI.Page
    {

        #region variables

        private string error = "";
        private CLASS.cs_sucursal cs_sucursal = new CLASS.cs_sucursal();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && Convert.ToInt32(this.Session["id_usuario"].ToString()) == 0)
                {
                    Response.Redirect("~/WFWebLogin.aspx");
                }

                if (IsPostBack)
                {
                    if (lblWarning.Text == "") { divWarning.Visible = false; }
                    if (lblError.Text == "") { divError.Visible = false; }
                    if (lblSuccess.Text == "") { divSucceful.Visible = false; }
                }
                else
                {
                    txtFechaInicial.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtFechaFin.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    ObtenerSucursales();
                }

            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            string id_sucursal = this.ddlSucursal.SelectedValue.ToString();

            if (int.Parse(id_sucursal) > 0)
            {
                if (this.txtFechaInicial.Text.ToString().Length < 1)
                    showWarning("Usted debe ingresar una fecha de inicio para poder generar el reporte.");
                else if (this.txtFechaFin.Text.ToString().Length < 1)
                    showWarning("Usted debe ingresar una fecha de fin para poder generar el reporte.");
                else
                {
                    string scriptEstadoCuenta = "window.open('WebReport.aspx?tipo_reporte=15" + "&id_sucursal=" + id_sucursal + "&id_empresa=" + Convert.ToInt32(this.Session["id_empresa"]) + "&fecha_inicio=" + this.txtFechaInicial.Text + "&fecha_fin=" + this.txtFechaFin.Text + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "NewWindow", scriptEstadoCuenta, true);
                }
            }
            else
                showWarning("Seleccione una sucursal para poder generar el reporte.");
        }


        #region metodos

        private void ObtenerSucursales()
        {
            try
            {
                int id_empresa = Convert.ToInt32(Session["id_empresa"]);
                //int id_empresa = 1;

                cs_sucursal = new CLASS.cs_sucursal();
                this.ddlSucursal.DataSource = cs_sucursal.ObtenerSucursalesPorEmpresa(ref error, id_empresa.ToString());
                this.ddlSucursal.DataValueField = "id_sucursal";
                this.ddlSucursal.DataTextField = "sucursal";
                this.ddlSucursal.DataBind();

                if (this.Session["id_rol"].ToString().Equals("3") || this.Session["id_rol"].ToString().Equals("4") || this.Session["id_rol"].ToString().Equals("5"))
                {
                    this.ddlSucursal.SelectedValue = this.Session["id_sucursal"].ToString();
                    //this.ddlSucursal.SelectedValue = "1";
                    this.ddlSucursal.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool showError(string error)
        {
            divError.Visible = true;
            lblError.Controls.Add(new LiteralControl(string.Format("<span style='color:Red'>{0}</span>", error)));
            return true;
        }

        private bool showWarning(string warning)
        {
            divWarning.Visible = true;
            lblWarning.Controls.Add(new LiteralControl(string.Format("<span style='color:Orange'>{0}</span>", warning)));
            return true;
        }

        #endregion

        protected void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            string id_sucursal = this.ddlSucursal.SelectedValue.ToString();

            if (int.Parse(id_sucursal) > 0)
            {
                if (this.txtFechaInicial.Text.ToString().Length < 1)
                    showWarning("Usted debe ingresar una fecha de inicio para poder generar el reporte.");
                else if (this.txtFechaFin.Text.ToString().Length < 1)
                    showWarning("Usted debe ingresar una fecha de fin para poder generar el reporte.");
                else
                {
                    string scriptEstadoCuenta = "window.open('WebReport.aspx?tipo_reporte=15" + "&id_sucursal=" + id_sucursal + "&id_empresa=" + Convert.ToInt32(this.Session["id_empresa"]) + "&fecha_inicio=" + this.txtFechaInicial.Text + "&fecha_fin=" + this.txtFechaFin.Text + "&tipo=excel');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "NewWindow", scriptEstadoCuenta, true);
                }
            }
            else
                showWarning("Seleccione una sucursal para poder generar el reporte.");
        }
    }
}