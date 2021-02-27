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
    public partial class frmAnularFactura : Form
    {
        #region variables
        private CLASES.cs_serie mSerie = new CLASES.cs_serie();
        private CLASES.cs_validate_number mValidate = new CLASES.cs_validate_number();
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

        public frmAnularFactura()
        {
            InitializeComponent();
            this.Text = "Anular factura";
            getSeries();
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

        private void btnAnularFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateInformation())
                {
                    if (validateBillIfisActive())
                    {
                        anulaFactura();
                    }
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
                if (cmbSerie.SelectedValue.ToString().Equals("0")) { showWarning("Debe seleccionar una serie para poder anular factura."); return false; }
                else if (txtNoFactura.Text.ToString().Equals("")) { showWarning("Debe ingresar un numero de factura para anular."); return false; }
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

        private bool validateBillIfisActive()
        {
            try
            {
                int estado = mFactura.validateBillIfIsActive(ref error, cmbSerie.SelectedValue.ToString(), txtNoFactura.Text.ToString());
                if (estado == 1)
                    return true;
                else if (estado == 0)
                {
                    showError("Esta factura ya fue anulada.");
                    return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
                return false;
            }
        }

        private bool anulaFactura()
        {
            try
            {
                if (mFactura.anulaFactura(ref error, cmbSerie.SelectedValue.ToString(), txtNoFactura.Text.ToString()))
                {
                    showSuccess("Se anulo correctamente la factura.");
                    return true;
                }
                else
                {
                    showError("No se pudo anular la factura, por favor verifique que esta factura no se haya anulado antes.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                showWarning(error + " " + ex.ToString());
                return false;
            }
        }
    }
}
