﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="WFPrestamo.aspx.cs" Inherits="PrestaVende.Public.WFPrestamo" %>
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
                                <asp:Button ID="btnGuardarPrestamo" runat="server" Width="200px" Text="Guardar préstamo" class="btn btn-primary" Visible="true"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnGuardarAvaluo" runat="server" Width="200px" Text="Guardar avaluo" class="btn btn-info" Visible="false" />
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
                                                            <h1>Creación de préstamo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
                                                        </td>
                                                        <td>
                                                            <big><asp:Label ID="lblCliente" runat="server">Cliente:&nbsp;&nbsp;</asp:Label></big>
                                                        </td>
                                                        <td>
                                                            <big><asp:Label ID="lblid_cliente" runat="server" Text="0"></asp:Label></big>
                                                            <big><asp:Label ID="lblnombre_cliente" runat="server"></asp:Label></big>
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
                                                    <td>
                                                        <asp:Label ID="lblNumeroPrestamo" runat="server" >NUMERO PRESTAMO</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTipoPrestamo" runat="server">TIPO PRESTAMO</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTotalPrestamo" runat="server" Text="TOTAL PRESTAMO"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblNumeroPrestamoNumero" runat="server" Text="0"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTipoPrestamo" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Q"></asp:Label>
                                                        <asp:Label ID="lblTotalPrestamoQuetzales" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCategoria" runat="server" Text="CATEGORIA"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSubCategoria" runat="server" Text="SUBCATEGORIA"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblProducto" runat="server" Text="PRODUCTO"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSubCategoria" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSubCategoria_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPeso" runat="server" Text="PESO"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPesoDescuento" runat="server" Text="PESO DESCUENTO"></asp:Label>
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPesoConDescuento" runat="server" Text="PESO CON DESCUENTO"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtPeso" runat="server" type="number" AutoPostBack="true" class="form-control" OnTextChanged="txtPeso_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPesoDescuento" type="number" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtPesoDescuento_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPesoConDescuento" type="number" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblKilataje" runat="server" Text="KILATAJE"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblObservaciones" runat="server" Text="OBSERVACIONES"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlKilataje" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlKilataje_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtObservaciones" runat="server" class="form-control"></asp:TextBox>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMarca" runat="server" Text="MARCA"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCaracteristicas" runat="server" Text="CARACTERISTICAS"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlMarca" runat="server" class="form-control"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCaracteristicas" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblValor" runat="server" Text="VALOR"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblIntereses" runat="server" Text="INTERESES"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtValor" type="number" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlIntereses" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success" Text="AGREGAR ARTICULO" />
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
                                                    <asp:GridView ID="gvProductoJoya" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-danger" FooterStyle-BackColor="#ff9a32" CommandName="crear" HeaderText="<center>X</center>" Text="->" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="id_producto" HeaderText="<center>ID</center>" SortExpression="id_producto" HtmlEncode="false"/>
                                                            <asp:BoundField DataField="peso" HeaderText="<center>Peso</center>" SortExpression="peso" HtmlEncode="false" />
                                                            <asp:BoundField DataField="kilataje" HeaderText="<center>Kilataje</center>" SortExpression="kilataje" HtmlEncode="false" />
                                                            <asp:BoundField DataField="valor" HeaderText="<center>Valor</center>" SortExpression="valor" HtmlEncode="false" />
                                                            <asp:BoundField DataField="caracteristicas" HeaderText="<center>Caracteristicas</center>" SortExpression="caracteristicas"  HtmlEncode="false"/>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#7C6F57" />
                                                        <FooterStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" />
                                                        <HeaderStyle BackColor="#016965" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#E3EAEB" />
                                                        <SelectedRowStyle BackColor="#ff9a32" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvProductoElectrodomesticos" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-danger" FooterStyle-BackColor="#ff9a32" CommandName="crear" HeaderText="<center>X</center>" Text="->" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="id_producto" HeaderText="<center>ID</center>" SortExpression="id_producto" HtmlEncode="false"/>
                                                            <asp:BoundField DataField="producto" HeaderText="<center>Producto</center>" SortExpression="producto" HtmlEncode="false" />
                                                            <asp:BoundField DataField="marca" HeaderText="<center>Marca</center>" SortExpression="marca" HtmlEncode="false" />
                                                            <asp:BoundField DataField="valor" HeaderText="<center>Valor</center>" SortExpression="valor" HtmlEncode="false" />
                                                            <asp:BoundField DataField="caracteristicas" HeaderText="<center>Caracteristicas</center>" SortExpression="caracteristicas"  HtmlEncode="false"/>
                                                            <asp:BoundField DataField="id_marca" HeaderText="<center>IDM</center>" SortExpression="id_marca" HtmlEncode="false"/>
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
