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
                    div_gridView.Visible = true;
                    btnSalir.Visible = true;
                    btnCreate.Visible = false;
                    btnCancel.Visible = false;
                    btnLiquidar.Visible = true;
               
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
                 
                  //GUARDA NUEVO
                  if (insertLiquidacion())
                  {
                      hideOrShowDiv(true);
                      getDataGrid();
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

        private bool insertLiquidacion()
        {
            try
            {
                int contadorError = 0;
                DataTable DtLiquidacionPrestamo = new DataTable();
                DtLiquidacionPrestamo = mLiquidacion.getPrestamos(ref error);

                foreach (DataRow item in DtLiquidacionPrestamo.Rows)
                {
                    if (mLiquidacion.insertLiquidacion(ref error, item[0].ToString()))
                    {
                    }
                    else
                    {
                        contadorError = contadorError + 1;
                        throw new SystemException("No se pudo liquidar prestamos, favor comunicarse con el administrador.");
                    }
                }

                if (contadorError == 0)
                { showSuccess("Prestamos liquidados exitosamente.");
                    getDataGrid();
                }
              
                return true;
            }
            catch (Exception ex)
            {
                showError(error + " - " + ex.ToString());
                return false;
            }
        }
        
        private void getDataGrid()
        {
            try
            {
              GrdVLiquidacion.DataSource = mLiquidacion.getPrestamos(ref error);
              GrdVLiquidacion.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }     
    }

}