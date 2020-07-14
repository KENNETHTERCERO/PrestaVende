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

                        //Limpiar();
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
                if (hidePanel)
                {
                    div_ingresa_datos.Visible = false;
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
    }
}