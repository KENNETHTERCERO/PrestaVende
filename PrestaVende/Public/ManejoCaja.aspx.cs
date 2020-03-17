using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class ManejoCaja : System.Web.UI.Page
    {

        private static string error = "";
        private CLASS.cs_caja cs_caja = new CLASS.cs_caja();

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
                CLASS.cs_sucursal cs_sucursal = new CLASS.cs_sucursal();
                int id_empresa = Convert.ToInt32(this.Session["id_empresa"].ToString());
                error = "";
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
                showError(ex.ToString() + error);
            }
        }

        private void ObtenerCajaGeneral()
        {
            try
            {
                cs_caja = new CLASS.cs_caja();
                error = "";
                this.ddlCajaGeneral.DataSource = cs_caja.getCajaGeneral(ref error, ddlSucursal.SelectedValue.ToString()) ;
                this.ddlCajaGeneral.DataValueField = "id_caja";
                this.ddlCajaGeneral.DataTextField = "nombre_caja";
                this.ddlCajaGeneral.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString() + error);
            }
        }

        private void ObtenerTipoMovimiento()
        {
            try
            {
                cs_caja = new CLASS.cs_caja();
                error = "";
                this.ddlTipoMovimiento.DataSource = cs_caja.getTipoMovimiento(ref error);
                this.ddlTipoMovimiento.DataValueField = "id_estado_caja";
                this.ddlTipoMovimiento.DataTextField = "estado_caja";
                this.ddlTipoMovimiento.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString() + error);
            }
        }

        private bool validaDatos()
        {
            try
            {
                if (this.ddlSucursal.SelectedValue.ToString() == "0"){showWarning("Debe seleccionar una sucursal para poder realizar el movimiento."); return false;}
                else if (this.ddlCajaGeneral.SelectedValue.ToString() == "0") { showWarning("Debe seleccionar una caja para poder realizar el movimiento."); return false; }
                else if (this.ddlTipoMovimiento.SelectedValue.ToString() == "0") { showWarning("Debe seleccionar un tipo de movimiento para poder realizar el movimiento."); return false; }
                else if (this.txtMonto.Text == "0") { showWarning("Debe ingresar un monto para poder realizar el movimiento."); return false; }
                else if (this.txtMonto.Text.ToString().Length == 0) { showWarning("Debe ingresar un monto para poder realizar el movimiento."); return false; }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void guardarMovimiento()
        {
            try
            {
                cs_caja = new CLASS.cs_caja();
                error = "";
                if (cs_caja.insertMovimientoCaja(ref error, this.ddlSucursal.SelectedValue.ToString(), this.txtMonto.Text.ToString(), this.ddlTipoMovimiento.SelectedValue.ToString()))
                {
                    showSuccess("Se realizo el " + this.ddlTipoMovimiento.Text.ToString() + " de Q" + this.txtMonto.Text.ToString());
                    ddlSucursal.SelectedValue = "0";
                    ddlCajaGeneral.SelectedValue = "0";
                    ddlTipoMovimiento.SelectedValue = "0";
                    txtMonto.Text = "0";
                }
                else
                {
                    showError("No se pudo realizar el " + this.ddlTipoMovimiento.Text.ToString() + " ************ " + error);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

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

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerCajaGeneral();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaDatos())
                {
                    guardarMovimiento();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

    }
}