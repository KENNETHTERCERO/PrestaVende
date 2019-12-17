using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class RecepcionLiquidaciones : System.Web.UI.Page
    {
        #region variables

        private CLASS.cs_recepcion_liquidacion clsRecepcion = new CLASS.cs_recepcion_liquidacion(); 

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            buscarPrestamo(-1);
        }

        protected void BtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            if (TxtPrestamo.Text.Length > 0)
            {
                buscarPrestamo(int.Parse(TxtPrestamo.Text));
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Debe ingresar prestamo a buscar.";
            }
        }        

        protected void GrdVLiquidacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    
                    GridViewRow selectedRow = GrdVLiquidacion.Rows[index];

                    if (selectedRow != null)
                    {
                        div_DatoSeleccionado.Visible = true;
                        lblPrestamoT.Text = selectedRow.Cells[0].ToString();
                        lblPrestamoDetalle.Text = selectedRow.Cells[1].ToString();
                        lblProducto.Text = selectedRow.Cells[2].ToString();
                        lblCantidad.Text = selectedRow.Cells[4].ToString();
                        lblValorPrestado.Text = selectedRow.Cells[5].ToString();
                        lblMontoLiquidado.Text = selectedRow.Cells[6].ToString();
                        lblPrecioSugerido.Text = selectedRow.Cells[7].ToString();
                        lblFechaLiquidacion.Text = selectedRow.Cells[8].ToString();

                    }

                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al seleccionar detalle. "  + ex.ToString();
            }
        }

        protected void btnAceptar_Click(object sender, ImageClickEventArgs e)
        {
            if (lblPrestamoT.Text.Length > 0)
            {
                if (txtPrecio.Text.Length > 0)
                {
                    string error = "";
                    bool respuesta = clsRecepcion.grabarDatosInventario(int.Parse(lblPrestamoDetalle.Text), 1, int.Parse(lblCantidad.Text), int.Parse(txtPrecio.Text), ref error);
                    if (respuesta == true)
                    {
                        lblSuccess.Text = "Producto almacenado con éxito.";

                    }
                    else
                    {
                        lblError.Text = "Error al guardar producto en inventario." + error;
                    }
                    limpiar();
                }
                else
                {
                    lblError.Text = "Debe ingresar el precio para el producto a guardar.";
                }
            }
            else
            {
                lblError.Text = "Debe seleccionar un producto a guardar.";
            }

        }

        #endregion

        #region Metodos

        private void buscarPrestamo(int intPrestamo)
        {
            try
            {
                div_gridView.Visible = true;
                GrdVLiquidacion.DataSource = clsRecepcion.obtenerLiquidaciones(intPrestamo);
                GrdVLiquidacion.DataBind();

            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = "Error al buscar prestamo: " + ex.ToString();
            }

        }

        private void limpiar()
        {
            div_DatoSeleccionado.Visible = true;
            TxtPrestamo.Text = "";
            txtPrecio.Text = "";
            lblPrestamoT.Text = "";
            lblPrestamoDetalle.Text = "";
            lblProducto.Text = "";
            lblCantidad.Text = "";
            lblValorPrestado.Text = "";
            lblMontoLiquidado.Text = "";
            lblPrecioSugerido.Text = "";
            lblFechaLiquidacion.Text = "";

            buscarPrestamo(-1);
        }

        #endregion

    }
}