<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="WFBusquedaCliente.aspx.cs" Inherits="PrestaVende.Public.WFBusquedaCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="uPanel">
        <ContentTemplate>
            <div class="container-fluid text-center">
                <div class="row content">
                    <div class="col-sm-2 sidenav">
                        <center>
                        <h2>Opciones</h2>
                            <div class=".btn-group-vertical">
                                <asp:Button ID="btnBack" runat="server" Width="200px" Text="Regresar" class="btn btn-default" Visible="true" OnClick="btnBack_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnCreateClient" runat="server" Width="200px" Text="Crear cliente" class="btn btn-primary" Visible="true" OnClick="btnCreateClient_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnGuardarUsuario" runat="server" Width="200px" Text="Guardar cliente" class="btn btn-info" Visible="true" OnClick="btnGuardarUsuario_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnAtras" runat="server" Width="200px" Text="Cancelar" class="btn btn-warning" Visible="false" />
                                <br />
                                <br />
                                <br />
                            </div>
                        </center>
                    </div>
                    <div class="col-sm-8 text-left">
                        <div>
                            <div align="center" runat="server" id="divWarning" visible="false" class="alert alert-warning alert-dismissable fade in">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                <strong>
                                    <asp:Label runat="server" ID="lblWarning" Text=""></asp:Label></strong>
                            </div>

                            <div align="center" runat="server" id="divError" visible="false" class="alert alert-danger alert-dismissable fade in">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                <strong>
                                    <asp:Label runat="server" ID="lblError" Text=""></asp:Label></strong>
                            </div>

                            <div align="center" runat="server" id="divSucceful" visible="false" class="alert alert-success alert-dismissable fade in">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                <strong>
                                    <asp:Label runat="server" ID="lblSuccess" Text=""></asp:Label></strong>
                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <div style="position: relative;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <h1>Edición de cliente&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                            </div>
                            <div class="panel-body">
                                <center>
                                    <br />
                                    <br />
                                    <div id="div_ingresa_datos" runat="server" visible="true">
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblIdCliente" runat="server" Text="ID CLIENTE"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblIdClienteNumero" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblPais" runat="server" Text="PAIS"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPais" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblDepartamento" runat="server" Text="DEPARTAMENTO"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDepartamento" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblMunicipio" runat="server" Text="MUNICIPIO"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlMunicipio" runat="server" class="form-control"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblDPI" runat="server" Text="DPI/PASAPORTE"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDPI" runat="server" type="text" class="form-control" ContentPlaceHolder="0000000000000" AccessKey="" Font-Bold="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblNit" runat="server" Text="NIT"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNit" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblPrimerNombre" runat="server" Text="PRIMER NOMBRE "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPrimerNombre" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblSegundoNombre" runat="server" Text="SEGUNDO NOMBRE "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSegundoNombre" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblTercerNombre" runat="server" Text="TERCER NOMBRE "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTercerNombre" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblPrimerApellido" runat="server" Text="PRIMER APELLIDO"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblSegundoApellido" runat="server" Text="SEGUNDO APELLIDO"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSegundoApellido" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblApellidoCasada" runat="server" Text="APELLIDO CASADA"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtApellidoCasada" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblProfesion" runat="server" Text="PROFESION"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlProfesion" runat="server" class="form-control" ></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblDireccion" runat="server" Text="DIRECCION"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDireccion" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblCorreoElectronico" runat="server" Text="CORREO ELECTRONICO"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCorreoElectronico" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblNumeroTelefono" runat="server" Text="NUMERO TELEFONO"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNumeroTelefono" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblCategoriaMedio" runat="server" Text="MEDIO"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCategoriaMedio" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoriaMedio_SelectedIndexChanged" ></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblSubCategoriaMedio" runat="server" Text="SUB MEDIO"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSubCategoriaMedio" runat="server" class="form-control"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblEstado" runat="server" Text="ESTADO"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlEstado" runat="server" class="form-control"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </center>
                            </div>
                            <div class="panel-footer">
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2 sidenav">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
