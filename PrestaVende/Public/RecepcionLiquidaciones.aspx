<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="RecepcionLiquidaciones.aspx.cs" Inherits="PrestaVende.Public.RecepcionLiquidaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
     <asp:UpdatePanel runat="server" ID="uPanel" >
        <ContentTemplate>
            <div class="container-fluid text-center" >
                <div class="row content">
                    <div class="col-sm-2 sidenav">
                        <center>
                        <h2>Opciones</h2>
                            <div class=".btn-group-vertical">
                                <br />
                                <br />
                                <br />
                                <button id="btnBack" onclick="goBack()" style="width: 200px;" class="btn btn-default">Regresar</button>
                                <br />
                                <br />
                                <br />                                                                                                                
                                <asp:Button ID="btnAceptar" runat="server" Width="200px" Text="Agregar a inventario" class="btn btn-success"  OnClick="btnAceptar_Click"/>
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
                        <div class="panel panel-primary" >
                            <div class="panel-heading" >
                                <div style="position: relative;">
                                                <table style="width: 100%; text-align:center">
                                                    <tr >
                                                        <td >
                                                            <h1> Recepción liquidaciones  &nbsp;&nbsp;</h1>
                                                        </td>
                                                       
                                                    </tr>
                                                </table>
                                            </div>
                            </div>
                            <div class="panel-body">
                                <center>
                                    <br />
                                    <br />
                                     <div  id="idv_datos_busqueda" runat="server" visible="true">
                                            <table style="width:60% ; text-align:center">
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="LblPrestamo" runat="server" Text="Préstamo"></asp:Label></big>
                                                    </td>
                                                   <td>
                                                       &nbsp;
                                                   </td>
                                                    <td >
                                                       
                                                            <asp:TextBox ID="TxtPrestamo" class="form-control" Width="250px" runat="server"  ></asp:TextBox>
                                                        
                                                    </td>
                                                    <td>
                                                        <%--<asp:ImageButton ID="BtnBuscar" runat="server" OnClick="BtnBuscar_Click" ImageUrl="~/Imagenes/search.png" Width="30"/>--%>
                                                        <asp:Button ID="btnBuscar" runat="server" Width="200px" Text="Buscar" class="btn btn-info" OnClick="btnBuscar_Click"/>                                
                                                    </td>
                                                </tr>
                                                </table>
                                         </div>
                                    <br />

                                                                                                   

                                    <div id="div_gridView" runat="server" visible ="false">
                                        <br />
                                        <table >
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GrdVLiquidacion" runat="server" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None" >
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>

                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="SelectedCheckBox" runat="server" AutoPostBack="True"  />
                                                                  </ItemTemplate>
                                                           </asp:TemplateField>
                                                                
                                                                <%--<asp:BoundField DataField="id_liquidacion" HeaderText="<center>ID</center>" SortExpression="id_liquidacion" HtmlEncode="false"/>--%>                                                            
                                                                <%--<asp:BoundField DataField="sucursal" HeaderText="<center>SUCURSAL</center>" SortExpression="sucursal" HtmlEncode="false" />--%>
                                                                <asp:BoundField DataField="numero_prestamo" HeaderText="<center>NUMERO DE PRESTAMO</center>" SortExpression="numero_prestamo" HtmlEncode="false" />
                                                                <asp:BoundField DataField="id_prestamo_detalle" HeaderText="<center> ID </center>" SortExpression="numero_prestamo"  HtmlEncode="false" />
                                                                <asp:BoundField DataField="id_producto" HeaderText="<center>ID PRODUCTO</center>" SortExpression="numero_prestamo" HtmlEncode="false" Visible="false" />
                                                                <asp:BoundField DataField="numero_linea" HeaderText="<center>LINEA</center>" SortExpression="numero_prestamo" HtmlEncode="false" />
                                                                <asp:BoundField DataField="producto" HeaderText="<center>PRODUCTO</center>" SortExpression="producto"  HtmlEncode="false"/>                                                                
                                                                <asp:BoundField DataField="cantidad" HeaderText="<center>CANTIDAD</center>" SortExpression="cantidad"  HtmlEncode="false"/>
                                                                <asp:BoundField DataField="valor" HeaderText="<center>VALOR PRESTADO</center>" SortExpression="valor"  HtmlEncode="false"/>
                                                                <asp:BoundField DataField="monto_liquidacion" HeaderText="<center>MONTO LIQUIDADO</center>" SortExpression="monto_liquidacion"  HtmlEncode="false"/>
                                                                <asp:BoundField DataField="precio_sugerido" HeaderText="<center>PRECIO SUGERIDO</center>" SortExpression="precio_sugerido"  HtmlEncode="false"/>
                                                                <asp:TemplateField HeaderText=" <center> PRECIO </center>" HeaderStyle-HorizontalAlign="Center" >
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtPrecio" runat="server" text="0.00" Width="80px"></asp:TextBox>
                                                                    </ItemTemplate>                                                                    
                                                                </asp:TemplateField>
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
