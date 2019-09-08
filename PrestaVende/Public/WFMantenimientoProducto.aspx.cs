using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantenimientoProducto : System.Web.UI.Page
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

        private CLASS.cs_producto mProducto = new CLASS.cs_producto();

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
                        getEstadoProducto();
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
                ddidProducto.Text = mProducto.getIDMaxProducto(ref error);
                hideOrShowDiv(false);
                cleanControls();
                isUpdate = false;
                ddIdSubCategoria.Items.Clear();
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
                txtDescripcionProducto.Text = "";
                txtPrecioSugerido.Text = "";
                ddlEstado.SelectedValue = "1";
                ddidCategoria.SelectedValue = "0";
                ddIdSubCategoria.ClearSelection();
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
                        if (updateProducto())
                        {
                            hideOrShowDiv(true);
                            getDataGrid();
                            isUpdate = false;
                        }
                    }
                    else
                    {
                        //GUARDA NUEVO
                        if (insertProducto())
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

        private bool insertProducto()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                if (mProducto.insertProducto(ref error, ddIdSubCategoria.SelectedValue.ToString(), txtDescripcionProducto.Text.ToString(), txtPrecioSugerido.Text.ToString(), ddlEstado.SelectedValue.ToString()))
                {
                    showSuccess("Se agrego el producto correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo agregar el producto, por favor, valide los datos y vuelva a intentarlo.");
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
                if (txtDescripcionProducto.Text.ToString().Length < 3) { showWarning("Usted debe agregar una descripción de producto para poder guardar."); return false; }
                else if (txtPrecioSugerido.Text.ToString().Length < 3) { showWarning("Usted debe un precio sugerido para poder guardar."); return false; }            
                else if (ddIdSubCategoria.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar una sub categoria poder guardar."); return false; }
                
                else
                    return true;            
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void getEstadoProducto()
        {
            try
            {
                ddlEstado.DataSource = mProducto.getEstadoProducto(ref error);
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
                ddidCategoria.DataSource = mProducto.getCategoria(ref error);
                ddidCategoria.DataValueField = "id_categoria";
                ddidCategoria.DataTextField = "categoria";
                ddidCategoria.DataBind();
                ddidCategoria.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        private void getSubCategoria()
        {
            try
            {
                ddIdSubCategoria.DataSource = mProducto.getSubCategoria(ref error, ddidCategoria.SelectedValue);
                ddIdSubCategoria.DataValueField = "id_sub_categoria";
                ddIdSubCategoria.DataTextField = "sub_categoria";
                ddIdSubCategoria.DataBind();
                ddIdSubCategoria.SelectedValue = "0";
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
                GrdVProducto.DataSource = mProducto.getProducto(ref error);
                GrdVProducto.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool updateProducto()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                string[] datosUpdate = new string[5];

                datosUpdate[0] = ddidProducto.Text;
                datosUpdate[1] = txtPrecioSugerido.Text;
                datosUpdate[2] = ddlEstado.SelectedValue;

                if (mProducto.updateProducto(ref error, datosUpdate))
                {
                    showSuccess("Se modifico el producto correctamente.");
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
                    GridViewRow selectedRow = GrdVProducto.Rows[index];

                    TableCell id_producto = selectedRow.Cells[1];
                    DtAreaEmpresa = mProducto.getObtieneDatosModificar(ref error, id_producto.Text.ToString());
              
                    foreach (DataRow item in DtAreaEmpresa.Rows)
                    {
                        ddidProducto.Text = item[0].ToString();
                        ddidCategoria.Text = mProducto.getNombreCategoria(ref error, item[1].ToString());
                        getSubCategoria();
                        ddIdSubCategoria.SelectedValue = item[1].ToString();
                        txtDescripcionProducto.Text = item[2].ToString();
                        txtPrecioSugerido.Text = item[3].ToString();
                        ddlEstado.SelectedValue = item[4].ToString();
                    }

                    ddidCategoria.Enabled = false;
                    ddIdSubCategoria.Enabled = false;
                    txtDescripcionProducto.ReadOnly = true;

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


        protected void ddidCategoria_TextChanged(object sender, EventArgs e)
        {
            ddIdSubCategoria.DataSource = null;
            getSubCategoria();
        }

        protected void ddidCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddIdSubCategoria.DataSource = null;
            getSubCategoria();
        }
    }

}