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
        private CLASS.cs_casilla        cs_casilla = new CLASS.cs_casilla();
        private CLASS.cs_prestamo       cs_prestamo = new CLASS.cs_prestamo();

        private static DataTable dtTablaJoyas;
        private static DataTable dtTablaArticulos;

        DataRow row;

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
                        if (lblWarning.Text == "") { divWarning.Visible = false; }
                        if (lblError.Text == "") { divError.Visible = false; }
                        if (lblSuccess.Text == "") { divSucceful.Visible = false; }
                    }
                    else
                    {
                        dtTablaJoyas = new DataTable("tablaJoyas");
                        dtTablaArticulos = new DataTable("tablaArticulos");
                        getClient();
                        getCategorias();
                        getPlanPrestamo();
                        getInteres();
                        getKilataje();
                        getMarca();
                        hideAllControls();
                        setColumnsJewelry();
                        setColumnsDifferentJewelry();
                        txtPesoDescuento.Text = "0";
                        getCasillas();
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
                ddlCategoria.DataSource = cs_categoria.getCategoriaComboBox(ref error);
                ddlCategoria.DataValueField = "id_categoria";
                ddlCategoria.DataTextField = "categoria";
                ddlCategoria.DataBind();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void getCasillas()
        {
            try
            {
                ddlCasilla.DataSource = cs_casilla.getCasillas(ref error);
                ddlCasilla.DataValueField = "id_casilla";
                ddlCasilla.DataTextField = "casilla";
                ddlCasilla.DataBind();
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

        private void getPrecioProducto()
        {
            try
            {
                decimal precio_por_gramo = 0, total_precio_por_peso = 0, gramo = 0;
                gramo = Convert.ToDecimal(txtPesoConDescuento.Text.ToString());
                foreach (DataRow item in cs_kilataje.getKilatajeByID(ref error, ddlKilataje.SelectedValue.ToString()).Rows)
                {
                    precio_por_gramo = Convert.ToDecimal(item[2].ToString());
                }
                total_precio_por_peso = gramo * precio_por_gramo;
                txtValor.Text = total_precio_por_peso.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void addArticuloJoya()
        {
            try
            {
                row = dtTablaJoyas.NewRow();
                row["id_producto"] = ddlProducto.SelectedValue;
                row["numero_linea"] = 1;
                row["joya"] = ddlProducto.SelectedItem.Text.ToString();
                row["kilataje"] = ddlKilataje.SelectedItem.Text.ToString();
                row["peso"] = txtPeso.Text;
                row["descuento"] = txtPesoDescuento.Text;
                row["pesoReal"] = txtPesoConDescuento.Text.ToString();
                row["valor"] = txtValor.Text.ToString();
                if (txtCaracteristicas.Text.ToString().Length > 0)
                {
                    row["caracteristicas"] = txtCaracteristicas.Text.ToString();
                }
                else
                    row["caracteristicas"] = "N/A";

                row["id_kilataje"] = ddlKilataje.SelectedValue.ToString();
                dtTablaJoyas.Rows.Add(row);
                int linea = 1;

                if (dtTablaJoyas.Rows.Count > 1)
                {
                    decimal totalPrestamo = 0;
                    foreach (DataRow item in dtTablaJoyas.Rows)
                    {
                        item["numero_linea"] = linea;
                        linea++;
                        totalPrestamo += Convert.ToDecimal(item["valor"].ToString());
                    }
                    lblTotalPrestamoQuetzales.Text = Math.Round(totalPrestamo, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    lblTotalPrestamoQuetzales.Text = txtValor.Text;
                }

                gvProductoJoya.DataSource = dtTablaJoyas;
                gvProductoJoya.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void addArticuloDifferentJoya()
        {
            try
            {
                row = dtTablaArticulos.NewRow();
                row["id_producto"]     = ddlProducto.SelectedValue.ToString();
                row["numero_linea"          ] = 1;
                row["producto"       ] = ddlProducto.SelectedItem.Text.ToString();
                row["marca"          ] = ddlMarca.SelectedItem.Text.ToString();
                row["valor"          ] = txtValor.Text;
                row["caracteristicas"] = txtCaracteristicas.Text;
                row["id_marca"] = ddlMarca.SelectedValue.ToString();
                //row[] = txtCaracteristicas.Text.ToString();
                //row[] = txtCaracteristicas.Text.ToString();
                //row["id_kilataje"] = txtCaracteristicas.Text.ToString();
                dtTablaArticulos.Rows.Add(row);
                int linea = 1;

                if (dtTablaArticulos.Rows.Count > 1)
                {
                    decimal totalPrestamo = 0;
                    foreach (DataRow item in dtTablaArticulos.Rows)
                    {
                        item["numero_linea"] = linea;
                        linea++;
                        totalPrestamo += Convert.ToDecimal(item["valor"].ToString());
                    }
                    lblTotalPrestamoQuetzales.Text = Math.Round(totalPrestamo, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    lblTotalPrestamoQuetzales.Text = txtValor.Text;
                }

                gvProductoElectrodomesticos.DataSource = dtTablaArticulos;
                gvProductoElectrodomesticos.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void setColumnsJewelry()
        {
            try
            {
                dtTablaJoyas.Columns.Add("id_producto");
                dtTablaJoyas.Columns.Add("numero_linea"          );
                dtTablaJoyas.Columns.Add("joya"           );
                dtTablaJoyas.Columns.Add("kilataje"       );
                dtTablaJoyas.Columns.Add("peso"           );
                dtTablaJoyas.Columns.Add("descuento"      );
                dtTablaJoyas.Columns.Add("pesoReal"       );
                dtTablaJoyas.Columns.Add("valor"          );
                dtTablaJoyas.Columns.Add("caracteristicas");
                dtTablaJoyas.Columns.Add("id_kilataje"    );
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void setColumnsDifferentJewelry()
        {
            try
            {
                dtTablaArticulos.Columns.Add("id_producto"    );
                dtTablaArticulos.Columns.Add("numero_linea"   );
                dtTablaArticulos.Columns.Add("producto"       );
                dtTablaArticulos.Columns.Add("marca"          );
                dtTablaArticulos.Columns.Add("valor"          );
                dtTablaArticulos.Columns.Add("caracteristicas");
                dtTablaArticulos.Columns.Add("id_marca");
                //dtTablaArticulos.Columns.Add();
                //dtTablaArticulos.Columns.Add();
                //dtTablaArticulos.Columns.Add();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validateJewelry()
        {
            try
            {
                if (ddlSubCategoria.SelectedValue.ToString().Equals("0")) { showWarning("Uste debe seleccionar una subcategoria para poder agregar articulo."); return false; }
                else if (ddlProducto.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar un producto antes de agregar articulo."); return false; }
                else if (txtPeso.Text.ToString().Equals("0") || txtPeso.Text.ToString().Equals("0.00") || txtPeso.Text.ToString().Length == 0) { showWarning("Usted debe ingresar el peso de la joya para poder agregar."); return false; }
                else if (txtPesoDescuento.Text.ToString().Length == 0) { showWarning("Debe agregar 0 si no hay descuento de peso que hacerle a la joya."); return false; }
                else if (ddlKilataje.SelectedValue.ToString().Equals("0")) { showWarning("Debe seleccionar el kilataje que corresponde a la Joya que esta agregando."); return false; }
                else if (txtValor.Text.ToString().Equals("0") || txtValor.Text.ToString().Equals("0.00") || txtValor.Text.ToString().Length == 0) { showWarning("Debe agregar el valor que se dara como prestamo para esa joya."); return false; }
                else
                    return true;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private bool validateArticulos()
        {
            try
            {
                if (ddlSubCategoria.SelectedValue.ToString().Equals("0")) { showWarning("Uste debe seleccionar una subcategoria para poder agregar articulo."); return false; }
                else if (ddlProducto.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar un producto antes de agregar articulo."); return false; }
                else if (ddlMarca.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar una marca para poder agregar."); return false; }
                //else if (txtPesoDescuento.Text.ToString().Length == 0) { showWarning("Debe agregar 0 si no hay descuento de peso que hacerle a la joya."); return false; }
                //else if (ddlKilataje.SelectedValue.ToString().Equals("0")) { showWarning("Debe seleccionar el kilataje que corresponde a la Joya que esta agregando."); return false; }
                else if (txtValor.Text.ToString().Equals("0") || txtValor.Text.ToString().Equals("0.00") || txtValor.Text.ToString().Length == 0) { showWarning("Debe agregar el valor que se dara como prestamo para esa joya."); return false; }
                else
                    return true;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void blockComboBox()
        {
            try
            {
                if (gvProductoJoya.Rows.Count == 0 && gvProductoElectrodomesticos.Rows.Count == 0)
                {
                    ddlCategoria.Enabled = true;
                }
                else
                {
                    ddlCategoria.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void cleanControls()
        {
            try
            {
                if (ddlCategoria.SelectedValue.ToString().Equals("1"))
                {
                    ddlSubCategoria.SelectedValue = "0";
                    ddlProducto.SelectedValue = "0";
                    txtPeso.Text = "";
                    txtPesoDescuento.Text = "";
                    txtPesoConDescuento.Text = "";
                    ddlKilataje.SelectedValue = "0";
                    txtObservaciones.Text = "";
                    txtValor.Text = "";
                }
                else
                {
                    ddlSubCategoria.SelectedValue = "0";
                    ddlProducto.SelectedValue = "0";
                    ddlMarca.SelectedValue = "0";
                    txtCaracteristicas.Text = "";
                    txtValor.Text = "";
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validateDataPrestamo()
        {
            try
            {
                if (ddlTipoPrestamo.SelectedValue.ToString().Equals("0")){showWarning("Debe seleccionar que tipo de prestamo necesita guardar."); return false;}
                else if (ddlCasilla.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar una casilla para poder guardar."); return false; }
                else if (ddlIntereses.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar que intereses necesita para el prestamo para poder guardar."); return false; }
                else if (gvProductoElectrodomesticos.Rows.Count.ToString().Equals("0") && gvProductoJoya.Rows.Count.ToString().Equals("0")) { showWarning("Usted debe agregar por lo menos un producto para poder guardar."); return false; }
                else
                    return true;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void guardarPrestamo()
        {
            try
            {
                if (ddlCategoria.SelectedValue.ToString().Equals("1"))
                    guardarPrestamoJoya();
                else
                    guardarPrestamoDifferentJoya();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void guardarPrestamoJoya()
        {
            try
            {
                if (cs_prestamo.guardar_prestamo(ref error, generaEncabezado(), dtTablaJoyas, ddlCategoria.SelectedValue.ToString()))
                {
                    showSuccess("Se creo prestamo correctamente.");
                    Response.Redirect("WFBusquedaCliente");
                }
                else
                    showError(error);
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void guardarPrestamoDifferentJoya()
        {
            try
            {
                if (cs_prestamo.guardar_prestamo(ref error, generaEncabezado(), dtTablaArticulos, ddlCategoria.SelectedValue.ToString()))
                {
                    showSuccess("Se creo prestamo correctamente.");
                    Response.Redirect("WFBusquedaCliente");
                }
                else
                    showError(error);
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private string[] generaEncabezado()
        {
            try
            {
                string[] encabezado = new string[12];

                encabezado[0] = lblid_cliente.Text.ToString();
                encabezado[1] = lblTotalPrestamoQuetzales.Text;
                encabezado[2] = "1";
                encabezado[3] = getFechaProximoPago().ToString();
                encabezado[4] = lblTotalPrestamoQuetzales.Text;
                encabezado[5] = CLASS.cs_usuario.usuario;
                encabezado[6] = ddlTipoPrestamo.SelectedValue.ToString();
                encabezado[7] = ddlIntereses.SelectedValue.ToString();
                encabezado[8] = ddlCasilla.SelectedValue.ToString();
                //encabezado[] = ;
                //encabezado[] = ;
                //encabezado[] = ;
                return encabezado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private DateTime getFechaProximoPago()
        {
            try
            {
                DateTime fechaReturn = DateTime.Now;

                fechaReturn = cs_prestamo.fecha_proximo_pago(ref error, ddlTipoPrestamo.SelectedValue.ToString());

                return fechaReturn;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return DateTime.Now;
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

        protected void ddlKilataje_SelectedIndexChanged(object sender, EventArgs e)
        {
            getPrecioProducto();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFPrincipal.aspx", true);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlProducto.SelectedValue.ToString().Equals("1"))
                {
                    if (validateJewelry())
                    {
                        addArticuloJoya();
                    }
                }
                else
                {
                    if (validateArticulos())
                    {
                        addArticuloDifferentJoya();
                    }
                }
                blockComboBox();
                cleanControls();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnGuardarPrestamo_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateDataPrestamo())
                {
                    guardarPrestamo();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void gvProductoElectrodomesticos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "borrar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    dtTablaArticulos.Rows[index].Delete();
                    gvProductoElectrodomesticos.DataSource = dtTablaJoyas;
                    gvProductoElectrodomesticos.DataBind();
                    blockComboBox();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void gvProductoJoya_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "borrar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    dtTablaJoyas.Rows[index].Delete();
                    gvProductoJoya.DataSource = dtTablaJoyas;
                    gvProductoJoya.DataBind();
                    blockComboBox();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
        #endregion
    }
}