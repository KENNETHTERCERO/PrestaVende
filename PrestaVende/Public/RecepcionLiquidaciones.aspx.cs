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

                if (cookie == null && Convert.ToInt32(Session["id_usuario"]) == 0)
                {
                    Response.Redirect("WFWebLogin.aspx");
                }
                
                    if (!IsPostBack)
                    {
                        buscarPrestamo(-1);
                    }
                    else
                    {
                        if (lblWarning.Text == "") { divWarning.Visible = false; }
                        if (lblError.Text == "") { divError.Visible = false; }
                        if (lblSuccess.Text == "") { divSucceful.Visible = false; }
                    }
                

            }
             catch (Exception ex)
             {
                 mostrarError(ex.ToString());
             }
        }



        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (this.TxtPrestamo.Text.Length > 0)
            {
                buscarPrestamo(Decimal.Parse(this.TxtPrestamo.Text));
            }
            else
            {
                mostrarError("Debe ingresar prestamo a buscar.");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            recibirPrestamos();
        }

        #endregion

        #region Metodos

        private void recibirPrestamos()
        {
            var rows = this.GrdVLiquidacion.Rows;
            int count = this.GrdVLiquidacion.Rows.Count;
            bool algunSeleccionado = false;
            string error1 = "", error2 = "";
            int contadorErrores = 0, contadorSinError = 0;

            try
            {
                bool respuesta = false;
                for (int i = 0; i < count; i++)
                {
                    bool isChecked = ((CheckBox)rows[i].FindControl("SelectedCheckBox")).Checked;
                    if (isChecked)
                    {
                        algunSeleccionado = true;
                        TextBox txtPrecioSeleccionado = ((TextBox)rows[i].FindControl("txtPrecio"));
                        Double precio = double.Parse(txtPrecioSeleccionado.Text);
                        Double PrecioSugerido = double.Parse(rows[i].Cells[9].Text);
                        error1 = error1 + " -*- ";

                        if (precio > 0)
                        {
                            if (precio >= PrecioSugerido)
                            {
                                if (clsRecepcion.grabarDatosInventario(int.Parse(rows[i].Cells[2].Text), 1, int.Parse(rows[i].Cells[6].Text), int.Parse(txtPrecioSeleccionado.Text), ref error2))
                                {
                                    error1 += error2;
                                    contadorSinError += 1;
                                }
                                else
                                {
                                    contadorErrores += 1;
                                }
                            }
                            else
                            {
                                mostrarError("El precio seleccionado no puede ser menor al precio sugerido. Del contrato: " + rows[i].Cells[1].Text + " ID: " + rows[i].Cells[2].Text + ".");
                                break;
                            }
                        }
                        else
                        {
                            mostrarError("El precio seleccionado no puede ser 0. Del contrato: " + rows[i].Cells[1].Text + " ID: " + rows[i].Cells[2].Text + ".");
                            break;
                        }
                    }
                }

                if (!algunSeleccionado)
                {
                    mostrarError("Debe seleccionar algún producto para guardar en inventario.");
                }
                else
                {
                    if (contadorSinError > 0 && contadorErrores == 0)
                    {
                        divSucceful.Visible = true;
                        lblSuccess.Text = contadorSinError.ToString()  + " productos recibidos con exito.";
                        limpiar();
                    }

                    if (contadorErrores > 0)
                    {
                        mostrarError("Existio algun error al guardar la recepcion. " + error1);
                        limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                mostrarError("Error en el proceso de guardado: " + ex.ToString());
            }
        }

        private void buscarPrestamo(Decimal intPrestamo)
        {
            try
            {
                div_gridView.Visible = true;

                clsRecepcion = new CLASS.cs_recepcion_liquidacion();
                DataSet dsRespuesta = clsRecepcion.obtenerLiquidaciones(intPrestamo, Convert.ToInt32(Session["id_sucursal"].ToString()));

                if (dsRespuesta.Tables[0].Rows.Count > 0)
                {
                    this.GrdVLiquidacion.DataSource = dsRespuesta;
                    this.GrdVLiquidacion.DataBind();
                }
                else
                {
                    mostrarError("No se encontró información para el préstamo buscado.");
                    this.TxtPrestamo.Text = "";
                    this.TxtPrestamo.Focus();
                }
            }
            catch (Exception ex)
            {
                mostrarError("Error al buscar prestamo: " + ex.ToString());
            }
        }

        private void limpiar()
        {

            this.TxtPrestamo.Text = "";                        
            buscarPrestamo(-1);
        }

        private void mostrarError(string mensaje)
        {
            this.divError.Visible = true;
            this.lblError.Text = mensaje;
        }

        

        #endregion
    }
}