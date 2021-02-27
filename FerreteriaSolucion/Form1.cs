using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerreteriaSolucion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            try
            {
                this.Text = "FERRETERIA";
                frmLogin showLogin = new frmLogin();
                showLogin.ShowDialog();
                showLogin.Location = new Point(0, 0);
                if (showLogin.opcionSeleccionada.Equals("facturacion"))
                {
                    msFacturacion.Location = new Point(0, 0);
                    msInventario.Visible = false;
                }
                else if (showLogin.opcionSeleccionada.Equals("inventario"))
                {
                    msInventario.Location = new Point(0, 0);
                    msFacturacion.Visible = false;
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cREARFACTURASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaAbierto("frmFacturacion"))
                {
                    FORMSFACTURACION.frmFacturacion facturacion = new FORMSFACTURACION.frmFacturacion();
                    facturacion.MdiParent = this;
                    facturacion.Show();
                    facturacion.Location = new Point(0, 0);
                    facturacion.WindowState = FormWindowState.Maximized;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private Boolean FormularioEstaAbierto(String NombreDelFrm)
        {
            try
            {
                if (this.MdiChildren.Length > 0)
                {
                    for (int i = 0; i < this.MdiChildren.Length; i++)
                    {
                        //MessageBox.Show(NombreDelFrm.Substring(NombreDelFrm.IndexOf("Frm_"), NombreDelFrm.Length - NombreDelFrm.IndexOf("Frm_")));
                        if (this.MdiChildren[i].Name == NombreDelFrm)
                        {
                            MessageBox.Show("El formulario solicitado ya se encuentra abierto");
                            return true;
                        }
                    }
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

        #region messages
        private void showSuccess(string message)
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

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 113 || e.KeyChar.ToString() == "r" || e.KeyChar.ToString() == "Q" )
                {

                }
                else
                {
                    if (e.KeyChar == Convert.ToChar(Keys.F2))
                    {
                        if (!FormularioEstaAbierto("frmFacturacion"))
                        {
                            FORMSFACTURACION.frmFacturacion facturacion = new FORMSFACTURACION.frmFacturacion();
                            facturacion.MdiParent = this;
                            facturacion.Show();
                            facturacion.Location = new Point(0, 0);
                            facturacion.WindowState = FormWindowState.Maximized;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.ToString() == "Q" || e.KeyCode.ToString() == "r")
                {

                }
                else
                {
                    if (e.KeyCode == Keys.F2)
                    {
                        if (!FormularioEstaAbierto("frmFacturacion"))
                        {
                            FORMSFACTURACION.frmFacturacion facturacion = new FORMSFACTURACION.frmFacturacion();
                            facturacion.MdiParent = this;
                            facturacion.Show();
                            facturacion.Location = new Point(0, 0);
                            facturacion.WindowState = FormWindowState.Maximized;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void rEIMPRIMIRFACTURAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaAbierto("frmReimpresionDeFacturas"))
                {
                    FORMSFACTURACION.frmReimpresionDeFacturas facturacion = new FORMSFACTURACION.frmReimpresionDeFacturas();
                    facturacion.MdiParent = this;
                    facturacion.Show();
                    facturacion.Location = new Point(0, 0);
                    facturacion.WindowState = FormWindowState.Maximized;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void aNULARFACTURAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaAbierto("frmAnularFactura"))
                {
                    FORMSFACTURACION.frmAnularFactura facturacion = new FORMSFACTURACION.frmAnularFactura();
                    facturacion.MdiParent = this;
                    facturacion.Show();
                    facturacion.Location = new Point(0, 0);
                    facturacion.WindowState = FormWindowState.Maximized;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cREARSERIEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaAbierto("frmMantenimientoSerie"))
                {
                    FORMSFACTURACION.frmMantenimientoSerie facturacion = new FORMSFACTURACION.frmMantenimientoSerie();
                    facturacion.MdiParent = this;
                    facturacion.Show();
                    facturacion.Location = new Point(0, 0);
                    facturacion.WindowState = FormWindowState.Maximized;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void rEPORTEFACTURASPORFECHAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaAbierto("frmReporteFacturas"))
                {
                    FORMSFACTURACION.frmReporteFacturas facturacion = new FORMSFACTURACION.frmReporteFacturas();
                    facturacion.MdiParent = this;
                    facturacion.Show();
                    facturacion.Location = new Point(0, 0);
                    facturacion.WindowState = FormWindowState.Maximized;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cATEGORIASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaAbierto("frmMantenimientoCategorias"))
                {
                    FORMSINVENTARIO.frmMantenimientoCategorias mant = new FORMSINVENTARIO.frmMantenimientoCategorias();
                    mant.MdiParent = this;
                    mant.Show();
                    mant.Location = new Point(0,0);
                    //mant.WindowState = FormWindowState.Maximized;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void sUBCATEGORIASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaAbierto("frmMantenimientoSubCategoria"))
                {
                    FORMSINVENTARIO.frmMantenimientoSubCategoria ver = new FORMSINVENTARIO.frmMantenimientoSubCategoria();
                    ver.MdiParent = this;
                    ver.Show();
                    ver.Location = new Point(0,0);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void pRODUCTOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaAbierto("frmMantenimientoProductos"))
                {
                    FORMSINVENTARIO.frmMantenimientoProductos ver = new FORMSINVENTARIO.frmMantenimientoProductos();
                    ver.MdiParent = this;
                    ver.Show();
                    ver.Location = new Point(0, 0);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void iNGRESODEPRODUCTOSAINVENTARIOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaAbierto("frmIngresoAInventario"))
                {
                    FORMSINVENTARIO.frmIngresoAInventario ver = new FORMSINVENTARIO.frmIngresoAInventario();
                    ver.MdiParent = this;
                    ver.Show();
                    ver.Location = new Point(0,0);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void uSUARIOSDESISTEMAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaAbierto("frmMantenimientoUsuario"))
                {
                    FORMSINVENTARIO.frmMantenimientoUsuario ver = new FORMSINVENTARIO.frmMantenimientoUsuario();
                    ver.MdiParent = this;
                    ver.Show();
                    ver.Location = new Point(0, 0);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void iNVENTARIOACTUALToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaAbierto("frmReporteInventarioActual"))
                {
                    FORMSINVENTARIO.frmReporteInventarioActual VER = new FORMSINVENTARIO.frmReporteInventarioActual();
                    VER.MdiParent = this;
                    VER.Show();
                    VER.Location = new Point(0, 0);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
