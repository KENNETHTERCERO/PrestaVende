using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class CambioPrecioProducto : System.Web.UI.Page
    {

        #region variables

        private CLASS.cs_cambio_precio clsCambioPrecio = new CLASS.cs_cambio_precio();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && (int)Session["id_usuario"] == 0)
                {
                    Response.Redirect("~/WFWebLogin.aspx");
                }
              
                    if (IsPostBack)
                    {
                        if (lblWarning.Text == "") { divWarning.Visible = false; }
                        if (lblError.Text == "") { divError.Visible = false; }
                        if (lblSuccess.Text == "") { divSucceful.Visible = false; }
                    }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #region metodos

        private void buscarPrestamo(Decimal intPrestamo, int id_sucursal)
        {
            try
            {
                div_gridView.Visible = true;

                DataSet dsRespuesta = clsCambioPrecio.buscarPrestamo(intPrestamo);

                if (dsRespuesta.Tables[0].Rows.Count > 0)
                {
                    GrdVInventario.DataSource = dsRespuesta;
                GrdVInventario.DataBind();
                }
                else
                {
                    showError("No se encontró información para el préstamo buscado." );
                    TxtPrestamo.Text = "";
                    TxtPrestamo.Focus();
                }
               

            }
            catch (Exception ex)
            {
                showError("Error al buscar préstamo: " + ex.ToString());
            }

        }

        private void limpiar()
        {

            TxtPrestamo.Text = "";
            GrdVInventario.DataSource = null;
            GrdVInventario.DataBind();
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (TxtPrestamo.Text.Length > 0)
            {
                buscarPrestamo(Decimal.Parse(TxtPrestamo.Text), Convert.ToInt32(Session["id_sucursal"]));
            }
            else
            {
                showError("Debe ingresar prestamo a buscar.");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            var rows = GrdVInventario.Rows;
            int count = GrdVInventario.Rows.Count;
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
                        decimal precio = Decimal.Parse(txtPrecioSeleccionado.Text);
                        

                        string error = "";

                        if (precio > 0)
                        {



                            bool respuesta = clsCambioPrecio.grabarPrecioInventario(int.Parse(rows[i].Cells[2].Text),precio, Convert.ToInt32(Session["id_usuario"]), ref error);
                                if (respuesta == true)
                                {
                                    showSuccess("Precio actualizado con éxito.");                                    
                                }
                                else
                                {
                                    showError("Error al actualizar el precio del producto seleccionado." + error);
                                }

                                limpiar();
                            
                        }
                        else
                        {
                            showError("El precio seleccionado debe ser mayor a cero.");
                        }

                    }
                }

                if (!algunSeleccionado)
                {
                    showError("Debe seleccionar algún producto para actualizar su precio.");
                }
            }
            catch (Exception ex)
            {
                showError("Error en el proceso de guardado: " + ex.ToString());
            }
        }

        

        
    }
}