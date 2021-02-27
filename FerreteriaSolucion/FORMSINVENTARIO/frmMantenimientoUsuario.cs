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
    public partial class frmMantenimientoUsuario : Form
    {
        #region variables
        private CLASES.cs_subcategoria mSubCategoria = new CLASES.cs_subcategoria();
        private CLASES.cs_usuario mUsuario = new CLASES.cs_usuario();

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

        public frmMantenimientoUsuario()
        {
            InitializeComponent();
            hideOrShowPanel(false);
            SetupDataGridView();
            getStadosSubCategoria();
            getDataGrid();
            getTipoAcceso();
        }

        private void btnExitMant_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                lblIDUsuarioNumero.Text = mUsuario.getMaxIDUsuario(ref error);
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
                if (txtUsuario.Text.ToString().Equals("")) { showWarning("Debe ingresar un usuario para poder crear usario de sistema."); return false; }
                else if (txtPassword.Text.ToString().Equals("")) { showWarning("Debe ingresar un password para poder crear usario de sistema."); return false; }
                else if (txtPrimerNombre.Text.ToString().Equals("")) { showWarning("Debe ingresar un nombre para poder crear usario de sistema."); return false; }
                else if (txtPrimerApellido.Text.ToString().Equals("")) { showWarning("Debe ingresar un apellido para poder crear usuario de sistema."); return false; }
                else if (cmbTipoAcceso.SelectedValue.ToString().Equals("0"))
                {
                    showWarning("Debe seleccionar un tipo de acceso para poder crear usuario.");
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

        private void getTipoAcceso()
        {
            try
            {
                cmbTipoAcceso.DataSource = mUsuario.getTipoAcceso();
                cmbTipoAcceso.ValueMember = "id";
                cmbTipoAcceso.DisplayMember = "descripcion";
                cmbTipoAcceso.SelectedValue = "0";
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
                dgvUsuario.Columns.Add("id_usuario", "ID");
                dgvUsuario.Columns.Add("usuario", "Usuario");
                dgvUsuario.Columns.Add("contrasenia", "Password");
                dgvUsuario.Columns.Add("primer_nombre", "Primer Nombre");
                dgvUsuario.Columns.Add("primer_apellido", "Primer Apellido");
                dgvUsuario.Columns.Add("estado_letras", "Estado");
                dgvUsuario.Columns.Add("tipo_acceso_letras", "Tipo acceso");
                dgvUsuario.Columns.Add("id_tipo_acceso", "Id tipo acceso");
                dgvUsuario.Columns.Add("estado", "state");

                dgvUsuario.Columns[1].Name = "ID";
                dgvUsuario.Columns[2].Name = "USUARIO";
                dgvUsuario.Columns[3].Name = "PASSWORD";
                dgvUsuario.Columns[4].Name = "PRIMERNOMBRE";
                dgvUsuario.Columns[5].Name = "PRIMERAPELLIDO";
                dgvUsuario.Columns[6].Name = "ESTADO";
                dgvUsuario.Columns[7].Name = "TIPOACCESO";
                dgvUsuario.Columns[8].Name = "IDTIPOACCESO";
                dgvUsuario.Columns[9].Name = "STATE";
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
                dgvUsuario.Rows.Clear();
                DataTable table = mUsuario.getUsuarios(ref error);
                foreach (DataRow row in table.Rows)
                {
                    string[] rows = { "Editar", row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString() };
                    dgvUsuario.Rows.Add(rows);
                }
            }
            catch (Exception ex)
            {
                showError(error + ex.ToString());
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
                    btnCrearUsuario.Visible = false;
                }
                else
                {
                    panelGrid.Visible = true;
                    panelControles.Visible = false;
                    btnCrearUsuario.Visible = true;
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
                txtUsuario.Text = "";
                txtPassword.Text = "";
                txtPrimerNombre.Text = "";
                txtPrimerApellido.Text = "";
                cmbTipoAcceso.SelectedValue = "0";
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
                if (mUsuario.updateUsuario(ref error, lblIDUsuarioNumero.Text.ToString(), txtUsuario.Text.ToString(), txtPassword.Text.ToString(), txtPrimerNombre.Text, txtPrimerApellido.Text.ToString(), cmbEstados.SelectedValue.ToString(), cmbTipoAcceso.SelectedValue.ToString()))
                {
                    getDataGrid();
                    showSuccess("Se ha actualizado correctamente el usuario.");
                }
                else
                {
                    showError("Ocurrio un error actualizando el usuario, por favor verifique la informacion y vuelva a intentarlo. " + error);
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
                if (mUsuario.insertUsuario(ref error, txtUsuario.Text.ToString(), txtPassword.Text.ToString(), txtPrimerNombre.Text, txtPrimerApellido.Text.ToString(), cmbEstados.SelectedValue.ToString(), cmbTipoAcceso.SelectedValue.ToString()))
                {
                    getDataGrid();
                    showSuccess("Se ha agregado correctamente el usuario.");
                }
                else
                {
                    showError("Ocurrio un error agregando el usuario, por favor verifique la informacion y vuelva a intentarlo. " + error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgvUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvUsuario.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    lblIDUsuarioNumero.Text = dgvUsuario[1, e.RowIndex].Value.ToString();
                    txtUsuario.Text = dgvUsuario[2, e.RowIndex].Value.ToString();
                    txtPassword.Text = dgvUsuario[3, e.RowIndex].Value.ToString();
                    txtPrimerNombre.Text = dgvUsuario[4, e.RowIndex].Value.ToString();
                    txtPrimerApellido.Text = dgvUsuario[5, e.RowIndex].Value.ToString();
                    cmbTipoAcceso.SelectedValue = dgvUsuario[8, e.RowIndex].Value.ToString();
                    cmbEstados.SelectedValue = dgvUsuario[9, e.RowIndex].Value.ToString();
                    isUpdate = true;
                    hideOrShowPanel(true);
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }
    }
}
