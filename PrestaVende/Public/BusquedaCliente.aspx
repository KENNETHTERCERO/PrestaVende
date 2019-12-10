<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusquedaCliente.aspx.cs" Inherits="PrestaVende.Public.BusquedaCliente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="True" />
    <asp:UpdatePanel runat="server" ID="uPanel">
        <ContentTemplate>
            <div class="container-fluid text-center">
                <div class="row content">
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
                                    <asp:Panel id="panSearch" runat="server" DefaultButton="btnBuscarCliente">
                                        <table>
                                            <tr>
                                                <td>
                                                    <h1>Búsqueda de cliente&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBusquedaCliente" runat="server" class="form-control" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnBuscarCliente" runat="server" class="btn btn-success" Text="Buscar cliente" OnClick="btnBuscarCliente_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClienteSeleccionado" runat="server" CssClass="form-control">CLIENTE SELECCIONADO&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblIdCliente" runat="server" CssClass="form-control"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNombreCliente" runat="server" CssClass="form-control"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="panel-body">
                                <center>
                                    <br />
                                    <br />
                                    <div id="div_gridView" runat="server" visible="True">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvCliente" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None" OnRowCommand="gvCliente_RowCommand" >
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-success" FooterStyle-BackColor="#ff9a32" CommandName="crear" HeaderText="<center>Crear<br/>Factura </center>" Text="->" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                            <asp:ButtonField ButtonType="Button" Visible="false" ControlStyle-CssClass="btn btn-success" FooterStyle-BackColor="#ff9a32" CommandName="editar" HeaderText="<center>Editar<br/>cliente</center>" Text="<>" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="id_cliente" HeaderText="<center>ID</center>" SortExpression="id_cliente" HtmlEncode="false"/>
                                                            <asp:BoundField DataField="DPI" HeaderText="<center>DPI</center>" SortExpression="DPI" HtmlEncode="false" />
                                                            <asp:BoundField DataField="nit" HeaderText="<center>NIT</center>" SortExpression="nit" HtmlEncode="false" />
                                                            <asp:BoundField DataField="nombre_completo" HeaderText="<center>NOMBRE</center>" SortExpression="nombre_completo" HtmlEncode="false" />
                                                            <asp:BoundField DataField="direccion" HeaderText="<center>DIRECCION</center>" SortExpression="direecion"  HtmlEncode="false"/>
                                                            <asp:BoundField DataField="estadoLetras" HeaderText="<center>Estado</center>" SortExpression="estadoLetras"  HtmlEncode="false"/>
                                                            <asp:BoundField DataField="estado" HeaderText="<center>IES</center>" SortExpression="estado" HtmlEncode="false" Visible="true"/>
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
    </form>
</body>
</html>
