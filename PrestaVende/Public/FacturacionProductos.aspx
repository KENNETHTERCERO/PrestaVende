<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="FacturacionProductos.aspx.cs" Inherits="PrestaVende.Public.FacturacionProductos" %>
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
                                <button id="btnBack" onclick="goBack()" style="width: 200px;" class="btn btn-default">Regresar</button>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="tbnFacturar" runat="server" Width="200px" Text="Facturar" CssClass="btn btn-primary" Visible="true"/>
                                <br />
                                <br />
                                <br />
                                <%--<asp:Button ID="btnGuardarAvaluo" runat="server" Width="200px" Text="Guardar avaluo" CssClass="btn btn-info" Visible="false" />--%>

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
                                    <h1>Venta de articulos&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
                                    <table>
                                        <tr>
                                            <td>
                                                <big><asp:Label ID="lblCliente" runat="server">BUSQUEDA PRESTAMO:&nbsp;&nbsp;</asp:Label></big>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            <td>
                                                <asp:Button ID="btnBuscar" runat="server" Width="200px" Text="Buscar articulos" CssClass="btn btn-success" Visible="true" OnClick="btnBuscar_Click"/>
                                                </td>
                                            <td>
                                                <asp:DropDownList ID="ddlArticulos" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAgregar" runat="server" Width="200px" Text="Agregar articulo" CssClass="btn btn-info" Visible="true"/>
                                                </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="panel-body">
                                <center>
                                    <br />
                                    <br />
                                    <div id="div_gridView" runat="server" visible="true">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvProductoFacturar" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-danger" FooterStyle-BackColor="#ff9a32" CommandName="borrar" HeaderText="<center>Borrar</center>" Text="X" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="id_producto"         HeaderText="<center>ID</center>"                SortExpression="id_producto"        HtmlEncode="false"/>
                                                            <asp:BoundField DataField="numero_linea"        HeaderText="<center>Linea</center>"             SortExpression="numero_linea"          HtmlEncode="false"/>
                                                            <asp:BoundField DataField="producto"            HeaderText="<center>Producto</center>"          SortExpression="producto"           HtmlEncode="false" />
                                                            <asp:BoundField DataField="marca"               HeaderText="<center>Marca</center>"             SortExpression="marca"              HtmlEncode="false" />
                                                            <asp:BoundField DataField="valor"               HeaderText="<center>Valor</center>"             SortExpression="valor"              HtmlEncode="false" />
                                                            <asp:BoundField DataField="caracteristicas"     HeaderText="<center>Caracteristicas</center>"   SortExpression="caracteristicas"    HtmlEncode="false"/>
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
