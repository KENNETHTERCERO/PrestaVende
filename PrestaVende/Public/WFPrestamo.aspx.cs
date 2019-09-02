using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFPrestamo : System.Web.UI.Page
    {
        private CLASS.cs_cliente        cs_cliente = new CLASS.cs_cliente();
        private CLASS.cs_categoria      cs_categoria = new CLASS.cs_categoria();
        private CLASS.cs_subcategoria   cs_subcategoria = new CLASS.cs_subcategoria();
        private CLASS.cs_producto       cs_producto = new CLASS.cs_producto();
        private CLASS.cs_plan_prestamo  cs_plan_prestamo = new CLASS.cs_plan_prestamo();
        private CLASS.cs_interes        cs_interes = new CLASS.cs_interes();
        private CLASS.cs_kilataje       cs_kilataje = new CLASS.cs_kilataje();
        private CLASS.cs_marca          cs_marca = new CLASS.cs_marca();

        string error = "";

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
                        //if (lblWarning.Text == "") { divWarning.Visible = false; }
                        //if (lblError.Text == "") { divError.Visible = false; }
                        //if (lblSuccess.Text == "") { divSucceful.Visible = false; }
                    }
                    else
                    {
                        getClient();
                        getCategorias();
                        getPlanPrestamo();
                        getInteres();
                        getKilataje();
                        getMarca();
                        hideAllControls();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #region funciones
        private void getMarca()
        {
            try
            {
                ddlMarca.DataSource = cs_marca.getMarca(ref error);
                ddlMarca.DataValueField = "id_marca";
                ddlMarca.DataTextField = "marca";
                ddlMarca.DataBind();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        private void getKilataje()
        {
            try
            {
                ddlKilataje.DataSource = cs_kilataje.getKilataje(ref error);
                ddlKilataje.DataValueField = "id_kilataje";
                ddlKilataje.DataTextField = "kilataje";
                ddlKilataje.DataBind();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        private void getInteres()
        {
            try
            {
                ddlIntereses.DataSource = cs_interes.getInteres(ref error);
                ddlIntereses.DataValueField = "id_interes";
                ddlIntereses.DataTextField = "interes";
                ddlIntereses.DataBind();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        private void getPlanPrestamo()
        {
            try
            {
                ddlTipoPrestamo.DataSource = cs_plan_prestamo.getPlanPrestamo(ref error);
                ddlTipoPrestamo.DataValueField = "id_plan_prestamo";
                ddlTipoPrestamo.DataTextField = "plan_prestamo";
                ddlTipoPrestamo.DataBind();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

        private void getCategorias()
        {
            try
            {
                ddlCategoria.DataSource = cs_categoria.getCategoria(ref error);
                ddlCategoria.DataValueField = "id_categoria";
                ddlCategoria.DataTextField = "categoria";
                ddlCategoria.DataBind();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void getSubCategorias(string id_categoria)
        {
            try
            {
                ddlSubCategoria.DataSource = cs_subcategoria.getSubCategoria(ref error, id_categoria);
                ddlSubCategoria.DataValueField = "id_sub_categoria";
                ddlSubCategoria.DataTextField = "sub_categoria";
                ddlSubCategoria.DataBind();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void getProductos(string id_subcategoria)
        {
            try
            {
                ddlProducto.DataSource = cs_producto.getProducto(ref error, id_subcategoria);
                ddlProducto.DataValueField = "id_producto";
                ddlProducto.DataTextField = "producto";
                ddlProducto.DataBind(); 
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void getClient()
        {
            try
            {
                string id_cliente = Request.QueryString["id_cliente"];
                lblid_cliente.Text = id_cliente;
                foreach(DataRow item in cs_cliente.getSpecificClient(ref error, id_cliente).Rows)
                {
                    lblnombre_cliente.Text = item[3].ToString() + " " + item[4].ToString() + " " + item[5].ToString() + " " + item[6].ToString();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void HideOrShowControls(string id_categoria)
        {
            try
            {
                if (id_categoria.Equals("1"))
                {
                    showControlsJewelry();
                }
                else
                {
                    showControlsDiffrentJewelry();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void showControlsJewelry()
        {
            try
            {
                //Ver controles de Joyas ----------
                lblPeso.Visible             = true;
                lblKilataje.Visible         = true;
                lblObservaciones.Visible    = true;
                lblPesoDescuento.Visible    = true;
                lblPesoConDescuento.Visible = true;
                txtPeso.Visible             = true;
                ddlKilataje.Visible         = true;
                txtObservaciones.Visible    = true;
                txtPesoDescuento.Visible    = true;
                txtPesoConDescuento.Visible = true;
                //---------------------------------
                //Ocultar controles de productos diferentes a joyas
                lblMarca.Visible            = false;
                lblCaracteristicas.Visible  = false;
                ddlMarca.Visible            = false;
                txtCaracteristicas.Visible  = false;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void showControlsDiffrentJewelry()
        {
            try
            {
                //Ver controles de Joyas ----------
                lblPeso.Visible             = false;
                lblKilataje.Visible         = false;
                lblObservaciones.Visible    = false;
                lblPesoDescuento.Visible    = false;
                lblPesoConDescuento.Visible = false;
                txtPeso.Visible             = false;
                ddlKilataje.Visible         = false;
                txtObservaciones.Visible    = false;
                txtPesoDescuento.Visible    = false;
                txtPesoConDescuento.Visible = false;
                //---------------------------------
                //Ocultar controles de productos diferentes a joyas
                lblMarca.Visible            = true;
                lblCaracteristicas.Visible  = true;
                ddlMarca.Visible            = true;
                txtCaracteristicas.Visible  = true;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void hideAllControls()
        {
            try
            {
                //Ver controles de Joyas ----------
                lblPeso.Visible             = false;
                lblKilataje.Visible         = false;
                lblObservaciones.Visible    = false;
                lblPesoDescuento.Visible    = false;
                lblPesoConDescuento.Visible = false;
                txtPeso.Visible             = false;
                ddlKilataje.Visible         = false;
                txtObservaciones.Visible    = false;
                txtPesoDescuento.Visible    = false;
                txtPesoConDescuento.Visible = false;
                //---------------------------------
                //Ocultar controles de productos diferentes a joyas
                lblMarca.Visible            = false;
                lblCaracteristicas.Visible  = false;
                ddlMarca.Visible            = false;
                txtCaracteristicas.Visible  = false;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
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

        #region controles
        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getSubCategorias(ddlCategoria.SelectedValue.ToString());
                HideOrShowControls(ddlCategoria.SelectedValue.ToString());
                if (ddlCategoria.SelectedValue.ToString().Equals("0"))
                {
                    hideAllControls();
                }
                getProductos("0");
                ddlSubCategoria.Focus();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void ddlSubCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getProductos(ddlSubCategoria.SelectedValue.ToString());
                ddlProducto.Focus();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void txtPeso_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPesoDescuento.Text.ToString().Length <= 0)
                {
                    txtPesoDescuento.Text = "0";
                }

                if (txtPeso.Text.ToString().Length <= 0)
                {
                    txtPeso.Text = "0";
                }

                txtPesoConDescuento.Text = Math.Round(Convert.ToDecimal(txtPeso.Text.ToString()) - Convert.ToDecimal(txtPesoDescuento.Text.ToString()), 2).ToString();
                txtPesoDescuento.Focus();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void txtPesoDescuento_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPesoDescuento.ToString().Length <= 0)
                {
                    txtPesoDescuento.Text = "0";
                }

                if (txtPeso.ToString().Length <= 0)
                {
                    txtPeso.Text = "0";
                }

                txtPesoConDescuento.Text = Math.Round(Convert.ToDecimal(txtPeso.Text.ToString()) - Convert.ToDecimal(txtPesoDescuento.Text.ToString()), 2).ToString();
                ddlKilataje.Focus();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
        #endregion

        protected void ddlKilataje_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}