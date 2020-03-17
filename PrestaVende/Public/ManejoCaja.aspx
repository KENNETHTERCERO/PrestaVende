<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="ManejoCaja.aspx.cs" Inherits="PrestaVende.Public.ManejoCaja" %>
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
                                <asp:Button ID="btnGuardar" runat="server" Width="200px" Text="Guardar" class="btn btn-success" Visible="true" OnClick="btnGuardar_Click"/>
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
                                                            <h1>Manejo caja general &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
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
                                                    <td >
                                                        <asp:Label ID="lblSerie" runat="server" Font-Bold="true">SUCURSAL</asp:Label>
                                                    </td>
                                                    
                                                    <td>
                                                        <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <asp:Label ID="lblCajaGeneral" runat="server" Font-Bold="true">CAJA GENERAL</asp:Label>
                                                    </td>
                                                    
                                                    <td>
                                                        <asp:DropDownList ID="ddlCajaGeneral" runat="server" CssClass="form-control" ></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <asp:Label ID="lblTipoMovimiento" runat="server" Font-Bold="true">TIPO MOVIMIENTO</asp:Label>
                                                    </td>
                                                    
                                                    <td>
                                                        <asp:DropDownList ID="ddlTipoMovimiento" runat="server" CssClass="form-control" ></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height:20px;"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMonto" runat="server" Text="MONTO" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMonto" runat="server" text ="0" type="number" min="0" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
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
