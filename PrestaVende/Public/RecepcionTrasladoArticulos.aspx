<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="RecepcionTrasladoArticulos.aspx.cs" Inherits="PrestaVende.Public.RecepcionTrasladoArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            top: auto;
            background-color: #FFFFFF;
            width: auto;
            border: 3px solid #ff9a32;
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #016a66;
                height: auto;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 9px;
                border-top-right-radius: 9px;
            }

            .modalPopup .body {
                /*min-height: auto;
                line-height: 30px;*/
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #016965;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

        div#divDataClienteSelect {
            border: 1px solid skyblue;
            width: 101%;
        }
        div#divSubCliente {
            border: 1px solid skyblue;
        }
        div#divBtnBusqueda {
            float: left;
        }
        div#divDataClienteSelect {
            float: left;
        }
        div#divPlanta, div#divEncabezadoCaja, div#divCantidadPeso, div#divButton {
            border-top: 1px solid skyblue;
            text-align: center;
        }
        .auto-style1 {
            height: 36px;
        }
    </style>
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
                                <asp:Button ID="btnAceptar" runat="server" Width="200px" Text="Aceptar" CssClass="btn btn-primary" Visible="true" OnClick="btnAceptar_Click" />
                                <br />
                                <br />
                                <br />
                                 <asp:Button ID="btnCancelar" runat="server" Width="200px" Text="Cancelar" CssClass="btn btn-warning" Visible="true" OnClick="btnCancelar_Click" />
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
                                    <h1>Recepción de artículos trasladados entre sucursales&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
                                    <table>
                                        <tr>                                            
                                            <td><big>
                                                <asp:Label ID="lblSucursal" runat="server">SUCURSAL:&nbsp;&nbsp;</asp:Label>
                                                </big></td>
                                            <td colspan="2">
                                                 <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td><big>
                                                <asp:Label ID="lblSerie" runat="server" Text="SERIE:"></asp:Label>
                                                </big></td>
                                            <td>
                                                <asp:DropDownList ID="ddlSerie" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSerie_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>                                            
                                        </tr>
                                         <tr>
                                            <td><big>
                                                <asp:Label ID="lblRecibo" runat="server" Text="RECIBOS PENDIENTES:"></asp:Label>
                                                </big></td>
                                            <td>
                                                <asp:DropDownList ID="ddlRecibos" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlRecibos_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
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
                                                    <asp:GridView ID="gvProductoTraslado" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None" OnRowCommand="gvProductoTraslado_RowCommand">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-danger" FooterStyle-BackColor="#ff9a32" CommandName="borrar" HeaderText="<center>Borrar</center>" Text="X" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="id_inventario"         HeaderText="<center>ID</center>"                SortExpression="id_inventario"        HtmlEncode="false"/>
                                                            <asp:BoundField DataField="sucursal_origen"        HeaderText="<center>Sucursal Origen</center>"             SortExpression="sucursal_origen"          HtmlEncode="false"/>
                                                            <asp:BoundField DataField="sucursal_destino"        HeaderText="<center>Sucursal Destino</center>"             SortExpression="sucursal_destino"          HtmlEncode="false"/>
                                                            <asp:BoundField DataField="producto"            HeaderText="<center>Producto</center>"          SortExpression="producto"           HtmlEncode="false" />
                                                            <asp:BoundField DataField="marca"               HeaderText="<center>Marca</center>"             SortExpression="marca"              HtmlEncode="false" />
                                                            <asp:BoundField DataField="monto_prestado"               HeaderText="<center>Valor prestado</center>"             SortExpression="monto_prestado"              HtmlEncode="false" />
                                                            <asp:BoundField DataField="valor"               HeaderText="<center>Valor Actual</center>"             SortExpression="valor"              HtmlEncode="false" />
                                                            <asp:TemplateField HeaderText=" <center> Precio </center>" HeaderStyle-HorizontalAlign="Center" >
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtPrecio" runat="server" text="0.00" Width="80px"></asp:TextBox>
                                                                    </ItemTemplate>                                                                    
                                                                </asp:TemplateField>
                                                            <asp:BoundField DataField="caracteristicas"     HeaderText="<center>Caracteristicas</center>"   SortExpression="caracteristicas"    HtmlEncode="false"/>
                                                            <asp:TemplateField HeaderText=" <center> Observaciones </center>" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtObservaciones" runat="server" text="" TextMode="MultiLine" style="overflow:hidden"  ></asp:TextBox>                                                                        
                                                                    </ItemTemplate>                                                                    
                                                                </asp:TemplateField>
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

