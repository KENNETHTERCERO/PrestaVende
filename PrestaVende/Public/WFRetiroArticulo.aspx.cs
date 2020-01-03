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
                    lblnombre_prestamo.Text = item[1].ToString();
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
                CheckBox checkBox = e.Row.Cells[4].Controls[0] as CheckBox;
                checkBox.Enabled = true;
            }
        }

        protected void btnRetirar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                foreach (GridViewRow row in gvArticulos.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[4].Controls[0] as CheckBox);
                        if (chkRow.Checked)
                        {

                        }
                    }
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