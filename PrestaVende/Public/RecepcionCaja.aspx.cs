using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class RecepcionCaja : System.Web.UI.Page
    {
        private CLASS.cs_asignacion_caja asignacion_caja = new CLASS.cs_asignacion_caja();
        private string error= "";
        private static string id_asignacion = "";
        private static DateTime fecha_asignacion, fecha_hoy;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                getDatosCaja();
                txtMontoRecepcion.Enabled = false;
                validaDatos();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #region funciones

        private void validaDatos()
        {
            try
            {
                fecha_hoy = DateTime.Now;

                if (fecha_asignacion.Date < fecha_hoy.Date)
                {
                    btnRecibirAsignacion.Enabled    = false;
                    chkRecibir.Enabled              = false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getDatosCaja()
        {
            try
            {
                id_asignacion = Request.QueryString["id_asignacion"];
                
                foreach (DataRow item in asignacion_caja.getAsignacionCaja(ref error, id_asignacion).Rows)
                {
                    CLASS.cs_usuario.puede_vender   = Convert.ToInt32(item[7].ToString());
                    txtMontoRecepcion.Text          = item[3].ToString();
                    lblNumeroCajaAsignada.Text      = item[6].ToString();
                    fecha_asignacion                = Convert.ToDateTime(item[5].ToString());
                    CLASS.cs_usuario.Saldo_caja     = Convert.ToDecimal(item[3].ToString());
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void cancelarRecepcion()
        {
            try
            {
                CLASS.cs_usuario.id_usuario = 0;
                CLASS.cs_usuario.id_empresa = 0;
                CLASS.cs_usuario.id_sucursal = 0;
                CLASS.cs_usuario.id_rol = 0;
                CLASS.cs_usuario.id_caja = 0;
                CLASS.cs_usuario.usuario = "";
                CLASS.cs_usuario.primer_nombre = "";
                CLASS.cs_usuario.primer_apellido = "";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validarCheck()
        {
            try
            {
                if (chkRecibir.Checked)
                {
                    return true;
                }
                else
                {
                    showWarning("Debe seleccionar el checkbox de recibir para poder continuar.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private bool recibirCaja()
        {
            try
            {
                if (asignacion_caja.recibirAsignacionCaja(ref error, id_asignacion, txtMontoRecepcion.Text.ToString()))
                {
                    Response.Redirect("~/Public/WFPrincipal.aspx", false);
                }
                return true;
            }
            catch (Exception ex)
            {
                showError(error + " ---- " + ex.ToString());
                return false;
            }
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

        //private bool showSuccess(string error)
        //{
        //    divSucceful.Visible = true;
        //    lblSuccess.Controls.Add(new LiteralControl(string.Format("<span style='color:Green'>{0}</span>", error)));
        //    return true;
        //}
        #endregion

        #region controles
        protected void btnCancelarRecepcion_Click(object sender, EventArgs e)
        {
            cancelarRecepcion();
            Response.Redirect("~/WebLogin.aspx", false);
        }

        protected void btnRecibirAsignacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarCheck())
                {
                    recibirCaja();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
        #endregion
    }
}