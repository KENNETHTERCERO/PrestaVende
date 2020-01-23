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
                DataTable dtCliente = cs_cliente.getSpecificClient(ref error, id_cliente);
                foreach (DataRow item in dtCliente.Rows)
                {
                    lblIdClienteNumero.Text = item[0].ToString();
                    txtDPI.Text = item[1].ToString();
                    txtNit.Text = item[2].ToString();
                    txtPrimerNombre.Text = item[3].ToString();
                    txtSegundoNombre.Text = item[4].ToString();
                    txtTercerNombre.Text = item[5].ToString();
                    txtPrimerApellido.Text = item[6].ToString();
                    txtSegundoApellido.Text = item[7].ToString();
                    txtApellidoCasada.Text = item[8].ToString();
                    txtDireccion.Text = item[9].ToString();
                    txtCorreoElectronico.Text = item[10].ToString();
                    txtNumeroTelefono.Text = item[11].ToString();
                    ddlEstado.SelectedValue = item[12].ToString();
                    ddlPais.SelectedValue = item[13].ToString();
                    getDepartamento(ddlPais.SelectedValue.ToString());
                    ddlDepartamento.SelectedValue = item[14].ToString();
                    getMunicipio(ddlDepartamento.SelectedValue.ToString());
                    ddlMunicipio.SelectedValue = item[15].ToString();
                    ddlSubCategoriaMedio.SelectedValue = item[16].ToString();
                    ddlCategoriaMedio.SelectedValue = item[17].ToString();
                    ddlProfesion.SelectedValue = item[18].ToString();
                    ddlNacionalidad.SelectedValue = item[21].ToString();
                }

                txtDPI.Enabled = false;
                txtNit.Enabled = false;
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
                ddlEstado.DataSource = cs_cliente.getStateClient(ref error);
                ddlEstado.DataValueField = "id";
                ddlEstado.DataTextField = "descripcion";
                ddlEstado.DataBind();
                ddlEstado.SelectedValue = "1";
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
                lblIdClienteNumero.Text = "0";
                txtDPI.Text = "";
                txtDPI.Enabled = true;
                txtNit.Text = "";
                txtNit.Enabled = true;
                txtPrimerNombre.Text = "";
                txtSegundoNombre.Text = "";
                txtPrimerApellido.Text = "";
                txtSegundoApellido.Text = "";
                txtDireccion.Text = "";
                txtCorreoElectronico.Text = "";
                txtNumeroTelefono.Text = "";
                ddlEstado.SelectedValue = "1";
                ddlPais.SelectedValue = "0";
                ddlDepartamento.SelectedValue = "0";
                ddlMunicipio.SelectedValue = "0";
                ddlCategoriaMedio.SelectedValue = "0";
                ddlSubCategoriaMedio.SelectedValue = "0";
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

                datosInsert[0] = txtDPI.Text;
                datosInsert[1] = txtNit.Text;
                datosInsert[2] = txtPrimerNombre.Text;
                datosInsert[3] = txtSegundoNombre.Text;
                datosInsert[4] = txtTercerNombre.Text;
                datosInsert[5] = txtPrimerApellido.Text;
                datosInsert[6] = txtSegundoApellido.Text;
                datosInsert[7] = txtApellidoCasada.Text;
                datosInsert[8] = txtDireccion.Text;
                datosInsert[9] = txtCorreoElectronico.Text;
                datosInsert[10] = txtNumeroTelefono.Text;
                datosInsert[11] = ddlEstado.SelectedValue.ToString();
                datosInsert[12] = ddlPais.SelectedValue.ToString();
                datosInsert[13] = ddlDepartamento.SelectedValue.ToString();
                datosInsert[14] = ddlMunicipio.SelectedValue.ToString();
                datosInsert[15] = ddlSubCategoriaMedio.SelectedValue.ToString();
                datosInsert[16] = ddlCategoriaMedio.SelectedValue.ToString();
                datosInsert[17] = ddlProfesion.SelectedValue.ToString();
                datosInsert[18] = ddlNacionalidad.SelectedValue.ToString();

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

                datosUpdate[0] = txtPrimerNombre.Text;
                datosUpdate[1] = txtSegundoNombre.Text;
                datosUpdate[2] = txtPrimerApellido.Text;
                datosUpdate[3] = txtSegundoApellido.Text;
                datosUpdate[4] = txtDireccion.Text;
                datosUpdate[5] = txtCorreoElectronico.Text;
                datosUpdate[6] = txtNumeroTelefono.Text;
                datosUpdate[7] = ddlEstado.SelectedValue.ToString();
                datosUpdate[8] = lblIdClienteNumero.Text.ToString();
                datosUpdate[9] = ddlProfesion.SelectedValue.ToString();
                datosUpdate[10] = ddlPais.SelectedValue.ToString();
                datosUpdate[11] = ddlDepartamento.SelectedValue.ToString();
                datosUpdate[12] = ddlMunicipio.SelectedValue.ToString();
                datosUpdate[13] = ddlCategoriaMedio.SelectedValue.ToString();
                datosUpdate[14] = ddlSubCategoriaMedio.SelectedValue.ToString();
                datosUpdate[15] = txtTercerNombre.Text;
                datosUpdate[16] = txtApellidoCasada.Text;
                datosUpdate[17] = ddlNacionalidad.SelectedValue.ToString();

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
                id_max = cs_cliente.getMaxIDClient(ref error);
                if (error == "")
                {
                    lblIdClienteNumero.Text = id_max;
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
                if (txtDPI.Text.ToString().Length == 0)
                {
                    showWarning("Debe ingresar un numero de DPI para poder guardar cliente.");
                    return false;
                }
                else if (txtDPI.Text.ToString().Length < 13)
                {
                    showWarning("Debe agregar el numero de DPI completo.");
                    return false;
                }
                else if (txtNit.Text.ToString().Length == 0)
                {
                    showWarning("Debe agregar el NIT para poder guardar.");
                    return false;
                }
                else if (txtPrimerNombre.Text.ToString().Length == 0)
                {
                    showWarning("Debe agregar el primer nombre para poder guardar.");
                    return false;
                }
                else if (txtPrimerApellido.Text.ToString().Length == 0)
                {
                    showWarning("Debe agregar el primer apellido para poder guardar.");
                    return false;
                }
                else if (txtNumeroTelefono.Text.ToString().Length == 0)
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
                ddlNacionalidad.DataSource = cs_manejo_pais.get_nacionalidad();
                ddlNacionalidad.DataValueField = "id_pais";
                ddlNacionalidad.DataTextField = "nacionalidad";
                ddlNacionalidad.DataBind();
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
                ddlPais.DataSource = cs_manejo_pais.get_pais();
                ddlPais.DataValueField = "id_pais";
                ddlPais.DataTextField = "pais";
                ddlPais.DataBind();
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
                ddlDepartamento.DataSource = cs_manejo_pais.get_Departamento(id_pais);
                ddlDepartamento.DataValueField = "id_departamento";
                ddlDepartamento.DataTextField = "departamento";
                ddlDepartamento.DataBind();
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
                ddlMunicipio.DataSource = cs_manejo_pais.get_municipio(id_departamento);
                ddlMunicipio.DataValueField = "id_municipio";
                ddlMunicipio.DataTextField = "municipio";
                ddlMunicipio.DataBind();
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
                ddlCategoriaMedio.DataSource = cs_manejo_medio.getCategoriaMedio();
                ddlCategoriaMedio.DataValueField = "id_categoria_medio";
                ddlCategoriaMedio.DataTextField = "categoria_medio";
                ddlCategoriaMedio.DataBind();
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
                ddlSubCategoriaMedio.DataSource = cs_manejo_medio.getSubCategoriaMedio(id_categoria_medio);
                ddlSubCategoriaMedio.DataValueField = "id_subcategoria_medio";
                ddlSubCategoriaMedio.DataTextField = "subcategoria_medio";
                ddlSubCategoriaMedio.DataBind();
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
                ddlProfesion.DataSource = cs_profesion.getProfesiones();
                ddlProfesion.DataValueField = "id_profesion";
                ddlProfesion.DataTextField = "profesion";
                ddlProfesion.DataBind();
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

                    Response.Redirect("WFListadoPrestamo?id_cliente=" + lblIdClienteNumero.Text);
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
                getDepartamento(ddlPais.SelectedValue.ToString());
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
                getMunicipio(ddlDepartamento.SelectedValue.ToString());
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
                getSubCategoriaMedio(ddlCategoriaMedio.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }


        #endregion
    }
}