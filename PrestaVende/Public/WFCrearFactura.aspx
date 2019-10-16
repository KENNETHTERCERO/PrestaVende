<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="WFCrearFactura.aspx.cs" Inherits="PrestaVende.Public.WFCrearFactura" %>
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
                                <asp:Button ID="btnBack" runat="server" Width="200px" Text="Regresar" class="btn btn-default" Visible="true"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnGuardarFactura" runat="server" Width="200px" Text="Guardar factura" class="btn btn-primary" Visible="true" OnClick="btnGuardarFactura_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnVerEstadoDeCuenta" runat="server" Width="200px" Text="Cancelar" class="btn btn-warning" Visible="true" />
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
                                                            <h1>Creación de factura&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
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
                            </div>
                            <div class="panel-body">
                                <center>
                                    <br />
                                    <br />
                                    <div id="div_ingresa_datos" runat="server" visible="true">
                                        <div>
                                            <table>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblCliente" runat="server" Text="CLIENTE"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblIdPrestamo" runat="server" Text="PRESTAMO"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblNombreCliente" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <big><asp:Label ID="lblNombrePrestamo" runat="server" Text="0"></asp:Label></big>
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSerie" runat="server">SERIE</asp:Label>
                                                    </td>                                                    
                                                    <td>
                                                        <asp:Label ID="lblNumeroFactura" runat="server" >NUMERO FACTURA</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTipoTransaccion" runat="server" Text="TIPO TRANSACCION"></asp:Label>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSerie" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSerie_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <big><asp:Label ID="lblNumeroPrestamoNumeroFactura" runat="server" Text="0"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <big><asp:Label ID="lblTransaccion" runat="server" Text="0"></asp:Label></big>
                                                    </td>                                                  
                                                </tr>
                                                
                                                <tr>     
                                                    <td>
                                                        <asp:Label ID="lblSubTotal" runat="server" Text="SUB TOTAL"></asp:Label>
                                                    </td>                                               
                                                    <td>
                                                        <asp:Label ID="lblIVA" runat="server" Text="IVA"></asp:Label>
                                                    </td>                                                    
                                                    <td>
                                                        <asp:Label ID="lblTotalFactura" runat="server" Text="TOTAL FACTURA"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblSubTotalFactura" runat="server" Text="0"></asp:Label></big>
                                                    </td>  
                                                    <td>
                                                        <big><asp:Label ID="lblIVAFactura" runat="server" Text="0"></asp:Label></big>
                                                    </td>  
                                                    <td>
                                                        <big><asp:Label ID="lblTotalFacturaV" runat="server" Text="0"></asp:Label></big>
                                                    </td> 
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div id="div_gridView" runat="server" visible="true">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvDetalleFactura" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>                                                            
                                                            <asp:BoundField DataField="numero_prestamo" HeaderText="<center>No Prestamo</center>" SortExpression="numero_prestamo" HtmlEncode="false" />
                                                            <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="<center>Fecha Ultimo Pago</center>" SortExpression="fecha_ultimo_pago" HtmlEncode="false" />
                                                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="<center>Fecha Proximo Pago</center>" SortExpression="fecha_proximo_pago" HtmlEncode="false" />                                                            
                                                            <asp:BoundField DataField="fecha_actual" HeaderText="<center>Fecha Actual</center>" SortExpression="fecha_actual"  HtmlEncode="false"/>
                                                            <asp:BoundField DataField="diferencia_dias" HeaderText="<center>Dias</center>" SortExpression="diferencia_dias"  HtmlEncode="false"/>                                                            
                                                            <asp:BoundField DataField="cargo" HeaderText="<center>Cargo</center>" SortExpression="cargo"  HtmlEncode="false"/>
                                                            <asp:BoundField DataField="saldo_prestamo" HeaderText="<center>Saldo</center>" SortExpression="saldo_prestamo"  HtmlEncode="false"/>
                                                            <asp:BoundField DataField="Cantidad" HeaderText="<center>Semanas</center>" SortExpression="Cantidad" HtmlEncode="false"/>
                                                            <asp:BoundField DataField="Precio" HeaderText="<center>Sub Total</center>" SortExpression="Precio"  HtmlEncode="false"/>                                                            
                                                            <asp:BoundField DataField="SubTotal" HeaderText="<center>Total</center>" SortExpression="SubTotal"  HtmlEncode="false" DataFormatString="{0:N2}"/>
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

