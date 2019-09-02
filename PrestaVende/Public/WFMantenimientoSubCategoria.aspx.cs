using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantenimientoSubCategoria : System.Web.UI.Page
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

        private CLASS.cs_subcategoria mSubCategoria = new CLASS.cs_subcategoria();

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
                        getEstadoSubCategoria();
                        getCategoria();
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
                ddIdSubCategoria.Text = mSubCategoria.getIDMaxSubCategoria(ref error);
                hideOrShowDiv(false);
                cleanControls();
                ddIdCategoria.Enabled = true;
                txtDescripcionSubCategoria.ReadOnly = false;
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
                txtDescripcionSubCategoria.Text = "";
                ddlEstado.SelectedValue = "1";
                ddIdCategoria.SelectedValue = "0";
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
                        if (updateSubCategoria())
                        {
                            hideOrShowDiv(true);
                            getDataGrid();
                            isUpdate = false;
                        }
                    }
                    else
                    {
                        //GUARDA NUEVO
                        if (insertSubCategoria())
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

        private bool insertSubCategoria()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                if (mSubCategoria.insertSubCategoria(ref error, ddIdSubCategoria.Text, ddIdCategoria.SelectedValue.ToString(), txtDescripcionSubCategoria.Text.ToString(), ddlEstado.SelectedValue.ToString(), thisDay.ToString("MM/dd/yyyy HH:mm:ss")))
                {
                    showSuccess("Se agrego el area empresa correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo agregar la sub categoria, por favor, valide los datos y vuelva a intentarlo.");
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
                if (ddIdCategoria.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar una categoria para poder guardar."); return false; }
                else if (txtDescripcionSubCategoria.Text.ToString().Length < 3) { showWarning("Usted debe agregar una descripcion para poder guardar."); return false; }            
                else
                    return true;            
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void getEstadoSubCategoria()
        {
            try
            {
                ddlEstado.DataSource = mSubCategoria.getEstadoSubCategoria(ref error);
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

        private void getCategoria()
        {
            try
            {
                ddIdCategoria.DataSource = mSubCategoria.getCategoria(ref error);
                ddIdCategoria.DataValueField = "id_categoria";
                ddIdCategoria.DataTextField = "categoria";
                ddIdCategoria.DataBind();
                ddIdCategoria.SelectedValue = "0";
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
                GrdVSubCategoria.DataSource = mSubCategoria.getSubCategorias(ref error);
                GrdVSubCategoria.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool updateSubCategoria()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                string[] datosUpdate = new string[5];

                datosUpdate[0] = ddIdSubCategoria.Text;
                datosUpdate[1] = ddIdCategoria.SelectedValue;
                datosUpdate[2] = txtDescripcionSubCategoria.Text;
                datosUpdate[3] = ddlEstado.SelectedValue;
                datosUpdate[4] = thisDay.ToString("MM/dd/yyyy HH:mm:ss");

                if (mSubCategoria.updateSubCategoria(ref error, datosUpdate))
                {
                    showSuccess("Se modifico la sub categoria correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo modificar la sub categoria, por favor, valide los datos y vuelva a intentarlo.");
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
                    DataTable DtSubCategoria;
                    GridViewRow selectedRow = GrdVSubCategoria.Rows[index];

                    TableCell id_sub_categoria = selectedRow.Cells[1];
                    DtSubCategoria = mSubCategoria.getObtieneDatosModificar(ref error, id_sub_categoria.Text.ToString());
              
                    foreach (DataRow item in DtSubCategoria.Rows)
                    {
                        ddIdSubCategoria.Text = item[0].ToString();
                        ddIdCategoria.SelectedValue = item[1].ToString();
                        txtDescripcionSubCategoria.Text = item[2].ToString();
                        ddlEstado.SelectedValue = item[3].ToString();
                    }

                    isUpdate = true;
                    hideOrShowDiv(false);
                    divSucceful.Visible = false;
                    ddIdCategoria.Enabled = false;                    
                    txtDescripcionSubCategoria.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
    }

}