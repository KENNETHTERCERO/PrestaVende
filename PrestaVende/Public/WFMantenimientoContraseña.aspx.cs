using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantenimientoContraseña : System.Web.UI.Page
    {
        private CLASS.cs_sucursal cs_sucursal = new CLASS.cs_sucursal();
        private CLASS.cs_usuario cs_usuario = new CLASS.cs_usuario();
        
        private string error = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null)
                {
                    Response.Redirect("WFWebLogin.aspx");
                }
                else
                {
                    
                    if (IsPostBack)
                    {
                        if (lblWarning.Text == "") { divWarning.Visible = false; }
                        if (lblError.Text == "") { divError.Visible = false; }
                        if (lblSuccess.Text == "") { divSucceful.Visible = false; }
                    }
                    else
                    {
                        Limpiar();
                        mostrarDatosUsuario();
                    }

                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #region Metodos

        private void mostrarDatosUsuario()
        {            
            try
            {
                if (this.Session["id_rol"].ToString().Equals("3") || this.Session["id_rol"].ToString().Equals("4") || this.Session["id_rol"].ToString().Equals("5"))
                {
                    hideOrShowDiv(false);
                    DataSet dsUsuario = ObtenerUsuarios(Int32.Parse(Session["id_usuario"].ToString()));

                    foreach (DataRow item in dsUsuario.Tables[0].Rows)
                    {
                        ddlEmpresa.SelectedValue = item["id_empresa"].ToString();
                        ddlSucursal.SelectedValue = item["id_sucursal"].ToString();
                        txtIDUsuario.Text = item["id_usuario"].ToString();
                        txtUsuario.Text = item["usuario"].ToString();
                    }

                }
                else
                {
                    hideOrShowDiv(true);
                    GrdUsuarios.DataSource = ObtenerUsuarios();
                    GrdUsuarios.DataBind();
                }

            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }            
        }

        private DataSet ObtenerUsuarios(int id_usuario = -1)
        {
            DataSet ds = new DataSet();

            try
            {
                cs_usuario = new CLASS.cs_usuario();
                ds = cs_usuario.ObtenerUsuarios(ref error, Int32.Parse(this.Session["id_sucursal"].ToString()), id_usuario);

            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }

            return ds;
        }

        private void ObtenerSucursales()
        {
            try
            {
                int id_empresa = Convert.ToInt32(Session["id_empresa"]);
                //int id_empresa = 1;

                cs_sucursal = new CLASS.cs_sucursal();
                this.ddlSucursal.DataSource = cs_sucursal.ObtenerSucursalesPorEmpresa(ref error, id_empresa.ToString(), ((Int16.Parse(this.Session["id_sucursal"].ToString()) == 0) ? true : false));
                this.ddlSucursal.DataValueField = "id_sucursal";
                this.ddlSucursal.DataTextField = "sucursal";
                this.ddlSucursal.DataBind();

                if (this.Session["id_rol"].ToString().Equals("3") || this.Session["id_rol"].ToString().Equals("4") || this.Session["id_rol"].ToString().Equals("5"))
                {
                    this.ddlSucursal.SelectedValue = this.Session["id_sucursal"].ToString();
                    //this.ddlSucursal.SelectedValue = "1";
                    this.ddlSucursal.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void ObtenerEmpresas()
        {
            try
            {

                cs_usuario = new CLASS.cs_usuario();
                this.ddlEmpresa.DataSource = cs_usuario.ObtenerEmpresa(ref error);
                this.ddlEmpresa.DataValueField = "id_empresa";
                this.ddlEmpresa.DataTextField = "empresa";
                this.ddlEmpresa.DataBind();

                if (this.Session["id_rol"].ToString().Equals("3") || this.Session["id_rol"].ToString().Equals("4") || this.Session["id_rol"].ToString().Equals("5"))
                {
                    this.ddlEmpresa.SelectedValue = this.Session["id_empresa"].ToString();
                    //this.ddlSucursal.SelectedValue = "1";
                    this.ddlEmpresa.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void hideOrShowDiv(bool hidePanel)
        {
            try
            {
                this.ddlEmpresa.Enabled = false;
                this.ddlSucursal.Enabled = false;
                txtIDUsuario.ReadOnly = true;
                txtUsuario.ReadOnly = true;

                if (hidePanel)
                {
                    div_ingresa_datos.Visible = true;
                    div_gridView.Visible = true;
                    btnSalir.Visible = true;                   
                    btnCancel.Visible = false;
                    btnGuardar.Visible = false;
                }
                else
                {
                    div_ingresa_datos.Visible = true;
                    div_gridView.Visible = false;
                    btnSalir.Visible = false;                    
                    btnCancel.Visible = true;
                    btnGuardar.Visible = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Limpiar()
        {
            ObtenerEmpresas();
            ObtenerSucursales();                        

            txtUsuario.Text = "";
            txtPassword.Text = "";
            txtNewPassword.Text = "";
            txtNewPassword2.Text = "";
           
        }

        private bool validarCampos()
        {
            
            if (txtUsuario.Text.Length > 0) { } else { showWarning("Debe llenar el campo Usuario."); return false; }
            if (txtPassword.Text.Length > 0) { } else { showWarning("Debe llenar el campo contraseña"); return false; }
            if (txtNewPassword.Text.Length > 0) { } else { showWarning("Debe llenar el campo nueva contraseña"); return false; }
            if (txtNewPassword2.Text.Length > 0) { } else { showWarning("Debe confirmar la nueva contraseña"); return false; }

            return true;
        }

        private bool validarPassword()
        {
            bool validacion = false;

            try
            {
                if (validarCampos())
                {
                    string[] respuesta = new string[15];

                    respuesta = cs_usuario.Login(txtUsuario.Text, txtPassword.Text);

                    if (!respuesta[0].Equals("true"))
                    {                        
                        showError("La contraseña actual no es correcta.");
                    }
                    else
                    {
                        if (txtNewPassword.Text.Equals(txtNewPassword2.Text))
                        {
                            validacion = true;
                        }
                        else
                        {
                            showError("La contraseña nueva y la confirmación deben ser iguales.");
                            txtNewPassword.Text = "";
                            txtNewPassword.Focus();
                            txtNewPassword2.Text = "";

                        }
                    }
                }
            }catch(Exception ex)
            {
                showError("Error al validar contraseña, error: " + ex.ToString());
            }
                return validacion;
        }

        private bool showError(string error)
        {
            divError.Visible = true;
            lblError.Controls.Add(new LiteralControl(string.Format("<span style='color:Red'>{0}</span>", error)));
            return true;
        }

        private bool showSuccess(string error)
        {
            divSucceful.Visible = true;
            lblSuccess.Controls.Add(new LiteralControl(string.Format("<span style='color:Green'>{0}</span>", error)));

            return true;
        }

        private bool showWarning(string warning)
        {
            divWarning.Visible = true;
            lblWarning.Controls.Add(new LiteralControl(string.Format("<span style='color:Orange'>{0}</span>", warning)));

            return true;
        }

        #endregion


        #region Eventos

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFPrincipal.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Limpiar();
            mostrarDatosUsuario();
        }       

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string error = "";
                bool respuesta = false;

                if (validarPassword())
                {

                   respuesta = cs_usuario.actualizarContraseña(ref error, Int32.Parse(txtIDUsuario.Text), Int32.Parse(ddlEmpresa.SelectedValue), Int32.Parse(ddlSucursal.SelectedValue),  txtNewPassword.Text);

                }

                if (respuesta)
                {
                    showSuccess("La contraseña fue actualizada exitosamente.");
                }
                else
                {
                    showError("No fue posible actualizar la contraseña.");
                }

            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }

            Limpiar();
            mostrarDatosUsuario();
        }

        protected void GrdUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "select")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataSet dsUsuario;
                    GridViewRow selectedRow = GrdUsuarios.Rows[index];

                    TableCell id_ususario = selectedRow.Cells[1];
                    dsUsuario = ObtenerUsuarios(Int32.Parse(id_ususario.Text.ToString()));

                    foreach (DataRow item in dsUsuario.Tables[0].Rows)
                    {
                        ddlEmpresa.SelectedValue = item["id_empresa"].ToString();
                        ddlSucursal.SelectedValue = item["id_sucursal"].ToString();
                        txtIDUsuario.Text = item["id_usuario"].ToString();
                        txtUsuario.Text = item["usuario"].ToString();                        
                    }
                                   
                    hideOrShowDiv(false);
                   
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #endregion
    }
}