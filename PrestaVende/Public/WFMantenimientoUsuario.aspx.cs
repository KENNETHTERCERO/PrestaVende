using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFMantenimientoUsuario : System.Web.UI.Page
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
                    //Pruebas
                    //    this.Session["id_empresa"] = 1;
                    //this.Session["id_rol"] = 1;
                    //this.Session["id_sucursal"] = 0;

                    if (IsPostBack)
                    {
                        if (lblWarning.Text == "") { divWarning.Visible = false; }
                        if (lblError.Text == "") { divError.Visible = false; }
                        if (lblSuccess.Text == "") { divSucceful.Visible = false; }
                    }
                    else
                    {

                        Limpiar();
                        hideOrShowDiv(true);

                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }



        #region Metodos

        private DataSet ObtenerUsuarios(int id_usuario = -1)
        {
            DataSet ds = new DataSet();

            try
            {
                cs_usuario = new CLASS.cs_usuario();
                ds =  cs_usuario.ObtenerUsuarios(ref error, Int32.Parse( this.Session["id_sucursal"].ToString()), id_usuario);                
                
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
                this.ddlSucursal.DataSource = cs_sucursal.ObtenerSucursalesPorEmpresa(ref error, id_empresa.ToString(), ((Int16.Parse( this.Session["id_sucursal"].ToString()) == 0) ? true : false));
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

        private void ObtenerRoles()
        {
            try
            {

                cs_usuario = new CLASS.cs_usuario();
                this.ddlRol.DataSource = cs_usuario.ObtenerRoles(ref error);
                this.ddlRol.DataValueField = "id_rol";
                this.ddlRol.DataTextField = "rol";
                this.ddlRol.DataBind();
                
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
                if (hidePanel)
                {
                    div_ingresa_datos.Visible = false;
                    div_gridView.Visible = true;
                    btnSalir.Visible = true;
                    btnCreate.Visible = true;
                    btnCancel.Visible = false;
                    btnGuardar.Visible = false;
                }
                else
                {
                    div_ingresa_datos.Visible = true;
                    div_gridView.Visible = false;
                    btnSalir.Visible = false;                    
                    btnCreate.Visible = false;
                    btnCambiarEstado.Visible = true;
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
            ObtenerRoles();
            GrdUsuarios.DataSource = ObtenerUsuarios();
            GrdUsuarios.DataBind();

            txtUsuario.Text = "";
            txtPrimerNombre.Text = "";
            txtSegundoNombre.Text = "";
            txtPrimerApellido.Text = "";
            txtSegundoApellido.Text = "";
            txtPassword.Text = "";
            ddlEstado.SelectedValue = "1";
          
        }

        private bool validarCampos(bool esActualizacion)
        {
            

            if (esActualizacion)
            {
                if (txtIDUsuario.Text.Length > 0) { } else { showWarning("Debe seleccionar un usuario."); return false; }
            }

            if(txtUsuario.Text.Length > 0) {  } else { showWarning("Debe llenar el campo Usuario."); return false; }            
            if (txtPrimerApellido.Text.Length > 0) {  } else { showWarning("Debe llenar al menos el primer apellido del usuario"); return false; }
            if (txtPrimerNombre.Text.Length > 0) {  } else { showWarning("Debe llenar al menos el primer apellido del usuario"); return false; }

            return true;
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

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Limpiar();
            hideOrShowDiv(false);
            txtUsuario.Focus();
            txtIDUsuario.ReadOnly = true;       
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string error = "";
                bool respuesta = false;

                if (validarCampos(txtIDUsuario.Text.Length >0))
                {

                    respuesta = cs_usuario.crearActualizarUsuario(ref error,(txtIDUsuario.Text.Length>0 ? Int32.Parse(txtIDUsuario.Text) : -1 ), Int32.Parse(ddlEmpresa.SelectedValue), Int32.Parse(ddlSucursal.SelectedValue), txtUsuario.Text, txtPassword.Text, txtPrimerNombre.Text, txtSegundoNombre.Text, txtPrimerApellido.Text, txtSegundoApellido.Text, Int32.Parse(ddlRol.SelectedValue), Int32.Parse(ddlEstado.SelectedValue), 0, false);

                }

                if (respuesta)
                {
                    showSuccess("El Usuario fue " + (txtIDUsuario.Text.Length > 0 ? "actualizado" : "almacenado")  + " con éxito.");
                }
                else
                {
                    showError("No fue posible " + (txtIDUsuario.Text.Length > 0 ? "actualizar" : "almacenar") + " el usuario. error: " + error);
                }

            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }

            Limpiar();
            hideOrShowDiv(true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Limpiar();
            hideOrShowDiv(true);
        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                string error="";
                bool respuesta = false;

                if (validarCampos(true))
                {

                    respuesta = cs_usuario.crearActualizarUsuario(ref error, Int32.Parse( txtIDUsuario.Text), Int32.Parse(ddlEmpresa.SelectedValue), Int32.Parse(ddlSucursal.SelectedValue), txtUsuario.Text, txtPassword.Text, txtPrimerNombre.Text, txtSegundoNombre.Text, txtPrimerApellido.Text, txtSegundoApellido.Text, Int32.Parse(ddlRol.SelectedValue), Int32.Parse(ddlEstado.SelectedValue), 0, true);

                }

                if (respuesta)
                {
                    showSuccess("El estado del usuario fue cambiado exitosamente.");                    
                }
                else
                {
                    showError("No fue posible cambiar el estado del usuario. error: " + error);
                }
               
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }

            Limpiar();
            hideOrShowDiv(true);
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
                        txtPassword.Text = item["password_user"].ToString();
                        txtPrimerNombre.Text = item["primer_nombre"].ToString();
                        txtSegundoNombre.Text = item["segundo_nombre"].ToString();
                        txtPrimerApellido.Text = item["primer_apellido"].ToString();
                        txtSegundoApellido.Text = item["segundo_apellido"].ToString();
                        ddlRol.SelectedValue = item["id_rol"].ToString();
                        ddlEstado.SelectedValue = item["estado"].ToString();
                       
                    }

                    txtIDUsuario.ReadOnly = true;
                    txtPassword.ReadOnly = true;
                    ddlEstado.Enabled = false;

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