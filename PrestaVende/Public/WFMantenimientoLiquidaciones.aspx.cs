using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantenimientoLiquidaciones : System.Web.UI.Page
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
                        getSucursal();
                        getEstadoLiquidacion();
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
                ddidLiquidacion.Text = mLiquidacion.getIDMaxLiquidacion(ref error);
                lblAnular.Visible = false;
                ChbxAnular.Visible = false;
                ddidSucursal.Enabled = true;
                txtNumeroPrestamo.ReadOnly = false;
                txtMontoLiquidacion.ReadOnly = false;               
                lblEstado.Visible = true;
                ddlEstado.Visible = true;

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
                txtMontoLiquidacion.Text = "";
                txtNumeroPrestamo.Text = "";
                ddlEstado.SelectedValue = "1";
                ddidSucursal.SelectedValue = "0";
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
                        if (updateLiquidacion())
                        {
                            hideOrShowDiv(true);
                            getDataGrid();
                            isUpdate = false;
                        }
                    }
                    else
                    {
                        //GUARDA NUEVO
                        if (insertLiquidacion())
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

        private bool insertLiquidacion()
        {
            try
            {
                DateTime thisDay = DateTime.Now;

                if (mLiquidacion.getValidaPrestamo(ref error, txtNumeroPrestamo.Text.ToString()))
                {
                    if (mLiquidacion.insertLiquidacion(ref error, ddidSucursal.SelectedValue.ToString(), txtNumeroPrestamo.Text.ToString(), txtMontoLiquidacion.Text.ToString(), ddlEstado.SelectedValue.ToString(), thisDay.ToString("MM/dd/yyyy HH:mm:ss"), thisDay.ToString("MM/dd/yyyy HH:mm:ss")))
                    {
                        showSuccess("Se agrego la liquidación correctamente.");
                        return true;
                    }
                    else
                    {
                        throw new SystemException("No se pudo agregar la liquidación, por favor, valide los datos y vuelva a intentarlo.");
                    }
                }
                else
                {
                    showWarning("No se pudo agregar la liquidación, por favor ingrese un número de prestamo válido.");
                    return false;
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
                if (txtNumeroPrestamo.Text.ToString().Length < 1) { showWarning("Usted debe agregar un número de prestamo para poder guardar."); return false; }
                else if (txtMontoLiquidacion.Text.ToString().Length < 3) { showWarning("Usted debe agregar una descripcion para poder guardar."); return false; }            
                else if (ddidSucursal.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar una sucursal para poder guardar."); return false; }
                
                else
                    return true;            
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void getEstadoLiquidacion()
        {
            try
            {
                ddlEstado.DataSource = mLiquidacion.getEstadoLiquidacion(ref error);
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

        private void getDataGrid()
        {
            try
            {
              GrdVLiquidacion.DataSource = mLiquidacion.getLiquidacion(ref error);
              GrdVLiquidacion.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool updateLiquidacion()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                string[] datosUpdate = new string[3];

                datosUpdate[0] = ddidLiquidacion.Text;
                datosUpdate[1] = thisDay.ToString("MM/dd/yyyy HH:mm:ss");

                if (ChbxAnular.Checked == true)
                {
                    datosUpdate[2] = "0";
                }
                else
                {
                    datosUpdate[2] = "1";
                }

                if (mLiquidacion.updateLiquidacion(ref error, datosUpdate))
                {
                    showSuccess("Se anulo la liquidación correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo anular la liquidación, por favor, valide los datos y vuelva a intentarlo.");
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
                    DataTable DtLiquidacion;
                    GridViewRow selectedRow = GrdVLiquidacion.Rows[index];

                    TableCell id_liquidacion = selectedRow.Cells[1];
                    DtLiquidacion = mLiquidacion.getObtieneDatosModificar(ref error, id_liquidacion.Text.ToString());
              
                    foreach (DataRow item in DtLiquidacion.Rows)
                    {
                        ddidLiquidacion.Text = item[0].ToString();
                        ddidSucursal.SelectedValue = item[1].ToString();
                        txtNumeroPrestamo.Text = item[2].ToString();
                        txtMontoLiquidacion.Text = item[3].ToString();
                        ddlEstado.SelectedValue = item[4].ToString();

                        ddidSucursal.Enabled = false;
                        txtNumeroPrestamo.ReadOnly = true;
                        txtMontoLiquidacion.ReadOnly = true;
                        lblAnular.Visible = true;
                        ChbxAnular.Visible = true;
                        lblEstado.Visible = false;
                        ddlEstado.Visible = false;
                        ChbxAnular.Checked = false;

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

        protected void ChbxAnular_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}