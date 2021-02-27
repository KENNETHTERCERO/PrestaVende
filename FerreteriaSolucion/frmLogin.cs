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
    public partial class frmLogin : Form
    {
        public string opcionSeleccionada = "";
        public static string id_usuario = "0";
        private CLASES.cs_usuario mUsuario = new CLASES.cs_usuario();
        public frmLogin()
        {
            InitializeComponent();
            this.Text = "Seleccionar aplicacion";
        }

        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            try
            {
                opcionSeleccionada = "facturacion";
                this.Close();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
            }
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            try
            {
                opcionSeleccionada = "inventario";
                this.Close();
            }
            catch (Exception ex)
            {
                showWarning(ex.ToString());
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

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateInformation())
                {
                    LogIng();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void LogIng()
        {
            string error = "";
            try
            {
                DataTable tab = mUsuario.logIn(ref error, txtUsuario.Text.ToString(), txtPassword.Text.ToString());
                
                if (tab.Rows.Count > 0)
                {
                    string tipo_acceso = "";
                    foreach (DataRow item in tab.Rows)
                    {
                        
                        id_usuario = item[0].ToString();
                        CLASES.cs_usuario.id_usuario = Convert.ToInt32(id_usuario);
                        tipo_acceso = item[3].ToString();
                    }

                    if (tipo_acceso.Equals("1"))
                    {
                        btnFacturacion.Visible = true;
                        btnInventario.Visible = true;
                    }
                    else if (tipo_acceso.Equals("2"))
                    {
                        btnFacturacion.Visible = true;
                        btnInventario.Visible = false;
                    }
                    else if (tipo_acceso.Equals("3"))
                    {
                        btnFacturacion.Visible = false;
                        btnInventario.Visible = true;
                    }
                }
                else
                {
                    showWarning("Usuario o contrase;a incorrectas, por favor valide.");
                }
            }
            catch (Exception ex)
            {
                showError(error + " " + ex.ToString());
            }
        }

        private bool validateInformation()
        {
            try
            {
                if (txtUsuario.Text.ToString().Equals(""))
                {
                    showWarning("Debe ingresar un usuario para poder ingresar.");
                    return false;
                }
                else if (txtPassword.Text.ToString().Equals(""))
                {
                    showWarning("Debe ingresar un password para poder ingresar.");
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex) 
            {
                showWarning(ex.ToString());
                return false;
            }
        }

        private void btnSalirDeAplicacion_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
