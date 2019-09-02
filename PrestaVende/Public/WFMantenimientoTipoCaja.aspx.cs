using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantenimientoTipoCaja : System.Web.UI.Page
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

        private CLASS.cs_tipo_caja mTipoCaja = new CLASS.cs_tipo_caja();

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
                        getEstadoAreaEmpresa();
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
                ddidTipoCaja.Text = mTipoCaja.getIDTipoCaja(ref error);
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
                //ddidTipoCaja.Text = "0";
                txtTipoCaja.Text = "";
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
                        if (updateTipoCaja())
                        {
                            hideOrShowDiv(true);
                            getDataGrid();
                            isUpdate = false;
                        }
                    }
                    else
                    {
                        //GUARDA NUEVO
                        if (insertTipoCaja())
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

        private bool insertTipoCaja()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                if (mTipoCaja.insertTipoCaja(ref error, txtTipoCaja.Text, ddlEstado.SelectedValue.ToString(), thisDay.ToString("MM/dd/yyyy HH:mm:ss"), thisDay.ToString("MM/dd/yyyy HH:mm:ss")))
                {
                    showSuccess("Se agrego el tipo de caja correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo agregar el tipo de caja, por favor, valide los datos y vuelva a intentarlo.");
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
                if (txtTipoCaja.Text.ToString().Equals("")) { showWarning("Usted debe agregar una descripción de tipo de caja para poder guardar."); return false; }                
                else
                    return true;            
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void getEstadoAreaEmpresa()
        {
            try
            {
                ddlEstado.DataSource = mTipoCaja.getEstadoTipoCaja(ref error);
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
       
        private void getDataGrid()
        {
            try
            {
                GrdVTipoCaja.DataSource = mTipoCaja.getTipoCaja(ref error);                
                GrdVTipoCaja.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool updateTipoCaja()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                string[] datosUpdate = new string[5];

                datosUpdate[0] = ddidTipoCaja.Text;
                datosUpdate[1] = txtTipoCaja.Text;
                datosUpdate[2] = ddlEstado.SelectedValue;
                datosUpdate[3] = thisDay.ToString("MM/dd/yyyy HH:mm:ss");
                datosUpdate[4] = thisDay.ToString("MM/dd/yyyy HH:mm:ss");

                if (mTipoCaja.updateTipoCaja(ref error, datosUpdate))
                {
                    showSuccess("Se modifico el tipo de caja correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo modificar el tipo de caja, por favor, valide los datos y vuelva a intentarlo.");
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
                    DataTable DtAreaEmpresa;
                    GridViewRow selectedRow = GrdVTipoCaja.Rows[index];

                    TableCell id_tipo_caja = selectedRow.Cells[1];
                    DtAreaEmpresa = mTipoCaja.getObtieneDatosTipoCajaModificar(ref error, id_tipo_caja.Text.ToString());
              
                    foreach (DataRow item in DtAreaEmpresa.Rows)
                    {
                        ddidTipoCaja.Text = item[0].ToString();
                        txtTipoCaja.Text = item[1].ToString();
                        ddlEstado.SelectedValue = item[2].ToString();
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