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
        private static string id_asignacion_recibida = "";
        private int IntCajaActualUsuario = 0;
        private static string id_usuario_caja_asignada = "";
        private CLASS.cs_asignacion_caja mAsignacionCaja = new CLASS.cs_asignacion_caja();

        #endregion


        #region eventos

        private void OpcionSalir()
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
                CLASS.cs_usuario.Saldo_caja = 0;
                CLASS.cs_usuario.id_tipo_caja = 0;
            }
            catch (Exception ex)
            {
                //showError(ex.ToString());
            }
        }

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
                        getCaja();
                        getDataGrid();
                        getUsuarioAsignado();
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

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                getIdAsignacion();
                hideOrShowDiv(false);

                if (CLASS.cs_usuario.id_rol == 5)
                {
                    string id_caja_asignada = mAsignacionCaja.getIDCajaAsignada(ref error);
                    ddIdCaja.SelectedValue = id_caja_asignada;
                    bloqueaCamposSegunTransaccion("cierre");
                }
                else
                {
                    bloqueaCamposSegunTransaccion("nuevo");
                }

                getEstadoCaja(ddIdCaja.SelectedValue);
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
                        ////ACTUALIZA REGISTRO
                        //if (insertAsignacionCaja())
                        //{
                        //     if ((ddIdEstadoCaja.SelectedValue.ToString() == "4") && (IntCajaActualUsuario == Convert.ToInt32(ddIdCaja.SelectedValue.ToString()))) //CUANDO ESTA EN ESTADO DE CIERRE DE LA MISMA CAJA SALE
                        //     {
                        
                        //     }
                        //     else
                        //     {
                        
                        //     }                           
                        //}
                        if (ChbxRecibir.Checked)
                        {
                            if (mAsignacionCaja.recibirCierreCaja(ref error, ddidAsignacion.Text.ToString(), txtMonto.Text.ToString(), ddIdCaja.SelectedValue.ToString()))
                            {
                                if (CLASS.cs_usuario.id_caja.ToString() == ddIdCaja.SelectedValue.ToString())
                                {
                                    OpcionSalir();
                                    Response.Redirect("~/WebLogin.aspx", false);
                                }
                                else
                                {
                                    hideOrShowDiv(true);
                                    getDataGrid();
                                    isUpdate = false;
                                }
                            }
                        }
                        else
                        {
                            showWarning("Usted no ha seleccionado que quiere recibir esta transaccion.");
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
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataTable DtAsignacionCaja = new DataTable();
                    GridViewRow selectedRow = GrdVAsignacionCaja.Rows[index];

                    TableCell id_asignacion_caja = selectedRow.Cells[1];

                    if (mAsignacionCaja.getValidandoEstadoAsignacion(ref error, id_asignacion_caja.Text.ToString()) == "0")
                    {
                        DtAsignacionCaja = mAsignacionCaja.getDatosAsignacionCaja(ref error, id_asignacion_caja.Text.ToString());

                        foreach (DataRow item in DtAsignacionCaja.Rows)
                        {
                            ddidAsignacion.Text = id_asignacion_caja.Text.ToString();
                            ddIdCaja.SelectedValue = item["id_caja"].ToString();
                            ddIdEstadoCaja.SelectedValue = item["id_estado_caja"].ToString();
                            txtMonto.Text = item["monto"].ToString();
                            ddIdUsuarioAsignado.SelectedValue = item["id_usuario_asignado"].ToString();
                            getEstadoCaja(ddIdCaja.SelectedValue);
                        }
                        bloqueaCamposSegunTransaccion("recepcionCierre");
                        isUpdate = true;
                        hideOrShowDiv(false);
                    }
                    else
                    {
                        showWarning("Asignación ya fue recibida.");
                    }
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

        protected void getUsuarioAsignado()
        {
            try
            {
                ddIdUsuarioAsignado.DataSource = mAsignacionCaja.getUsuarioAsignado(ref error);
                ddIdUsuarioAsignado.DataValueField = "id_usuario";
                ddIdUsuarioAsignado.DataTextField = "usuario";
                ddIdUsuarioAsignado.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        protected void getEstadosCajaUpdate(string id_caja)
        {
            try
            {
                ddIdEstadoCaja.DataSource = mAsignacionCaja.getEstadosCajas(ref error, id_caja);
                ddIdEstadoCaja.DataValueField = "id_estado_caja";
                ddIdEstadoCaja.DataTextField = "estado_caja";
                ddIdEstadoCaja.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        protected void getEstadoCaja(string id_caja)
        {
            try
            {
                ddIdEstadoCaja.DataSource = mAsignacionCaja.getEstadoCajaPorRol(ref error, id_caja, ref id_usuario_caja_asignada);
                ddIdEstadoCaja.DataValueField = "id_estado_caja";
                ddIdEstadoCaja.DataTextField = "estado_caja";
                ddIdEstadoCaja.DataBind();

                if (id_usuario_caja_asignada.Equals("")|| id_usuario_caja_asignada.Equals("0"))
                {
                    id_usuario_caja_asignada = "0";
                }
                ddIdUsuarioAsignado.SelectedValue = id_usuario_caja_asignada;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        private void bloqueaCamposSegunTransaccion(string opcion)
        {
            try
            {
                if (opcion.Equals("nuevo"))
                {
                    ddIdCaja.Enabled = true;
                    ddIdEstadoCaja.Enabled = true;
                    txtMonto.Enabled = true;
                    ddIdUsuarioAsignado.Enabled = true;
                    ChbxRecibir.Visible = false;
                    lblRecibir.Visible = false;
                }
                else if (opcion.Equals("cierre"))
                {
                    if (CLASS.cs_usuario.id_rol == 5)
                    {
                        ddIdCaja.Enabled = false;
                        ddIdEstadoCaja.Enabled = false;
                        txtMonto.Enabled = true;
                        ddIdUsuarioAsignado.Enabled = false;
                        ChbxRecibir.Visible = false;
                        lblRecibir.Visible = false;
                    }
                    else
                    {
                        ddIdCaja.Enabled = true;
                        ddIdEstadoCaja.Enabled = true;
                        txtMonto.Enabled = true;
                        ddIdUsuarioAsignado.Enabled = false;
                        ChbxRecibir.Visible = false;
                        lblRecibir.Visible = false;
                    }
                }
                else if (opcion.Equals("recepcionCierre"))
                {
                    ddIdCaja.Enabled = false;
                    ddIdEstadoCaja.Enabled = false;
                    txtMonto.Enabled = false;
                    ddIdUsuarioAsignado.Enabled = false;
                    ChbxRecibir.Visible = true;
                    lblRecibir.Visible = true;
                }
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
                //else if (ddIdEstadoCaja.SelectedValue.Equals("0")) { showWarning("Usted debe seleccionar un estado de caja válido."); return false; }
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

                if (mAsignacionCaja.insertAsignacionCaja(ref error, ddidAsignacion.Text, ddIdCaja.SelectedValue.ToString(), ddIdEstadoCaja.SelectedValue.ToString(), txtMonto.Text, "1", thisDay.ToString("MM/dd/yyyy HH:mm:ss"), thisDay.ToString("MM/dd/yyyy HH:mm:ss"), CLASS.cs_usuario.usuario, ddIdUsuarioAsignado.SelectedValue.ToString(), blnRecibir, id_asignacion_recibida, ref IntCajaActualUsuario))                  
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

        protected void ddIdCaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            getEstadoCaja(ddIdCaja.SelectedValue);
        }
    }
}

   