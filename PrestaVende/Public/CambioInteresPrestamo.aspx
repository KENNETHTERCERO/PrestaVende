<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="CambioInteresPrestamo.aspx.cs" Inherits="PrestaVende.Public.CambioInteresPrestamo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                <asp:Button ID="btnGuardar" runat="server" Width="200px" Text="Actualizar" class="btn btn-success" OnClick="btnGuardar_Click"  />
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
                        <div class="panel panel-primary" >
                            <div class="panel-heading" >
                                <div style="position: relative;">
                                                <table style="width: 100%; text-align:center">
                                                    <tr >
                                                        <td >
                                                            <h1> Actualización de Interés en préstamo &nbsp;&nbsp;</h1>
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
                                                        <asp:Button ID="btnBuscar" runat="server" Width="200px" Text="Buscar" class="btn btn-info" OnClick="btnBuscar_Click" />                                
                                                    </td>
                                                </tr>
                                                </table>
                                         </div>
                                    <br />

                                                                                                   

                                    <div id="div_DatosPrestamo" runat="server" visible ="false">
                                        <br />
                                      <table>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <big><asp:Label ID="lblEmpresaE" runat="server" Text="EMPRESA"></asp:Label></big>
                                                        </b>
                                                    </td>
                                                    <td>
                                                        <br />
                                                    </td>
                                                    <td>
                                                        <center>
                                                         <big><asp:Label ID="lblEmpresa" runat="server" Text=""></asp:Label></big>
                                                        </center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                       <b> <big><asp:Label ID="lblSucursalE" runat="server" Text="SUCURSAL"></asp:Label></big></b>
                                                    </td>
                                                    <td>
                                                        <br />
                                                    </td>
                                                    <td>
                                                         <center>
                                                             <big><asp:Label ID="lblSucursal" runat="server" Text=""></asp:Label></big>
                                                         </center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                 <tr>
                                                    <td>
                                                       <b> <big><asp:Label ID="lblNoPrestamoE" runat="server" Text=" NÚMERO PRÉSTAMO "></asp:Label></big></b>
                                                    </td>
                                                     <td>
                                                        <br />
                                                    </td>
                                                    <td>
                                                        <center>
                                                            <big><asp:Label ID="lblNoPrestamo" runat="server" Text=""></asp:Label></big>
                                                        </center>
                                                    </td>
                                                </tr> 
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b><big><asp:Label ID="lblFechaUPE" runat="server" Text="FECHA ÚLTIMO PAGO "></asp:Label></big></b>
                                                    </td>
                                                    <td>
                                                        <br />
                                                    </td>
                                                    <td>
                                                        <center>
                                                            <big><asp:Label ID="lblFechaUP" runat="server" Text=""></asp:Label></big>
                                                        </center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b><big><asp:Label ID="lblFechaPPE" runat="server" Text="FECHA PRÓXIMO PAGO"></asp:Label></big></b>
                                                    </td>
                                                    <td>
                                                        <br />
                                                    </td>
                                                    <td>
                                                         <center>
                                                             <big><asp:Label ID="lblFechaPP" runat="server" Text=""></asp:Label></big>
                                                         </center>
                                                    </td>
                                                </tr>    
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b><big><asp:Label ID="lblEstadoE" runat="server" Text="ESTADO DEL PRÉSTAMO"></asp:Label></big></b>
                                                    </td>
                                                    <td>
                                                        <br />
                                                    </td>
                                                    <td>
                                                         <center>
                                                             <big><asp:Label ID="lblEstado" runat="server" Text=""></asp:Label></big>
                                                         </center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b><big><asp:Label ID="lblSaldoE" runat="server" Text="SALDO ACTUAL"></asp:Label></big></b>
                                                    </td>
                                                    <td>
                                                        <br />
                                                    </td>
                                                    <td>
                                                         <center>
                                                             <big><asp:Label ID="lblSaldo" runat="server" Text=""></asp:Label></big>
                                                         </center>
                                                    </td>
                                                </tr> 
                                                 <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b><big><asp:Label ID="lblInteres" runat="server" Text="INTERÉS"></asp:Label></big></b>
                                                    </td>
                                                    <td>
                                                        <br />
                                                    </td>
                                                    <td>
                                                         <center>
                                                             <asp:DropDownList ID="ddlIntereses" runat="server" AutoPostBack="true" CssClass="form-control" ></asp:DropDownList>
                                                         </center>
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
