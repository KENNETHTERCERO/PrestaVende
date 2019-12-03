<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="RecepcionLiquidaciones.aspx.cs" Inherits="PrestaVende.Public.RecepcionLiquidaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel runat="server" ID="uPanel" >
        <ContentTemplate>
            <div class="container-fluid text-center">
                <div class="row content">
                   <%-- <div class="col-sm-2 sidenav">
                        <center>
                        <h2>Opciones</h2>
                            <div class=".btn-group-vertical">
                                <asp:Button ID="btnSalir" runat="server" Width="200px" Text="Regresar" class="btn btn-default" Visible="false" OnClick="btnSalir_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnCreate" runat="server" Width="200px" Text="Crear" class="btn btn-success" Visible="false" OnClick="btnCreate_Click"/>
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
                    --%><div class="col-sm-8 text-left">
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
                                                            <h1> Recepción liquidaciones  &nbsp;&nbsp;</h1>
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
                                     <div  id="idv_datos_busqueda" runat="server" visible="true">
                                            <table >
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="LblPrestamo" runat="server" Text="Préstamo"></asp:Label></big>
                                                    </td>
                                                    <td>

                                                    </td>
                                                    <td>
                                                        <center>
                                                            <asp:TextBox ID="TxtPrestamo" runat="server" ></asp:TextBox>
                                                        </center>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="BtnBuscar" runat="server" OnClick="BtnBuscar_Click" ImageUrl="~/Imagenes/search.png" Width="30"/>
                                                    </td>
                                                </tr>
                                                </table>
                                         </div>
                                    <br />

                                    <div id="div_DatoSeleccionado" runat="server" visible ="false">
                                        <table>
                                            <tr>
                                                <td width="20%" style="text-align:left">
                                                    Préstamo
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    <asp:Label ID="lblPrestamoT" runat="server"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:Label ID="lblPrestamoDetalle" runat="server" Visible="false"></asp:Label>
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    Producto
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    <asp:Label ID="lblProducto" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <br />
                                            <br />

                                            <tr>
                                                <td width="20%" style="text-align:left">
                                                    Cantidad
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    <asp:Label ID="lblCantidad" runat="server"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    Valor prestado
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    <asp:Label ID="lblValorPrestado" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <br />

                                            <tr>
                                                <td width="20%" style="text-align:left">
                                                    Monto liquidado
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    <asp:Label ID="lblMontoLiquidado" runat="server"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    Precio Sugerido
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    <asp:Label ID="lblPrecioSugerido" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <br />

                                             <tr>
                                                <td width="20%" style="text-align:left">
                                                    Fecha Liquidación
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    <asp:Label ID="lblFechaLiquidacion" runat="server"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    Precio
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <br />

                                             <tr>
                                                <td width="20%" style="text-align:left">
                                                   
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    
                                                </td>
                                                <td width="20%">
                                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar en Inventario" />
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    
                                                </td>
                                                <td width="20%" style="text-align:left">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                                                                                    

                                    <div id="div_gridView" runat="server" visible ="false">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GrdVLiquidacion" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None" OnRowCommand ="GrdVLiquidacion_RowCommand">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" FooterStyle-BackColor="#ff9a32" CommandName="select" HeaderText="" Text="->" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                                <%--<asp:BoundField DataField="id_liquidacion" HeaderText="<center>ID</center>" SortExpression="id_liquidacion" HtmlEncode="false"/>--%>                                                            
                                                                <%--<asp:BoundField DataField="sucursal" HeaderText="<center>SUCURSAL</center>" SortExpression="sucursal" HtmlEncode="false" />--%>
                                                                <asp:BoundField DataField="numero_prestamo" HeaderText="<center>NUMERO DE PRESTAMO</center>" SortExpression="numero_prestamo" HtmlEncode="false" />
                                                                <asp:BoundField DataField="id_prestamo_detalle" HeaderText="<center>ID DETALLE</center>" SortExpression="numero_prestamo" HtmlEncode="false" />
                                                                <asp:BoundField DataField="id_producto" HeaderText="<center>ID PRODUCTO</center>" SortExpression="numero_prestamo" HtmlEncode="false" />
                                                                <asp:BoundField DataField="producto" HeaderText="<center>PRODUCTO</center>" SortExpression="producto"  HtmlEncode="false"/>                                                                
                                                                <asp:BoundField DataField="cantidad" HeaderText="<center>CANTIDAD</center>" SortExpression="cantidad"  HtmlEncode="false"/>
                                                                <asp:BoundField DataField="valor" HeaderText="<center>VALOR PRESTADO</center>" SortExpression="valor"  HtmlEncode="false"/>
                                                                <asp:BoundField DataField="monto_liquidacion" HeaderText="<center>MONTO LIQUIDADO</center>" SortExpression="monto_liquidacion"  HtmlEncode="false"/>
                                                                <asp:BoundField DataField="precio_sugerido" HeaderText="<center>PRECIO SUGERIDO</center>" SortExpression="precio_sugerido"  HtmlEncode="false"/>
                                                               <%-- <asp:TemplateField HeaderText="PRECIO" >
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtPrecio" runat="server" ></asp:TextBox>
                                                                    </ItemTemplate>                                                                    
                                                                </asp:TemplateField>--%>
                                                                <asp:BoundField DataField="fecha_liquidacion" HeaderText="<center>FECHA LIQUIDACION</center>" SortExpression="fecha_liquidacion"  HtmlEncode="false"/>
                                                                
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
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnAceptar" runat="server" OnClick="btnAceptar_Click"  />
                                                </td>
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
                    <br />
                </div>
            </div>           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
