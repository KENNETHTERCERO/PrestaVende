using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFRetiroArticulo : System.Web.UI.Page
    {
        private CLASS.cs_prestamo cs_prestamo_local = new CLASS.cs_prestamo();
        private static string error = "";
        private static decimal saldo_prestamo = 0;
        private static decimal monto_prestamo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && (int)Session["id_usuario"] == 0)
                {
                    Response.Redirect("~/WFWebLogin.aspx");
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
                        getPrestamo();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }            
        }

        private void getPrestamo()
        {
            try
            {
                string id_prestamo = Request.QueryString["id_prestamo"];
                DataTable dt = cs_prestamo_local.ObtenerRetiroArticulo(ref error, id_prestamo);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "0")
                        btnRetirar.Visible = false;
                }
                else
                    btnRetirar.Visible = false;

                foreach (DataRow item in cs_prestamo_local.ObtenerPrestamoEspecifico(ref error, id_prestamo).Rows)
                {
                    lblnombre_prestamo.Text = item[1].ToString() + "      ";
                    saldo_prestamo = decimal.Parse(item[7].ToString());
                    monto_prestamo = decimal.Parse(item[9].ToString());
                    Session["id_interes_proyeccion"] = item[10].ToString();
                    Session["monto_proyeccion"] = item[7].ToString();
                    Session["id_plan_prestamo_proyeccion"] = item[11].ToString();

                    lblValorSaldoPrestamo.Text = Math.Round(Convert.ToDecimal(monto_prestamo - saldo_prestamo), 2).ToString();                    
                }

                gvArticulos.DataSource = cs_prestamo_local.GetDetallePrestamo(ref error, id_prestamo);
                gvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool showError(string error)
        {
            divError.Visible = true;
            lblError.Controls.Add(new LiteralControl(string.Format("<span style='color:Red'>{0}</span>", error)));
            return true;
        }

        private bool showWarning(string warning)
        {
            divWarning.Visible = true;
            lblWarning.Controls.Add(new LiteralControl(string.Format("<span style='color:Orange'>{0}</span>", warning)));
            return true;
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox checkBox = e.Row.Cells[5].Controls[0] as CheckBox;
                    //CheckBox checkBox = (CheckBox)e.Row.Cells[5].FindControl("retirada");

                    if (checkBox.Checked == false)
                        checkBox.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnRetirar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                decimal total_disponible_retiro = monto_prestamo - saldo_prestamo;
                decimal total_retiro_actual = 0;
                decimal total_retiro_anterior = 0;
                string id_prestamo_detalles = ""; 

                foreach (GridViewRow row in gvArticulos.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[5].Controls[0] as CheckBox);
                        decimal valorRow = decimal.Parse(row.Cells[4].Text);
                        if (chkRow.Checked == true && chkRow.Enabled == false)
                            total_retiro_anterior = total_retiro_anterior + valorRow;
                        else if (chkRow.Checked == true && chkRow.Enabled == true)
                        {
                            if (id_prestamo_detalles == "")
                                id_prestamo_detalles = row.Cells[6].Text;
                            else
                                id_prestamo_detalles = id_prestamo_detalles + "|" + row.Cells[6].Text;

                            total_retiro_actual = total_retiro_actual + valorRow;
                        }
                    }
                }
                                
                char[] spearator = {'|'};
                string[] array_prestamos_detalles = id_prestamo_detalles.Split(spearator);

                total_disponible_retiro = total_disponible_retiro - total_retiro_anterior;

                if (total_disponible_retiro <= 0)
                    showWarning("No cuenta con los abonos disponibles para realizar el retiro.");
                else
                {
                    if (total_disponible_retiro >= total_retiro_actual)
                    {
                        if (cs_prestamo_local.guardar_retiros_articulo(ref error, array_prestamos_detalles))
                        {
                            string id_prestamo = Request.QueryString["id_prestamo"];

                            string scriptEstadoCuenta = "window.open('WebReport.aspx?tipo_reporte=3" + "&numero_prestamo=" + lblnombre_prestamo.Text + "');";
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "NewWindow", scriptEstadoCuenta, true);                            

                            string scriptText = "alert('my message'); window.location='WFFacturacion.aspx?id_prestamo=" + id_prestamo + "'";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
                        }
                        else
                            showError("Error al almacenar los datos.");
                    }
                    else
                        showWarning("No cuenta con los abonos disponibles para realizar el retiro.");
                }

            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void gvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}