<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="WFRetiroArticulo.aspx.cs" Inherits="PrestaVende.Public.WFRetiroArticulo" %>
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
                                <asp:Button ID="btnRetirar" runat="server" Width="200px" Text="Retirar" class="btn btn-primary" Visible="true" OnClick="btnRetirar_Click"/>
                                <br />
                                <br />
                                <br />
                                <button id="btnBack" onclick="goBack()" style="width: 200px;" class="btn btn-default">Regresar</button>
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
                                            <h1>Retiro de articulos&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
                                        </td>
                                        <td>
                                            <big><asp:Label ID="lblPrestamo" runat="server">Prestamo:&nbsp;&nbsp;</asp:Label></big>
                                        </td>
                                        <td>
                                            <big><asp:Label ID="lblnombre_prestamo" runat="server"></asp:Label></big>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <big><asp:Label ID="lblSaldoPrestamo" runat="server">Abonos:&nbsp;&nbsp;</asp:Label></big>
                                        </td>
                                        <td>
                                            <big><asp:Label ID="lblValorSaldoPrestamo" runat="server"></asp:Label></big>
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
                                                    <asp:GridView ID="gvArticulos" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None" OnRowCommand="gvArticulos_RowCommand" OnRowDataBound = "OnRowDataBound">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>                                                            
                                                            <asp:BoundField DataField="numero_prestamo" HeaderText="<center>No. Prestamo</center>" SortExpression="numero_prestamo" HtmlEncode="false" />
                                                            <asp:BoundField DataField="idproducto" HeaderText="<center>ID Producto</center>" SortExpression="idproducto" HtmlEncode="false" Visible="False"/>
                                                            <asp:BoundField DataField="producto" HeaderText="<center>Producto</center>" SortExpression="producto" HtmlEncode="false" />
                                                            <asp:BoundField DataField="cantidad" HeaderText="<center>Cantidad</center>" SortExpression="cantidad" HtmlEncode="false" />
                                                            <asp:BoundField DataField="valor" HeaderText="<center>Valor</center>" SortExpression="valor"  HtmlEncode="false" />     
                                                            <asp:CheckBoxField DataField="retirada" SortExpression="retirada" HeaderText="<center>Retirada</center>"/>         
                                                            <asp:BoundField DataField="id_prestamo_detalle" HeaderText="<center>ID</center>" SortExpression="id_prestamo_detalle"  HtmlEncode="false" />          
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
