using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class AnulacionFactura : System.Web.UI.Page
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
                this.lblTipoReimpresion.Text = "ANULACION DE FACTURA";
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
                cs_factura = new CLASS.cs_factura();
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

        private void anularFactura()
        {
            try
            {
                if (this.txtNumeroDocumento.Text.ToString() == "")
                {
                    showWarning("Debe ingresar un numero de documento para poder anular.");
                }
                else if (this.ddlSerie.SelectedValue == "0")
                {
                    showWarning("Debe seleccionar una serie para poder anular.");
                }
                else
                {
                    cs_factura = new CLASS.cs_factura();

                    error = "";
                    if (cs_factura.anularFactura(ref error, this.Session["id_sucursal"].ToString(), this.ddlSerie.SelectedValue.ToString(), this.txtNumeroDocumento.Text.ToString()))
                    {
                        showSuccess("Factura anulada correctamente.");
                    }
                    else
                    {
                        showError(error + ".");
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void ObtenerSeries()
        {
            try
            {
                int id_tipo_serie = 0;
                id_tipo_serie = 1;
                int id_sucursal = Convert.ToInt32(Session["id_sucursal"]);
                cs_serie = new CLASS.cs_serie();
                this.ddlSerie.DataSource = cs_serie.ObtenerSeriesImpresion(ref error, id_sucursal, id_tipo_serie);
                this.ddlSerie.DataValueField = "id_serie";
                this.ddlSerie.DataTextField = "serie";
                this.ddlSerie.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString() + " " + error);
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

        private bool showSuccess(string error)
        {
            divSucceful.Visible = true;
            lblSuccess.Controls.Add(new LiteralControl(string.Format("<span style='color:Green'>{0}</span>", error)));
            return true;
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            anularFactura();
        }
    }
}