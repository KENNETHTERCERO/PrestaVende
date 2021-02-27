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
    public partial class frmReimpresionDeFacturas : Form
    {
        #region variables
        private CLASES.cs_serie mSerie = new CLASES.cs_serie();
        private CLASES.cs_validate_number mValidate = new CLASES.cs_validate_number();

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

        public frmReimpresionDeFacturas()
        {
            InitializeComponent();
            this.Text = "Reimpresion de facturas";
            getSeries();
        }

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

        private void txtNoFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                mValidate.SoloNumerosSinSignos(e);
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void btnImprimirFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateInformation())
                {
                    imprimirFactura();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validateInformation()
        {
            try
            {
                if (cmbSerie.SelectedValue.ToString().Equals("0")){ showWarning("Debe seleccionar una serie para poder reimprimir factura."); return false; }
                else if (txtNoFactura.Text.ToString().Equals("")){ showWarning("Debe ingresar un numero de factura para reimprimir."); return false; }
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

        private void imprimirFactura()
        {
            try
            {
                FormularioEstaAbierto("reportViewer");
                reportViewer verReporte = new reportViewer();
                verReporte.imprimirFactura(cmbSerie.SelectedValue.ToString(), txtNoFactura.Text.ToString());
                verReporte.Show();
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
    }
}
