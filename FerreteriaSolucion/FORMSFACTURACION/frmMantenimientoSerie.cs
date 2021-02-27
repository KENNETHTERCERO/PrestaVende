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
    public partial class frmMantenimientoSerie : Form
    {
        #region variables
        private CLASES.cs_validate_number mValidate = new CLASES.cs_validate_number();
        private CLASES.cs_serie mSerie = new CLASES.cs_serie();

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

        public frmMantenimientoSerie()
        {
            InitializeComponent();
            this.Text = "Mantenimiento series";
            txtNumeroInicial.Text = "";
            txtNumeroFinal.Text = "";
            hideOrShowPanel(false);
            getStadosSeries();
            SetupDataGridView();
            getDataGrid();

        }

        private void getStadosSeries()
        {
            try
            {
                cmbEstados.DataSource = mSerie.estadosSerie();
                cmbEstados.ValueMember = "id";
                cmbEstados.DisplayMember = "descripcion";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void txtNumeroInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            mValidate.SoloNumerosSinSignos(e);
        }

        private void txtNumeroFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            mValidate.SoloNumerosSinSignos(e);
        }

        private void txtNumeroFinal_Leave(object sender, EventArgs e)
        {
            try
            {
                int numero_inicial = int.Parse(txtNumeroInicial.Text);
                int numero_final = 0;
                
                if (txtNumeroFinal.Text.ToString().Equals(""))
                {
                    numero_final = 0;
                }
                else
                {
                    numero_final = int.Parse(txtNumeroFinal.Text);
                }

                int total = (numero_final - numero_inicial) + 1;

                if (total < 1)
                {
                    showWarning("El numero inicial no debe ser mayor al numero final, por favor verifiquelo y agregue el numero correcto.");
                    txtNumeroFinal.Focus();
                }
                else
                {
                    txtNumeroDeFacturas.Text = total.ToString();
                    txtResolucion.Focus();
                }
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
                //this.dgvSeries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {this.Edit});
                //this.Edit.HeaderText = "Editar";
                //this.Edit.Name = "Edit";
                //this.Edit.ReadOnly = true;
                //this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;

                dgvSeries.Columns.Add("id_serie", "ID");
                dgvSeries.Columns.Add("serie", "Serie");
                dgvSeries.Columns.Add("numero_inicial", "Numero inicial");
                dgvSeries.Columns.Add("numero_final", "Numero final");
                dgvSeries.Columns.Add("numero_facturas", "Numero facturas");
                dgvSeries.Columns.Add("resolucion", "Resolucion");
                dgvSeries.Columns.Add("fecha_resolucion", "Fecha resolucion");
                dgvSeries.Columns.Add("estado", "Estado");
                dgvSeries.Columns.Add("id_estado", "ID Estado");

                dgvSeries.Columns[1].Name = "ID";
                dgvSeries.Columns[2].Name = "SERIE";
                dgvSeries.Columns[3].Name = "NUMERO INICIAL";
                dgvSeries.Columns[4].Name = "NUMERO FINAL";
                dgvSeries.Columns[5].Name = "NUMERO FACTURAS";
                dgvSeries.Columns[6].Name = "RESOLUCION";
                dgvSeries.Columns[7].Name = "FECHA RESOLUCION";
                dgvSeries.Columns[8].Name = "ESTADO";
                dgvSeries.Columns[9].Name = "ID ESTADO";
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
                dgvSeries.Rows.Clear();
                DataTable table = mSerie.getAllSeries(ref error);
                foreach (DataRow row in table.Rows)
                {
                    string[] rows = { "Editar", row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString() };
                    dgvSeries.Rows.Add(rows);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void dgvSeries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSeries.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    lblIDSerieNumero.Text = dgvSeries[1, e.RowIndex].Value.ToString();
                    txtSerie.Text = dgvSeries[2, e.RowIndex].Value.ToString();
                    txtNumeroInicial.Text = dgvSeries[3, e.RowIndex].Value.ToString();
                    txtNumeroFinal.Text = dgvSeries[4, e.RowIndex].Value.ToString();
                    txtNumeroDeFacturas.Text = dgvSeries[5, e.RowIndex].Value.ToString();
                    txtResolucion.Text = dgvSeries[6, e.RowIndex].Value.ToString();
                    dtpFechaResolucion.Text = dgvSeries[7, e.RowIndex].Value.ToString();
                    cmbEstados.SelectedValue = dgvSeries[9, e.RowIndex].Value.ToString();
                    isUpdate = true;
                    hideOrShowPanel(true);
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
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
                        updateSerie();
                        isUpdate = false;
                        hideOrShowPanel(false);
                    }
                    else
                    {
                        insertSerie();
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

        private bool validateInformationSerie()
        {
            try
            {
                if (txtSerie.Text.ToString().Equals("")){ showWarning("Debe ingresar serie para poder agregarla."); return false; }
                else if (txtNumeroInicial.Text.ToString().Equals("")) { showWarning("Debe ingresar numero inicial para poder agregar serie."); return false; }
                else if (txtNumeroFinal.Text.ToString().Equals("")) { showWarning("Debe ingresar numero final para poder agregar serie."); return false; }
                else if (txtResolucion.Text.ToString().Equals("")) { showWarning("Debe agregar resolucion para poder agregar serie."); return false; }
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

        private void updateSerie()
        {
            try
            {
                DateTime _fecha = DateTime.Parse(dtpFechaResolucion.Text);
                string fecha = _fecha.Month.ToString() + "-" + _fecha.Day.ToString() + "-" + _fecha.Year.ToString();

                if (mSerie.updateSerie(ref error, txtSerie.Text.ToString(), txtNumeroDeFacturas.Text.ToString(), txtNumeroInicial.Text.ToString(), txtNumeroFinal.Text.ToString(), txtResolucion.Text.ToString(), fecha, cmbEstados.SelectedValue.ToString(), lblIDSerieNumero.Text))
                {
                    getDataGrid();
                    showSuccess("Se ha actualizado correctamente la serie.");
                }
                else
                {
                    showError("Ocurrio un error actualizando la serie, por favor verifique la informacion y vuelva a intentarlo.");
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void insertSerie()
        {
            try
            {
                DateTime _fecha = DateTime.Parse(dtpFechaResolucion.Text);
                string fecha = _fecha.Month.ToString() + "-" + _fecha.Day.ToString() + "-" + _fecha.Year.ToString();

                if (mSerie.insertSerie(ref error, txtSerie.Text.ToString(), txtNumeroDeFacturas.Text.ToString(), txtNumeroInicial.Text.ToString(), txtNumeroFinal.Text.ToString(), "0", txtResolucion.Text.ToString(), fecha))
                {
                    getDataGrid();
                    showSuccess("Se ha agregado correctamente la serie.");
                }
                else
                {
                    showError("Ocurrio un error agregando la serie, por favor verifique la informacion y vuelva a intentarlo.");
                }
            }
            catch (Exception)
            {

                throw;
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
                    btnCrearSerie.Visible = false;
                }
                else
                {
                    panelGrid.Visible = true;
                    panelControles.Visible = false;
                    btnCrearSerie.Visible = true;
                }
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void btnCrearSerie_Click(object sender, EventArgs e)
        {
            try
            {
                lblIDSerieNumero.Text = mSerie.getMaxIdSerie(ref error);
                hideOrShowPanel(true);
                cleanControls();
            }
            catch (Exception ex)
            {
                showError(error + ex.ToString());
            }
        }

        private void cleanControls()
        {
            try
            {
                txtSerie.Text = "";
                txtNumeroInicial.Text = "";
                txtNumeroFinal.Text = "";
                txtNumeroDeFacturas.Text = "0";
                txtResolucion.Text = "";
                dtpFechaResolucion.Text = DateTime.Now.ToString();
                cmbEstados.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void frmMantenimientoSerie_Load(object sender, EventArgs e)
        {

        }
    }
}
