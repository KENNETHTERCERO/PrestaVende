using CrystalDecisions.CrystalReports.Engine;
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
    public partial class reportViewer : Form
    {
        #region variables
        private CLASES.cs_parametros_generales mParametros = new CLASES.cs_parametros_generales();
        private CLASES.cs_factura mFactura = new CLASES.cs_factura();

        private static string error = "";
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

        public reportViewer()
        {
            InitializeComponent();
        }

        public void imprimirFactura(string id_serie, string numero_factura)
        {
            try
            {
                DataTable data = mFactura.impresionFactura(ref error, id_serie, numero_factura);
                ReportDocument crystal = new ReportDocument();
                crystal.Load(@"C:\REPORTES\" + mParametros.rptFactura(ref error));

                crystal.SetDataSource(data);
                crystalReportViewer.ReportSource = crystal;
                
                                
                
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }
    }
}
