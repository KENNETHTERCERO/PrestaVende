<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="WFFacturacion.aspx.cs" Inherits="PrestaVende.Public.WFFacturacion" %>
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
                                <asp:Button ID="btnCobroIntereses" runat="server" Width="200px" Text="Renovacion" class="btn btn-success" Visible="true" OnClick="btnCobroIntereses_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnAbonoCapital" runat="server" Width="200px" Text="Abono a Capital" class="btn btn-primary" Visible="true" OnClick="btnAbonoCapital_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnCancelacion" runat="server" Width="200px" Text="Cancelacion" class="btn btn-danger" Visible="true" OnClick="btnCancelacion_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnBack" runat="server" Width="200px" Text="Regresar" class="btn btn-default" Visible="true" OnClick="btnBack_Click"/>
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
                                <table>
                                    <tr>
                                        <td>
                                            <h1>Facturas&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
                                        </td>
                                        <td>
                                            <big><asp:Label ID="lblPrestamo" runat="server">Prestamo:&nbsp;&nbsp;</asp:Label></big>
                                        </td>
                                        <td>
                                            <big><asp:Label ID="lblnombre_prestamo" runat="server"></asp:Label></big>
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
                                                    <asp:GridView ID="gvFactura" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None" OnRowCommand="gvPrestamo_RowCommand" >
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>                                                            
                                                            <asp:BoundField DataField="serie" HeaderText="<center>Serie</center>" SortExpression="serie" HtmlEncode="false" />
                                                            <asp:BoundField DataField="numero_factura" HeaderText="<center>No. Factura</center>" SortExpression="numero_factura" HtmlEncode="false" />
                                                            <asp:BoundField DataField="numero_prestamo" HeaderText="<center>Prestamo</center>" SortExpression="numero_prestamo" HtmlEncode="false" />
                                                            <asp:BoundField DataField="fecha_transaccion" HeaderText="<center>Fecha</center>" SortExpression="fecha_transaccion"  HtmlEncode="false"/>                                                            
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
