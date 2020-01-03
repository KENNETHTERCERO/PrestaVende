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
        private CLASS.cs_prestamo cs_prestamo = new CLASS.cs_prestamo();
        private string error = "";
        private decimal saldo_prestamo = 0;
        private decimal monto_prestamo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && CLASS.cs_usuario.id_usuario == 0)
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
                foreach (DataRow item in cs_prestamo.ObtenerPrestamoEspecifico(ref error, id_prestamo).Rows)
                {
                    lblnombre_prestamo.Text = item[1].ToString() + "      ";
                    lblValorSaldoPrestamo.Text = item[7].ToString();
                    saldo_prestamo = decimal.Parse(item[7].ToString());
                    monto_prestamo = decimal.Parse(item[9].ToString());
                }

                gvArticulos.DataSource = cs_prestamo.GetDetallePrestamo(ref error, id_prestamo);
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

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox checkBox = e.Row.Cells[5].Controls[0] as CheckBox;
                //CheckBox checkBox = (CheckBox)e.Row.Cells[5].FindControl("retirada");

                if (checkBox.Checked == false)
                    checkBox.Enabled = true;
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

                foreach (GridViewRow row in gvArticulos.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[5].Controls[0] as CheckBox);
                        decimal valorRow = decimal.Parse(row.Cells[4].Text);
                        if (chkRow.Checked == true && chkRow.Enabled == false)
                            total_retiro_actual = total_retiro_actual + valorRow;
                        else if (chkRow.Checked == true && chkRow.Enabled == true)
                            total_retiro_anterior = total_retiro_anterior + valorRow;
                    }
                }

                total_disponible_retiro = total_disponible_retiro - total_retiro_anterior;

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