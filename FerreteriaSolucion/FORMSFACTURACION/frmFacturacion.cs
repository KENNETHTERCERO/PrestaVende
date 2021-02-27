using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerreteriaSolucion.FORMSFACTURACION
{
    public partial class frmFacturacion : Form
    {
        #region variables
        private CLASES.cs_validate_number mValidateNumber = new CLASES.cs_validate_number();
        private CLASES.cs_serie mSerie = new CLASES.cs_serie();
        private CLASES.cs_cliente mCliente = new CLASES.cs_cliente();
        private CLASES.cs_producto mProducto = new CLASES.cs_producto();
        private CLASES.cs_factura mFactura = new CLASES.cs_factura();


        private static decimal precio_total_factura = 0;
        private static decimal precio_total_factura_con_descuento = 0;
        private static decimal cambioPrecioEnter = 0, cambioPrecioLeave = 0;
        private static string error = "";
        private static bool updateYesOrNo = false;

        private static DataTable tableDataProductSpecific = new DataTable("data");

        public event System.Windows.Forms.DataGridViewDataErrorEventHandler DataError;

        #endregion
        public frmFacturacion()
        {
            InitializeComponent();
            this.Text = "Facturacion";
            getSeries();
            getTipoCliente();
            SetupDataGridView();
            setUpCmbBusquedaProducto();
        }

        #region messages
        private void showSuccess(string message)
        {
            MessageBox.Show(message, "Bien hecho", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showSuccess(string message, string nombre_ventana)
        {
            MessageBox.Show(message, "Bien hecho", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showWarning(string _error)
        {
            MessageBox.Show(_error, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void showWarning(string _error, string nombre_ventana)
        {
            MessageBox.Show(_error, nombre_ventana, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void showError(string _error)
        {
            MessageBox.Show(_error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void showError(string _error, string nombreVentana)
        {
            MessageBox.Show(_error, nombreVentana, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        #endregion

        private void getSeries()
        {
            try
            {
                cmbSerie.DataSource = mSerie.getSeriesToBill(ref error);
                cmbSerie.DisplayMember = "serie";
                cmbSerie.ValueMember = "id_serie";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + error);
            }
        }

        private void getTipoCliente()
        {
            try
            {
                cmbTipoCliente.DataSource = mCliente.getTypeCustomer(ref error);
                cmbTipoCliente.DisplayMember = "descripcion";
                cmbTipoCliente.ValueMember = "id_tipo_cliente";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + error);
            }
        }

        private void cmbSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbSerie.SelectedValue.ToString().Equals("0"))
            {
                getNumberOfBill();
            }
            else
            {
                lblNoFacturaNumero.Text = "0";
                lblFacturasRestantesNumero.Text = "0";
            }
        }

        private void getNumberOfBill()
        {
            try
            {
                string numero_factura = "", facturas_restantes = "";
                numero_factura = mSerie.numberMaxBill(ref error, ref facturas_restantes, cmbSerie.SelectedValue.ToString());

                lblNoFacturaNumero.Text = numero_factura;
                lblFacturasRestantesNumero.Text = facturas_restantes;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getCliente()
        {
            try
            {
                if (txtNit.Text.ToString().Equals("C/F") || txtNit.Text.ToString().Equals(""))
                {
                    
                }
                else
                {
                    if (mCliente.getDataCliente(ref error, txtNit.Text.ToString()).Rows.Count > 0)
                    {
                        hidePanelBusquedaCliente(true);
                        showWarning($"El nit {txtNit.Text} de cliente ya existe, por favor verifique y realice la busqueda correspondiente.");
                        txtBusqueda.Focus();
                    }
                    else
                    {
                        hidePanelBusquedaCliente(false);
                        lblClienteNuevoNumero.Text = mCliente.getMaxIdCliente(ref error);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString() + error);
            }
        }

        private void hidePanelBusquedaCliente(bool condicion)
        {
            try
            {
                if (condicion != true)//Panel Nuevo cliente esta visible
                {
                    panelNuevoCliente.Visible = true;
                    panelBusquedaCliente.Visible = false;
                }
                else //panel busqueda cliente
                {
                    panelNuevoCliente.Visible = false;
                    panelBusquedaCliente.Visible = true;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void btnCancelarCliente_Click(object sender, EventArgs e)
        {
            hidePanelBusquedaCliente(true);
            cleanLabelDatosCliente("limpiarCreacionCliente");
            cleanLabelDatosCliente("limpiarBusquedaCliente");
        }

        private void cleanLabelDatosCliente(string opcion)
        {
            try
            {
                if (opcion.Equals("limpiarBusquedaCliente"))
                {
                    //txtNit.Text = "";
                    lblNombreCliente.Text = "CLIENTE";
                    lblIdClienteNumero.Text = "0";
                    lblDireccionSeleccionadoDescripcion.Text = "";
                }
                else if (opcion.Equals("limpiarCreacionCliente"))
                {
                    txtPrimeroNombre.Text = "";
                    txtSegundoNombre.Text = "";
                    txtPrimeroApellido.Text = "";
                    txtSegundoApellido.Text = "";
                    txtDireccion.Text = "";
                    cmbTipoCliente.SelectedValue = "0";
                    lblClienteNuevoNumero.Text = "0";
                    txtNit.Text = "";
                }

            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateInformation())
                {
                    if (updateYesOrNo == true)
                    {
                        if (updateCliente())
                        {
                            seleccionDatosCliente();
                            cleanLabelDatosCliente("limpiarCreacionCliente");
                            hidePanelBusquedaCliente(true);
                        }
                    }
                    else
                    {
                        if (insertCliente())
                        {
                            seleccionDatosCliente();
                            cleanLabelDatosCliente("limpiarCreacionCliente");
                            hidePanelBusquedaCliente(true);
                        }
                    }
                    txtCodigoProducto.Focus();
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private bool validateInformation()
        {
            try
            {
                if (txtPrimeroNombre.Text.ToString().Equals("")) { showWarning("Debe agregar el primer nombre para poder guardar un nuevo cliente.", "Importante"); return false; }
                else if (txtPrimeroNombre.Text.ToString().Length < 3) { showWarning("Debe agregar un primer nombre para poder guardar un nuevo cliente.", "Importante"); return false; }
                else if (txtPrimeroApellido.Text.ToString().Equals("")) { showWarning("Debe agregar el primer apellido para poder guardar un nuevo cliente.", "Importante"); return false; }
                else if (txtPrimeroApellido.Text.ToString().Length < 3) { showWarning("Debe agregar un primer nombre para poder guardar un nuevo cliente.", "Importante"); return false; }
                else if (txtDireccion.Text.ToString().Equals("")) { showWarning("Debe agregar direccion para poder guardar un nuevo cliente.", "Importante"); return false; }
                else if (txtDireccion.Text.ToString().Length < 6) { showWarning("Debe agregar el primer nombre para poder guardar un nuevo cliente.", "Importante"); return false; }
                else if (cmbTipoCliente.SelectedValue.ToString().Equals("0")) { showWarning("Debe seleccionar que tipo de cliente.", "Importante"); return false; }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void seleccionDatosCliente()
        {
            try
            {
                lblNombreCliente.Text = txtPrimeroNombre.Text + " " + txtSegundoNombre.Text + " " + txtPrimeroApellido.Text + " " + txtSegundoApellido.Text;
                lblDireccionSeleccionadoDescripcion.Text = txtDireccion.Text;
                lblIdClienteNumero.Text = lblClienteNuevoNumero.Text;
                lblNitSeleccionado.Text = txtNit.Text;
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private bool updateCliente()
        {
            try
            {
                if (mCliente.updateCliente(ref error, txtPrimeroNombre.Text, txtSegundoNombre.Text, txtPrimeroApellido.Text, txtSegundoApellido.Text, txtDireccion.Text, lblIdClienteNumero.Text, cmbTipoCliente.SelectedValue.ToString()))
                {
                    showSuccess("El cliente ha sido actualizado correctamente");
                    return true;
                }
                else
                {
                    showWarning("No se pudo actualizar cliente, intente de nuevo.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private bool insertCliente()
        {
            try
            {
                if (mCliente.insertCliente(ref error, txtPrimeroNombre.Text, txtSegundoNombre.Text, txtPrimeroApellido.Text, txtSegundoApellido.Text, txtDireccion.Text, txtNit.Text, cmbTipoCliente.SelectedValue.ToString()))
                {
                    showSuccess("El cliente ha sido agregado correctamente");
                    return true;
                }
                else
                {
                    showWarning("No se pudo agragar cliente nuevo, intentelo de nuevo.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtBusqueda.Text.ToString().Equals("NUEVO"))
                    {
                        hidePanelBusquedaCliente(false);
                        clienteNuevoCF();
                    }
                    else
                    {
                        getEspecificClient();
                    }
                }
                else
                {
                    mValidateNumber.upperCaseCharacterPress(e);
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void clienteNuevoCF()
        {
            try
            {
                lblClienteNuevoNumero.Text = mCliente.getMaxIdCliente(ref error);
                lblNitSeleccionado.Text = "";
                string value = txtBusqueda.Text.ToString();
                char delimiter = ' ';
                if (txtBusqueda.Text.ToString().Equals(""))
                {
                    txtNit.Focus();
                }
                else
                {
                    string[] substrings = value.Split(delimiter);
                    if (substrings.Count() == 1)
                    {
                        txtPrimeroNombre.Text = substrings[0].Replace(" ", "");
                        txtNit.Focus();
                    }
                    else if (substrings.Count() == 2)
                    {
                        txtPrimeroNombre.Text = substrings[0].Replace(" ", "");
                        txtPrimeroApellido.Text = substrings[1].Replace(" ", "");
                        txtNit.Focus();
                    }
                    else if (substrings.Count() == 3)
                    {
                        txtPrimeroNombre.Text = substrings[0].Replace(" ", "");
                        txtSegundoNombre.Text = substrings[1].Replace(" ", "");
                        txtPrimeroApellido.Text = substrings[2].Replace(" ", "");
                        txtNit.Focus();
                    }
                    else if (substrings.Count() == 4)
                    {
                        txtPrimeroNombre.Text = substrings[0].Replace(" ", "");
                        txtSegundoNombre.Text = substrings[1].Replace(" ", "");
                        txtPrimeroApellido.Text = substrings[2].Replace(" ", "");
                        txtPrimeroApellido.Text = substrings[3].Replace(" ", "");
                        txtNit.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void getEspecificClient()
        {
            try
            {
                DataTable paraComboBox = new DataTable("paraCombo");
                string condicion = String.Format("CAST(prov.id_cliente AS varchar(50)) like '%{0}%' " +
                                                      "OR LTRIM(RTRIM(prov.primer_nombre)) + ' ' + LTRIM(RTRIM(prov.primer_apellido)) LIKE '%{0}%' " +
                                                      "OR LTRIM(RTRIM(prov.primer_nombre)) + ' ' + LTRIM(RTRIM(prov.segundo_apellido)) LIKE '%{0}%' " +
                                                      "OR LTRIM(RTRIM(prov.segundo_nombre)) + ' ' + LTRIM(RTRIM(prov.primer_apellido)) LIKE '%{0}%' " +
                                                      "OR LTRIM(RTRIM(prov.segundo_nombre)) + ' ' + LTRIM(RTRIM(prov.segundo_apellido)) LIKE '%{0}%' " +
                                                      "OR LTRIM(RTRIM(prov.primer_nombre)) + ' ' + LTRIM(RTRIM(prov.segundo_nombre)) + ' ' + LTRIM(RTRIM(prov.primer_apellido)) + ' ' + LTRIM(RTRIM(prov.segundo_apellido)) LIKE '%{0}%' " +
                                                      "OR prov.nit like '%{0}%' " +
                                                      "OR prov.DIRECCION LIKE '%{0}%'", txtBusqueda.Text.Trim());
                DataTable recibeTable = new DataTable("recepcion");
                recibeTable = mCliente.findClient(ref error, condicion, ref paraComboBox);

                if (recibeTable.Rows.Count > 1)
                {
                    cmbBusquedaCliente.DataSource = paraComboBox;
                    cmbBusquedaCliente.DisplayMember = "nombre";
                    cmbBusquedaCliente.ValueMember = "id_cliente";
                    cmbBusquedaCliente.SelectedValue = "0";
                }
                else if (recibeTable.Rows.Count == 1)
                {
                    setClient(recibeTable);
                    cmbBusquedaCliente.DataSource = paraComboBox;
                    cmbBusquedaCliente.DisplayMember = "nombre";
                    cmbBusquedaCliente.ValueMember = "id_cliente";
                    cmbBusquedaCliente.SelectedValue = "0";
                }
                else
                {
                    hidePanelBusquedaCliente(false);
                    clienteNuevoCF();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void setClient(DataTable recibeTable)
        {
            try
            {
                string id_cliente = "0";
                foreach (DataRow row in recibeTable.Rows)
                {
                    lblIdClienteNumero.Text = row[0].ToString();
                    id_cliente = row[0].ToString();
                    lblNombreCliente.Text = row[2].ToString();
                    lblDireccionSeleccionadoDescripcion.Text = row[3].ToString();
                    lblNitSeleccionado.Text = row[1].ToString();
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("System.NullReferenceException: Referencia a objeto no establecida como instancia de un objeto."))
                {

                }
                else
                {
                    showWarning(ex.ToString());
                }
            }
        }

        private void txtNit_Leave(object sender, EventArgs e)
        {
            try
            {
                getCliente();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void txtPrimeroNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            mValidateNumber.upperCaseCharacterPress(e);
        }

        private void txtSegundoNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            mValidateNumber.upperCaseCharacterPress(e);
        }

        private void txtPrimeroApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            mValidateNumber.upperCaseCharacterPress(e);
        }

        private void txtBusqueda_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtBusqueda.Text.ToString().Equals(""))
                {
                    if (txtBusqueda.Text.ToString().Equals("NUEVO"))
                    {
                        hidePanelBusquedaCliente(false);
                        clienteNuevoCF();
                    }
                    else
                    {
                        getEspecificClient();
                    }
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!lblIdClienteNumero.Text.ToString().Equals("0"))
                {
                    txtCodigoProducto.Focus();
                    updateYesOrNo = true;
                    getClientWithID(lblIdClienteNumero.Text.ToString());
                    hidePanelBusquedaCliente(false);
                }
            }
            catch(Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void getClientWithID(string _id_cliente)
        {
            DataTable recibeTable = mCliente.getOneClientForID(ref error, _id_cliente);
            string id_cliente = "0";
            if (recibeTable.Rows.Count > 0)
            {
                foreach (DataRow row in recibeTable.Rows)
                {
                    lblClienteNuevoNumero.Text = row[0].ToString().Replace(" ", "");
                    txtPrimeroNombre.Text = row[1].ToString().Replace(" ", "");
                    txtSegundoNombre.Text = row[2].ToString().Replace(" ", "");
                    txtPrimeroApellido.Text = row[3].ToString().Replace(" ", "");
                    txtSegundoApellido.Text = row[4].ToString().Replace(" ", "");
                    txtDireccion.Text = row[5].ToString().Replace(" ", " ");
                    cmbTipoCliente.SelectedValue = row[6].ToString();
                    txtNit.Text = row[7].ToString();
                }
            }
            else
            {
                if (id_cliente.Equals("0"))
                {
                    hidePanelBusquedaCliente(false);
                    lblClienteNuevoNumero.Text = mCliente.getMaxIdCliente(ref error);
                }
                else
                {
                    hidePanelBusquedaCliente(true);
                }
            }
        }

        private void txtCodigoProducto_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtCodigoProducto.Text.ToString().Equals(""))
                {
                    getSpecificCodeProduct("txt");
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    getSpecificCodeProduct("txt");
                }
                else
                {
                    mValidateNumber.upperCaseCharacterPress(e);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getSpecificCodeProduct(string cmb_or_txt)
        {
            try
            {
                if (txtCodigoProducto.Text.ToString().Length < 7 && cmbBusquedaPorDescripcion.SelectedValue.ToString().Equals("0"))
                {
                    if (!txtCodigoProducto.Text.ToString().Equals(""))
                    {
                        showWarning("Debe ingresar un codigo completo o seleccionar un producto, para poder realizar la busqueda.", "Importante");
                    }
                }
                else
                {
                    if (cmb_or_txt.Equals("cmb"))
                    {
                        if (!cmbBusquedaPorDescripcion.Text.ToString().Equals(""))
                        {
                            tableDataProductSpecific = mProducto.getDataProductSpecificID(ref error, cmbBusquedaPorDescripcion.SelectedValue.ToString());
                            txtCantidad.Enabled = true;
                            txtCantidad.Focus();
                        }
                    }
                    else if (cmb_or_txt.Equals("txt"))
                    {
                        tableDataProductSpecific = mProducto.getDataProductSpecific(ref error, txtCodigoProducto.Text);
                    }

                    if (tableDataProductSpecific == null)
                    {

                    }
                    else
                    {
                        foreach (DataRow row in tableDataProductSpecific.Rows)
                        {
                            lblProducto.Text = "DESCRIPCION PRODUCTO: " + row[1].ToString() + "\n" +
                                                "CANTIDAD: " + row[4].ToString() + "\n" +
                                                "PRECIO UNITARIO: " + row[5].ToString();
                            txtCodigoProducto.Text = row[3].ToString();
                        }

                        if (lblProducto.Text.ToString().Equals("PRODUCTO"))
                        {
                            showWarning("Producto no tiene existencias o no existe valide el codigo por favor.");
                        }
                        else if (tableDataProductSpecific.Rows.Count <= 0 && !cmbBusquedaPorDescripcion.SelectedValue.ToString().Equals("0"))
                        {
                            showWarning("No tiene inventario de este producto.");
                            txtCodigoProducto.Text = "";
                            lblProducto.Text = "PRODUCTO";
                            txtCantidad.Enabled = false;
                        }
                        else
                        {
                            if (cmb_or_txt.Equals("cmb"))
                            {
                                if (cmbBusquedaPorDescripcion.SelectedValue.ToString().Equals("0"))
                                {
                                    txtCodigoProducto.Text = "";
                                    lblProducto.Text = "PRODUCTO";
                                    txtCantidad.Enabled = true;
                                    txtCantidad.Focus();
                                }
                            }
                            else
                            {
                                if (cmbBusquedaPorDescripcion.SelectedValue.ToString().Equals("0") || !txtCodigoProducto.Text.ToString().Equals(""))
                                {
                                    txtCantidad.Enabled = true;
                                    txtCantidad.Focus();
                                }
                                else
                                {
                                    if (!txtCodigoProducto.Text.ToString().Equals("") && !cmbBusquedaPorDescripcion.SelectedValue.ToString().Equals("0"))
                                    {
                                        txtCantidad.Enabled = true;
                                        txtCantidad.Focus();
                                    }
                                    else
                                    {
                                        txtCodigoProducto.Text = "";
                                        lblProducto.Text = "PRODUCTO";
                                        txtCantidad.Enabled = true;
                                        txtCantidad.Focus();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bool contain = false;
                contain = error.Contains("System.Data.SqlClient.SqlException(0x80131904): Conversion failed when converting the varchar value 'System.Data.DataRowView' to data type int.");
                if (contain)
                {

                }
                else
                {
                    showError(error + " " + ex.ToString());
                }
            }
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtCodigoProducto.Text.ToString().Equals("") || !cmbBusquedaPorDescripcion.SelectedValue.ToString().Equals("0"))
                {
                    if (!txtCantidad.Text.ToString().Equals(""))
                    {
                        if (validaCantidadEInsertaDatos())
                        {
                            cleanFindProduct();
                        }
                    }
                    else
                    {
                        showWarning("Por favor agregue que cantidad de articulo va agregar al detalle.", "Importante");
                    }
                }
                else
                {
                    showWarning("Por favor para poder agregar un articulo a la factura debe buscarlo primero y agregar la cantidad que se facturara.", "Importante");
                    txtCodigoProducto.Focus();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validaCantidadEInsertaDatos()
        {
            try
            {
                bool returnBool = false;
                foreach (DataRow row in tableDataProductSpecific.Rows)
                {
                    if(Convert.ToInt32(txtCantidad.Text.ToString()) <= Convert.ToInt32(row[4].ToString()))
                    {
                        int row_index_grid = 0;
                        decimal total_fila = Convert.ToInt32(txtCantidad.Text) * Convert.ToDecimal(row[5].ToString());

                        if (validaSiExisteEnGrid(txtCodigoProducto.Text.ToString(), ref row_index_grid))
                        {
                            int cantidad_anterior = 0, cantidad_total = 0;
                            cantidad_anterior = int.Parse(dgvDetalleFactura[5, row_index_grid].Value.ToString());
                            cantidad_total = cantidad_anterior + int.Parse(txtCantidad.Text.ToString());
                            dgvDetalleFactura[5, row_index_grid].Value = cantidad_total.ToString();
                            
                            decimal total_anterior = 0, total_nueva_fila = 0;
                            total_nueva_fila = decimal.Parse(row[5].ToString()) * decimal.Parse(txtCantidad.Text.ToString());
                            total_anterior = decimal.Parse(dgvDetalleFactura[7, row_index_grid].Value.ToString());
                            total_fila += total_anterior;
                            dgvDetalleFactura[7, row_index_grid].Value = total_fila.ToString();
                            lblTotalFacturaNumero.Text = Convert.ToString((decimal.Parse(lblTotalFacturaNumero.Text.ToString()) + total_nueva_fila));
                            returnBool = true;
                        }
                        else
                        {
                            string[] rows = { "X", row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), txtCantidad.Text.ToString(), row[5].ToString(), total_fila.ToString() };
                            agregaDatosEnGrid(rows);
                            lblTotalFacturaNumero.Text = Convert.ToString((decimal.Parse(lblTotalFacturaNumero.Text.ToString()) + total_fila));
                            returnBool = true;
                        }
                    }
                    else
                    {
                        showWarning("Usted no cuenta con suficientes productos en inventario para facturar, tiene actualmente " + row[4].ToString() + " articulos de codigo " + row[3].ToString());
                        returnBool = false;
                    }
                }
                return returnBool;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void cleanFindProduct()
        {
            try
            {
                txtCodigoProducto.Text = "";
                txtCantidad.Text = "";
                lblProducto.Text = "PRODUCTO";
                txtCodigoProducto.Focus();
                txtCantidad.Enabled = false;
                cmbBusquedaPorDescripcion.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void agregaDatosEnGrid(string[] row)
        {
            try
            {
                dgvDetalleFactura.Rows.Add(row);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool validaSiExisteEnGrid(string codigo_producto, ref int rowIdex)
        {
            try
            {
                bool returnBool = false;
                foreach (DataGridViewRow item in dgvDetalleFactura.Rows)
                {
                    if (item.Cells[4].Value.ToString().Equals(codigo_producto))
                    {
                        rowIdex = int.Parse(item.Index.ToString());
                        returnBool = true;
                    }
                    else
                    {
                        returnBool = false;
                    }
                }
                return returnBool;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                dgvDetalleFactura.Columns.Add("id_producto", "ID");
                dgvDetalleFactura.Columns.Add("descripcion", "DESCRIPCION");
                dgvDetalleFactura.Columns.Add("marca", "MARCA");
                dgvDetalleFactura.Columns.Add("codigo", "CODIGO");
                dgvDetalleFactura.Columns.Add("cantidad", "CANTIDAD");
                dgvDetalleFactura.Columns.Add("precio_unitario", "PRECIO UNITARIO");
                dgvDetalleFactura.Columns.Add("total_fila", "TOTAL FILA");

                dgvDetalleFactura.Columns[1].ReadOnly = true;
                dgvDetalleFactura.Columns[2].ReadOnly = true;
                dgvDetalleFactura.Columns[3].ReadOnly = true;
                dgvDetalleFactura.Columns[4].ReadOnly = true;
                dgvDetalleFactura.Columns[5].ReadOnly = true;
                dgvDetalleFactura.Columns[6].ReadOnly = false;
                dgvDetalleFactura.Columns[7].ReadOnly = true;


                // Tipo de dato Decimal
                dgvDetalleFactura.Columns[6].ValueType = Type.GetType("System.Decimal");

                // Formato de la columna: dos decimales
                dgvDetalleFactura.Columns[6].DefaultCellStyle.Format = "N2";

                dgvDetalleFactura.Columns[1].Name = "ID";
                dgvDetalleFactura.Columns[2].Name = "DESCRIPCION";
                dgvDetalleFactura.Columns[3].Name = "MARCA";
                dgvDetalleFactura.Columns[4].Name = "CODIGO";
                dgvDetalleFactura.Columns[5].Name = "CANTIDAD";
                dgvDetalleFactura.Columns[6].Name = "PRECIO UNITARIO";
                dgvDetalleFactura.Columns[7].Name = "TOTAL FILA";

                dgvDetalleFactura.Columns[0].Width = 60;
                dgvDetalleFactura.Columns[1].Width = 60;
                dgvDetalleFactura.Columns[2].Width = 300;
                dgvDetalleFactura.Columns[7].Width = 135;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void setUpCmbBusquedaProducto()
        {
            try
            {
                DataTable table = mProducto.getAllProducts(ref error);
                cmbBusquedaPorDescripcion.DataSource = table;

                cmbBusquedaPorDescripcion.DisplayMember = "descripcion";
                cmbBusquedaPorDescripcion.ValueMember = "id";

                cmbBusquedaPorDescripcion.AutoCompleteCustomSource = mProducto.LoadAutoComplete();
                cmbBusquedaPorDescripcion.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbBusquedaPorDescripcion.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void dgvDetalleFactura_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDetalleFactura.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    decimal valor_a_restar = 0;
                    valor_a_restar = decimal.Parse(dgvDetalleFactura[7, e.RowIndex].Value.ToString());
                    dgvDetalleFactura.Rows.RemoveAt(e.RowIndex);
                    lblTotalFacturaNumero.Text = Convert.ToString((decimal.Parse(lblTotalFacturaNumero.Text.ToString()) - valor_a_restar));
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            mValidateNumber.SoloNumerosSinSignos(e);
        }

        private void cmbBusquedaPorDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cmbBusquedaPorDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!cmbBusquedaPorDescripcion.SelectedValue.ToString().Equals("0") || !cmbBusquedaPorDescripcion.Text.ToString().Equals(""))
                {
                    getSpecificCodeProduct("cmb");

                }
                else if(cmbBusquedaPorDescripcion.SelectedValue.ToString().Equals("0"))
                {
                    lblProducto.Text = "PRODUCTO";
                    txtCodigoProducto.Text = "";
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaInformation())
                {
                    if (precio_total_factura_con_descuento < precio_total_factura)
                    {
                        guardarFactura();
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show($"Usted realizo cambio de precio en los detalles de la factura, esta seguro que desea aplicarlo a la factura?", "DESCUENTO", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            guardarFactura();
                            //Se comento esta parte del codigo ya que ahora se maneja el descuento por fila 29/05/2019
                            //if (aplicarDescuento())
                            //{
                            //    guardarFactura();
                            //}
                            //else
                            //{
                            //    throw new SystemException("No se pudo aplicar el descuento, por favor verifique la informacion.");
                            //}
                        }
                        //else if (dialogResult == DialogResult.No)
                        //{
                        //    guardarFactura();
                        //}
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool aplicarDescuento()
        {
            try
            {
                precio_total_factura = decimal.Parse(lblTotalFacturaNumero.Text);
                decimal descuento = decimal.Parse(txtDescuento.Text.ToString()) / 100;
                decimal total_fila = 0;
                decimal total_descuento_por_fila = 0;
                decimal total_con_descuento = 0;
                decimal total_factura = 0;
                foreach (DataGridViewRow item in dgvDetalleFactura.Rows)
                {
                    total_fila = decimal.Parse(item.Cells[7].Value.ToString());
                    total_descuento_por_fila = total_fila * descuento;
                    total_con_descuento = total_fila - total_descuento_por_fila;
                    item.Cells[7].Value = total_con_descuento.ToString();
                    total_factura += total_con_descuento;
                }
                precio_total_factura = total_factura;
                lblTotalFacturaNumero.Text = total_factura.ToString();
                return true;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
                return false;
            }
        }

        private void guardarFactura()
        {
            try
            {
                DataTable table = ToDataTable(dgvDetalleFactura);
                if (mFactura.insertEncabezadoFactura(ref error, cmbSerie.SelectedValue.ToString(), lblNoFacturaNumero.Text, lblIdClienteNumero.Text, lblTotalFacturaNumero.Text, table))
                {
                    showSuccess($"Se agrego factura No.{lblNoFacturaNumero.Text} de serie {cmbSerie.Text} correctamente.", "Exito");
                    imprimirFactura();
                    cleanLabelDatosCliente("limpiarBusquedaCliente");
                    dgvDetalleFactura.Rows.Clear();
                    lblTotalFacturaNumero.Text = "0";
                    txtDescuento.Text = "";
                    chkDescuento.Checked = false;
                    precio_total_factura_con_descuento = 0;
                    precio_total_factura = 0;
                    cmbSerie.SelectedValue = "0";
                    txtBusqueda.Text = "";
                    cmbBusquedaCliente.SelectedValue = "0";
                    cmbSerie.Focus();
                }
                else
                {
                    showError("No se pudo guardar la factura, por favor verifique toda la informacion.");
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private DataTable ToDataTable(DataGridView dataGridView)
        {
            try
            {
                var dt = new DataTable();
                foreach (DataGridViewColumn dataGridViewColumn in dataGridView.Columns)
                {
                    if (dataGridViewColumn.Visible)
                    {
                        dt.Columns.Add();
                    }
                }
                var cell = new object[dataGridView.Columns.Count];
                foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
                {
                    for (int i = 0; i < dataGridViewRow.Cells.Count; i++)
                    {
                        cell[i] = dataGridViewRow.Cells[i].Value;
                    }
                    dt.Rows.Add(cell);
                }
                return dt;
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
                return null;
            }
        }

        private bool validaInformation()
        {
            try
            {
                if (cmbSerie.SelectedValue.ToString().Equals("0")){ showWarning("Usted debe seleccionar una serie para poder facturar."); return false; }
                else if (lblNombreCliente.Text.ToString().Equals("CLIENTE")) { showWarning("Debe buscar un cliente para poder facturar."); return false; }
                else if (lblNombreCliente.Text.ToString().Equals("")) { showWarning("Debe buscar un cliente para poder facturar."); return false; }
                else if (dgvDetalleFactura.Rows.Count == 0) { showWarning("Debe productos al detalle para poder facturar."); return false; }
                else
                    return true;
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
                return false;
            }
        }

        private void cmbBusquedaCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!cmbBusquedaCliente.SelectedValue.ToString().Equals("0") && !cmbBusquedaCliente.Text.ToString().Equals(""))
                {
                    getSpecificClient();
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void getSpecificClient()
        {
            try
            {
                DataTable regreso = new DataTable("findclientID");
                regreso = mCliente.findClientForID(ref error, cmbBusquedaCliente.SelectedValue.ToString());
                if (regreso != null)
                {
                    setClient(regreso);
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void txtNit_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    getCliente();
                }
                else
                {
                    mValidateNumber.SoloNumerosNit(e);
                    mValidateNumber.upperCaseCharacterPress(e);
                }

                if (e.KeyChar == Convert.ToChar(Keys.C))
                {
                    txtNit.Text = "/F";
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void frmFacturacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.ToString() == "r" || e.KeyChar.ToString() == "R")
                {
                    
                }
                else
                {
                    if (e.KeyChar == Convert.ToChar(Keys.F3))
                    {
                        if (validaInformation())
                        {
                            guardarFactura();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void imprimirFactura()
        {
            try
            {
                FormularioEstaAbierto("reportViewer");
                reportViewer verReporte = new reportViewer();
                verReporte.imprimirFactura(cmbSerie.SelectedValue.ToString(), lblNoFacturaNumero.Text.ToString());
                verReporte.Show();

                if (int.Parse(lblFacturasRestantesNumero.Text.ToString()) <= 50)
                {
                    showWarning($"Usted solo tiene {lblFacturasRestantesNumero.Text} facturas para imprimir.");
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private Boolean FormularioEstaAbierto(String NombreDelFrm)
        {
            try
            {
                FormCollection collection = Application.OpenForms;

                foreach (Form item in collection)
                {
                    if (item.Text.Equals(NombreDelFrm))
                    {
                        if (MessageBox.Show("Actualmente tiene una factura en pantalla, desea imprimirla si o no?", "Informacion", MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
                        {
                            return false;
                        }
                        else
                        {
                            item.Close();
                            return true;
                        }
                    }
                    //else
                    //{
                    //    return false;
                    //}
                }
                return false;
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
                return false;
            }
        }

        private void btnClienteNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                hidePanelBusquedaCliente(false);
                clienteNuevoCF();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        //Se comento esta parte del codigo debido a que se cambio la forma de realizar el descuento, ahora el descuento es por producto. 29/05/2019
        //private void chkDescuento_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (chkDescuento.Checked)
        //        {
        //            txtDescuento.Visible = true;
        //        }
        //        else
        //        {
        //            txtDescuento.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        showError(ex.ToString());
        //    }
        //}

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            mValidateNumber.SoloNumerosConUnPunto(e, txtDescuento.Text);
        }

        private void btnAplicarDescuento_Click(object sender, EventArgs e)
        {
            if (precio_total_factura == precio_total_factura_con_descuento)
            {
                aplicarDescuento();
            }
        }

        private void frmFacturacion_Load(object sender, EventArgs e)
        {
            try
            {
                dgvDetalleFactura.Columns[1].ReadOnly = true;
                dgvDetalleFactura.Columns[2].ReadOnly = true;
                dgvDetalleFactura.Columns[3].ReadOnly = true;
                dgvDetalleFactura.Columns[4].ReadOnly = true;
                dgvDetalleFactura.Columns[5].ReadOnly = true;
                dgvDetalleFactura.Columns[6].ReadOnly = false;
                dgvDetalleFactura.Columns[7].ReadOnly = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void dgvDetalleFactura_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.ToString().Contains("Input string was not in a correct format."))
            {
                showError("Solo se permiten números en el precio.", "Error en precio.");
                dgvDetalleFactura.CancelEdit();
            }
        }

        private void dgvDetalleFactura_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex.Equals(6))
                {
                    dgvDetalleFactura[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Green;
                    cambioPrecioEnter = Convert.ToDecimal(dgvDetalleFactura[e.ColumnIndex, e.RowIndex].Value.ToString());
                }
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void dgvDetalleFactura_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                decimal cantidad = 0, precioEnter = 0, precioNuevo, diferencia;
                cambioPrecioLeave = Convert.ToDecimal(dgvDetalleFactura[e.ColumnIndex, e.RowIndex].Value.ToString());
                
                if (cambioPrecioLeave != cambioPrecioEnter)
                {
                    dgvDetalleFactura[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Yellow;

                    cantidad = Convert.ToDecimal(dgvDetalleFactura[5, e.RowIndex].Value.ToString());
                    precioNuevo = cambioPrecioLeave * cantidad;
                    dgvDetalleFactura[7, e.RowIndex].Value = precioNuevo;
                    precioEnter = cantidad * cambioPrecioEnter;

                    if (precioNuevo > precioEnter)
                    {
                        diferencia = precioNuevo - precioEnter;
                        lblTotalFacturaNumero.Text = Convert.ToString(Convert.ToDecimal(lblTotalFacturaNumero.Text) + diferencia);
                    }
                    else
                    {
                        diferencia = precioEnter - precioNuevo;
                        lblTotalFacturaNumero.Text = Convert.ToString(Convert.ToDecimal(lblTotalFacturaNumero.Text) - diferencia);
                    }
                }
                else
                {
                    dgvDetalleFactura[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
