using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFListadoPrestamo : System.Web.UI.Page
    {
        private CLASS.cs_prestamo cs_prestamo = new CLASS.cs_prestamo();
        private CLASS.cs_cliente cs_cliente = new CLASS.cs_cliente();
        private string error = "";

        #region funciones

        private void getPrestamo()
        {
            try
            {
                string id_prestamo = Request.QueryString["id_cliente"];

                gvPrestamo.DataSource = cs_prestamo.ObtenerPrestamos(ref error, id_prestamo);
                gvPrestamo.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getClient()
        {
            try
            {
                string id_cliente = Request.QueryString["id_cliente"];
                lblid_cliente.Text = id_cliente;
                foreach (DataRow item in cs_cliente.getSpecificClient(ref error, id_cliente).Rows)
                {
                    lblnombre_cliente.Text = item[3].ToString() + " " + item[4].ToString() + " " + item[5].ToString() + " " + item[6].ToString();
                    lblDireccionTexto.Text = item[9].ToString();
                    lblTelefonoTexto.Text = item[11].ToString();
                    lblFechaCreacionTexto.Text = item[19].ToString();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getNumeroPrestamosCancelados()
        {
            try
            {
                string id_cliente = Request.QueryString["id_cliente"];
                lblid_cliente.Text = id_cliente;
                foreach (DataRow item in cs_prestamo.getCountPrestamosCancelados(ref error, id_cliente, this.Session["id_empresa"].ToString()).Rows)
                {
                    lblPrestamosCanceladosNumero.Text = item[0].ToString();
                    lblPrestamosCanceladosMonto.Text = item[1].ToString();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getNumeroPrestamosActivos()
        {
            try
            {
                string id_cliente = Request.QueryString["id_cliente"];
                lblid_cliente.Text = id_cliente;
                foreach (DataRow item in cs_prestamo.getCountPrestamosActivos(ref error, id_cliente, this.Session["id_empresa"].ToString()).Rows)
                {
                    lblPrestamosActivosNumero.Text = item[0].ToString();
                    lblPrestamosActivosMonto.Text = item[1].ToString();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getNumeroPrestamosLiquidados()
        {
            try
            {
                string id_cliente = Request.QueryString["id_cliente"];
                lblid_cliente.Text = id_cliente;
                foreach (DataRow item in cs_prestamo.getCountPrestamosLiquidados(ref error, id_cliente, this.Session["id_empresa"].ToString()).Rows)
                {
                    lblPrestamosLiquidadosNumero.Text = item[0].ToString();
                    lblPrestamosLiquidadosMonto.Text = item[1].ToString();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
        #endregion

        #region controls
        protected void Page_Load(object sender, EventArgs e)
        {
            getClient();
            getPrestamo();
            getNumeroPrestamosActivos();
            getNumeroPrestamosCancelados();
            getNumeroPrestamosLiquidados();
        }
        
        protected void gvPrestamo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "crear")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    
                    GridViewRow selectedRow = gvPrestamo.Rows[index];
                    TableCell id_prestamo = selectedRow.Cells[1];
                    Session["id_prestamo_to_sub_facturacion"] = id_prestamo.Text.ToString();
                    Response.Redirect("WFFacturacion.aspx?id_prestamo=" + id_prestamo.Text);

                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnEditarCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFBusquedaCliente?accion=editar&id_cliente=" + lblid_cliente.Text);
        }

        protected void btnNuevoPrestamo_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFPrestamo?id_cliente=" + lblid_cliente.Text);
        }
        #endregion

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

        
    }
}