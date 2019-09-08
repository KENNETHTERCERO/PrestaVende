using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantenimientoCaja : System.Web.UI.Page
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

        private CLASS.cs_caja mCaja = new CLASS.cs_caja();
        private CLASS.cs_liquidacion mLiquidacion = new CLASS.cs_liquidacion();

        #endregion

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
                        getDataGrid();
                        getEstadoCaja();
                        getSucursal();
                        getTipoCaja();
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
                ddidCaja.Text = mCaja.getIDMaxCaja(ref error);
                hideOrShowDiv(false);
                cleanControls();
            }
            catch (Exception)
            {          
                throw;
            }
        }

        private void cleanControls()
        {
            try
            {
                txtNombreCaja.Text = "";
                txtSaldo.Text = "";
                ddidSucursal.SelectedValue = "0";
                ddidTipoCaja.SelectedValue = "0";
                ddlEstado.SelectedValue = "1";
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
                if (validateInformation())
                {
                    if (isUpdate)
                    {
                        //ACTUALIZA REGISTRO
                        if (updateCaja())
                        {
                            hideOrShowDiv(true);
                            getDataGrid();
                            isUpdate = false;
                        }
                    }
                    else
                    {
                        //GUARDA NUEVO
                        if (insertCaja())
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

        private bool insertCaja()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                if (mCaja.insertCaja(ref error, ddidSucursal.SelectedValue.ToString(), ddidTipoCaja.SelectedValue.ToString(), "1", txtNombreCaja.Text.ToString(), txtSaldo.Text.ToString(), ddlEstado.SelectedValue.ToString(), thisDay.ToString("MM/dd/yyyy HH:mm:ss"), thisDay.ToString("MM/dd/yyyy HH:mm:ss")))
                {
                    showSuccess("Se agrego la caja correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo agregar la caja, por favor, valide los datos y vuelva a intentarlo.");
                }
            }
            catch (Exception ex)
            {
                showError(error + " - " + ex.ToString());
                return false;
            }
        }

        private bool validateInformation()
        {
            try
            {
                if (txtNombreCaja.Text.ToString().Length < 3) { showWarning("Usted debe agregar un nombre de caja para poder guardar."); return false; }
                else if (txtSaldo.Text.ToString().Length < 3) { showWarning("Usted debe agregar un saldo para poder guardar."); return false; }            
                else if (ddidSucursal.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar una sucursal para poder guardar."); return false; }
                else if (ddidTipoCaja.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar un tipo de caja para poder guardar."); return false; }
                else
                    return true;            
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void getEstadoCaja()
        {
            try
            {
                ddlEstado.DataSource = mCaja.getEstadoCaja(ref error);
                ddlEstado.DataValueField = "id";
                ddlEstado.DataTextField = "estado";
                ddlEstado.DataBind();
                ddlEstado.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        private void getSucursal()
        {
            try
            {
                ddidSucursal.DataSource = mLiquidacion.getSucursal(ref error);
                ddidSucursal.DataValueField = "id_sucursal";
                ddidSucursal.DataTextField = "sucursal";
                ddidSucursal.DataBind();
                ddidSucursal.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        private void getTipoCaja()
        {
            try
            {
                ddidTipoCaja.DataSource = mCaja.getTipoCaja(ref error);
                ddidTipoCaja.DataValueField = "id_tipo_caja";
                ddidTipoCaja.DataTextField = "tipo_caja";
                ddidTipoCaja.DataBind();
                ddidTipoCaja.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        private void getDataGrid()
        {
            try
            {
                GrdVCaja.DataSource = mCaja.getCaja(ref error);
                GrdVCaja.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool updateCaja()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                string[] datosUpdate = new string[7];

                datosUpdate[0] = ddidCaja.Text;
                datosUpdate[1] = ddidSucursal.SelectedValue;
                datosUpdate[2] = ddidTipoCaja.SelectedValue;
                datosUpdate[3] = txtNombreCaja.Text;
                datosUpdate[4] = txtSaldo.Text;
                datosUpdate[5] = ddlEstado.SelectedValue;
                datosUpdate[6] = thisDay.ToString("MM/dd/yyyy HH:mm:ss");

                if (mCaja.updateCaja(ref error, datosUpdate))
                {
                    showSuccess("Se modifico la caja correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo modificar la caja, por favor, valide los datos y vuelva a intentarlo.");
                }
            }
            catch (Exception ex)
            {
                showError(error + " - " + ex.ToString());
                return false;
            }
        }

        protected void gvSize_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataTable DtCaja;
                    GridViewRow selectedRow = GrdVCaja.Rows[index];

                    TableCell id_caja = selectedRow.Cells[1];
                    DtCaja = mCaja.getObtieneDatosModificar(ref error, id_caja.Text.ToString());
              
                    foreach (DataRow item in DtCaja.Rows)
                    {
                        ddidCaja.Text = item[0].ToString();
                        ddidSucursal.SelectedValue = item[1].ToString();
                        ddidTipoCaja.SelectedValue = item[2].ToString();
                        txtNombreCaja.Text = item[4].ToString();
                        txtSaldo.Text = item[5].ToString();
                        ddlEstado.SelectedValue = item[6].ToString();
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
    }

}