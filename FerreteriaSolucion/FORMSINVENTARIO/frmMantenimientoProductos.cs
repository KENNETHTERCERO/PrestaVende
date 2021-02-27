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
    public partial class frmMantenimientoProductos : Form
    {
        #region variables
        private CLASES.cs_subcategoria mSubCategoria = new CLASES.cs_subcategoria();
        private CLASES.cs_producto mProducto = new CLASES.cs_producto();
        private CLASES.cs_validate_number mValidate = new CLASES.cs_validate_number();

        private static bool isUpdate = false;
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

        public frmMantenimientoProductos()
        {
            InitializeComponent();
            this.Text = "Mantenimiento productos";
            hideOrShowPanel(false);
            SetupDataGridView();
            getStadosSubCategoria();
            getDataGrid();
            getSubCategorias();
        }

        private void btnCrearProducto_Click(object sender, EventArgs e)
        {
            try
            {
                lblIDProductoNumero.Text = mProducto.getMaxProducto(ref error);
                hideOrShowPanel(true);
                cleanControls();
                isUpdate = false;
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
                    if (isUpdate == true)
                    {
                        updateProducto();
                        isUpdate = false;
                        hideOrShowPanel(false);
                    }
                    else
                    {
                        insertProducto();
                        isUpdate = false;
                        hideOrShowPanel(false);
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            hideOrShowPanel(false);
        }

        private void btnExitMant_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvProductos.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    lblIDProductoNumero.Text = dgvProductos[1, e.RowIndex].Value.ToString();
                    cmbSubCategoria.SelectedValue = dgvProductos[2, e.RowIndex].Value.ToString();
                    txtDescripcionProducto.Text = dgvProductos[4, e.RowIndex].Value.ToString();
                    txtMarca.Text = dgvProductos[5, e.RowIndex].Value.ToString();
                    txtCodigoProducto.Text = dgvProductos[6, e.RowIndex].Value.ToString();
                    cmbEstados.SelectedValue = dgvProductos[8, e.RowIndex].Value.ToString();
                    isUpdate = true;
                    hideOrShowPanel(true);
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private bool validateInformationSerie()
        {
            try
            {
                if (cmbSubCategoria.SelectedValue.ToString().Equals("0")) { showWarning("Debe seleccionar una sub categoria para poder guardar producto."); return false; }
                else if (txtDescripcionProducto.Text.ToString().Equals("")) { showWarning("Debe agregar descripcion de producto para poder agregarlo."); return false; }
                else if (txtMarca.Text.ToString().Equals("")) { showWarning("Debe ingresar una marca para poder guardar producto."); return false; }
                else if (txtCodigoProducto.Text.ToString().Equals("")) { showWarning("Debe agregar un codigo para poder agregar producto."); return false; }
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

        private void getStadosSubCategoria()
        {
            try
            {
                cmbEstados.DataSource = mSubCategoria.estadosSubCategoria(ref error);
                cmbEstados.ValueMember = "id";
                cmbEstados.DisplayMember = "descripcion";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void getSubCategorias()
        {
            try
            {
                cmbSubCategoria.DataSource = mSubCategoria.getSubCategoriasCMB(ref error);
                cmbSubCategoria.ValueMember = "id";
                cmbSubCategoria.DisplayMember = "descripcion";
                cmbSubCategoria.SelectedValue = "0";
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
                dgvProductos.Columns.Add("id_producto", "ID");
                dgvProductos.Columns.Add("id_subcategoria", "idsubcat");
                dgvProductos.Columns.Add("subcategoria", "Subcategoria");
                dgvProductos.Columns.Add("descripcion", "Descripcion");
                dgvProductos.Columns.Add("marca_producto", "Marca producto");
                dgvProductos.Columns.Add("codigo", "Codigo");
                dgvProductos.Columns.Add("estado_letras", "Estado");
                dgvProductos.Columns.Add("estado", "state");

                dgvProductos.Columns[1].Name = "ID";
                dgvProductos.Columns[2].Name = "IDSUBCAT";
                dgvProductos.Columns[3].Name = "SUBCATEGORIA";
                dgvProductos.Columns[4].Name = "DESCRIPCION";
                dgvProductos.Columns[5].Name = "MARCAPRODUCTO";
                dgvProductos.Columns[6].Name = "CODIGO";
                dgvProductos.Columns[7].Name = "ESTADO";
                dgvProductos.Columns[8].Name = "STATE";
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
                DataTable table = mProducto.getProductToMaintenance(ref error);
                foreach (DataRow row in table.Rows)
                {
                    string[] rows = { "Editar", row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString()};
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
                    btnCrearProducto.Visible = false;
                }
                else
                {
                    panelGrid.Visible = true;
                    panelControles.Visible = false;
                    btnCrearProducto.Visible = true;
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
                cmbSubCategoria.SelectedValue = "1";
                txtDescripcionProducto.Text = "";
                txtMarca.Text = "";
                txtCodigoProducto.Text = "";
                cmbEstados.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void updateProducto()
        {
            try
            {
                if (mProducto.updateProducto(ref error, cmbSubCategoria.SelectedValue.ToString(), txtDescripcionProducto.Text.ToString(), txtMarca.Text.ToString(), cmbEstados.SelectedValue.ToString(), txtCodigoProducto.Text.ToString(), lblIDProductoNumero.Text.ToString()))
                {
                    getDataGrid();
                    showSuccess("Se ha actualizado correctamente el producto.");
                }
                else
                {
                    showError("Ocurrio un error actualizando el producto, por favor verifique la informacion y vuelva a intentarlo. " + error);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void insertProducto()
        {
            try
            {
                if (mProducto.insertProducto(ref error, cmbSubCategoria.SelectedValue.ToString(), txtDescripcionProducto.Text.ToString(), txtMarca.Text.ToString(), cmbEstados.SelectedValue.ToString(), txtCodigoProducto.Text.ToString(), txtCantidad.Text.ToString(), txtPrecioUnitario.Text.ToString(), txtPrecioCosto.Text.ToString()))
                {
                    getDataGrid();
                    showSuccess("Se ha agregado correctamente el producto.");
                }
                else
                {
                    showError("Ocurrio un error agregando el producto, por favor verifique la informacion y vuelva a intentarlo. " + error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            mValidate.SoloNumerosSinSignos(e);
        }
    }
}
