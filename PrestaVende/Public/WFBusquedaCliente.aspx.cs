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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = Request.Cookies["userLogin"];

                if (cookie == null && CLASS.cs_usuario.id_usuario == 0)
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
                        hideOrShowDiv(true);
                        getEstados();
                        getPaises();
                        getCategoriaMedio();
                        getProfesion();
                    }
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        #region funciones
        
        private void hideOrShowDiv(bool hidePanel)
        {
            try
            {
                if (hidePanel.Equals(true))
                {
                    div_ingresa_datos.Visible = false;
                    div_gridView.Visible = true;
                    btnBack.Visible = true;
                    btnCreateClient.Visible = true;
                    btnAtras.Visible = false;
                    btnGuardarUsuario.Visible = false;
                }
                else
                {
                    div_ingresa_datos.Visible = true;
                    div_gridView.Visible = false;
                    btnBack.Visible = false;
                    btnCreateClient.Visible = false;
                    btnAtras.Visible = true;
                    btnGuardarUsuario.Visible = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void getClients()
        {
            try
            {
                string condicion = "";
                if (validateTXTFind())
                {
                    condicion = "DPI LIKE '%" + txtBusquedaCliente.Text +"%' OR " +
                                "nit LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "primer_nombre + ' ' + primer_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "primer_nombre + ' ' + segundo_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "primer_nombre + ' ' + segundo_nombre + ' ' + primer_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "primer_nombre + ' ' + segundo_nombre + ' ' + segundo_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "segundo_nombre + ' ' + primer_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "segundo_nombre + ' ' + segundo_apellido LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "correo_electronico LIKE '%" + txtBusquedaCliente.Text + "%' OR " +
                                "numero_telefono LIKE '%" + txtBusquedaCliente.Text + "%'";
                    gvCliente.DataSource = cs_cliente.findClient(ref error, condicion);
                    gvCliente.DataBind();
                }
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool validateTXTFind()
        {
            if (txtBusquedaCliente.Text.ToString().Length == 0)
            {
                showWarning("Debe ingresar criterios de busqueda.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void editClient(string id_usuario)
        {
            try
            {
                hideOrShowDiv(false);
                DataTable dtCliente = cs_cliente.getSpecificClient(ref error, id_usuario);
                foreach (DataRow item in dtCliente.Rows)
                {
                    lblIdClienteNumero.Text = item[0].ToString();
                    txtDPI.Text = item[1].ToString();
                    txtNit.Text = item[2].ToString();
                    txtPrimerNombre.Text = item[3].ToString();
                    txtSegundoNombre.Text = item[4].ToString();
                    txtPrimerApellido.Text = item[5].ToString();
                    txtSegundoApellido.Text = item[6].ToString();
                    txtDireccion.Text = item[7].ToString();
                    txtCorreoElectronico.Text = item[8].ToString();
                    txtNumeroTelefono.Text = item[9].ToString();
                    ddlEstado.SelectedValue = item[10].ToString();
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
                string[] datosInsert = new string[15];

                datosInsert[0] = txtDPI.Text;
                datosInsert[1] = txtNit.Text;
                datosInsert[2] = txtPrimerNombre.Text;
                datosInsert[3] = txtSegundoNombre.Text;
                datosInsert[4] = txtPrimerApellido.Text;
                datosInsert[5] = txtSegundoApellido.Text;
                datosInsert[6] = txtDireccion.Text;
                datosInsert[7] = txtCorreoElectronico.Text;
                datosInsert[8] = txtNumeroTelefono.Text;
                datosInsert[9] = ddlEstado.SelectedValue.ToString();
                datosInsert[10] = ddlPais.SelectedValue.ToString();
                datosInsert[11] = ddlDepartamento.SelectedValue.ToString();
                datosInsert[12] = ddlMunicipio.SelectedValue.ToString();
                datosInsert[13] = ddlSubCategoriaMedio.SelectedValue.ToString();
                datosInsert[14] = ddlCategoriaMedio.SelectedValue.ToString();

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
                try
                {
                    string[] datosUpdate = new string[10];

                    datosUpdate[0] = txtPrimerNombre.Text;
                    datosUpdate[1] = txtSegundoNombre.Text;
                    datosUpdate[2] = txtPrimerApellido.Text;
                    datosUpdate[3] = txtSegundoApellido.Text;
                    datosUpdate[4] = txtDireccion.Text;
                    datosUpdate[5] = txtCorreoElectronico.Text;
                    datosUpdate[6] = txtNumeroTelefono.Text;
                    datosUpdate[7] = ddlEstado.SelectedValue.ToString();
                    datosUpdate[8] = lblIdClienteNumero.Text.ToString();

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
                    hideOrShowDiv(true);
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
                hideOrShowDiv(false);
                isUpdate = false;
                txtBusquedaCliente.Text = "";
                getIDMaxClient();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            try
            {
                hideOrShowDiv(true);
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFPrincipal.aspx");
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            getClients();
        }

        protected void gvCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "crear")
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    GridViewRow selectedRow = gvCliente.Rows[index];
                    TableCell id_cliente = selectedRow.Cells[2];
                    Session["id_cliente_to_sub_cliente"] = id_cliente.Text.ToString();
                    Response.Redirect("WFPrestamo.aspx?id_cliente=" + id_cliente.Text);

                }
                else if (e.CommandName == "editar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    GridViewRow selectedRow = gvCliente.Rows[index];
                    TableCell id_cliente = selectedRow.Cells[2];
                    isUpdate = true;
                    hideOrShowDiv(false);
                    divSucceful.Visible = false;
                    editClient(id_cliente.Text.ToString());
                }
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