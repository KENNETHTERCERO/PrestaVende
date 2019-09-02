using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantenimientoCategoria : System.Web.UI.Page
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

        private CLASS.cs_categoria mCategoria = new CLASS.cs_categoria();

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
                        getEstadoCategoria();
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
                ddIdCategoria.Text = mCategoria.getIDMaxCategoria(ref error);
                hideOrShowDiv(false);
                cleanControls();
                txtDescripcionCategoria.ReadOnly = false;
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
                txtDescripcionCategoria.Text = "";
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
                        if (updateCategoria())
                        {
                            hideOrShowDiv(true);
                            getDataGrid();
                            isUpdate = false;
                        }
                    }
                    else
                    {
                        //GUARDA NUEVO
                        if (insertCategoria())
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

        private bool insertCategoria()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                if (mCategoria.insertCategoria(ref error, ddIdCategoria.Text, txtDescripcionCategoria.Text, ddlEstado.SelectedValue.ToString(), thisDay.ToString("MM/dd/yyyy HH:mm:ss")))
                {
                    showSuccess("Se agrego la categoria correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo agregar la categoria, por favor, valide los datos y vuelva a intentarlo.");
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
                if (txtDescripcionCategoria.Text.ToString().Equals("")) { showWarning("Usted debe agregar una descripción para poder guardar."); return false; }
                 else
                    return true;            
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void getEstadoCategoria()
        {
            try
            {
                ddlEstado.DataSource = mCategoria.getEstadoCategoria(ref error);
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
                GrdVCategoria.DataSource = mCategoria.getCategoria(ref error);
                GrdVCategoria.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool updateCategoria()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                string[] datosUpdate = new string[4];

                datosUpdate[0] = ddIdCategoria.Text;
                datosUpdate[1] = txtDescripcionCategoria.Text;
                datosUpdate[2] = ddlEstado.SelectedValue;
                datosUpdate[3] = thisDay.ToString("MM/dd/yyyy HH:mm:ss");

                if (mCategoria.updateCategoria(ref error, datosUpdate))
                {
                    showSuccess("Se modifico la categoria correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo modificar la categoria, por favor, valide los datos y vuelva a intentarlo.");
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
                    GridViewRow selectedRow = GrdVCategoria.Rows[index];

                    TableCell id_categoria = selectedRow.Cells[1];
                    DtAreaEmpresa = mCategoria.getObtieneDatosModificar(ref error, id_categoria.Text.ToString());
              
                    foreach (DataRow item in DtAreaEmpresa.Rows)
                    {
                        ddIdCategoria.Text = item[0].ToString();
                        txtDescripcionCategoria.Text = item[1].ToString();
                        ddlEstado.SelectedValue = item[2].ToString();

                        txtDescripcionCategoria.ReadOnly = true;
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