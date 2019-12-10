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
                                <button id="btnBack" onclick="goBack()" style="width: 200px;" class="btn btn-default">Regresar</button>
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
                                                        <!--<td>
                                                            <big><asp:Label ID="lblPrestamo" runat="server">Prestamo:&nbsp;&nbsp;</asp:Label></big>
                                                        </td>
                                                        <td>
                                                            <big><asp:Label ID="lblnombre_prestamo" runat="server"></asp:Label></big>
                                                        </td>-->
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
                                                        <asp:Label ID="lblCliente" runat="server" Text="CLIENTE" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width:40px"></td>
                                                    <td>
                                                        <asp:Label ID="lblIdPrestamo" runat="server" Text="PRESTAMO"  Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblCodigoCliente" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <big><asp:Label ID="lblNombrePrestamo" runat="server" Text="0"></asp:Label></big>
                                                    </td>                                                                                                                                                      
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblNombreCliente" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height:20px;"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height:20px;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right:5px;">
                                                        <asp:Label ID="lblSerie" runat="server" Font-Bold="true">SERIE</asp:Label>
                                                    </td>                       
                                                    <td>
                                                        <asp:DropDownList ID="ddlSerie" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSerie_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </td>  
                                                    <td></td>                           
                                                    <td style="padding-right:5px;">
                                                        <asp:Label ID="lblNumeroFactura" runat="server" Font-Bold="true">NUMERO FACTURA</asp:Label>
                                                    </td>
                                                    <td>
                                                        <big><asp:Label ID="lblNumeroPrestamoNumeroFactura" runat="server" Text="0"></asp:Label></big>
                                                    </td>
                                                </tr>
                                                <tr>                                                    
                                                    <td style="padding-right:5px;">
                                                        <asp:Label ID="lblTipoTransaccion" runat="server" Text="TIPO TRANSACCION" Font-Bold="true"></asp:Label>
                                                    </td>                                                    
                                                    <td>
                                                        <big><asp:Label ID="lblTransaccion" runat="server" Text="0"></asp:Label></big>
                                                    </td>       
                                                    <td></td>  
                                                    <td style="padding-right:5px;">
                                                        <asp:Label ID="lblSubTotal" runat="server" Text="SUB TOTAL" Font-Bold="true"></asp:Label>
                                                    </td>     
                                                    <td>
                                                        <big><asp:Label ID="lblSubTotalFactura" runat="server" Text="0"></asp:Label></big>
                                                    </td>                                       
                                                </tr>
                                                
                                                <tr>                                                                                                      
                                                    <td style="padding-right:5px;">
                                                        <asp:Label ID="lblIVA" runat="server" Text="IVA" Font-Bold="true"></asp:Label>
                                                    </td>      
                                                    <td>
                                                        <big><asp:Label ID="lblIVAFactura" runat="server" Text="0"></asp:Label></big>
                                                    </td>
                                                    <td></td>                                              
                                                    <td style="padding-right:5px;">
                                                        <asp:Label ID="lblTotalFactura" runat="server" Text="TOTAL FACTURA" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <big><asp:Label ID="lblTotalFacturaV" runat="server" Text="0"></asp:Label></big>
                                                    </td> 
                                                </tr>
                                                <tr>     
                                                    <td style="padding-right:5px;">
                                                        <big><asp:Label ID="lblInteres" runat="server" Text="INTERES" Font-Bold="true"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <big><asp:Label ID="lblValorInteres" runat="server" Text="0"></asp:Label></big>
                                                    </td>
                                                    <td></td>
                                                    <td style="padding-right:5px;">
                                                        <big><asp:Label ID="lblSemanas" runat="server" Text="SEMANAS" Font-Bold="true"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSemanas" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSemanas_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    </td > 
                                                    <td>
                                                        <div>
                                                            <asp:ImageButton ID="imgBtnBuscaSubSemana" runat="server" 
                                                                AlternateText="Busca Sub Semana" 
                                                                ImageUrl="~/Public/image/candado.png"
                                                                Height="30" Width="30" Visible="true" OnClick="imgBtnBuscaSubSemana_Click"/>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <%--aqui va la parte de modal windows--%>
                                                            <asp:Panel ID="panelModalSubCliente" class="modalPopup" runat="server" Style="display: none; width:400px; height:400px; overflow:scroll; resize: vertical;" align="center">
                                                                <div class="header">
                                                                    Autorización encargado
                                                                </div>
                                                                <div class="body">
                                                                    <iframe style="width: 300px; height: 300px;" id="Iframe1" src="AutorizacionEncargado.aspx" runat="server"></iframe>
                                                                </div>
                                                                <div class="footer" align="center">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="btnCancelMSubCliente" runat="server" Text="Cancelar" class="no" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnAceptMSubCliente" runat="server" Text="Aceptar" class="yes" OnClick="btnAcept_Click"/>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </asp:Panel>
                                                            <ajaxToolkit:ModalPopupExtender 
                                                                ID="ModalPopupExtender2" 
                                                                runat="server"
                                                                CancelControlID="btnCancelMSubCliente"
                                                                PopupControlID="panelModalSubCliente"
                                                                TargetControlID="imgBtnBuscaSubSemana"
                                                                BackgroundCssClass="modalBackground"
                                                                PopupDragHandleControlID="panelModalSubCliente"
                                                                Drag="true"
                                                                RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                DropShadow="false">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                        </div>
                                                    </td> 
                                                </tr>
                                                <tr>     
                                                    <td>
                                                        <asp:Label ID="lblAbonoCapital" runat="server" Text="ABONO A CAPITAL"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAbonoCapital" runat="server" CssClass="form-control" Value="0.00" type="number" step="0.01"></asp:TextBox>
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
                                                            <asp:BoundField DataField="Precio" HeaderText="<center>Sub Total</center>" SortExpression="Precio"  HtmlEncode="false" DataFormatString="{0:N2}"/>                                                            
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

