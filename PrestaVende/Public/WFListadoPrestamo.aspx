<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="WFListadoPrestamo.aspx.cs" Inherits="PrestaVende.Public.WFListadoPrestamo" %>
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
                                <asp:Button ID="btnNuevoPrestamo" runat="server" Width="200px" Text="Nuevo prestamo" class="btn btn-success" Visible="true" OnClick="btnNuevoPrestamo_Click"/>
                                <br />
                                <br />
                                <div style="background-color: cornflowerblue; border-radius: 5px;">
                                    <table style="color:white; ">
                                        <tr>
                                            <td>
                                                Prestamos
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h4 style="color: darkgreen;">Activos</h4> 
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;<asp:Label ID="lblPrestamosActivosNumero" runat="server" Text="0" style="color: darkgreen;"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;Q&nbsp;<asp:Label ID="lblPrestamosActivosMonto" runat="server" Text="0" style="color: darkgreen;"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h4 style="color: yellow;">Cancelados</h4>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;<asp:Label ID="lblPrestamosCanceladosNumero" runat="server" Text="0" Style="color: yellow;"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;Q&nbsp;<asp:Label ID="lblPrestamosCanceladosMonto" runat="server" Text="0" style="color: limegreen;"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h4 style="color: red;">Liquidados</h4>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;<asp:Label ID="lblPrestamosLiquidadosNumero" runat="server" Text="0" Style="color: red;"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;Q&nbsp;<asp:Label ID="lblPrestamosLiquidadosMonto" runat="server" Text="0" style="color: limegreen;"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
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
                                <h1>Prestamos&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
                                <table>
                                    <tr>
                                        <td>
                                            <big><asp:Label ID="lblCliente" runat="server">CLIENTE:&nbsp;&nbsp;</asp:Label></big>
                                        </td>
                                        <td>
                                            <big><asp:Label ID="lblid_cliente" runat="server" Text="0"></asp:Label></big>
                                            <big><asp:Label ID="lblnombre_cliente" runat="server"></asp:Label></big>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnEditarCliente" runat="server" Width="200px" Text="Editar Cliente" style="color: white;" class="btn btn-link" Visible="true" OnClick="btnEditarCliente_Click"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <big><asp:Label ID="lblDireccion" runat="server">DIRECCION:&nbsp;&nbsp;</asp:Label></big>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDireccionTexto" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <big><asp:Label ID="lblTelefono" runat="server">TELEFONO:&nbsp;&nbsp;</asp:Label></big>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTelefonoTexto" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <big><asp:Label ID="lblFecha" runat="server">FECHA CREACION:&nbsp;&nbsp;</asp:Label></big>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaCreacionTexto" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                 </table>
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
                                                    <asp:GridView ID="gvPrestamo" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None" OnRowCommand="gvPrestamo_RowCommand" >
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-success" FooterStyle-BackColor="#ff9a32" CommandName="crear" HeaderText="<center>Crear<br/>Factura </center>" Text="->" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="id_prestamo_encabezado" HeaderText="<center>ID</center>" SortExpression="id_prestamo_encabezado" HtmlEncode="false"/>
                                                            <asp:BoundField DataField="numero_prestamo" HeaderText="<center>No. Prestamo</center>" SortExpression="numero_prestamo" HtmlEncode="false" />
                                                            <asp:BoundField DataField="sucursal" HeaderText="<center>Sucursal</center>" SortExpression="sucursal" HtmlEncode="false" />
                                                            <asp:BoundField DataField="total_prestamo" HeaderText="<center>Total</center>" SortExpression="total_prestamo" HtmlEncode="false" />
                                                            <asp:BoundField DataField="fecha_creacion_prestamo" HeaderText="<center>Fecha Creacion</center>" SortExpression="fecha_creacion_prestamo"  HtmlEncode="false"/>
                                                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="<center>Fecha Proximo Pago</center>" SortExpression="fecha_proximo_pago"  HtmlEncode="false"/>
                                                            <asp:BoundField DataField="saldo_prestamo" HeaderText="<center>Saldo</center>" SortExpression="saldo_prestamo" HtmlEncode="false" Visible="true"/>
                                                            <asp:BoundField DataField="plan_prestamo" HeaderText="<center>Plan Prestamo</center>" SortExpression="plan_prestamo" HtmlEncode="false" Visible="true"/>
                                                            <asp:BoundField DataField="Cliente" HeaderText="<center>Cliente</center>" SortExpression="Cliente" HtmlEncode="false" Visible="true"/>
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
