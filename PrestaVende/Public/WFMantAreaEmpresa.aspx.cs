using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantAreaEmpresa : System.Web.UI.Page
    {
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
                        getState();
                        getTipoCorte();
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
                lblIdNumero.Text = mEsqueje.getIDMaxEsqueje(ref error);
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
                txtTipo.Text = "";
                txtCantidadpormedida.Text = "";
                txtPesoportray.Text = "";
                ddlTipoCorte.SelectedValue = "0";
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
                        if (updateEsqueje())
                        {
                            hideOrShowDiv(true);
                            getDataGrid();
                            isUpdate = false;
                        }
                    }
                    else
                    {
                        //GUARDA NUEVO
                        if (insertEsqueje())
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

        private bool insertEsqueje()
        {
            try
            {
                if (mEsqueje.insertEsqueje(ref error, txtDescripcion.Text.ToString(), ddlEstado.SelectedValue.ToString(), txtTipo.Text.ToString(), txtCantidadpormedida.Text.ToString(), txtPesoportray.Text.ToString(), ddlTipoCorte.SelectedValue.ToString()))
                {
                    showSuccess("Se agrego esqueje correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo agregar esqueje, por favor, valide los datos y vuelva a intentarlo.");
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
                else if (txtTipo.Text.ToString().Equals("")) { showWarning("Usted debe agregar un tipo para poder guardar."); return false; }
                else if (ddlTipoCorte.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar un tipo de corte para poder guardar."); return false; }
                else
                    return true;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void getState()
        {
            try
            {
                ddlEstado.DataSource = mEsqueje.getEstados(ref error);
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

        private void getTipoCorte()
        {
            try
            {
                ddlTipoCorte.DataSource = mEsqueje.getTipoCorte(ref error);
                ddlTipoCorte.DataValueField = "id_corte";
                ddlTipoCorte.DataTextField = "descripcion";
                ddlTipoCorte.DataBind();
                ddlTipoCorte.SelectedValue = "0";
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
                gvSize.DataSource = mEsqueje.getEsquejeMaintenance(ref error);
                gvSize.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool updateEsqueje()
        {
            try
            {
                if (mEsqueje.updateEsqueje(ref error, lblIdNumero.Text.ToString(), txtDescripcion.Text.ToString(), ddlEstado.SelectedValue.ToString(), txtTipo.Text.ToString(), txtCantidadpormedida.Text.ToString(), txtPesoportray.Text.ToString(), ddlTipoCorte.SelectedValue.ToString()))
                {
                    showSuccess("Se modifico esqueje correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo modificar esqueje, por favor, valide los datos y vuelva a intentarlo.");
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

                    GridViewRow selectedRow = gvSize.Rows[index];
                    TableCell id_esqueje = selectedRow.Cells[1];
                    TableCell descripcion = selectedRow.Cells[2];
                    TableCell tipo = selectedRow.Cells[3];
                    TableCell cantidad_por_medida = selectedRow.Cells[4];
                    TableCell peso_por_tray = selectedRow.Cells[5];
                    TableCell id_tipo_corte = selectedRow.Cells[8];
                    TableCell estado = selectedRow.Cells[9];

                    isUpdate = true;
                    setControlsEdit(id_esqueje.Text.ToString(), descripcion.Text.ToString(), estado.Text.ToString(), tipo.Text.ToString(), cantidad_por_medida.Text.ToString(), peso_por_tray.Text.ToString(), id_tipo_corte.Text.ToString());
                    hideOrShowDiv(false);
                    divSucceful.Visible = false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void setControlsEdit(string id_esqueje, string descripcion, string estado, string tipo, string cantidadPorMedida, string pesoPorTray, string id_tipo_corte)
        {
            try
            {
                lblIdNumero.Text = id_esqueje;
                txtDescripcion.Text = descripcion;
                ddlEstado.SelectedValue = estado;
                txtTipo.Text = tipo;
                txtCantidadpormedida.Text = cantidadPorMedida;
                txtPesoportray.Text = pesoPorTray;
                ddlTipoCorte.SelectedValue = id_tipo_corte;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
    }

}
}