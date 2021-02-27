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
    public partial class frmReporteFacturas : Form
    {
        private CLASES.cs_factura mFactura = new CLASES.cs_factura();

        private static string error;

        public frmReporteFacturas()
        {
            InitializeComponent();
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

        private void frmReporteFacturas_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            exportToExcel();
        }

        private void exportToExcel()
        {
            try
            {
                string fecha_inicial = DateTime.Parse(dtpFechaInicial.Text).Month + "-" + DateTime.Parse(dtpFechaInicial.Text).Day + "-" + DateTime.Parse(dtpFechaInicial.Text).Year;
                string fecha_final = DateTime.Parse(dtpFechaFinal.Text).Month + "-" + DateTime.Parse(dtpFechaFinal.Text).Day + "-" + DateTime.Parse(dtpFechaFinal.Text).Year;
                exportExcel(mFactura.reporteFacturas(ref error, fecha_inicial, fecha_final));
            }
            catch (Exception)
            {
               
            }
        }

        private bool validateDate()
        {
            try
            {
                if (DateTime.Parse(dtpFechaFinal.Text) < DateTime.Parse(dtpFechaInicial.Text))
                {
                    showWarning("La fecha inicial no puede ser mayor a fecha final.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void exportExcel(DataTable tabla)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                excel.Application.Workbooks.Add(true);

                int indiceColumna = 0;

                foreach (DataColumn col in tabla.Columns)
                {
                    indiceColumna++;
                    excel.Cells[1, indiceColumna] = col.ColumnName;
                }

                int indiceFila = 0;

                foreach (DataRow row in tabla.Rows)
                {
                    indiceFila++;
                    indiceColumna = 0;
                    foreach (DataColumn col in tabla.Columns)
                    {
                        indiceColumna++;
                        excel.Cells[indiceFila + 1, indiceColumna] = row[col.ColumnName].ToString();
                    }
                }

                excel.Visible = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }
    }
}
