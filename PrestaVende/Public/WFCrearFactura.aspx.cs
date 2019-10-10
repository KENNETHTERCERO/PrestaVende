using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFCrearFactura : System.Web.UI.Page
    {
        private CLASS.cs_prestamo cs_prestamo = new CLASS.cs_prestamo();
        private CLASS.cs_factura cs_factura = new CLASS.cs_factura();
        private CLASS.cs_transaccion cs_transaccion = new CLASS.cs_transaccion();
        private CLASS.cs_serie cs_serie = new CLASS.cs_serie();
        private string error = "";

        #region funciones

        private void getPrestamo()
        {
            try
            {
                string id_prestamo = Request.QueryString["id_prestamo"];
                foreach (DataRow item in cs_prestamo.ObtenerPrestamoEspecifico(ref error, id_prestamo).Rows)
                {
                    lblnombre_prestamo.Text = item[1].ToString() + " - Cliente: " + item[2].ToString() + " " + item[3].ToString() + " " + item[4].ToString() + " " + item[5].ToString();
                    lblNombrePrestamo.Text = item[1].ToString();
                    lblNombreCliente.Text = item[2].ToString() + " " + item[3].ToString() + " " + item[4].ToString() + " " + item[5].ToString();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getTransaccion()
        {
            try
            {
                string id_tipo_transaccion = "0";

                switch (Request.QueryString["id_tipo"])
                {
                    case "1":
                        id_tipo_transaccion = "8";
                        break;
                    case "2":
                        id_tipo_transaccion = "9";
                        break;
                    case "3":
                        id_tipo_transaccion = "10";
                        break;
                }

                foreach (DataRow item in cs_transaccion.ObtenerTransaccion(ref error, id_tipo_transaccion).Rows)                
                    lblTransaccion.Text = item[1].ToString();                                
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getSeries()
        {
            try
            {
                int id_sucursal = CLASS.cs_usuario.id_sucursal;
                ddlSerie.DataSource = cs_serie.getSerieDDL(ref error,id_sucursal);
                ddlSerie.DataValueField = "id_serie";
                ddlSerie.DataTextField = "serie";
                ddlSerie.DataBind();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        private void getDetalleFactura()
        {
            string id_prestamo = Request.QueryString["id_prestamo"];

            DataSet ds = new DataSet();

            ds = cs_factura.ObtenerDetalleFacturas(ref error, id_prestamo);

            if(ds.Tables.Count > 0)
            {
                gvDetalleFactura.DataSource = ds.Tables[0];
                gvDetalleFactura.DataBind();

                DataTable dt = new DataTable();

                dt = ds.Tables[1];

                lblSubTotalFactura.Text = dt.Rows[0]["SubTotal"].ToString();
                lblIVAFactura.Text = dt.Rows[0]["IVA"].ToString();
                lblTotalFacturaV.Text = dt.Rows[0]["Total"].ToString();
            }           

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

            }
            else
            {
                getDetalleFactura();
                getPrestamo();
                getTransaccion();
                getSeries();
            }
        }

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

        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id_serie = int.Parse(ddlSerie.SelectedValue.ToString());

                if (id_serie > 0)
                    lblNumeroPrestamoNumeroFactura.Text = (cs_serie.getCorrelativoSerie(ref error, id_serie) + 1).ToString();
                else
                    lblNumeroPrestamoNumeroFactura.Text = "0";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }            
        }
    }
}
