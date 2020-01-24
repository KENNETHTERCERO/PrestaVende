using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class ReporteInventario : System.Web.UI.Page
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

                if (cookie == null && Convert.ToInt32(Session["id_usuario"]) == 0)
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
            string id_sucuarsal = ddlSucursal.SelectedValue.ToString();

            if (int.Parse(id_sucuarsal) > 0)
            {
                string scriptEstadoCuenta = "window.open('WebReport.aspx?tipo_reporte=9" + "&id_sucursal=" + id_sucuarsal +  "');";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "NewWindow", scriptEstadoCuenta, true);
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
                ddlSucursal.DataSource = cs_sucursal.ObtenerSucursalesPorEmpresa(ref error, id_empresa.ToString());
                ddlSucursal.DataValueField = "id_sucursal";
                ddlSucursal.DataTextField = "sucursal";
                ddlSucursal.DataBind();

                ddlSucursal.SelectedValue = Session["id_sucursal"].ToString();


                if (Convert.ToInt32(Session["id_rol"]) == 3 || Convert.ToInt32(Session["id_rol"]) == 4 || Convert.ToInt32(Session["id_rol"]) == 5)
                    ddlSucursal.Enabled = false;
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


    }
}