using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantenimientoAsignacionCaja : System.Web.UI.Page
    {
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

        #region variables 

        private static string error = "";
        private static bool isUpdate = false;

        private CLASS.cs_asignacion_caja mAsignacionCaja = new CLASS.cs_asignacion_caja();

        #endregion


        #region eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null)
                {
                    Response.Redirect("WFWebLogin.aspx");
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
                        hideOrShowDiv(true);
                        getIdAsignacion();
                        getEstadoAsignacion();
                        getCaja();
                        getDataGrid();
                        getUsuarioAsignado();
                        getEstadoCaja();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void hideOrShowDiv(bool hidePanel)
        {
            try
            {
                if (hidePanel.Equals(true))
                {
                    div_ingresa_datos.Visible = false;
                    div_gridView.Visible = true;
                    btnSalir.Visible = true;
                    btnCreate.Visible = true;
                    btnCancel.Visible = false;
                    btnGuardar.Visible = false;                 
                }
                else
                {
                    div_ingresa_datos.Visible = true;
                    div_gridView.Visible = false;
                    btnSalir.Visible = false;
                    btnCreate.Visible = false;
                    btnCancel.Visible = true;
                    btnGuardar.Visible = true;                   
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFPrincipal.aspx");
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                getIdAsignacion();
                hideOrShowDiv(false);
                cleanControls();
                ddIdCaja.Enabled = true;
                ChbxRecibir.Visible = false;
                lblRecibir.Visible = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateInformation())
                {
                    if (isUpdate)
                    {
                       //ACTUALIZA REGISTRO
                       if (insertAsignacionCaja())
                       {
                           hideOrShowDiv(true);
                           getDataGrid();
                           isUpdate = false;
                       }
                    }
                    else
                    {
                        //GUARDA NUEVO
                        if (insertAsignacionCaja())
                        {
                            hideOrShowDiv(true);
                            getDataGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            hideOrShowDiv(true);
        }

        protected void gvSize_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "select")
                {
                    cleanControls();
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataTable DtAsignacionCaja;
                    GridViewRow selectedRow = GrdVAsignacionCaja.Rows[index];

                    TableCell id_asignacion_caja = selectedRow.Cells[1];
                    DtAsignacionCaja = mAsignacionCaja.getDatosAsignacionCaja(ref error, id_asignacion_caja.Text.ToString());

                    foreach (DataRow item in DtAsignacionCaja.Rows)
                    {
                        ddidAsignacion.Text = mAsignacionCaja.getIDMaxAsignacionCaja(ref error);
                        ddIdCaja.SelectedValue = item[1].ToString();
                        ddIdEstadoCaja.SelectedValue = item[2].ToString();
                        ddIdEstado.SelectedValue = item[4].ToString();
                        ddIdUsuarioAsignado.SelectedValue = item[8].ToString();

                        ddIdCaja.Enabled = false;
                        ddIdEstadoCaja.Visible = false;
                        ddIdUsuarioAsignado.Visible = false;
                        ddIdEstado.Visible = false;
                        lblCaja.Visible = false;
                        lblEstado.Visible = false;
                        lblEstadoCaja.Visible = false;
                        lblUsuarioAsignado.Visible = false;
                        ChbxRecibir.Visible = true;
                        lblRecibir.Visible = true;
                    }

                    isUpdate = true;
                    hideOrShowDiv(false);
                    divSucceful.Visible = false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void GrdVAsignacionCaja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ChbxRecibir_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion


        #region Procedimientos y Funciones

        protected void getIdAsignacion()
        {        
            try
            {
                ddidAsignacion.Text = mAsignacionCaja.getIDMaxAsignacionCaja(ref error);
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
            
        }

        protected void getCaja()
        {
            try
            {
                ddIdCaja.DataSource = mAsignacionCaja.getCaja(ref error);
                ddIdCaja.DataValueField = "id_caja";
                ddIdCaja.DataTextField = "nombre_caja";
                ddIdCaja.DataBind();
                ddIdCaja.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        protected void getDataGrid()
        {
            try
            {
                GrdVAsignacionCaja.DataSource = mAsignacionCaja.getAsignacionCaja(ref error);
                GrdVAsignacionCaja.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void getEstadoAsignacion()
        {
            try
            {
                ddIdEstado.DataSource = mAsignacionCaja.getEstadoAsignacionCaja(ref error);
                ddIdEstado.DataValueField = "id";
                ddIdEstado.DataTextField = "estado";
                ddIdEstado.DataBind();
                ddIdEstado.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        protected void getUsuarioAsignado()
        {
            try
            {
                ddIdUsuarioAsignado.DataSource = mAsignacionCaja.getUsuarioAsignado(ref error);
                ddIdUsuarioAsignado.DataValueField = "id_usuario";
                ddIdUsuarioAsignado.DataTextField = "usuario";
                ddIdUsuarioAsignado.DataBind();
                ddIdUsuarioAsignado.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        protected void getEstadoCaja()
        {
            try
            {
                ddIdEstadoCaja.DataSource = mAsignacionCaja.getEstadoCajaPorRol(ref error);
                ddIdEstadoCaja.DataValueField = "id_estado_caja";
                ddIdEstadoCaja.DataTextField = "estado_caja";
                ddIdEstadoCaja.DataBind();
                ddIdEstadoCaja.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        private void cleanControls()
        {
            try
            {
                //getIdAsignacion();
                ddIdEstado.SelectedValue = "1";
                ddIdEstadoCaja.SelectedIndex = -1;
                ddIdCaja.SelectedIndex = -1;
                ddIdUsuarioAsignado.SelectedIndex = -1;
                txtMonto.Text = "0";
                ChbxRecibir.Checked = false;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validateInformation()
        {
            try
            {
                if (txtMonto.Text.ToString().Equals("0")) { showWarning("Usted debe ingresar un monto válido."); return false; }
                else if (ddIdCaja.SelectedValue.Equals("0")) { showWarning("Usted debe seleccionar una caja válida."); return false; }
                else if (ddIdEstadoCaja.SelectedValue.Equals("0")) { showWarning("Usted debe seleccionar un estado de caja válido."); return false; }
                else if (ddIdUsuarioAsignado.SelectedValue.Equals("0")) { showWarning("Usted debe seleccionar un usuario válido."); return false; }

                else
                    return true;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }


        private bool insertAsignacionCaja()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                Boolean blnRecibir = false;

                if (ChbxRecibir.Checked == true)
                {
                    blnRecibir = true;
                }

                if (mAsignacionCaja.insertAsignacionCaja(ref error, ddidAsignacion.Text, ddIdCaja.SelectedValue.ToString(), ddIdEstadoCaja.SelectedValue.ToString(), txtMonto.Text, ddIdEstado.SelectedValue.ToString(), thisDay.ToString("MM/dd/yyyy HH:mm:ss"), thisDay.ToString("MM/dd/yyyy HH:mm:ss"), CLASS.cs_usuario.usuario, ddIdUsuarioAsignado.SelectedValue.ToString(), blnRecibir))                  
                {
                    showSuccess("Se realizó la asignación de caja correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo agregar la asignación de caja, por favor, valide los datos y vuelva a intentarlo.");
                }
            }
            catch (Exception ex)
            {
                showError(error + " - " + ex.ToString());
                return false;
            }

        }
        #endregion

    }
}

   