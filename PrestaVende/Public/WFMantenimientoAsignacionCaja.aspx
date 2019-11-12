<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeFile="WFMantenimientoAsignacionCaja.aspx.cs" Inherits="PrestaVende.Public.WFMantenimientoAsignacionCaja" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="uPanel" >
        <ContentTemplate>
            <div class="container-fluid text-center">
                <div class="row content">
                    <div class="col-sm-2 sidenav">
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
                                                            <h1>Mantenimiento Asignación de Caja &nbsp;&nbsp;</h1>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </div>
                            </div>
                            <div class="panel-body" >
                                <center>
                                    <br />
                                    <br />
                                    <div id="div_ingresa_datos" runat="server" visible="true">
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblIdAsignacion" runat="server" Text="ID ASIGNACIÓN"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <center>
                                                        <asp:Label ID="ddidAsignacion" runat="server" Text="0"  ></asp:Label>
                                                        </center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblCaja" runat="server" Text="CAJA"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:DropDownList ID="ddIdCaja" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddIdCaja_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                </tr>         
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblEstadoCaja" runat="server" Text="ESTADO CAJA "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddIdEstadoCaja" runat="server" class="form-control"   />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblMontoActual" runat="server" Text="MONTO ACTUAL"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtMontoActual" type="decimal" text="0" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblMonto" runat="server" Text="MONTO"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtMonto" type="decimal" text="0" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>                                                                                                       
                                                   
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblUsuarioAsignado" runat="server" Text="USUARIO ASIGNADO"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:DropDownList ID="ddIdUsuarioAsignado" runat="server" class="form-control"></asp:DropDownList>
                                                    </td>
                                                </tr>   
                                                <tr>
                                                   <td>
                                                       <big><asp:Label ID="lblRecibir" runat="server" Text="RECIBIR"></asp:Label></big>
                                                   </td>
                                                    <td>    
                                                         <asp:CheckBox ID="ChbxRecibir" runat="server" oncheckedchanged="ChbxRecibir_CheckedChanged" AutoPostBack="true" class="form-control">

                                                         </asp:CheckBox>
                                                    </td>
                                                </tr>
                                                                                                                                                                                                                                                                                 
                                              </table>
                                        </div>
                                    </div>                                       

                                    <div id="div_gridView" runat="server" visible ="false">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <div style="overflow:auto; width:100%; height:300px; align:left;">
                                                    <asp:GridView ID="GrdVAsignacionCaja" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None" OnRowCommand="gvSize_RowCommand" OnSelectedIndexChanged="GrdVAsignacionCaja_SelectedIndexChanged">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" FooterStyle-BackColor="#ff9a32" CommandName="select" HeaderText="" Text="->" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                                <asp:BoundField DataField="id_asignacion_caja" HeaderText="<center>ID</center>" SortExpression="id_asignacion_caja" HtmlEncode="false"/>                                                            
                                                                <asp:BoundField DataField="id_caja" HeaderText="<center>NO CAJA</center>" SortExpression="id_caja" HtmlEncode="false" />
                                                                <asp:BoundField DataField="nombre_caja" HeaderText="<center>CAJA</center>" SortExpression="nombre_caja" HtmlEncode="false" />
                                                                <asp:BoundField DataField="estado_caja" HeaderText="<center>ESTADO CAJA</center>" SortExpression="estado_caja"  HtmlEncode="false"/>
                                                                <asp:BoundField DataField="monto" HeaderText="<center>MONTO</center>" SortExpression="monto"  HtmlEncode="false"/>                                                                
                                                                <asp:BoundField DataField="estado_asignacion" HeaderText="<center>ESTADO</center>" SortExpression="estado_asignacion"  HtmlEncode="false"/>
                                                                <asp:BoundField DataField="usuario_asigna" HeaderText="<center>USUARIO ASIGNA</center>" SortExpression="usuario_asigna"  HtmlEncode="false"/>
                                                                <asp:BoundField DataField="usuario_asignado" HeaderText="<center>USUARIO ASIGNADO</center>" SortExpression="usuario_asignado"  HtmlEncode="false"/>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#7C6F57" />
                                                        <FooterStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" />
                                                        <HeaderStyle BackColor="#016965" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#E3EAEB" />
                                                        <SelectedRowStyle BackColor="#ff9a32" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>
                                                    </div>
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