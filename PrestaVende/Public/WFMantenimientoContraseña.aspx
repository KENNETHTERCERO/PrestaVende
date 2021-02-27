<%@ Page Title="Mantenimiento contraseña" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="WFMantenimientoContraseña.aspx.cs" Inherits="PrestaVende.Public.WFMantenimientoContraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel runat="server" ID="uPanel" >
        <ContentTemplate>
            <div class="container-fluid text-center">
                <div class="row content">
                    <div class="col-sm-2 sidenav">
                        <center>
                        <h2>Opciones</h2>
                            <div class=".btn-group-vertical">
                                <asp:Button ID="btnSalir" runat="server" Width="200px" Text="Regresar" class="btn btn-default" Visible="false" OnClick="btnSalir_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnGuardar" runat="server" Width="200px" Text="Guardar" class="btn btn-info" Visible="false" OnClick="btnGuardar_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnCancel" runat="server" Width="200px" Text="Cancelar" class="btn btn-warning" Visible="false" OnClick="btnCancel_Click"/>
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
                                                            <h1>Cambio de contraseña &nbsp;&nbsp;</h1>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </div>
                            </div>
                            <div class="panel-body">
                                <center>
                                    <br />
                                    <br />
                                    <div id="div_ingresa_datos" runat="server" visible="false">
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblEmpresa" runat="server" Text="EMPRESA"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <center>
                                                         <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                        </center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblSucursal" runat="server" Text="SUCURSAL"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                    </td>
                                                </tr>      
                                                 <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblIDUsuario" runat="server" Text=" ID USUARIO "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtIDUsuario" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>   
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblUsuario" runat="server" Text="USUARIO "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUsuario" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblPassword" runat="server" Text="CONTRASEÑA ACTUAL"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                                    </td>
                                                </tr>    
                                                
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblNewPassword" runat="server" Text="NUEVA CONTRASEÑA"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtNewPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblNewPassword2" runat="server" Text="CONFIRMAR CONTRASEÑA"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtNewPassword2" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                                    </td>
                                                </tr> 
                                                                                                                                                                                                                                                                                                                                                                                                                                  
                                              </table>
                                        </div>
                                    </div>                                       

                                    <div id="div_gridView" runat="server" visible ="false">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GrdUsuarios" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None"  OnRowCommand="GrdUsuarios_RowCommand">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" FooterStyle-BackColor="#ff9a32" CommandName="select" HeaderText="" Text="->" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                                <asp:BoundField DataField="id_usuario" HeaderText="<center>ID </center>" SortExpression="id_usuario" HtmlEncode="false"/>                                                            
                                                                <asp:BoundField DataField="empresa" HeaderText="<center>EMPRESA </center>" SortExpression="empresa" HtmlEncode="false" />                                                                                                                                
                                                                <asp:BoundField DataField="sucursal" HeaderText="<center>SUCURSAL </center>" SortExpression="sucursal"  HtmlEncode="false"/>                                                                
                                                                <asp:BoundField DataField="usuario" HeaderText="<center>USUARIO </center>" SortExpression="usuario"  HtmlEncode="false"/>
                                                                <asp:BoundField DataField="primer_nombre" HeaderText="<center>PRIMER NOMBRE </center>" SortExpression="primer_nombre"  HtmlEncode="false"/>                                                                
                                                                <asp:BoundField DataField="segundo_nombre" HeaderText="<center>SEGUNDO NOMBRE </center>" SortExpression="segundo_nombre"  HtmlEncode="false"/>                                                                
                                                                <asp:BoundField DataField="primer_apellido" HeaderText="<center>PRIMER APELLIDO </center>" SortExpression="primer_apellido"  HtmlEncode="false"/>                                                                
                                                                <asp:BoundField DataField="segundo_apellido" HeaderText="<center>SEGUNDO APELLIDO </center>" SortExpression="segundo_apellido"  HtmlEncode="false"/>                                                                
                                                                <asp:BoundField DataField="id_rol" HeaderText="<center>ROL DEL USUARIO </center>" SortExpression="id_rol"  HtmlEncode="false"/>                                                                
                                                        </Columns>
                                                        <EditRowStyle BackColor="#7C6F57" />
                                                        <FooterStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" />
                                                        <HeaderStyle BackColor="#016965" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#E3EAEB" />
                                                        <SelectedRowStyle BackColor="#ff9a32" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                        </table>
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
