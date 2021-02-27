using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class CambioInteresPrestamo : System.Web.UI.Page
    {
        private CLASS.cs_interes cs_interes = new CLASS.cs_interes();

        
        string error = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && Convert.ToInt32(Session["id_usuario"]) == 0)
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

                        limpiar();
                        getInteres();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
        #region Metodos
        private void buscarPrestamo(Decimal intPrestamo, int id_sucursal)
        {
            try
            {                

                DataSet dsRespuesta = cs_interes.buscarPrestamo(intPrestamo, id_sucursal);

                if (dsRespuesta.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in dsRespuesta.Tables[0].Rows)
                    {
                        div_DatosPrestamo.Visible = true;
                        lblEmpresa.Text = item["empresa"].ToString();
                        lblSucursal.Text = item["sucursal"].ToString();
                        lblNoPrestamo.Text = item["numero_prestamo"].ToString();
                        lblFechaUP.Text = item["fecha_ultimo_pago"].ToString();
                        lblFechaPP.Text = item["fecha_proximo_pago"].ToString();
                        lblEstado.Text = item["estado_prestamo"].ToString();
                        lblSaldo.Text = item["saldo_prestamo"].ToString();                        
                        ddlIntereses.SelectedValue = item["id_interes"].ToString();
                    }
                }
                else
                {
                    showError("No se encontró información para el préstamo buscado.");
                    this.TxtPrestamo.Text = "";
                    this.TxtPrestamo.Focus();
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
            lblEmpresa.Text = "";
            lblSucursal.Text = "";
            lblNoPrestamo.Text = "";
            lblFechaUP.Text = "";
            lblFechaPP.Text = "";
            lblEstado.Text = "";
            lblSaldo.Text = "";
                        
            div_DatosPrestamo.Visible = false;
        }

        private void getInteres()
        {
            try
            {
                this.ddlIntereses.DataSource = cs_interes.getInteres(ref error);
                this.ddlIntereses.DataValueField = "id_interes";
                this.ddlIntereses.DataTextField = "interes";
                this.ddlIntereses.DataBind();

                ddlIntereses.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        private bool validarCambio()
        {
            bool respuesta= false;

            if(int.Parse(ddlIntereses.SelectedValue) > 0)
            {
                respuesta = true;
            }

            return respuesta;
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

        #endregion

        #region Eventos
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string error = "";
                bool respuesta = false;

                if (validarCambio())
                {
                    respuesta = cs_interes.actualizarInteresPrestamo(ref error, Int64.Parse(TxtPrestamo.Text), ((this.Session["id_rol"].ToString().Equals("1") || (this.Session["id_rol"].ToString().Equals("6"))) ? -1 : Convert.ToInt32(Session["id_sucursal"])), int.Parse(ddlIntereses.SelectedValue) );
                }
                else
                {
                    showWarning("No se ha seleccionado un interés distinto al configurado actualmente.");
                }

                if (respuesta)
                {
                    showSuccess("El tipo de interés del préstamo fue actualizado exitosamente.");
                }
                else
                {
                    showError("No fue posible actualizar el tipo de interés del préstamo.");
                }

            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }

            limpiar();
            getInteres();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TxtPrestamo.Text.Length > 0)
                {
                    buscarPrestamo(Decimal.Parse(this.TxtPrestamo.Text),  (((Convert.ToInt32(Session["id_rol"]) == 1) || (Convert.ToInt32(Session["id_rol"]) == 6)) ? -1 : Convert.ToInt32(Session["id_sucursal"])));
                }
                else
                {
                    showError("Debe ingresar prestamo a buscar.");
                }
            }catch(Exception ex)
            {
                showError("Error al buscar prestamo, error: " + ex.Message);
            }
        }
        #endregion
    }
}