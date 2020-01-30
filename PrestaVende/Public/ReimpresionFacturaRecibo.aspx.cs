using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class ReimpresionFacturaRecibo : System.Web.UI.Page
    {
        private string error = "";
        private static string reporte = "";
        private CLASS.cs_sucursal cs_sucursal;
        private CLASS.cs_serie cs_serie;
        private CLASS.cs_recibo cs_recibo;
        private CLASS.cs_factura cs_factura;

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
                        cs_sucursal = new CLASS.cs_sucursal();
                        cs_serie = new CLASS.cs_serie();
                        cs_recibo = new CLASS.cs_recibo();
                        cs_factura = new CLASS.cs_factura();
                        ObtenerSucursales();
                        setNombreReimpresion();
                        ObtenerSeries();
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
                if (reporte == "2")
                {
                    this.lblTipoReimpresion.Text = "REIMPRESION DE FACTURA";
                }
                else if (reporte == "5")
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
                int id_empresa = Convert.ToInt32(Session["id_empresa"]);
                this.ddlSucursal.DataSource = cs_sucursal.ObtenerSucursalesPorEmpresa(ref error, id_empresa.ToString());
                this.ddlSucursal.DataValueField = "id_sucursal";
                this.ddlSucursal.DataTextField = "sucursal";
                this.ddlSucursal.DataBind();

                this.ddlSucursal.SelectedValue = Session["id_sucursal"].ToString();

                if (Convert.ToInt32(Session["id_rol"]) == 3 || Convert.ToInt32(Session["id_rol"]) == 4 || Convert.ToInt32(Session["id_rol"]) == 5)
                    this.ddlSucursal.Enabled = false;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private int validaDatos(ref string numero_prestamo)
        {
            try
            {
                int id_documento = 0;
                string resultado = "";

                if (txtNumeroDocumento.Text.ToString() == "")
                {
                    showWarning("Debe ingresar un numero de documento para poder imprimir.");
                    return 0;
                }
                else if(ddlSerie.SelectedValue == "0")
                {
                    showWarning("Debe seleccionar una serie para imprimir documento.");
                    return 0;
                }
                else
                {
                    if (reporte == "2")
                    {
                        cs_factura = new CLASS.cs_factura();
                        DataTable data = new DataTable();
                        numero_prestamo = "";
                        data = cs_factura.getIDFactura(ref error, this.ddlSerie.SelectedValue.ToString(), this.txtNumeroDocumento.Text.ToString());
                        resultado = data.Rows[0]["id_factura_encabezado"].ToString();
                        numero_prestamo = data.Rows[0]["numero_prestamo"].ToString();
                    }
                    else
                    {
                        cs_recibo = new CLASS.cs_recibo();
                        resultado = cs_recibo.getIDRecibo(ref error, this.ddlSerie.SelectedValue.ToString(), this.txtNumeroDocumento.Text.ToString());
                    }

                    if (resultado == "" || resultado == "0" || resultado.Length <= 0)
                    {
                        showWarning("El numero de documento ingresado no es valido, por favor verifique.");
                        return 0;
                    }
                    else
                    {
                        id_documento = Convert.ToInt32(resultado.ToString());
                        return id_documento;
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return 0;
            }
        }

        private void ObtenerSeries()
        {
            try
            {
                int id_tipo_reporte = 0;
                if (reporte == "2")
                {
                    id_tipo_reporte = 1;
                }
                else if (reporte == "5")
                {
                    id_tipo_reporte = 2;
                }
                int id_sucursal = Convert.ToInt32(Session["id_sucursal"]);
                this.ddlSerie.DataSource = cs_serie.ObtenerSeriesImpresion(ref error, id_sucursal, id_tipo_reporte);
                this.ddlSerie.DataValueField = "id_serie";
                this.ddlSerie.DataTextField = "serie";
                this.ddlSerie.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString() + " " + error);
            }
        }

        private void imprimirReporte(string id_documento, string numero_prestamo)
        {
            try
            {
                reporte = Request.QueryString.Get("reporte");
                if (reporte == "2")
                {
                    string scriptReporte = "window.open('WebReport.aspx?tipo_reporte=" + reporte + "&id_factura= " + id_documento + " &id_sucursal=" + this.ddlSucursal.SelectedValue.ToString() + "&numero_contrato=" + numero_prestamo + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReporte", scriptReporte, true);
                }
                else if(reporte == "5")
                {
                    string scriptReporte = "window.open('WebReport.aspx?tipo_reporte=" + reporte + "&id_recibo= " + id_documento + " &id_sucursal=" + this.ddlSucursal.SelectedValue.ToString() + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReporte", scriptReporte, true);
                }
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
            try
            {
                string numero_prestamo = "";
                int id_documento = validaDatos(ref numero_prestamo);
                if (id_documento > 0)
                {
                    imprimirReporte(id_documento.ToString(), numero_prestamo);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
    }
}