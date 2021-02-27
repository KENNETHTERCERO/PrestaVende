using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantenimientoEmpresa : System.Web.UI.Page
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

        private CLASS.cs_Empresa mEmpresa = new CLASS.cs_Empresa();

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
                        getAreaEmpresa();
                        getTipoEmpresa();
                        getEstadoEmpresa();
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
                ddidEmpresa.Text = mEmpresa.getIDMaxEmpresa(ref error);
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
                ddidAreaEmpresa2.SelectedValue = "0";
                ddidTipoEmpresa.SelectedValue = "0";
                txtNombreEmpresa.Text = "";
                txtNitEmpresa.Text = "";
                txtDireccionEmpresa.Text = "";
                txtPatente.Text = "";
                txtLibro.Text = "";
                txtFolio.Text = "";
                ddEstado.SelectedValue = "1";
                ChbxVende.Checked = false;
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
                        if (updateEmpresa())
                        {
                            hideOrShowDiv(true);
                            getDataGrid();
                            isUpdate = false;
                        }
                    }
                    else
                    {
                        //GUARDA NUEVO
                        if (insertEmpresa())
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

        private bool insertEmpresa()
        {
            try
            {
               
                DateTime thisDay = DateTime.Now;
                if (mEmpresa.insertEmpresa(ref error, ddidEmpresa.Text, ddidAreaEmpresa2.SelectedValue.ToString(), ddidTipoEmpresa.SelectedValue.ToString(), txtNombreEmpresa.Text.ToString(), txtNitEmpresa.Text.ToString(),
                    txtDireccionEmpresa.Text.ToString(), txtPatente.Text.ToString(), txtLibro.Text.ToString(), txtFolio.Text.ToString(), ddEstado.SelectedValue.ToString(), 
                    thisDay.ToString("MM/dd/yyyy HH:mm:ss"), thisDay.ToString("MM/dd/yyyy HH:mm:ss"), ChbxVende.Checked))
                {
                    showSuccess("Se agrego la empresa correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo agregar la empresa, por favor, valide los datos y vuelva a intentarlo.");
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
                if (ddidAreaEmpresa2.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar un area de empresa para poder guardar."); return false; }
                else if (ddidTipoEmpresa.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar un tipo de empresa para poder guardar."); return false; }            
                else if (txtNombreEmpresa.Text.ToString().Length < 3) { showWarning("Usted debe ingresar un nombre de empresa para poder guardar."); return false; }
                else if (txtNitEmpresa.Text.ToString().Length < 9) { showWarning("Usted debe ingresar NIT válido para poder guardar."); return false; }
                else if (txtDireccionEmpresa.Text.ToString().Length < 3) { showWarning("Usted debe ingresar una dirección de empresa para poder guardar."); return false; }
                else if (txtPatente.Text.ToString().Length < 1) { showWarning("Usted debe ingresar número de patente para poder guardar."); return false; }
                else if (txtLibro.Text.ToString().Length < 1) { showWarning("Usted debe ingresar un número de libro para poder guardar."); return false; }
                else if (txtFolio.Text.ToString().Length < 1) { showWarning("Usted debe ingresar número de folio para poder guardar."); return false; }
                else
                    return true;            
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void getEstadoEmpresa()
        {
            try
            {
                ddEstado.DataSource = mEmpresa.getEstadoEmpresa(ref error);
                ddEstado.DataValueField = "id";
                ddEstado.DataTextField = "estado";
                ddEstado.DataBind();
                ddEstado.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        private void getAreaEmpresa()
        {
            try
            {
                ddidAreaEmpresa2.DataSource = mEmpresa.getAreaEmpresa(ref error);
                ddidAreaEmpresa2.DataValueField = "id_area_empresa";
                ddidAreaEmpresa2.DataTextField = "descripcion";
                ddidAreaEmpresa2.DataBind();
                ddidAreaEmpresa2.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        private void getTipoEmpresa()
        {
            try
            {
                ddidTipoEmpresa.DataSource = mEmpresa.getTipoEmpresa(ref error);
                ddidTipoEmpresa.DataValueField = "id_tipo_empresa";
                ddidTipoEmpresa.DataTextField = "tipo_empresa";
                ddidTipoEmpresa.DataBind();
                ddidTipoEmpresa.SelectedValue = "0";
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
                GrdVEmpresa.DataSource = mEmpresa.getEmpresa(ref error);               
                GrdVEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool updateEmpresa()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                string[] datosUpdate = new string[13];

                datosUpdate[0] = ddidEmpresa.Text;
                datosUpdate[1] = ddidAreaEmpresa2.SelectedValue;
                datosUpdate[2] = ddidTipoEmpresa.SelectedValue;
                datosUpdate[3] = txtNombreEmpresa.Text;
                datosUpdate[4] = txtNitEmpresa.Text;
                datosUpdate[5] = txtDireccionEmpresa.Text;
                datosUpdate[6] = txtPatente.Text;
                datosUpdate[7] = txtLibro.Text;
                datosUpdate[8] = txtFolio.Text;
                datosUpdate[9] = ddEstado.SelectedValue;
                datosUpdate[10] = thisDay.ToString("MM/dd/yyyy HH:mm:ss");
                datosUpdate[11] = ChbxVende.Checked.ToString();

                if (mEmpresa.updateEmpresa(ref error, datosUpdate))
                {
                    showSuccess("Se modifico la empresa correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo modificar la empresa, por favor, valide los datos y vuelva a intentarlo.");
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
                    DataTable DtEmpresa;
                    GridViewRow selectedRow = GrdVEmpresa.Rows[index];

                    TableCell id_empresa = selectedRow.Cells[1];
                    DtEmpresa = mEmpresa.getObtieneDatosModificar(ref error, id_empresa.Text.ToString());
              
                    foreach (DataRow item in DtEmpresa.Rows)
                    {
                        ddidEmpresa.Text = item[0].ToString();
                        ddidAreaEmpresa2.SelectedValue = item[1].ToString();
                        ddidTipoEmpresa.Text = item[2].ToString();
                        txtNombreEmpresa.Text = item[3].ToString();
                        txtNitEmpresa.Text = item[4].ToString();
                        txtDireccionEmpresa.Text = item[5].ToString();
                        txtPatente.Text = item[6].ToString();
                        txtLibro.Text = item[7].ToString();
                        txtFolio.Text = item[8].ToString();
                        ddEstado.SelectedValue = item[9].ToString();

                        if (item[10].ToString() == "0")
                        {
                            ChbxVende.Checked = false;
                        }
                        else
                        {
                            ChbxVende.Checked = true;
                        }
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
        
        protected void ChbxVende_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}