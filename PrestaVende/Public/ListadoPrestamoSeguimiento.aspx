<%@ Page Language="C#"  MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="ListadoPrestamoSeguimiento.aspx.cs" Inherits="PrestaVende.Public.ListadoPrestamoSeguimiento" %>

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
                                <div>
                                    <asp:Panel ID="panelSeguimiento" class="modalPopup" runat="server" Style="display: none; width:750px; height:400px; overflow:scroll; resize: vertical;" align="center">
                                        <div class="header">
                                            Ingreso de seguimiento.
                                        </div>
                                        <div class="body">
                                            <table style="width: 80%;margin-left: 10%;">
                                                <tr>
                                                    <td><br /></td>
                                                    <td><br /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTipoSeguimiento" runat="server">Tipo Seguimiento: </asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTipoSeguimiento" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><br /></td>
                                                    <td><br /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDescripcion" runat="server">Descripcion Seguimiento: </asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDescripcion" TextMode="multiline" Columns="50" Rows="5" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>   
                                        </div>
                                        <div class="footer" align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnCancelSeguimiento" runat="server" Text="Cancelar" class="no" OnClick="btnCancelarModal_Click"/>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnAceptSeguimiento" runat="server" Text="Aceptar" class="yes" OnClick="btnAceptModal_Click"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                    <ajaxToolkit:ModalPopupExtender 
                                        ID="ModalPopupExtenderProyeccion"
                                        runat="server"
                                        CancelControlID="btnCancelSeguimiento"
                                        PopupControlID="panelSeguimiento"
                                        TargetControlID="btnSeguimiento"
                                        BackgroundCssClass="modalBackground"
                                        PopupDragHandleControlID="panelSeguimiento"
                                        Drag="true"
                                        RepositionMode="RepositionOnWindowResizeAndScroll"
                                        DropShadow="false">
                                    </ajaxToolkit:ModalPopupExtender>
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
                                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-success" FooterStyle-BackColor="#ff9a32" CommandName="crear" HeaderText="<center>Ver<br/>Seguimientos </center>" Text="->" >
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
                                                            <asp:BoundField DataField="garantia" HeaderText="<center>Garantia</center>" SortExpression="garantia" HtmlEncode="false" Visible="true"/>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#7C6F57" />
                                                        <FooterStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" />
                                                        <HeaderStyle BackColor="#016965" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#E3EAEB" />
                                                        <SelectedRowStyle BackColor="#ff9a32" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>     
                                                    <br />
                                                    <asp:Button ID="btnSeguimiento" runat="server" Width="200px" Text="Seguimientos" CssClass="btn btn-danger" Visible="false"/> 
                                                    <b><big><asp:Label ID="lblSeguimientoNumeroPrestamo" runat="server" Visible="false">&nbsp;&nbsp;PRESTAMO:&nbsp;&nbsp;</asp:Label></big></b>
                                                    <b><big><asp:Label ID="lblNumeroPrestamoSeguimiento" runat="server" Visible="false"></asp:Label></big></b>
                                                    <br />                                                        
                                                    <br />      
                                                    <asp:GridView ID="gvSeguimientos" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField="tipo_seguimiento"    HeaderText="<center>Tipo Seguimiento</center>"  SortExpression="tipo_seguimiento"        HtmlEncode="false"/>
                                                            <asp:BoundField DataField="descripcion"         HeaderText="<center>Descripcion</center>"       SortExpression="descripcion"          HtmlEncode="false"/>
                                                            <asp:BoundField DataField="fecha_creacion"            HeaderText="<center>Fecha</center>"    SortExpression="fecha_creacion"           HtmlEncode="false" />
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
