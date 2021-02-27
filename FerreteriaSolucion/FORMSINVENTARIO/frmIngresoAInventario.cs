using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerreteriaSolucion.FORMSINVENTARIO
{
    public partial class frmIngresoAInventario : Form
    {
        #region variables
        private CLASES.cs_inventario mInventario = new CLASES.cs_inventario();
        private CLASES.cs_validate_number mValidate = new CLASES.cs_validate_number();

        private static string error;
        #endregion
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

        public frmIngresoAInventario()
        {
            InitializeComponent();
            this.Text = "Ingreso a inventario";
            hideOrShowPanel(false);
            SetupDataGridView();
            getDataGrid();
            getStadosProductos();
        }

        private void btnAgregarAInventario_Click(object sender, EventArgs e)
        {
            try
            {
                hideOrShowPanel(true);
                cleanControls();
            }
            catch (Exception ex)
            {
                showError(error + ex.ToString());
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateInformationSerie())
                {
                    updateInventario();
                    hideOrShowPanel(false);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                hideOrShowPanel(false);
                getDataGrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnExitMant_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool validateInformationSerie()
        {
            try
            {
                if (cmbProducto.SelectedValue.ToString().Equals("0")) { showWarning("Debe seleccionar un producto para poder agregar al inventario."); return false; }
                else if (txtCantidad.Text.ToString().Equals("")) { showWarning("Debe ingresar cantidad para poder agregarlo a inventario."); return false; }
                else if (decimal.Parse(txtCantidad.Text.ToString()) <= 0 ) { showWarning("Debe ingresar cantidad para poder agregarlo a inventario."); return false; }
                else if (txtPrecioUnitario.Text.ToString().Equals("")) { showWarning("Debe ingresar un precio unitario para poder agregar."); return false; }
                else if (decimal.Parse(txtPrecioUnitario.Text.ToString()) <= 0) { showWarning("Debe ingresar precio unitario para poder agregar a inventario."); return false; }
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

        private void getStadosProductos()
        {
            try
            {
                cmbProducto.DataSource = mInventario.getProductosToCMB(ref error);
                cmbProducto.ValueMember = "id";
                cmbProducto.DisplayMember = "descripcion";
                cmbProducto.SelectedValue = "0";

                cmbProducto.AutoCompleteCustomSource = mInventario.LoadAutoComplete();
                cmbProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                dgvProductos.Columns.Add("id_inventario", "ID");
                dgvProductos.Columns.Add("descripcionCategoria", "Descripcion Categoria");
                dgvProductos.Columns.Add("descripcionSubCategoria", "Subcategoria");
                dgvProductos.Columns.Add("producto", "Producto");
                dgvProductos.Columns.Add("marca", "Marca");
                dgvProductos.Columns.Add("codigo_producto", "Codigo");
                dgvProductos.Columns.Add("stock", "Cantidad disponible");

                dgvProductos.Columns[0].Name = "ID";
                dgvProductos.Columns[1].Name = "DESCRIPCIONCATEGORIA";
                dgvProductos.Columns[2].Name = "SUBCATEGORIA";
                dgvProductos.Columns[3].Name = "PRODUCTO";
                dgvProductos.Columns[4].Name = "MARCA";
                dgvProductos.Columns[5].Name = "CODIGO";
                dgvProductos.Columns[6].Name = "STOCK";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getDataGrid()
        {
            try
            {
                dgvProductos.Rows.Clear();
                DataTable table = mInventario.getInventarioGrid(ref error);
                foreach (DataRow row in table.Rows)
                {
                    string[] rows = { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString() };
                    dgvProductos.Rows.Add(rows);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void hideOrShowPanel(bool hidePanelGrid)
        {
            try
            {
                if (hidePanelGrid)
                {
                    panelGrid.Visible = false;
                    panelControles.Visible = true;
                    btnAgregarAInventario.Visible = false;
                }
                else
                {
                    panelGrid.Visible = true;
                    panelControles.Visible = false;
                    btnAgregarAInventario.Visible = true;
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void cleanControls()
        {
            try
            {
                txtCantidad.Text = "";
                txtPrecioUnitario.Text = "";
                cmbProducto.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void updateInventario()
        {
            try
            {
                if (mInventario.updateInventario(ref error, txtCantidad.Text.ToString(), txtPrecioUnitario.Text.ToString(), cmbProducto.SelectedValue.ToString(), txtPrecioCosto.Text))
                {
                    getDataGrid();
                    showSuccess($"Se ha actualizado correctamente el inventario. Se agregaron {txtCantidad.Text.ToString()} articulos del siguiente producto {cmbProducto.Text}");
                }
                else
                {
                    showError("Ocurrio un error actualizando el inventario, por favor verifique la informacion y vuelva a intentarlo. " + error);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            mValidate.SoloNumerosSinSignos(e);
        }
    }
}
