using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantAreaEmpresa : System.Web.UI.Page
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

        private CLASS.cs_AreaEmpresa mAreaEmpresa = new CLASS.cs_AreaEmpresa();

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
                        getPais();
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
                ddidAreaEmpresa.Text = mAreaEmpresa.getIDMaxAreaEmpresa(ref error);
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
                txtDescripcion.Text = "";
                ddlEstado.SelectedValue = "1";
                ddidPais.SelectedValue = "0";
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
                        if (updateAreaEmpresa())
                        {
                            hideOrShowDiv(true);
                            getDataGrid();
                            isUpdate = false;
                        }
                    }
                    else
                    {
                        //GUARDA NUEVO
                        if (insertAreaEmpresa())
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

        private bool insertAreaEmpresa()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                if (mAreaEmpresa.insertAreaEmpresa(ref error, ddidAreaEmpresa.Text, ddidPais.SelectedValue.ToString(), txtDescripcion.Text.ToString(), ddlEstado.SelectedValue.ToString(), thisDay.ToString("MM/dd/yyyy HH:mm:ss"), thisDay.ToString("MM/dd/yyyy HH:mm:ss")))
                {
                    showSuccess("Se agrego el area empresa correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo agregar area empresa, por favor, valide los datos y vuelva a intentarlo.");
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
                if (txtDescripcion.Text.ToString().Equals("")) { showWarning("Usted debe agregar una descripcion para poder guardar."); return false; }
                else if (txtDescripcion.Text.ToString().Length < 3) { showWarning("Usted debe agregar una descripcion para poder guardar."); return false; }            
                else if (ddidPais.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar un país para poder guardar."); return false; }
                //else if (ddlEstado.SelectedValue.ToString = "1") { showWarning("Usted debe seleccionar un estado para poder guardar."); return false; }
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
                ddlEstado.DataSource = mAreaEmpresa.getEstadoAreaEmpresa(ref error);
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

        private void getPais()
        {
            try
            {
                ddidPais.DataSource = mAreaEmpresa.getPais(ref error);
                ddidPais.DataValueField = "id_pais";
                ddidPais.DataTextField = "descripcion";
                ddidPais.DataBind();
                ddidPais.SelectedValue = "0";
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
                GrdVAreaEmpresa.DataSource = mAreaEmpresa.getAreaEmpresa(ref error);
                //GrdVAreaEmpresa.Columns[2].Visible = false;
                //GrdVAreaEmpresa.Columns[8].Visible = false;
                GrdVAreaEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool updateAreaEmpresa()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                string[] datosUpdate = new string[5];

                datosUpdate[0] = ddidAreaEmpresa.Text;
                datosUpdate[1] = ddidPais.SelectedValue;
                datosUpdate[2] = txtDescripcion.Text;
                datosUpdate[3] = ddlEstado.SelectedValue;
                datosUpdate[4] = thisDay.ToString("MM/dd/yyyy HH:mm:ss");

                if (mAreaEmpresa.updateAreaEmpresa(ref error, datosUpdate))
                {
                    showSuccess("Se modifico el area empresa correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo modificar el area empresa, por favor, valide los datos y vuelva a intentarlo.");
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
                    GridViewRow selectedRow = GrdVAreaEmpresa.Rows[index];

                    TableCell id_area_empresa = selectedRow.Cells[1];
                    DtAreaEmpresa = mAreaEmpresa.getObtieneDatosModificar(ref error, id_area_empresa.Text.ToString());
              
                    foreach (DataRow item in DtAreaEmpresa.Rows)
                    {
                        ddidAreaEmpresa.Text = item[0].ToString();
                        ddidPais.SelectedValue = item[1].ToString();
                        txtDescripcion.Text = item[2].ToString();
                        ddlEstado.SelectedValue = item[3].ToString();
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