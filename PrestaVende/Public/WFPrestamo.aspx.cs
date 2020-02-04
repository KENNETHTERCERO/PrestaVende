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
                        dtTablaJoyas = new DataTable("tablaJoyas");
                        dtTablaArticulos = new DataTable("tablaArticulos");
                        setColumnsJewelry();
                        setColumnsDifferentJewelry();
                        Session["CurrentTableJoyas"] = dtTablaJoyas;
                        Session["CurrentTableArticulos"] = dtTablaArticulos;
                        getClient();
                        getCategorias();
                        getPlanPrestamo();
                        getInteres();
                        getKilataje();
                        getMarca();
                        hideAllControls();
                        this.txtPesoDescuento.Text = "0";
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
                this.lblNumeroPrestamoNumero.Text = cs_prestamo.getMaxNumeroPrestamo(ref error);
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
                this.ddlMarca.DataSource = cs_marca.getMarca(ref error);
                this.ddlMarca.DataValueField = "id_marca";
                this.ddlMarca.DataTextField = "marca";
                this.ddlMarca.DataBind();
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
                this.ddlKilataje.DataSource = cs_kilataje.getKilataje(ref error);
                this.ddlKilataje.DataValueField = "id_kilataje";
                this.ddlKilataje.DataTextField = "kilataje";
                this.ddlKilataje.DataBind();
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
                this.ddlIntereses.DataSource = cs_interes.getInteres(ref error);
                this.ddlIntereses.DataValueField = "id_interes";
                this.ddlIntereses.DataTextField = "interes";
                this.ddlIntereses.DataBind();
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
                this.ddlTipoPrestamo.DataSource = cs_plan_prestamo.getPlanPrestamo(ref error);
                this.ddlTipoPrestamo.DataValueField = "id_plan_prestamo";
                this.ddlTipoPrestamo.DataTextField = "plan_prestamo";
                this.ddlTipoPrestamo.DataBind();
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
                this.ddlCategoria.DataSource = cs_categoria.getCategoriaComboBox(ref error);
                this.ddlCategoria.DataValueField = "id_categoria";
                this.ddlCategoria.DataTextField = "categoria";
                this.ddlCategoria.DataBind();
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
                this.ddlCasilla.DataSource = cs_casilla.getCasillas(ref error, this.ddlCategoria.SelectedValue.ToString());
                this.ddlCasilla.DataValueField = "id_casilla";
                this.ddlCasilla.DataTextField = "casilla";
                this.ddlCasilla.DataBind();
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
                this.ddlTipo.DataSource = cs_prestamo.getTipo(ref error);
                this.ddlTipo.DataValueField = "id_tipo";
                this.ddlTipo.DataTextField = "opcion";
                this.ddlTipo.DataBind();
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
                this.ddlSubCategoria.DataSource = cs_subcategoria.getSubCategoria(ref error, id_categoria);
                this.ddlSubCategoria.DataValueField = "id_sub_categoria";
                this.ddlSubCategoria.DataTextField = "sub_categoria";
                this.ddlSubCategoria.DataBind();
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
                this.ddlProducto.DataSource = cs_producto.getProducto(ref error, id_subcategoria);
                this.ddlProducto.DataValueField = "id_producto";
                this.ddlProducto.DataTextField = "producto";
                this.ddlProducto.DataBind();
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
                cs_cliente = new CLASS.cs_cliente();
                foreach (DataRow item in cs_cliente.getSpecificClient(ref error, id_cliente).Rows)
                {
                    this.lblnombre_cliente.Text = item[3].ToString() + " " + item[4].ToString() + " " + item[5].ToString() + " " + item[6].ToString();
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
                this.lblPeso.Visible = true;
                this.lblKilataje.Visible = true;
                this.lblObservaciones.Visible = true;
                this.lblPesoDescuento.Visible = true;
                this.lblPesoConDescuento.Visible = true;
                this.txtPeso.Visible = true;
                this.ddlKilataje.Visible = true;
                this.txtObservaciones.Visible = true;
                this.txtPesoDescuento.Visible = true;
                this.txtPesoConDescuento.Visible = true;
                this.txtValor.Enabled = false;
                this.lblRedondeo.Visible = true;
                this.txtRedondeo.Visible = true;
                this.btnRedondear.Visible = true;
                //---------------------------------
                //Ocultar controles de productos diferentes a joyas
                this.lblMarca.Visible = false;
                this.lblCaracteristicas.Visible = false;
                this.ddlMarca.Visible = false;
                this.txtCaracteristicas.Visible = false;
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
                this.lblPeso.Visible = false;
                this.lblKilataje.Visible = false;
                this.lblObservaciones.Visible = false;
                this.lblPesoDescuento.Visible = false;
                this.lblPesoConDescuento.Visible = false;
                this.txtPeso.Visible = false;
                this.ddlKilataje.Visible = false;
                this.txtObservaciones.Visible = false;
                this.txtPesoDescuento.Visible = false;
                this.txtPesoConDescuento.Visible = false;
                this.lblRedondeo.Visible = false;
                this.txtRedondeo.Visible = false;
                this.btnRedondear.Visible = false;
                //---------------------------------
                //Ocultar controles de productos diferentes a joyas
                this.lblMarca.Visible = true;
                this.lblCaracteristicas.Visible = true;
                this.ddlMarca.Visible = true;
                this.txtCaracteristicas.Visible = true;
                this.txtValor.Enabled = true;
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
                this.lblPeso.Visible = false;
                this.lblKilataje.Visible = false;
                this.lblObservaciones.Visible = false;
                this.lblPesoDescuento.Visible = false;
                this.lblPesoConDescuento.Visible = false;
                this.txtPeso.Visible = false;
                this.ddlKilataje.Visible = false;
                this.txtObservaciones.Visible = false;
                this.txtPesoDescuento.Visible = false;
                this.txtPesoConDescuento.Visible = false;

                //---------------------------------
                //Ocultar controles de productos diferentes a joyas
                this.lblMarca.Visible = false;
                this.lblCaracteristicas.Visible = false;
                this.ddlMarca.Visible = false;
                this.txtCaracteristicas.Visible = false;
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
                gramo = Convert.ToDecimal(this.txtPesoConDescuento.Text.ToString());
                foreach (DataRow item in cs_kilataje.getKilatajeByID(ref error, this.ddlKilataje.SelectedValue.ToString()).Rows)
                {
                    precio_por_gramo = Convert.ToDecimal(item[2].ToString());
                }
                total_precio_por_peso = gramo * precio_por_gramo;
                this.txtValor.Text = total_precio_por_peso.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private decimal getPrecioProducto(string peso, string id_kilataje)
        {
            try
            {
                decimal precio_por_gramo = 0, total_precio_por_peso = 0, gramo = 0;
                gramo = Convert.ToDecimal(peso);
                foreach (DataRow item in cs_kilataje.getKilatajeByID(ref error, id_kilataje).Rows)
                {
                    precio_por_gramo = Convert.ToDecimal(item["precio_kilataje"].ToString());
                }
                total_precio_por_peso = gramo * precio_por_gramo;
                return total_precio_por_peso;
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
                if (Session["CurrentTableJoyas"] != null)
                {
                    DataTable test = (DataTable)this.Session["CurrentTableJoyas"];
                    row = test.NewRow();
                    row["id_producto"] = this.ddlProducto.SelectedValue;
                    row["numero_linea"] = 1;
                    row["joya"] = this.ddlProducto.SelectedItem.Text.ToString();
                    row["kilataje"] = this.ddlKilataje.SelectedItem.Text.ToString();
                    row["peso"] = this.txtPeso.Text;
                    row["descuento"] = this.txtPesoDescuento.Text;
                    row["pesoReal"] = this.txtPesoConDescuento.Text.ToString();
                    row["valor"] = this.txtValor.Text.ToString();

                    if (this.txtObservaciones.Text.ToString().Length > 0 || this.txtObservaciones.Text.ToString() != "")
                    {
                        row["caracteristicas"] = this.txtObservaciones.Text.ToString();
                    }
                    else
                        row["caracteristicas"] = "N/A";

                    row["id_kilataje"] = this.ddlKilataje.SelectedValue.ToString();
                    test.Rows.Add(row);

                    dtTablaJoyas = test;
                    Session["CurrentTableJoyas"] = test;

                    this.gvProductoJoya.DataSource = dtTablaJoyas;
                    this.gvProductoJoya.DataBind();
                }
                else
                {
                    DataTable test = (DataTable)this.Session["CurrentTableJoyas"];
                    DataRow drCurrectRow = null;
                    drCurrectRow = dtTablaJoyas.NewRow();
                    drCurrectRow["id_producto"] = this.ddlProducto.SelectedValue;
                    drCurrectRow["numero_linea"] = 1;
                    drCurrectRow["joya"] = this.ddlProducto.SelectedItem.Text.ToString();
                    drCurrectRow["kilataje"] = this.ddlKilataje.SelectedItem.Text.ToString();
                    drCurrectRow["peso"] = this.txtPeso.Text;
                    drCurrectRow["descuento"] = this.txtPesoDescuento.Text;
                    drCurrectRow["pesoReal"] = this.txtPesoConDescuento.Text.ToString();
                    drCurrectRow["valor"] = this.txtValor.Text.ToString();
                    if (this.txtObservaciones.Text.ToString().Length > 0 || this.txtObservaciones.Text.ToString() != "")
                    {
                        drCurrectRow["caracteristicas"] = this.txtObservaciones.Text.ToString();
                    }
                    else
                        drCurrectRow["caracteristicas"] = "N/A";

                    row["id_kilataje"] = this.ddlKilataje.SelectedValue.ToString();
                    test.Rows.Add(row);

                    dtTablaJoyas = test;
                    Session["CurrentTableJoyas"] = test;

                    this.gvProductoJoya.DataSource = dtTablaJoyas;
                    this.gvProductoJoya.DataBind();
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
                if (ViewState["CurrentTableArticulos"] != null)
                {
                    DataTable testArticulos = (DataTable)this.Session["CurrentTableArticulos"];
                    row = testArticulos.NewRow();
                    row["id_producto"] = this.ddlProducto.SelectedValue.ToString();
                    row["numero_linea"] = 1;
                    row["producto"] = this.ddlProducto.SelectedItem.Text.ToString();
                    row["marca"] = this.ddlMarca.SelectedItem.Text.ToString();
                    row["valor"] = this.txtValor.Text;
                    row["caracteristicas"] = this.txtCaracteristicas.Text;
                    row["id_marca"] = this.ddlMarca.SelectedValue.ToString();

                    testArticulos.Rows.Add(row);
                    dtTablaArticulos = testArticulos;
                    Session["CurrentTableArticulos"] = testArticulos;

                    gvProductoElectrodomesticos.DataSource = dtTablaArticulos;
                    gvProductoElectrodomesticos.DataBind();
                }
                else
                {
                    DataTable testArticulos = (DataTable)this.Session["CurrentTableArticulos"];
                    DataRow drCurrectRow = null;

                    drCurrectRow = testArticulos.NewRow();
                    drCurrectRow["id_producto"] = this.ddlProducto.SelectedValue.ToString();
                    drCurrectRow["numero_linea"] = 1;
                    drCurrectRow["producto"] = this.ddlProducto.SelectedItem.Text.ToString();
                    drCurrectRow["marca"] = this.ddlMarca.SelectedItem.Text.ToString();
                    drCurrectRow["valor"] = this.txtValor.Text;
                    drCurrectRow["caracteristicas"] = this.txtCaracteristicas.Text;
                    drCurrectRow["id_marca"] = this.ddlMarca.SelectedValue.ToString();

                    testArticulos.Rows.Add(drCurrectRow);
                    dtTablaArticulos = testArticulos;
                    Session["CurrentTableArticulos"] = testArticulos;

                    this.gvProductoElectrodomesticos.DataSource = dtTablaArticulos;
                    this.gvProductoElectrodomesticos.DataBind();
                }
                calculaTotalPrestamo();
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
                getDataProyeccion();
                if (dtTablaJoyas.Rows.Count > 1)
                {
                    decimal totalPrestamo = 0;
                    foreach (DataRow item in dtTablaJoyas.Rows)
                    {
                        item["numero_linea"] = linea;
                        linea++;
                        totalPrestamo += Convert.ToDecimal(item["valor"].ToString());
                    }
                    this.lblTotalPrestamoQuetzales.Text = Math.Round(totalPrestamo, 2, MidpointRounding.AwayFromZero).ToString();
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
                    this.lblTotalPrestamoQuetzales.Text = Math.Round(totalPrestamo, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    this.lblTotalPrestamoQuetzales.Text = txtValor.Text;
                }
                getDataProyeccion();
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
                if (this.ddlSubCategoria.SelectedValue.ToString().Equals("0")) { showWarning("Uste debe seleccionar una subcategoria para poder agregar articulo."); return false; }
                else if (this.ddlProducto.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar un producto antes de agregar articulo."); return false; }
                else if (this.txtPeso.Text.ToString().Equals("0") || this.txtPeso.Text.ToString().Equals("0.00") || this.txtPeso.Text.ToString().Length == 0) { showWarning("Usted debe ingresar el peso de la joya para poder agregar."); return false; }
                else if (this.txtPesoDescuento.Text.ToString().Length == 0) { showWarning("Debe agregar 0 si no hay descuento de peso que hacerle a la joya."); return false; }
                else if (this.ddlKilataje.SelectedValue.ToString().Equals("0")) { showWarning("Debe seleccionar el kilataje que corresponde a la Joya que esta agregando."); return false; }
                else if (this.txtValor.Text.ToString().Equals("0") || this.txtValor.Text.ToString().Equals("0.00") || this.txtValor.Text.ToString().Length == 0) { showWarning("Debe agregar el valor que se dara como prestamo para esa joya."); return false; }
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
                if (this.ddlSubCategoria.SelectedValue.ToString().Equals("0")) { showWarning("Uste debe seleccionar una subcategoria para poder agregar articulo."); return false; }
                else if (this.ddlProducto.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar un producto antes de agregar articulo."); return false; }
                else if (this.ddlMarca.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar una marca para poder agregar."); return false; }
                //else if (txtPesoDescuento.Text.ToString().Length == 0) { showWarning("Debe agregar 0 si no hay descuento de peso que hacerle a la joya."); return false; }
                //else if (ddlKilataje.SelectedValue.ToString().Equals("0")) { showWarning("Debe seleccionar el kilataje que corresponde a la Joya que esta agregando."); return false; }
                else if (this.txtValor.Text.ToString().Equals("0") || this.txtValor.Text.ToString().Equals("0.00") || this.txtValor.Text.ToString().Length == 0) { showWarning("Debe agregar el valor que se dara como prestamo para esa joya."); return false; }
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
                if (this.gvProductoJoya.Rows.Count == 0 && this.gvProductoElectrodomesticos.Rows.Count == 0)
                {
                    this.ddlCategoria.Enabled = true;
                }
                else
                {
                    this.ddlCategoria.Enabled = false;
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
                if (this.ddlCategoria.SelectedValue.ToString().Equals("1"))
                {
                    this.ddlSubCategoria.SelectedValue = "0";
                    this.ddlProducto.SelectedValue = "0";
                    this.txtPeso.Text = "";
                    this.txtPesoDescuento.Text = "";
                    this.txtPesoConDescuento.Text = "";
                    this.ddlKilataje.SelectedValue = "0";
                    this.txtObservaciones.Text = "";
                    this.txtValor.Text = "";
                }
                else
                {
                    this.ddlSubCategoria.SelectedValue = "0";
                    this.ddlProducto.SelectedValue = "0";
                    this.ddlMarca.SelectedValue = "0";
                    this.txtCaracteristicas.Text = "";
                    this.txtValor.Text = "";
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
                if (this.ddlTipoPrestamo.SelectedValue.ToString().Equals("0")) { showWarning("Debe seleccionar que tipo de prestamo necesita guardar."); return false; }
                else if (this.ddlCasilla.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar una casilla para poder guardar."); return false; }
                else if (this.ddlIntereses.SelectedValue.ToString().Equals("0")) { showWarning("Usted debe seleccionar que intereses necesita para el prestamo para poder guardar."); return false; }
                else if (this.gvProductoElectrodomesticos.Rows.Count.ToString().Equals("0") && this.gvProductoJoya.Rows.Count.ToString().Equals("0")) { showWarning("Usted debe agregar por lo menos un producto para poder guardar."); return false; }
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
                if (this.ddlCategoria.SelectedValue.ToString().Equals("1"))
                {
                    guardarPrestamoJoya();
                    string scriptText = "alert('my message'); window.location='WFListadoPrestamo.aspx?id_cliente=" + this.lblid_cliente.Text + "'";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
                }
                else
                {
                    guardarPrestamoDifferentJoya();
                    string scriptText = "alert('my message'); window.location='WFListadoPrestamo.aspx?id_cliente=" + this.lblid_cliente.Text + "'";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
                }
                    
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
                cs_prestamo = new CLASS.cs_prestamo();
                
                if (cs_prestamo.guardar_prestamo(ref error, generaEncabezado(), (DataTable)this.Session["CurrentTableJoyas"], this.ddlCategoria.SelectedValue.ToString(), ref numero_prestamo_guardado))
                {
                    this.lblNumeroPrestamoNumero.Text = numero_prestamo_guardado;
                    showSuccess("Se creo prestamo correctamente.");

                    string script = "window.open('WebReport.aspx?tipo_reporte=1" + "&numero_prestamo=" + numero_prestamo_guardado + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "OpenContrato", script, true);

                    Session["CurrentTableJoyas"] = null;

                    string prueba = numero_prestamo_guardado;
                    string scriptEstadoCuenta = "window.open('WebReport.aspx?tipo_reporte=3" + "&numero_prestamo=" + prueba + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "OpenEstadoCuenta", scriptEstadoCuenta, true);

                    Session["CurrentTableJoyas"] = null;

                    string segunda = prueba;
                    string scriptEtiqueta = "window.open('WebReport.aspx?tipo_reporte=4" + "&numero_prestamo=" + segunda + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "OpenEtiqueta", scriptEtiqueta, true);
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
                cs_prestamo = new CLASS.cs_prestamo();
                if (cs_prestamo.guardar_prestamo(ref error, generaEncabezado(), (DataTable)this.Session["CurrentTableArticulos"], this.ddlCategoria.SelectedValue.ToString(), ref numero_prestamo_guardado))
                {
                    this.lblNumeroPrestamoNumero.Text = numero_prestamo_guardado;
                    showSuccess("Se creo prestamo correctamente.");
                    string script = "window.open('WebReport.aspx?tipo_reporte=1" + "&numero_prestamo=" + this.lblNumeroPrestamoNumero.Text + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "OpenContrato", script, true);

                    Session["CurrentTableArticulos"] = null;

                    string scriptEstadoCuenta = "window.open('WebReport.aspx?tipo_reporte=3" + "&numero_prestamo=" + this.lblNumeroPrestamoNumero.Text + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "OpenEstadoCuenta", scriptEstadoCuenta, true);

                    Session["CurrentTableArticulos"] = null;

                    string scriptEtiqueta = "window.open('WebReport.aspx?tipo_reporte=4" + "&numero_prestamo=" + this.lblNumeroPrestamoNumero.Text + "');";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "OpenEtiqueta", scriptEtiqueta, true);

                    Session["CurrentTableArticulos"] = null;
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
                if (this.txtMontoARecalcular.Visible)
                {
                    montoOriginal = Convert.ToDecimal(this.lblTotalPrestamoQuetzales.Text);
                    nuevoMonto = Convert.ToDecimal(this.txtMontoARecalcular.Text);
                }
                else
                {
                    if (txtRedondeo.Text.ToString().Length <= 0)
                    {
                        montoOriginal = Convert.ToDecimal(this.lblTotalPrestamoQuetzales.Text);
                        nuevoMonto = Convert.ToDecimal(this.lblTotalPrestamoQuetzales.Text);
                    }
                    else
                    {
                        montoOriginal = Convert.ToDecimal(this.lblTotalPrestamoQuetzales.Text) - Convert.ToDecimal(this.txtRedondeo.Text.ToString());
                        nuevoMonto = Convert.ToDecimal(this.lblTotalPrestamoQuetzales.Text);
                    }
                }
                encabezado[0] = this.lblid_cliente.Text.ToString();
                encabezado[1] = nuevoMonto.ToString();
                encabezado[2] = "1";
                encabezado[3] = getFechaProximoPago().ToString();
                encabezado[4] = this.lblTotalPrestamoQuetzales.Text;
                encabezado[5] = Session["usuario"].ToString();
                encabezado[6] = this.ddlTipoPrestamo.SelectedValue.ToString();
                encabezado[7] = this.ddlIntereses.SelectedValue.ToString();
                encabezado[8] = this.ddlCasilla.SelectedValue.ToString();
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
                if (Convert.ToInt32(Session["id_caja"]) == 0)
                {
                    showWarning("Usted no tiene caja asignada.");
                    return false;
                }
                else if (this.txtMontoARecalcular.Visible && Convert.ToDecimal(Session["saldo_caja"]) < Convert.ToDecimal(this.txtMontoARecalcular.Text.ToString()))
                {
                    showWarning("El saldo de su caja es menor al monto del prestamo que quiere emitir, solicite un incremento de capital.");
                    return false;
                }
                else if (!this.txtMontoARecalcular.Visible && Convert.ToDecimal(Session["saldo_caja"]) < Convert.ToDecimal(this.lblTotalPrestamoQuetzales.Text.ToString()))
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
                if (this.gvProductoElectrodomesticos.Rows.Count > 0)
                {
                    DataTable dtRecalculoDatos = new DataTable("dtRecalculo");
                    dtRecalculoDatos = (DataTable)this.Session["CurrentTableArticulos"];
                    foreach (DataRow item in dtRecalculoDatos.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item["valor"].ToString()) / Convert.ToDecimal(this.lblTotalPrestamoQuetzales.Text.ToString()), 4);
                        montoFila = porcentaje * Convert.ToDecimal(this.txtMontoARecalcular.Text.ToString());
                        montoFila = Math.Round(montoFila, 2);
                        item["valor"] = montoFila.ToString();
                    }
                    Session["CurrentTableArticulos"] = dtRecalculoDatos;
                    dtTablaArticulos = dtRecalculoDatos;
                    this.gvProductoElectrodomesticos.DataSource = dtTablaArticulos;
                    this.gvProductoElectrodomesticos.DataBind();
                }
                else
                {
                    DataTable dtRecalculoDatos = new DataTable("dtRecalculo");
                    dtRecalculoDatos = (DataTable)this.Session["CurrentTableJoyas"];
                    foreach (DataRow item in dtRecalculoDatos.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item["valor"].ToString()) / Convert.ToDecimal(this.lblTotalPrestamoQuetzales.Text.ToString()), 4);
                        montoFila = porcentaje * Convert.ToDecimal(this.txtMontoARecalcular.Text.ToString());
                        montoFila = Math.Round(montoFila, 2);
                        item["valor"] = montoFila.ToString();
                    }
                    Session["CurrentTableJoyas"] = dtRecalculoDatos;
                    dtTablaJoyas = dtRecalculoDatos;
                    this.gvProductoJoya.DataSource = dtTablaJoyas;
                    this.gvProductoJoya.DataBind();
                }
                getDataProyeccion();
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
                if (gvProductoElectrodomesticos.Rows.Count > 0 && ddlCategoria.SelectedValue.ToString() != "1")
                {
                    foreach (GridViewRow item in gvProductoElectrodomesticos.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item.Cells[5].Text.ToString()) / Convert.ToDecimal(lblTotalPrestamoQuetzales.Text.ToString()), 4);
                        montoRedondeo = porcentaje * Convert.ToDecimal(txtRedondeo.Text.ToString());
                        montoPorFilaConRedondeo = Convert.ToDecimal(item.Cells[5].Text.ToString()) + Math.Round(montoRedondeo, 2);
                        item.Cells[5].Text = montoPorFilaConRedondeo.ToString();
                    }

                    foreach (DataRow item in dtTablaArticulos.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item["valor"].ToString()) / Convert.ToDecimal(lblTotalPrestamoQuetzales.Text.ToString()), 4);
                        montoRedondeo = porcentaje * Convert.ToDecimal(txtRedondeo.Text.ToString());
                        montoPorFilaConRedondeo = Convert.ToDecimal(item["valor"].ToString()) + Math.Round(montoRedondeo, 2);
                        item["valor"] = montoPorFilaConRedondeo.ToString();
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

                    foreach (DataRow item in dtTablaJoyas.Rows)
                    {
                        porcentaje = 0;
                        porcentaje = Math.Round(Convert.ToDecimal(item["valor"].ToString()) / Convert.ToDecimal(lblTotalPrestamoQuetzales.Text.ToString()), 4);
                        montoRedondeo = porcentaje * Convert.ToDecimal(txtRedondeo.Text.ToString());
                        montoPorFilaConRedondeo = Convert.ToDecimal(item["valor"].ToString()) + Math.Round(montoRedondeo, 2);
                        item["valor"] = montoPorFilaConRedondeo.ToString();
                    }
                }
                sumaTotalPrestamo = Convert.ToDecimal(lblTotalPrestamoQuetzales.Text.ToString()) + Convert.ToDecimal(txtRedondeo.Text.ToString());
                lblTotalPrestamoQuetzales.Text = sumaTotalPrestamo.ToString();

                getDataProyeccion();
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
                if (this.txtValor.Text.ToString().Length <= 0 || this.txtValor.Text.ToString().Equals("0"))
                {
                    showWarning("Debe seleccionar un producto, agregar peso y descuento, seleccionar kilataje antes de realizar redondeo.");
                }
                else
                {
                    decimal txtValorAvaluo = 0, valorRedondeo = 0;
                    txtValorAvaluo = Convert.ToDecimal(this.txtValor.Text.ToString());
                    valorRedondeo = Convert.ToDecimal(this.txtRedondeo.Text.ToString());
                    this.txtValor.Text = Convert.ToString(txtValorAvaluo + valorRedondeo);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                throw;
            }
        }

        private void validaSeleccionTipoPlanPrestamo()
        {
            try
            {
                decimal totalPrestamo = 0;
                string id_TipoPlan = "";
                if (this.lblTotalPrestamoQuetzales.Text.ToString().Equals("") || this.lblTotalPrestamoQuetzales.Text.ToString().Equals("0"))
                    totalPrestamo = 0;
                else
                    totalPrestamo = Convert.ToDecimal(this.lblTotalPrestamoQuetzales.Text.ToString());

                if (this.ddlTipo.SelectedValue.ToString().Equals("1"))
                {
                    id_TipoPlan = cs_interes.getIdInteres(ref error, totalPrestamo.ToString());
                    this.ddlTipoPrestamo.SelectedValue = id_TipoPlan;
                }
                //ddlTipoPrestamo.Enabled = false;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void recalculaValorPorProducto()
        {
            try
            {
                //getPrecioProducto
                if (this.gvProductoElectrodomesticos.Rows.Count > 0)
                {
                    DataTable dtRecalculoDatos = new DataTable("dtRecalculo");
                    dtRecalculoDatos = (DataTable)this.Session["CurrentTableArticulos"];
                    foreach (DataRow item in dtRecalculoDatos.Rows)
                    {
                        item["valor"] = this.lblTotalPrestamoQuetzales.Text;
                    }
                    Session["CurrentTableArticulos"] = dtRecalculoDatos;
                    dtTablaArticulos = dtRecalculoDatos;
                    this.gvProductoElectrodomesticos.DataSource = dtTablaArticulos;
                    this.gvProductoElectrodomesticos.DataBind();
                }
                else
                {
                    DataTable dtRecalculoDatos = new DataTable("dtRecalculo");
                    dtRecalculoDatos = (DataTable)this.Session["CurrentTableJoyas"];
                    foreach (DataRow item in dtRecalculoDatos.Rows)
                    {
                        item["valor"] = getPrecioProducto(item["pesoReal"].ToString(), item["id_kilataje"].ToString()).ToString();
                    }
                    Session["CurrentTableJoyas"] = dtRecalculoDatos;
                    dtTablaJoyas = dtRecalculoDatos;
                    this.gvProductoJoya.DataSource = dtTablaJoyas;
                    this.gvProductoJoya.DataBind();
                }

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
                getSubCategorias(this.ddlCategoria.SelectedValue.ToString());
                HideOrShowControls(this.ddlCategoria.SelectedValue.ToString());
                if (this.ddlCategoria.SelectedValue.ToString().Equals("0"))
                {
                    hideAllControls();
                }
                this.ddlIntereses.SelectedValue = cs_producto.getIDInteresCategoria(ref error, this.ddlCategoria.SelectedValue);
                this.ddlIntereses.Enabled = false;
                getProductos("0");
                ddlSubCategoria.Focus();
                getCasillas();
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
                getProductos(this.ddlSubCategoria.SelectedValue.ToString());
                this.ddlProducto.Focus();
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
                if (this.txtPesoDescuento.Text.ToString().Length <= 0)
                {
                    this.txtPesoDescuento.Text = "0";
                }

                if (this.txtPeso.Text.ToString().Length <= 0)
                {
                    this.txtPeso.Text = "0";
                }

                this.txtPesoConDescuento.Text = Math.Round(Convert.ToDecimal(this.txtPeso.Text.ToString()) - Convert.ToDecimal(this.txtPesoDescuento.Text.ToString()), 2).ToString();
                this.txtPesoDescuento.Focus();
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
                if (this.txtPesoDescuento.ToString().Length <= 0)
                {
                    this.txtPesoDescuento.Text = "0";
                }

                if (this.txtPeso.ToString().Length <= 0)
                {
                    this.txtPeso.Text = "0";
                }

                this.txtPesoConDescuento.Text = Math.Round(Convert.ToDecimal(this.txtPeso.Text.ToString()) - Convert.ToDecimal(this.txtPesoDescuento.Text.ToString()), 2).ToString();
                this.ddlKilataje.Focus();
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
            this.txtObservaciones.Focus();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ddlCategoria.SelectedValue.ToString().Equals("1") || this.ddlCategoria.Text.ToString().Equals("JOYAS"))
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

        private void getDataProyeccion()
        {
            try
            {
                Session["id_interes_proyeccion"] = this.ddlIntereses.SelectedValue;
                if (txtMontoARecalcular.Visible)
                {
                    Session["monto_proyeccion"] = this.txtMontoARecalcular.Text;
                }
                else
                {
                    Session["monto_proyeccion"] = this.lblTotalPrestamoQuetzales.Text;
                }

                Session["id_plan_prestamo_proyeccion"] = this.ddlTipoPrestamo.SelectedValue;
                this.gvProyeccion.DataSource = cs_prestamo.getDTProyeccion(ref error);
                this.gvProyeccion.DataBind();
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
                    DataTable dtActual = (DataTable)this.Session["CurrentTableArticulos"];
                    dtActual.Rows[index].Delete();
                    dtTablaArticulos = dtActual;
                    Session["CurrentTableArticulos"] = dtActual;
                    this.gvProductoElectrodomesticos.DataSource = dtTablaArticulos;
                    this.gvProductoElectrodomesticos.DataBind();
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
                    DataTable dtActual = (DataTable)this.Session["CurrentTableJoyas"];
                    dtActual.Rows[index].Delete();
                    Session["CurrentTableJoyas"] = dtActual;
                    dtTablaJoyas = dtActual;
                    this.gvProductoJoya.DataSource = dtActual;
                    this.gvProductoJoya.DataBind();
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
            recalculaValorPorProducto();
            recalculoMontoPrestamo();
        }

        protected void btnAcept_Click(object sender, EventArgs e)
        {
            try
            {
                if (CLASS.cs_usuario.autorizado)
                {
                    this.txtMontoARecalcular.Visible = true;
                    this.btnRecalcularValorPrestamoTotal.Visible = true;
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
            getDataProyeccion();
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ddlTipo.SelectedValue.ToString().Equals("1"))
                {
                    if (Convert.ToDecimal(this.lblTotalPrestamoQuetzales.Text) <= 500)
                    {
                        this.ddlTipoPrestamo.SelectedValue = "1";
                    }
                    else
                    {
                        this.ddlTipoPrestamo.SelectedValue = "2";
                    }
                }
                else if (this.ddlTipo.SelectedValue.ToString().Equals("2"))
                {
                    this.ddlTipoPrestamo.SelectedValue = "3";
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
    }
}