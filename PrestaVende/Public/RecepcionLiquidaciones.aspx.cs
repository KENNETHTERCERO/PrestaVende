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
             try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null)
                {
                   // Response.Redirect("WFWebLogin.aspx");
                }
                
                    if (!IsPostBack)
                    {

                        buscarPrestamo(-1);
                        
                    }
                

            }
             catch (Exception ex)
             {
                 mostrarError(ex.ToString());
             }
        }



        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (TxtPrestamo.Text.Length > 0)
            {
                buscarPrestamo(Decimal.Parse(TxtPrestamo.Text));
            }
            else
            {
                mostrarError("Debe ingresar prestamo a buscar.");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            var rows = GrdVLiquidacion.Rows;
            int count = GrdVLiquidacion.Rows.Count;
            bool algunSeleccionado = false;

            try
            {

                for (int i = 0; i < count; i++)
                {
                    bool isChecked = ((CheckBox)rows[i].FindControl("SelectedCheckBox")).Checked;
                    if (isChecked)
                    {
                        algunSeleccionado = true;
                        //Do what you want
                        TextBox txtPrecioSeleccionado = ((TextBox)rows[i].FindControl("txtPrecio"));
                        Double precio = double.Parse(txtPrecioSeleccionado.Text);
                        Double PrecioSugerido = double.Parse(rows[i].Cells[9].Text);

                        string error = "";

                        if (precio > 0)
                        {


                            if (precio >= PrecioSugerido)
                            {
                                bool respuesta = clsRecepcion.grabarDatosInventario(int.Parse(rows[i].Cells[2].Text), 1, int.Parse(rows[i].Cells[6].Text), int.Parse(txtPrecioSeleccionado.Text), ref error);
                                if (respuesta == true)
                                {

                                    divSucceful.Visible = true;
                                    lblSuccess.Text = "Producto almacenado con éxito.";
                                }
                                else
                                {
                                    mostrarError("Error al guardar producto en inventario." + error);
                                }

                                limpiar();
                            }
                            else
                            {
                                mostrarError("El precio seleccionado no puede ser menor al precio sugerido.");
                            }
                        }
                        else
                        {
                            mostrarError("El precio seleccionado debe ser mayor a cero.");
                        }

                    }
                }

                if (!algunSeleccionado)
                {
                    mostrarError("Debe seleccionar algún producto para guardar en inventario.");
                }
            }
            catch (Exception ex)
            {
                mostrarError("Error en el proceso de guardado: " + ex.ToString());
            }
                
        }       

        #endregion

        #region Metodos

        private void buscarPrestamo(Decimal intPrestamo)
        {
            try
            {
                div_gridView.Visible = true;                
                GrdVLiquidacion.DataSource = clsRecepcion.obtenerLiquidaciones(intPrestamo);
                GrdVLiquidacion.DataBind();

            }
            catch (Exception ex)
            {
                mostrarError("Error al buscar prestamo: " + ex.ToString());
            }

        }

        private void limpiar()
        {
            
            TxtPrestamo.Text = "";                        
            buscarPrestamo(-1);
        }

        private void mostrarError(string mensaje)
        {
            divError.Visible = true;
            lblError.Text = mensaje;
        }

        

        #endregion
    }
}