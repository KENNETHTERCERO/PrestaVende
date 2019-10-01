using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantenimientoSerie : System.Web.UI.Page
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

        private CLASS.cs_serie mSerie = new CLASS.cs_serie();

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
                        getEstadoSerie();
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
                ddidSerie.Text = mSerie.getIDMaxSerie(ref error);
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
                ddidSucursal.SelectedValue = "0";
                txtSerie.Text = "";
                txtResolucion.Text = "";
                txtFechaResolucion.Text = "";
                txtNumeroDeFacturas.Text = "";
                ddEstado.SelectedValue = "1";
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
                        if (updateSerie())
                        {
                            hideOrShowDiv(true);
                            getDataGrid();
                            isUpdate = false;
                        }
                    }
                    else
                    {
                        //GUARDA NUEVO
                        if (insertSerie())
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

        private bool insertSerie()
        {
            try
            {

                DateTime thisDay = DateTime.Now;
                if (mSerie.insertSerie(ref error, ddidSerie.Text, txtSerie.Text.ToString(), txtResolucion.Text.ToString(), txtFechaResolucion.Text.ToString(),
                    ddEstado.SelectedValue.ToString(), ddidSucursal.SelectedValue.ToString(),
                    txtNumeroDeFacturas.Text.ToString()))
                {
                    showSuccess("Se agrego la serie correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo agregar la serie, por favor, valide los datos y vuelva a intentarlo.");
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
                if (ddidSucursal.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar una sucursal para poder guardar."); return false; }                
                else if (txtSerie.Text.ToString().Length < 1) { showWarning("Usted debe ingresar una serie para poder guardar."); return false; }                
                else if (txtResolucion.Text.ToString().Length < 1) { showWarning("Usted debe ingresar una resolucion para poder guardar."); return false; }
                else if (txtFechaResolucion.Text.ToString().Length < 1) { showWarning("Usted debe ingresar una fecha de resolucion para poder guardar."); return false; }                
                else if (txtNumeroDeFacturas.Text.ToString().Length < 1) { showWarning("Usted debe ingresar un numero de factura para poder guardar."); return false; }
                else
                    return true;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void getEstadoSerie()
        {
            try
            {
                ddEstado.DataSource = mSerie.getEstadoSerie(ref error);
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

        private void getSucursal()
        {
            try
            {
                ddidSucursal.DataSource = mSerie.getSucursal(ref error);
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
                GrdVSerie.DataSource = mSerie.getSerie(ref error);
                GrdVSerie.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool updateSerie()
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                string[] datosUpdate = new string[13];

                datosUpdate[0] = ddidSerie.Text;
                datosUpdate[1] = txtSerie.Text;
                datosUpdate[2] = txtResolucion.Text;
                datosUpdate[3] = txtFechaResolucion.Text;
                datosUpdate[4] = ddEstado.SelectedValue;                
                datosUpdate[6] = ddidSucursal.SelectedValue;                
                datosUpdate[8] = txtNumeroDeFacturas.Text;
                datosUpdate[9] = ddEstado.SelectedValue;

                if (mSerie.updateSerie(ref error, datosUpdate))
                {
                    showSuccess("Se modifico la serie correctamente.");
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo modificar la serie, por favor, valide los datos y vuelva a intentarlo.");
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
                    DataTable DtSerie;
                    GridViewRow selectedRow = GrdVSerie.Rows[index];

                    TableCell id_serie = selectedRow.Cells[1];
                    DtSerie = mSerie.getObtieneDatosModificar(ref error, id_serie.Text.ToString());

                    foreach (DataRow item in DtSerie.Rows)
                    {
                        ddidSerie.Text = item[0].ToString();
                        txtSerie.Text = item[1].ToString();
                        txtResolucion.Text = item[2].ToString();
                        string dia, mes, anio;
                        dia = Convert.ToDateTime(item[3].ToString()).Day.ToString();
                        mes = Convert.ToDateTime(item[3].ToString()).Month.ToString();
                        anio = Convert.ToDateTime(item[3].ToString()).Year.ToString();

                        txtFechaResolucion.Text = dia + "-" + mes + "-" + anio;
                        ddEstado.SelectedValue = item[4].ToString();
                        ddidSucursal.SelectedValue = item[5].ToString();                        
                        txtNumeroDeFacturas.Text = item[7].ToString();
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