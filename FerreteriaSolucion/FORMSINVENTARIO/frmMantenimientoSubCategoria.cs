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
    public partial class frmMantenimientoSubCategoria : Form
    {
        #region variables
        private CLASES.cs_subcategoria mSubCategoria = new CLASES.cs_subcategoria();
        private CLASES.cs_categoria mCategoria = new CLASES.cs_categoria();

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

        public frmMantenimientoSubCategoria()
        {
            InitializeComponent();
            this.Text = "Mantenimiento sub categorias";
            hideOrShowPanel(false);
            SetupDataGridView();
            getStadosSubCategoria();
            getDataGrid();
            getCategorias();
        }

        private void btnCrearCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                lblIDCategoriaNumero.Text = mSubCategoria.getMaxIdSubCategoria(ref error);
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
                        updateSubCategoria();
                        isUpdate = false;
                        hideOrShowPanel(false);
                    }
                    else
                    {
                        insertSubCategoria();
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

        private bool validateInformationSerie()
        {
            try
            {
                if (txtCategoria.Text.ToString().Equals("")) { showWarning("Debe ingresar categoria para poder agregarla."); return false; }
                else if (cmbCategoria.SelectedValue.ToString().Equals("0"))
                {
                    showWarning("Debe seleccionar una categoria para poder agregar la subcategoria.");
                    return false;
                }
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

        private void getCategorias()
        {
            try
            {
                cmbCategoria.DataSource = mCategoria.getCategoriasCMB(ref error);
                cmbCategoria.ValueMember = "id_categoria";
                cmbCategoria.DisplayMember = "descripcion";
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
                dgvCategorias.Columns.Add("id_sub_categoria", "ID");
                dgvCategorias.Columns.Add("id_categoria", "idcat");
                dgvCategorias.Columns.Add("categoria", "Categoria");
                dgvCategorias.Columns.Add("descripcion", "Descripcion");
                dgvCategorias.Columns.Add("estado_letras", "Estado");
                dgvCategorias.Columns.Add("estado", "state");

                dgvCategorias.Columns[1].Name = "ID";
                dgvCategorias.Columns[2].Name = "IDCAT";
                dgvCategorias.Columns[3].Name = "CATEGORIA";
                dgvCategorias.Columns[4].Name = "DESCRIPCION";
                dgvCategorias.Columns[5].Name = "ESTADO";
                dgvCategorias.Columns[6].Name = "STATE";
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
                dgvCategorias.Rows.Clear();
                DataTable table = mSubCategoria.getSubCategorias(ref error);
                foreach (DataRow row in table.Rows)
                {
                    string[] rows = { "Editar", row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString() };
                    dgvCategorias.Rows.Add(rows);
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
                    btnCrearCategoria.Visible = false;
                }
                else
                {
                    panelGrid.Visible = true;
                    panelControles.Visible = false;
                    btnCrearCategoria.Visible = true;
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
                txtCategoria.Text = "";
                cmbEstados.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void updateSubCategoria()
        {
            try
            {
                if (mSubCategoria.updateCategoria(ref error, txtCategoria.Text.ToString(), cmbEstados.SelectedValue.ToString(), lblIDCategoriaNumero.Text.ToString(), cmbCategoria.SelectedValue.ToString()))
                {
                    getDataGrid();
                    showSuccess("Se ha actualizado correctamente la categoria.");
                }
                else
                {
                    showError("Ocurrio un error actualizando la categoria, por favor verifique la informacion y vuelva a intentarlo. " + error);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void insertSubCategoria()
        {
            try
            {
                if (mSubCategoria.insertCategoria(ref error, txtCategoria.Text.ToString(), cmbEstados.SelectedValue.ToString(), cmbCategoria.SelectedValue.ToString()))
                {
                    getDataGrid();
                    showSuccess("Se ha agregado correctamente la categoria.");
                }
                else
                {
                    showError("Ocurrio un error agregando la categoria, por favor verifique la informacion y vuelva a intentarlo. " + error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvCategorias.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    lblIDCategoriaNumero.Text = dgvCategorias[1, e.RowIndex].Value.ToString();
                    txtCategoria.Text = dgvCategorias[4, e.RowIndex].Value.ToString();
                    cmbEstados.SelectedValue = dgvCategorias[6, e.RowIndex].Value.ToString();
                    cmbCategoria.SelectedValue = dgvCategorias[2, e.RowIndex].Value.ToString();
                    isUpdate = true;
                    hideOrShowPanel(true);
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void btnExitMant_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
