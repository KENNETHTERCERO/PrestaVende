﻿using System;
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
                this.txtMontoRecepcion.Enabled = false;
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
                    this.btnRecibirAsignacion.Enabled    = false;
                    this.chkRecibir.Enabled              = false;
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
                    Session["puede_vender"]         = Convert.ToInt32(item["puede_vender"].ToString());
                    this.txtMontoRecepcion.Text          = item[3].ToString();
                    this.lblNumeroCajaAsignada.Text      = item[6].ToString();
                    fecha_asignacion                = Convert.ToDateTime(item[5].ToString());
                    Session["saldo_caja"]           = Convert.ToDecimal(item["monto"].ToString());
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
                Session["id_usuario"] = 0;
                Session["id_empresa"] = 0;
                Session["id_sucursal"] = 0;
                Session["id_rol"] = 0;
                Session["id_caja"] = 0;
                Session["usuario"] = "";
                Session["id_tipo_caja"] = 0;
                Session["primer_nombre"] = "";
                Session["primer_apellido"] = "";
                Session["saldo_caja"] = 0;
                Session.RemoveAll();
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
                if (this.chkRecibir.Checked)
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
                if (asignacion_caja.recibirAsignacionCaja(ref error, id_asignacion, this.txtMontoRecepcion.Text.ToString()))
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