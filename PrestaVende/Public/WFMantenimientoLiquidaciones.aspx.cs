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

        DataRow row = null;
        private static DataTable dtTablaPrestamos;
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
                        dtTablaPrestamos = new DataTable("tablaPrestamos");
                        setColumnsPrestamo();
                        Session["CurrentTablePrestamos"] = dtTablaPrestamos;
                        hideOrShowDiv(true);
                        //getDataGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void setColumnsPrestamo()
        {
            try
            {
                dtTablaPrestamos.Columns.Add("id_prestamo_encabezado");
                dtTablaPrestamos.Columns.Add("numero_prestamo");
                dtTablaPrestamos.Columns.Add("total_prestamo");
                dtTablaPrestamos.Columns.Add("saldo_prestamo");
                dtTablaPrestamos.Columns.Add("estado_prestamo");
                
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
                this.div_gridView.Visible = true;
                this.btnSalir.Visible = true;
                this.btnCreate.Visible = false;
                this.btnCancel.Visible = false;
                this.btnLiquidar.Visible = true;
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
     
        protected void btnLiquidar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaPrestamosAgregados())
                {
                    if (insertLiquidacion())
                    {
                        hideOrShowDiv(true);
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            hideOrShowDiv(true);
        }

        private bool validaPrestamosAgregados()
        {
            try
            {
                DataTable prestamosALiquidar = (DataTable)this.Session["CurrentTablePrestamos"];

                if (prestamosALiquidar.Rows.Count > 0)
                {
                    if (prestamosALiquidar.Rows.Count != GrdVLiquidacion.Rows.Count)
                    {
                        showError("El numero de prestamos agregado en el grid y el numero de prestamos de tabla de session es diferente, por favor salga del sistema y vuelva a ingresar los contratos.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    showWarning("Para poder liquidar usted debe agregar contratos antes.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private bool insertLiquidacion()
        {
            int contadorError = 0, contadorLiquidaciones = 0;
            try
            {
                error = "";
                mLiquidacion = new CLASS.cs_liquidacion();

                DataTable prestamosALiquidar = (DataTable)this.Session["CurrentTablePrestamos"];

                foreach (DataRow item in prestamosALiquidar.Rows)
                {
                    if (mLiquidacion.insertLiquidacion(ref error, item["id_prestamo_encabezado"].ToString()))// Cells[""].Text.ToString()))
                    {
                        contadorLiquidaciones++;
                    }
                    else
                    {
                        contadorError = contadorError + 1;
                        throw new SystemException("No se pudo liquidar prestamo" + item["numero_prestamo"].ToString() + ", favor comunicarse con el administrador. " + error);
                    }
                }

                if (contadorError == 0)
                {
                    showSuccess("Se liquidaron " + contadorLiquidaciones.ToString() + " contratos correctamente.");
                    this.GrdVLiquidacion.DataSource = null;
                    this.GrdVLiquidacion.DataBind();
                }
              
                return true;
            }
            catch (Exception ex)
            {
                if (contadorLiquidaciones > 0)
                {
                    showSuccess("Se liquidaron " + contadorLiquidaciones.ToString() + " contratos correctamente.");
                }
                showError(contadorError + " contratos con error. " + ex.ToString() + error);
                return false;
            }
        }

        private void addPrestamo()
        {
            try
            {
                if ((DataTable)this.Session["CurrentTablePrestamos"] != null)
                {
                    DataTable dtTablaSession = (DataTable)this.Session["CurrentTablePrestamos"];
                    DataTable dtTabla = new DataTable("dtLiquidacion");
                    dtTabla = mLiquidacion.getPrestamosLiquidacion(ref error, this.txtBusquedaPrestamo.Text.ToString());

                    if (dtTabla.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtTabla.Rows)
                        {
                            row = dtTablaSession.NewRow();
                            row["id_prestamo_encabezado"] = item[0].ToString();
                            row["numero_prestamo"] = item[1].ToString();
                            row["total_prestamo"] = item[2].ToString();
                            row["saldo_prestamo"] = item[3].ToString();
                            row["estado_prestamo"] = item[4].ToString();
                        }

                        dtTablaSession.Rows.Add(row);
                        Session["CurrentTablePrestamos"] = dtTablaSession;
                        this.GrdVLiquidacion.DataSource = dtTablaSession;
                        this.GrdVLiquidacion.DataBind();
                    }
                    else
                    {
                        showWarning("Prestamo " + this.txtBusquedaPrestamo.Text.ToString() + " no disponible para Liquidar, por favor valide las fechas si ya vencio.");
                    }                    
                }
                else
                {
                    DataTable dtTablaSession = (DataTable)this.Session["CurrentTablePrestamos"];
                    DataTable dtTabla = new DataTable("dtLiquidacion");
                    dtTabla = mLiquidacion.getPrestamosLiquidacion(ref error, this.txtBusquedaPrestamo.Text.ToString());

                    if (dtTabla.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtTabla.Rows)
                        {
                            row = dtTablaSession.NewRow();
                            row["id_prestamo_encabezado"] = item[0].ToString();
                            row["numero_prestamo"] = item[1].ToString();
                            row["total_prestamo"] = item[2].ToString();
                            row["saldo_prestamo"] = item[3].ToString();
                            row["estado_prestamo"] = item[4].ToString();
                        }

                        dtTablaSession.Rows.Add(row);
                        Session["CurrentTablePrestamos"] = dtTablaSession;
                        this.GrdVLiquidacion.DataSource = dtTablaSession;
                        this.GrdVLiquidacion.DataBind();
                    }
                    else
                    {
                        showWarning("Prestamo " + this.txtBusquedaPrestamo.Text.ToString() + " no disponible para Liquidar, por favor valide las fechas si ya vencio.");
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnBuscarPrestamo_Click(object sender, EventArgs e)
        {
            addPrestamo();
            this.txtBusquedaPrestamo.Text = "";
        }

        protected void GrdVLiquidacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "borrar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataTable dtActual = (DataTable)this.Session["CurrentTablePrestamos"];
                    dtActual.Rows[index].Delete();
                    Session["CurrentTablePrestamos"] = dtActual;
                    this.GrdVLiquidacion.DataSource = dtActual;
                    this.GrdVLiquidacion.DataBind();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
    }

}