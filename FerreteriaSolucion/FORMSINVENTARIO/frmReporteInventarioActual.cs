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
    public partial class frmReporteInventarioActual : Form
    {
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

        private CLASES.cs_inventario mInventario = new CLASES.cs_inventario();
        private static string error = "";
        public frmReporteInventarioActual()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                exportExcel(mInventario.getReporteInventarioActual(ref error));
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
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
                showWarning(ex.ToString());
                throw;
            }
        }
    }
}
