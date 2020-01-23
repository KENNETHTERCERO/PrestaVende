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
                Session["id_usuario"] = 0;
                Session["id_empresa"] = 0;
                Session["id_sucursal"] = 0;
                Session["id_rol"] = 0;
                Session["id_caja"] = 0;
                Session["usuario"] = "";
                Session["primer_nombre"] = "";
                Session["primer_apellido"] = "";
                Session["saldo_caja"] = 0;
                Session["id_tipo_caja"] = 0;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
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

                if (Convert.ToInt32(Session["id_rol"]) == 5)
                {
                    mAsignacionCaja = new CLASS.cs_asignacion_caja();
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
            catch (Exception ex)
            {
                showError("Error obteniendo Id de caja asignada a usuario." + ex.ToString());
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
                        if (ChbxRecibir.Checked)
                        {
                            mAsignacionCaja = new CLASS.cs_asignacion_caja();
                            if (ddIdEstadoCaja.SelectedValue.ToString().Equals("4"))
                            {
                                if (mAsignacionCaja.recibirCierreCaja(ref error, ddidAsignacion.Text.ToString(), txtMonto.Text.ToString(), ddIdCaja.SelectedValue.ToString(), ddIdUsuarioAsignado.SelectedValue.ToString()))
                                {
                                    if (Convert.ToString(Session["id_caja"]) == ddIdCaja.SelectedValue.ToString() || (ddIdEstadoCaja.SelectedValue.ToString() == "4" && Convert.ToUInt32(Session["id_rol"]) == 5))
                                    {
                                        OpcionSalir();
                                        Response.Redirect("~/WebLogin.aspx", false);
                                    }
                                    hideOrShowDiv(true);
                                    getDataGrid();
                                    isUpdate = false;
                                }
                                else
                                {
                                    showError("No se pudo realizar recepcion de cierre de caja." + error);
                                }
                            }
                            else if (ddIdEstadoCaja.SelectedValue.ToString().Equals("7"))
                            {
                                if (mAsignacionCaja.recibirIncrementoCapitalCaja(ref error, ddidAsignacion.Text.ToString(), txtMonto.Text.ToString(), ddIdCaja.SelectedValue.ToString(), ddIdUsuarioAsignado.SelectedValue.ToString()))
                                {
                                    if (Convert.ToString(Session["id_caja"]) == ddIdCaja.SelectedValue.ToString() || ddIdEstadoCaja.SelectedValue.ToString() == "7")
                                    {
                                        OpcionSalir();
                                        Response.Redirect("~/WebLogin.aspx", false);
                                    }
                                    hideOrShowDiv(true);
                                    getDataGrid();
                                    isUpdate = false;
                                }
                                else
                                {
                                    showError("No se pudo realizar recepcion de incremento a capital." + error);
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
                    mAsignacionCaja = new CLASS.cs_asignacion_caja();
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

        protected void ddIdCaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            getEstadoCaja(ddIdCaja.SelectedValue);
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
                mAsignacionCaja = new CLASS.cs_asignacion_caja();
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
                mAsignacionCaja = new CLASS.cs_asignacion_caja();
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
                mAsignacionCaja = new CLASS.cs_asignacion_caja();
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
                mAsignacionCaja = new CLASS.cs_asignacion_caja();
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
                mAsignacionCaja = new CLASS.cs_asignacion_caja();
                ddIdEstadoCaja.DataSource = mAsignacionCaja.getEstadoCajaPorRol(ref error, id_caja, ref id_usuario_caja_asignada);
                ddIdEstadoCaja.DataValueField = "id_estado_caja";
                ddIdEstadoCaja.DataTextField = "estado_caja";
                ddIdEstadoCaja.DataBind();

                if (id_usuario_caja_asignada.Equals("") || id_usuario_caja_asignada.Equals("0"))
                {
                    id_usuario_caja_asignada = "0";
                }
                else
                {
                    ddIdUsuarioAsignado.SelectedValue = id_usuario_caja_asignada;
                }
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
                    ChbxRecibir.Checked = false;
                    lblRecibir.Visible = false;
                    txtMonto.Text = "";
                    id_usuario_caja_asignada = "0";
                }
                else if (opcion.Equals("cierre"))
                {
                    if (Convert.ToInt32(Session["id_rol"]) == 5)
                    {
                        ddIdCaja.Enabled = false;
                        ddIdEstadoCaja.Enabled = true;
                        txtMonto.Enabled = true;
                        ddIdUsuarioAsignado.Enabled = false;
                        ChbxRecibir.Visible = false;
                        ChbxRecibir.Checked = false;
                        lblRecibir.Visible = false;
                        txtMonto.Text = "";
                    }
                    else
                    {
                        ddIdCaja.Enabled = true;
                        ddIdEstadoCaja.Enabled = true;
                        txtMonto.Enabled = true;
                        ddIdUsuarioAsignado.Enabled = false;
                        ChbxRecibir.Visible = false;
                        ChbxRecibir.Checked = false;
                        lblRecibir.Visible = false;
                        txtMonto.Text = "";
                    }
                }
                else if (opcion.Equals("recepcionCierre"))
                {
                    ddIdCaja.Enabled = false;
                    ddIdEstadoCaja.Enabled = false;
                    txtMonto.Enabled = false;
                    ddIdUsuarioAsignado.Enabled = false;
                    ChbxRecibir.Visible = true;
                    ChbxRecibir.Checked = false;
                    lblRecibir.Visible = true;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validaMontoCierre()
        {
            try
            {
                if (ddIdEstadoCaja.SelectedValue.ToString() == "4")
                {
                    decimal saldo_caja_validacion = 0;
                    mAsignacionCaja = new CLASS.cs_asignacion_caja();
                    saldo_caja_validacion = mAsignacionCaja.getSaldoCajaValidacion(ref error, Convert.ToInt32(ddIdCaja.SelectedValue.ToString()));

                    if (saldo_caja_validacion == 0 && error.Length > 0)
                    {
                        showError(error);
                        return false;
                    }
                    else if (saldo_caja_validacion != Convert.ToDecimal(txtMonto.Text.ToString()))
                    {
                        showError("El monto ingresado no es igual al saldo de caja.");
                        return false;
                    }
                    else
                        return true;
                }
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

        private bool validateInformation()
        {
            try
            {
                if (txtMonto.Text.ToString().Length <= 0){ showWarning("Debe ingresar monto para poder realizar accion."); return false; }
                else if (Convert.ToDecimal(txtMonto.Text.ToString()) <= 0) { showWarning("Usted debe ingresar un monto válido."); return false; }
                else if (ddIdCaja.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar una caja válida."); return false; }
                else if (ddIdEstadoCaja.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar un estado de caja válido."); return false; }
                else if (ddIdUsuarioAsignado.SelectedValue.Equals("0")) { showWarning("Usted debe seleccionar un usuario válido."); return false; }
                else if (!validaMontoCierre()){ showWarning("No se puede realizar el cierre de caja."); return false; }
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
                bool blnRecibir = false;

                if (ChbxRecibir.Checked == true)
                {
                    blnRecibir = true;
                }
                mAsignacionCaja = new CLASS.cs_asignacion_caja();
                if (mAsignacionCaja.insertAsignacionCaja(ref error, ddidAsignacion.Text, ddIdCaja.SelectedValue.ToString(), ddIdEstadoCaja.SelectedValue.ToString(), txtMonto.Text, "1", thisDay.ToString("MM/dd/yyyy HH:mm:ss"), thisDay.ToString("MM/dd/yyyy HH:mm:ss"), Session["usuario"].ToString(), ddIdUsuarioAsignado.SelectedValue.ToString(), blnRecibir, id_asignacion_recibida, ref IntCajaActualUsuario))
                {
                    if (ddIdEstadoCaja.SelectedValue.ToString() == "2")
                    {
                        showSuccess("Se realizó la asignación de caja correctamente.");
                    }
                    else if (ddIdEstadoCaja.SelectedValue.ToString() == "7")
                    {
                        showSuccess("Se realizó incremento de caja correctamente.");
                    }
                    else if (ddIdEstadoCaja.SelectedValue.ToString() == "4")
                    {
                        if (ddIdEstadoCaja.SelectedValue.ToString() == "4" && Convert.ToInt32(Session["id_rol"]) == 5)
                        {
                            OpcionSalir();
                            Response.Redirect("~/WebLogin.aspx", false);
                        }
                        else
                        {
                            showSuccess("Se realizó cierre de caja correctamente.");
                        }
                    }
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

   