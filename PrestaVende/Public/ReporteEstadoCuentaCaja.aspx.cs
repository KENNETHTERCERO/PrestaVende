﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class ReporteEstadoCuentaCaja : System.Web.UI.Page
    {
        private string error = "";
        private CLASS.cs_sucursal cs_sucursal = new CLASS.cs_sucursal();
        private CLASS.cs_caja cs_caja = new CLASS.cs_caja();
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
                        ObtenerSucursales();
                        ObtenerCajas();
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
                int id_empresa = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);
                ddlSucursal.DataSource = cs_sucursal.ObtenerSucursalesPorUsuario(ref error, id_empresa.ToString());
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

        private void ObtenerCajas()
        {
            try
            {
                //int id_empresa = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);
                ddlCaja.DataSource = cs_caja.getCajasCombo(ref error,  ddlSucursal.SelectedValue.ToString());
                ddlCaja.DataValueField = "id_caja";
                ddlCaja.DataTextField = "nombre_caja";
                ddlCaja.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
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
            string id_sucuarsal = this.ddlSucursal.SelectedValue.ToString();

            if (int.Parse(id_sucuarsal) > 0)
                if (this.txtFechaInicial.Text.ToString().Length < 1)
                    showWarning("Usted debe ingresar una fecha de inicio para poder generar el reporte.");
                else if (this.txtFechaFin.Text.ToString().Length < 1)
                    showWarning("Usted debe ingresar una fecha de fin para poder generar el reporte.");
                else
                {
                    string FechaInicial = this.txtFechaInicial.Text.ToString();
                    string FechaFinal = this.txtFechaFin.Text.ToString();
                    string id_caja = this.ddlCaja.SelectedValue.ToString();
                    string scriptEstadoCuenta = "window.open('WebReport.aspx?tipo_reporte=7" + "&id_sucursal=" + id_sucuarsal + "&fecha_inicio=" + FechaInicial + "&fecha_fin=" + FechaFinal + "&id_caja=" + id_caja +"');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "EstadoDeCuenta", scriptEstadoCuenta, true);
                }
            else
                showWarning("Seleccione una sucursal para poder generar el reporte.");
        }

        protected void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            string id_sucuarsal = this.ddlSucursal.SelectedValue.ToString();

            if (int.Parse(id_sucuarsal) > 0)
                if (this.txtFechaInicial.Text.ToString().Length < 1)
                    showWarning("Usted debe ingresar una fecha de inicio para poder generar el reporte.");
                else if (this.txtFechaFin.Text.ToString().Length < 1)
                    showWarning("Usted debe ingresar una fecha de fin para poder generar el reporte.");
                else
                {
                    string FechaInicial = this.txtFechaInicial.Text.ToString();
                    string FechaFinal = this.txtFechaFin.Text.ToString();
                    string id_caja = this.ddlCaja.SelectedValue.ToString();
                    string scriptEstadoCuenta = "window.open('WebReport.aspx?tipo_reporte=7" + "&id_sucursal=" + id_sucuarsal + "&fecha_inicio=" + FechaInicial + "&fecha_fin=" + FechaFinal + "&id_caja=" + id_caja + "&tipo=excel');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "EstadoDeCuenta", scriptEstadoCuenta, true);
                }
            else
                showWarning("Seleccione una sucursal para poder generar el reporte.");
        }
    }
}