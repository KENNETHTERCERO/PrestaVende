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
        private CLASS.cs_cliente cs_cliente = new CLASS.cs_cliente();
        private CLASS.cs_categoria cs_categoria = new CLASS.cs_categoria();
        private CLASS.cs_subcategoria cs_subcategoria = new CLASS.cs_subcategoria();
        private CLASS.cs_producto cs_producto = new CLASS.cs_producto();
        private CLASS.cs_plan_prestamo cs_plan_prestamo = new CLASS.cs_plan_prestamo();
        private CLASS.cs_interes cs_interes = new CLASS.cs_interes();
        private CLASS.cs_kilataje cs_kilataje = new CLASS.cs_kilataje();
        private CLASS.cs_marca cs_marca = new CLASS.cs_marca();
        private CLASS.cs_casilla cs_casilla = new CLASS.cs_casilla();
        private CLASS.cs_prestamo cs_prestamo = new CLASS.cs_prestamo();

        private static DataTable dtTablaJoyas;
        private static DataTable dtTablaArticulos;

        DataRow row = null;

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
                        ViewState["CurrentTableJoyas"] = dtTablaJoyas;
                        ViewState["CurrentTableArticulos"] = dtTablaArticulos;
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
                        getNumeroPrestamo();
                        getTipo();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #region funciones
        private void getNumeroPrestamo()
        {
            try
            {
                lblNumeroPrestamoNumero.Text = cs_prestamo.getMaxNumeroPrestamo(ref error);
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + " " + error);
            }
        }

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

        private void getTipo()
        {
            try
            {
                ddlTipo.DataSource = cs_prestamo.getTipo(ref error);
                ddlTipo.DataValueField = "id_tipo";
                ddlTipo.DataTextField = "opcion";
                ddlTipo.DataBind();
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
                foreach (DataRow item in cs_cliente.getSpecificClient(ref error, id_cliente).Rows)
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
                lblPeso.Visible = true;
                lblKilataje.Visible = true;
                lblObservaciones.Visible = true;
                lblPesoDescuento.Visible = true;
                lblPesoConDescuento.Visible = true;
                txtPeso.Visible = true;
                ddlKilataje.Visible = true;
                txtObservaciones.Visible = true;
                txtPesoDescuento.Visible = true;
                txtPesoConDescuento.Visible = true;
                txtValor.Enabled = false;
                lblRedondeo.Visible = true;
                txtRedondeo.Visible = true;
                btnRedondear.Visible = true;
                //---------------------------------
                //Ocultar controles de productos diferentes a joyas
                lblMarca.Visible = false;
                lblCaracteristicas.Visible = false;
                ddlMarca.Visible = false;
                txtCaracteristicas.Visible = false;
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
                lblPeso.Visible = false;
                lblKilataje.Visible = false;
                lblObservaciones.Visible = false;
                lblPesoDescuento.Visible = false;
                lblPesoConDescuento.Visible = false;
                txtPeso.Visible = false;
                ddlKilataje.Visible = false;
                txtObservaciones.Visible = false;
                txtPesoDescuento.Visible = false;
                txtPesoConDescuento.Visible = false;
                lblRedondeo.Visible = false;
                txtRedondeo.Visible = false;
                btnRedondear.Visible = false;
                //---------------------------------
                //Ocultar controles de productos diferentes a joyas
                lblMarca.Visible = true;
                lblCaracteristicas.Visible = true;
                ddlMarca.Visible = true;
                txtCaracteristicas.Visible = true;
                txtValor.Enabled = true;
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
                lblPeso.Visible = false;
                lblKilataje.Visible = false;
                lblObservaciones.Visible = false;
                lblPesoDescuento.Visible = false;
                lblPesoConDescuento.Visible = false;
                txtPeso.Visible = false;
                ddlKilataje.Visible = false;
                txtObservaciones.Visible = false;
                txtPesoDescuento.Visible = false;
                txtPesoConDescuento.Visible = false;

                //---------------------------------
                //Ocultar controles de productos diferentes a joyas
                lblMarca.Visible = false;
                lblCaracteristicas.Visible = false;
                ddlMarca.Visible = false;
                txtCaracteristicas.Visible = false;
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
                if (ViewState["CurrentTableJoyas"] != null)
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

                    ViewState["CurrentTableJoyas"] = dtTablaJoyas;

                    gvProductoJoya.DataSource = dtTablaJoyas;
                    gvProductoJoya.DataBind();
                }
                else
                {
                    DataTable test = (DataTable)ViewState["CurrentTableJoyas"];
                    DataRow drCurrectRow = null;
                    drCurrectRow = dtTablaJoyas.NewRow();
                    drCurrectRow["id_producto"] = ddlProducto.SelectedValue;
                    drCurrectRow["numero_linea"] = 1;
                    drCurrectRow["joya"] = ddlProducto.SelectedItem.Text.ToString();
                    drCurrectRow["kilataje"] = ddlKilataje.SelectedItem.Text.ToString();
                    drCurrectRow["peso"] = txtPeso.Text;
                    drCurrectRow["descuento"] = txtPesoDescuento.Text;
                    drCurrectRow["pesoReal"] = txtPesoConDescuento.Text.ToString();
                    drCurrectRow["valor"] = txtValor.Text.ToString();
                    if (txtCaracteristicas.Text.ToString().Length > 0)
                    {
                        drCurrectRow["caracteristicas"] = txtCaracteristicas.Text.ToString();
                    }
                    else
                        drCurrectRow["caracteristicas"] = "N/A";

                    drCurrectRow["id_kilataje"] = ddlKilataje.SelectedValue.ToString();
                    test.Rows.Add(drCurrectRow);
                    ViewState["CurrentTableJoyas"] = test;

                    gvProductoJoya.DataSource = test;
                    gvProductoJoya.DataBind();

                    
                }
                calculaTotalPrestamo();
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
                row["id_producto"] = ddlProducto.SelectedValue.ToString();
                row["numero_linea"] = 1;
                row["producto"] = ddlProducto.SelectedItem.Text.ToString();
                row["marca"] = ddlMarca.SelectedItem.Text.ToString();
                row["valor"] = txtValor.Text;
                row["caracteristicas"] = txtCaracteristicas.Text;
                row["id_marca"] = ddlMarca.SelectedValue.ToString();

                dtTablaArticulos.Rows.Add(row);
                calculaTotalPrestamo();

                gvProductoElectrodomesticos.DataSource = dtTablaArticulos;
                gvProductoElectrodomesticos.DataBind();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void calculaTotalPrestamo()
        {
            try
            {
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
                else if (dtTablaArticulos.Rows.Count > 1)
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
                getDataProyeccion("AgregarArticulo");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void setColumnsJewelry()
        {
            try
            {
                dtTablaJoyas.Columns.Add("id_producto");
                dtTablaJoyas.Columns.Add("numero_linea");
                dtTablaJoyas.Columns.Add("joya");
                dtTablaJoyas.Columns.Add("kilataje");
                dtTablaJoyas.Columns.Add("peso");
                dtTablaJoyas.Columns.Add("descuento");
                dtTablaJoyas.Columns.Add("pesoReal");
                dtTablaJoyas.Columns.Add("valor");
                dtTablaJoyas.Columns.Add("caracteristicas");
                dtTablaJoyas.Columns.Add("id_kilataje");
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
                dtTablaArticulos.Columns.Add("id_producto");
                dtTablaArticulos.Columns.Add("numero_linea");
                dtTablaArticulos.Columns.Add("producto");
                dtTablaArticulos.Columns.Add("marca");
                dtTablaArticulos.Columns.Add("valor");
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
                if (ddlTipoPrestamo.SelectedValue.ToString().Equals("0")) { showWarning("Debe seleccionar que tipo de prestamo necesita guardar."); return false; }
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
                string numero_prestamo_guardado = "";
                if (cs_prestamo.guardar_prestamo(ref error, generaEncabezado(), dtTablaJoyas, ddlCategoria.SelectedValue.ToString(), ref numero_prestamo_guardado))
                {
                    lblNumeroPrestamoNumero.Text = numero_prestamo_guardado;
                    showSuccess("Se creo prestamo correctamente.");
                    string script = "window.open('WebReport.aspx?tipo_reporte=1" + "&numero_prestamo=" + lblNumeroPrestamoNumero.Text + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "OpenWindow", script, true);

                    string scriptEstadoCuenta = "window.open('WebReport.aspx?tipo_reporte=3" + "&numero_prestamo=" + lblNumeroPrestamoNumero.Text + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "NewWindow", scriptEstadoCuenta, true);

                    string scriptEtiqueta = "window.open('WebReport.aspx?tipo_reporte=4" + "&numero_prestamo=" + lblNumeroPrestamoNumero.Text + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "OpenWindowEtiqueta", scriptEtiqueta, true);

                    string scriptText = "alert('my message'); window.location='WFListadoPrestamo.aspx?id_cliente=" + lblid_cliente.Text + "'";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
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
                string numero_prestamo_guardado = "";
                if (cs_prestamo.guardar_prestamo(ref error, generaEncabezado(), dtTablaArticulos, ddlCategoria.SelectedValue.ToString(), ref numero_prestamo_guardado))
                {
                    lblNumeroPrestamoNumero.Text = numero_prestamo_guardado;
                    showSuccess("Se creo prestamo correctamente.");
                    string script = "window.open('WebReport.aspx?tipo_reporte=1" + "&numero_prestamo=" + lblNumeroPrestamoNumero.Text + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "OpenWindow", script, true);

                    string scriptEstadoCuenta = "window.open('WebReport.aspx?tipo_reporte=3" + "&numero_prestamo=" + lblNumeroPrestamoNumero.Text + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "NewWindow", scriptEstadoCuenta, true);

                    string scriptEtiqueta = "window.open('WebReport.aspx?tipo_reporte=4" + "&numero_prestamo=" + lblNumeroPrestamoNumero.Text + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "OpenWindowEtiqueta", scriptEtiqueta, true);

                    string scriptText = "alert('my message'); window.location='WFListadoPrestamo.aspx?id_cliente=" + lblid_cliente.Text + "'";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
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
                decimal montoOriginal = 0, nuevoMonto = 0;
                if (txtMontoARecalcular.Visible)
                {
                    montoOriginal = Convert.ToDecimal(lblTotalPrestamoQuetzales.Text);
                    nuevoMonto = Convert.ToDecimal(txtMontoARecalcular.Text);
                }
                else
                {
                    montoOriginal = Convert.ToDecimal(lblTotalPrestamoQuetzales.Text);
                    nuevoMonto = Convert.ToDecimal(lblTotalPrestamoQuetzales.Text);
                }
                encabezado[0] = lblid_cliente.Text.ToString();
                encabezado[1] = nuevoMonto.ToString();
                encabezado[2] = "1";
                encabezado[3] = getFechaProximoPago().ToString();
                encabezado[4] = lblTotalPrestamoQuetzales.Text;
                encabezado[5] = CLASS.cs_usuario.usuario;
                encabezado[6] = ddlTipoPrestamo.SelectedValue.ToString();
                encabezado[7] = ddlIntereses.SelectedValue.ToString();
                encabezado[8] = ddlCasilla.SelectedValue.ToString();
                encabezado[9] = montoOriginal.ToString();
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

        private bool validaCaja()
        {
            try
            {
                if (CLASS.cs_usuario.id_caja == 0)
                {
                    showWarning("Usted no tiene caja asignada.");
                    return false;
                }
                else if (CLASS.cs_usuario.Saldo_caja < Convert.ToDecimal(lblTotalPrestamoQuetzales.Text.ToString()))
                {
                    showWarning("El saldo de su caja es menor al monto del prestamo que quiere emitir, solicite un incremento de capital.");
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void recalculoMontoPrestamo()
        {
            try
            {
                decimal montoFila = 0, porcentaje = 0;
                if (gvProductoElectrodomesticos.Rows.Count > 0)
                {
                    foreach (GridViewRow item in gvProductoElectrodomesticos.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item.Cells[5].Text.ToString()) / Convert.ToDecimal(lblTotalPrestamoQuetzales.Text.ToString()), 4);
                        montoFila = porcentaje * Convert.ToDecimal(txtMontoARecalcular.Text.ToString());
                        montoFila = Math.Round(montoFila, 2);
                        item.Cells[5].Text = montoFila.ToString();
                    }
                }
                else
                {
                    foreach (GridViewRow item in gvProductoJoya.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item.Cells[8].Text.ToString()) / Convert.ToDecimal(lblTotalPrestamoQuetzales.Text.ToString()), 4);
                        montoFila = porcentaje * Convert.ToDecimal(txtMontoARecalcular.Text.ToString());
                        montoFila = Math.Round(montoFila, 2);
                        item.Cells[8].Text = montoFila.ToString();
                    }
                }
                getDataProyeccion("Recalculo");
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void recalculoRedondeoPrestamo()
        {
            try
            {
                decimal montoRedondeo = 0, porcentaje = 0, sumaTotalPrestamo = 0, montoPorFilaConRedondeo = 0;
                if (gvProductoElectrodomesticos.Rows.Count > 0)
                {
                    foreach (GridViewRow item in gvProductoElectrodomesticos.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item.Cells[5].Text.ToString()) / Convert.ToDecimal(lblTotalPrestamoQuetzales.Text.ToString()), 4);
                        montoRedondeo = porcentaje * Convert.ToDecimal(txtRedondeo.Text.ToString());
                        montoPorFilaConRedondeo = Convert.ToDecimal(item.Cells[5].Text.ToString()) + Math.Round(montoRedondeo, 2);
                        item.Cells[5].Text = montoPorFilaConRedondeo.ToString();
                    }
                }
                else
                {
                    foreach (GridViewRow item in gvProductoJoya.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item.Cells[8].Text.ToString()) / Convert.ToDecimal(lblTotalPrestamoQuetzales.Text.ToString()), 4);
                        montoRedondeo = porcentaje * Convert.ToDecimal(txtRedondeo.Text.ToString());
                        montoPorFilaConRedondeo = Convert.ToDecimal(item.Cells[8].Text.ToString()) + Math.Round(montoRedondeo, 2);
                        item.Cells[8].Text = montoPorFilaConRedondeo.ToString();
                    }
                }
                sumaTotalPrestamo = Convert.ToDecimal(lblTotalPrestamoQuetzales.Text.ToString()) + Convert.ToDecimal(txtRedondeo.Text.ToString());
                lblTotalPrestamoQuetzales.Text = sumaTotalPrestamo.ToString();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void aplicarRedondeo()
        {
            try
            {
                if (txtValor.Text.ToString().Length <= 0 || txtValor.Text.ToString().Equals("0"))
                {
                    showWarning("Debe seleccionar un producto, agregar peso y descuento, seleccionar kilataje antes de realizar redondeo.");
                }
                else
                {
                    decimal txtValorAvaluo = 0, valorRedondeo = 0;
                    txtValorAvaluo = Convert.ToDecimal(txtValor.Text.ToString());
                    valorRedondeo = Convert.ToDecimal(txtRedondeo.Text.ToString());
                    txtValor.Text = Convert.ToString(txtValorAvaluo + valorRedondeo);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void validaSeleccionTipoPlanPrestamo()
        {
            try
            {
                decimal totalPrestamo = 0;
                string id_TipoPlan = "";
                if (lblTotalPrestamoQuetzales.Text.ToString().Equals("") || lblTotalPrestamoQuetzales.Text.ToString().Equals("0"))
                    totalPrestamo = 0;
                else
                    totalPrestamo = Convert.ToDecimal(lblTotalPrestamoQuetzales.Text.ToString());

                if (ddlTipo.SelectedValue.ToString().Equals("1"))
                {
                    id_TipoPlan = cs_interes.getIdInteres(ref error, totalPrestamo.ToString());
                    ddlTipoPrestamo.SelectedValue = id_TipoPlan;
                }
                //ddlTipoPrestamo.Enabled = false;
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
                ddlIntereses.SelectedValue = cs_producto.getIDInteresCategoria(ref error, ddlCategoria.SelectedValue);
                ddlIntereses.Enabled = false;
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
                getPrecioProducto();
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
                getPrecioProducto();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void ddlKilataje_SelectedIndexChanged(object sender, EventArgs e)
        {
            getPrecioProducto();
            txtObservaciones.Focus();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCategoria.SelectedValue.ToString().Equals("1") || ddlCategoria.Text.ToString().Equals("JOYAS"))
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
                validaSeleccionTipoPlanPrestamo();
                blockComboBox();
                cleanControls();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void getDataProyeccion(string boton)
        {
            try
            {
                if (boton.Equals("AgregarArticulo"))
                {
                    CLASS.cs_prestamo.id_interes_proyeccion = ddlIntereses.SelectedValue;
                    CLASS.cs_prestamo.monto_proyeccion = lblTotalPrestamoQuetzales.Text;
                    CLASS.cs_prestamo.id_plan_prestamo_proyeccion = ddlTipoPrestamo.SelectedValue;
                    gvProyeccion.DataSource = cs_prestamo.getDTProyeccion(ref error);
                    gvProyeccion.DataBind();
                }
                else
                {
                    CLASS.cs_prestamo.id_interes_proyeccion = ddlIntereses.SelectedValue;
                    CLASS.cs_prestamo.monto_proyeccion = txtMontoARecalcular.Text;
                    CLASS.cs_prestamo.id_plan_prestamo_proyeccion = ddlTipoPrestamo.SelectedValue;
                    gvProyeccion.DataSource = cs_prestamo.getDTProyeccion(ref error);
                    gvProyeccion.DataBind();
                }
                
            }
            catch (Exception ex)
            {
                showError(error + " / " + ex.ToString());
            }
        }

        protected void btnGuardarPrestamo_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateDataPrestamo())
                {
                    if (validaCaja())
                    {
                        guardarPrestamo();
                    }
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
                    calculaTotalPrestamo();
                    validaSeleccionTipoPlanPrestamo();
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
                    calculaTotalPrestamo();
                    validaSeleccionTipoPlanPrestamo();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnRecalcularValorPrestamoTotal_Click(object sender, EventArgs e)
        {
            recalculoMontoPrestamo();
        }

        protected void btnAcept_Click(object sender, EventArgs e)
        {
            try
            {
                if (CLASS.cs_usuario.autorizado)
                {
                    txtMontoARecalcular.Visible = true;
                    btnRecalcularValorPrestamoTotal.Visible = true;
                    CLASS.cs_usuario.autorizado = false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnRedondear_Click(object sender, EventArgs e)
        {
            recalculoRedondeoPrestamo();
        }
        #endregion

        protected void btnProyeccion_Click(object sender, EventArgs e)
        {

        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipo.SelectedValue.ToString().Equals("1"))
                {
                    ddlTipoPrestamo.SelectedValue = "1";
                }
                else if (ddlTipo.SelectedValue.ToString().Equals("2"))
                {
                    ddlTipoPrestamo.SelectedValue = "3";
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
    }
}