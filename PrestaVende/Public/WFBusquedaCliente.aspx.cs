using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFBusquedaCliente : System.Web.UI.Page
    {
        private CLASS.cs_cliente cs_cliente = new CLASS.cs_cliente();
        private CLASS.cs_manejo_pais cs_manejo_pais = new CLASS.cs_manejo_pais();
        private CLASS.cs_manejo_medios cs_manejo_medio = new CLASS.cs_manejo_medios();
        private CLASS.cs_profesion cs_profesion = new CLASS.cs_profesion();
        private string error = "";
        private static bool isUpdate = false;
        private static string accion = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && Convert.ToInt32(Session["id_usuario"]) == 0)
                {
                    Response.Redirect("~/WFWebLogin.aspx");
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
                        accion = Request.QueryString["accion"];
                        //hideOrShowDiv(true);
                        getEstados();
                        getPaises();
                        getCategoriaMedio();
                        getProfesion();
                        getNacionalidad();

                        if (accion.Equals("editar"))
                        {
                            isUpdate = true;
                            string id_cliente = Request.QueryString["id_cliente"];
                            editClient(id_cliente);
                        }
                        else
                        {
                            getIDMaxClient();
                            isUpdate = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #region funciones

        private void editClient(string id_cliente)
        {
            try
            {
                cs_cliente = new CLASS.cs_cliente();
                DataTable dtCliente = cs_cliente.getSpecificClient(ref error, id_cliente);
                foreach (DataRow item in dtCliente.Rows)
                {
                    this.lblIdClienteNumero.Text = item[0].ToString();
                    this.txtDPI.Text = item[1].ToString();
                    this.txtNit.Text = item[2].ToString();
                    this.txtPrimerNombre.Text = item[3].ToString();
                    this.txtSegundoNombre.Text = item[4].ToString();
                    this.txtTercerNombre.Text = item[5].ToString();
                    this.txtPrimerApellido.Text = item[6].ToString();
                    this.txtSegundoApellido.Text = item[7].ToString();
                    this.txtApellidoCasada.Text = item[8].ToString();
                    this.txtDireccion.Text = item[9].ToString();
                    this.txtCorreoElectronico.Text = item[10].ToString();
                    this.txtNumeroTelefono.Text = item[11].ToString();
                    this.ddlEstado.SelectedValue = item[12].ToString();
                    this.ddlPais.SelectedValue = item[13].ToString();
                    this.getDepartamento(ddlPais.SelectedValue.ToString());
                    this.ddlDepartamento.SelectedValue = item[14].ToString();
                    this.getMunicipio(ddlDepartamento.SelectedValue.ToString());
                    this.ddlMunicipio.SelectedValue = item[15].ToString();
                    this.ddlSubCategoriaMedio.SelectedValue = item[16].ToString();
                    this.ddlCategoriaMedio.SelectedValue = item[17].ToString();
                    this.ddlProfesion.SelectedValue = item[18].ToString();
                    this.ddlNacionalidad.SelectedValue = item[21].ToString();
                }

                this.txtDPI.Enabled = false;
                this.txtNit.Enabled = false;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getEstados()
        {
            try
            {
                this.ddlEstado.DataSource = cs_cliente.getStateClient(ref error);
                this.ddlEstado.DataValueField = "id";
                this.ddlEstado.DataTextField = "descripcion";
                this.ddlEstado.DataBind();
                this.ddlEstado.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                showError(error + ' ' + ex.ToString());
            }
        }

        private void clearControls()
        {
            try
            {
                this.lblIdClienteNumero.Text = "0";
                this.txtDPI.Text = "";
                this.txtDPI.Enabled = true;
                this.txtNit.Text = "";
                this.txtNit.Enabled = true;
                this.txtPrimerNombre.Text = "";
                this.txtSegundoNombre.Text = "";
                this.txtPrimerApellido.Text = "";
                this.txtSegundoApellido.Text = "";
                this.txtDireccion.Text = "";
                this.txtCorreoElectronico.Text = "";
                this.txtNumeroTelefono.Text = "";
                this.ddlEstado.SelectedValue = "1";
                this.ddlPais.SelectedValue = "0";
                this.ddlDepartamento.SelectedValue = "0";
                this.ddlMunicipio.SelectedValue = "0";
                this.ddlCategoriaMedio.SelectedValue = "0";
                this.ddlSubCategoriaMedio.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void insertClient()
        {
            try
            {
                string[] datosInsert = new string[19];

                datosInsert[0] = this.txtDPI.Text;
                datosInsert[1] = this.txtNit.Text;
                datosInsert[2] = this.txtPrimerNombre.Text;
                datosInsert[3] = this.txtSegundoNombre.Text;
                datosInsert[4] = this.txtTercerNombre.Text;
                datosInsert[5] = this.txtPrimerApellido.Text;
                datosInsert[6] = this.txtSegundoApellido.Text;
                datosInsert[7] = this.txtApellidoCasada.Text;
                datosInsert[8] = this.txtDireccion.Text;
                datosInsert[9] = this.txtCorreoElectronico.Text;
                datosInsert[10] = this.txtNumeroTelefono.Text;
                datosInsert[11] = this.ddlEstado.SelectedValue.ToString();
                datosInsert[12] = this.ddlPais.SelectedValue.ToString();
                datosInsert[13] = this.ddlDepartamento.SelectedValue.ToString();
                datosInsert[14] = this.ddlMunicipio.SelectedValue.ToString();
                datosInsert[15] = this.ddlSubCategoriaMedio.SelectedValue.ToString();
                datosInsert[16] = this.ddlCategoriaMedio.SelectedValue.ToString();
                datosInsert[17] = this.ddlProfesion.SelectedValue.ToString();
                datosInsert[18] = this.ddlNacionalidad.SelectedValue.ToString();

                cs_cliente = new CLASS.cs_cliente();
                if (cs_cliente.insertClient(ref error, datosInsert) > 0)
                {
                    showSuccess("Se creo cliente sin problema.");
                }
                else
                {
                    showError(error);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void editClient()
        {
            try
            {
                string[] datosUpdate = new string[18];

                datosUpdate[0] = this.txtPrimerNombre.Text;
                datosUpdate[1] = this.txtSegundoNombre.Text;
                datosUpdate[2] = this.txtPrimerApellido.Text;
                datosUpdate[3] = this.txtSegundoApellido.Text;
                datosUpdate[4] = this.txtDireccion.Text;
                datosUpdate[5] = this.txtCorreoElectronico.Text;
                datosUpdate[6] = this.txtNumeroTelefono.Text;
                datosUpdate[7] = this.ddlEstado.SelectedValue.ToString();
                datosUpdate[8] = this.lblIdClienteNumero.Text.ToString();
                datosUpdate[9] = this.ddlProfesion.SelectedValue.ToString();
                datosUpdate[10] = this.ddlPais.SelectedValue.ToString();
                datosUpdate[11] = this.ddlDepartamento.SelectedValue.ToString();
                datosUpdate[12] = this.ddlMunicipio.SelectedValue.ToString();
                datosUpdate[13] = this.ddlCategoriaMedio.SelectedValue.ToString();
                datosUpdate[14] = this.ddlSubCategoriaMedio.SelectedValue.ToString();
                datosUpdate[15] = this.txtTercerNombre.Text;
                datosUpdate[16] = this.txtApellidoCasada.Text;
                datosUpdate[17] = this.ddlNacionalidad.SelectedValue.ToString();

                cs_cliente = new CLASS.cs_cliente();

                if (cs_cliente.updateClient(ref error, datosUpdate) > 0)
                {
                    showSuccess("Se edito cliente sin problema.");
                }
                else
                {
                    showError(error);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private void getIDMaxClient()
        {
            try
            {
                string id_max;
                error = "";
                cs_cliente = new CLASS.cs_cliente();
                id_max = cs_cliente.getMaxIDClient(ref error);
                if (error == "")
                {
                    this.lblIdClienteNumero.Text = id_max;
                }
                else
                {
                    //hideOrShowDiv(true);
                    showWarning("No se pudo obtener ID correctamente." + error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool validateDataText()
        {
            try
            {
                if (this.txtDPI.Text.ToString().Length == 0)
                {
                    showWarning("Debe ingresar un numero de DPI para poder guardar cliente.");
                    return false;
                }
                else if (this.txtDPI.Text.ToString().Length < 13)
                {
                    showWarning("Debe agregar el numero de DPI completo.");
                    return false;
                }
                else if (this.txtNit.Text.ToString().Length == 0)
                {
                    showWarning("Debe agregar el NIT para poder guardar.");
                    return false;
                }
                else if (this.txtPrimerNombre.Text.ToString().Length == 0)
                {
                    showWarning("Debe agregar el primer nombre para poder guardar.");
                    return false;
                }
                else if (this.txtPrimerApellido.Text.ToString().Length == 0)
                {
                    showWarning("Debe agregar el primer apellido para poder guardar.");
                    return false;
                }
                else if (this.txtNumeroTelefono.Text.ToString().Length == 0)
                {
                    showWarning("Debe agregar el numero de telefono para poder guardar.");
                    return false;
                }
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

        private void getNacionalidad()
        {
            try
            {
                cs_manejo_pais = new CLASS.cs_manejo_pais();
                this.ddlNacionalidad.DataSource = cs_manejo_pais.get_nacionalidad();
                this.ddlNacionalidad.DataValueField = "id_pais";
                this.ddlNacionalidad.DataTextField = "nacionalidad";
                this.ddlNacionalidad.DataBind();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void getPaises()
        {
            try
            {
                cs_manejo_pais = new CLASS.cs_manejo_pais();
                this.ddlPais.DataSource = cs_manejo_pais.get_pais();
                this.ddlPais.DataValueField = "id_pais";
                this.ddlPais.DataTextField = "pais";
                this.ddlPais.DataBind();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void getDepartamento(string id_pais)
        {
            try
            {
                cs_manejo_pais = new CLASS.cs_manejo_pais();
                this.ddlDepartamento.DataSource = cs_manejo_pais.get_Departamento(id_pais);
                this.ddlDepartamento.DataValueField = "id_departamento";
                this.ddlDepartamento.DataTextField = "departamento";
                this.ddlDepartamento.DataBind();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void getMunicipio(string id_departamento)
        {
            try
            {
                cs_manejo_pais = new CLASS.cs_manejo_pais();
                this.ddlMunicipio.DataSource = cs_manejo_pais.get_municipio(id_departamento);
                this.ddlMunicipio.DataValueField = "id_municipio";
                this.ddlMunicipio.DataTextField = "municipio";
                this.ddlMunicipio.DataBind();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void getCategoriaMedio()
        {
            try
            {
                cs_manejo_medio = new CLASS.cs_manejo_medios();
                this.ddlCategoriaMedio.DataSource = cs_manejo_medio.getCategoriaMedio();
                this.ddlCategoriaMedio.DataValueField = "id_categoria_medio";
                this.ddlCategoriaMedio.DataTextField = "categoria_medio";
                this.ddlCategoriaMedio.DataBind();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void getSubCategoriaMedio(string id_categoria_medio)
        {
            try
            {
                cs_manejo_medio = new CLASS.cs_manejo_medios();
                this.ddlSubCategoriaMedio.DataSource = cs_manejo_medio.getSubCategoriaMedio(id_categoria_medio);
                this.ddlSubCategoriaMedio.DataValueField = "id_subcategoria_medio";
                this.ddlSubCategoriaMedio.DataTextField = "subcategoria_medio";
                this.ddlSubCategoriaMedio.DataBind();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void getProfesion()
        {
            try
            {
                this.ddlProfesion.DataSource = cs_profesion.getProfesiones();
                this.ddlProfesion.DataValueField = "id_profesion";
                this.ddlProfesion.DataTextField = "profesion";
                this.ddlProfesion.DataBind();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region messages
        private bool showWarning(string warning)
        {
            divWarning.Visible = true;
            lblWarning.Controls.Add(new LiteralControl(string.Format("<span style='color:Orange'>{0}</span>", warning)));
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
        #endregion

        #region controls
        protected void btnCreateClient_Click(object sender, EventArgs e)
        {
            try
            {
                clearControls();
                getIDMaxClient();
                isUpdate = false;
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateDataText())
                {
                    if (isUpdate)
                    {
                        editClient();
                    }
                    else
                    {
                        insertClient();
                    }

                    Response.Redirect("WFListadoPrestamo?id_cliente=" + this.lblIdClienteNumero.Text);
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getDepartamento(this.ddlPais.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getMunicipio(this.ddlDepartamento.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void ddlCategoriaMedio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getSubCategoriaMedio(this.ddlCategoriaMedio.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }


        #endregion
    }
}