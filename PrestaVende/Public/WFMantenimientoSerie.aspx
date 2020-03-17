<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="WFMantenimientoSerie.aspx.cs" Inherits="PrestaVende.Public.WFMantenimientoSerie" %>
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
                                <button id="btnBack" onclick="goBack()" style="width: 200px;" class="btn btn-default">Regresar</button>
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
                                                            <h1>Mantenimiento Serie &nbsp;&nbsp;</h1>
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
                                    <div id="div_ingresa_datos" runat="server" visible="true">
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblIdSerie" runat="server" Text="CODIGO SERIE"></asp:Label></big>
                                                    </td>
                                                    <td>
                                                        <center>
                                                        <asp:Label ID="ddidSerie" runat="server" Text="0"  ></asp:Label>
                                                        </center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblIdSucursal" runat="server" Text="SUCURSAL "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:DropDownList ID="ddidSucursal" runat="server" class="form-control"></asp:DropDownList>
                                                    </td>
                                                </tr> 

                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblSerie" runat="server" Text="SERIE "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtSerie" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>  

                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblResolucion" runat="server" Text="RESOLUCION "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtResolucion" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>    
                                                
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblFechaResolucion" runat="server" Text="FECHA RESOLUCION "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtFechaResolucion" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                                    </td>
                                                </tr>                                                
                                                
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblNumeroDeFacturas" runat="server" Text="NUMERO DE FACTURAS "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtNumeroDeFacturas" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>    
                                                
                                                <tr>
                                                    <td>
                                                        <big><asp:Label ID="lblEstado" runat="server" Text="ESTADO "></asp:Label></big>
                                                    </td>
                                                    <td>
                                                         <asp:DropDownList ID="ddEstado" runat="server" class="form-control"></asp:DropDownList>
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
                                                    <asp:GridView ID="GrdVSerie" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None" OnRowCommand="gvSize_RowCommand">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" FooterStyle-BackColor="#ff9a32" CommandName="select" HeaderText="" Text="->" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                                <asp:BoundField DataField="id_serie" HeaderText="<center>ID </center>" SortExpression="id_serie" HtmlEncode="false"/>                                                            
                                                                <asp:BoundField DataField="serie" HeaderText="<center>SERIE </center>" SortExpression="serie" HtmlEncode="false" />
                                                                <asp:BoundField DataField="resolucion" HeaderText="<center>RESOLUCION </center>" SortExpression="resolucion" HtmlEncode="false" />
                                                                <asp:BoundField DataField="fecha_resolucion" HeaderText="<center>FECHA RESOLUCION </center>" SortExpression="fecha_resolucion"  HtmlEncode="false"/>  
                                                                <asp:BoundField DataField="estado" HeaderText="<center>ESTADO </center>" SortExpression="estado"  HtmlEncode="false"/>                                                                                                                               
                                                                <asp:BoundField DataField="sucursal" HeaderText="<center>SUCURSAL </center>" SortExpression="sucursal"  HtmlEncode="false"/>                                                                
                                                                <asp:BoundField DataField="correlativo" HeaderText="<center>CORRELATIVO </center>" SortExpression="correlativo"  HtmlEncode="false"/>                                                                
                                                                <asp:BoundField DataField="numero_de_facturas" HeaderText="<center>NUMERO DE FACTURAS </center>" SortExpression="numero_de_facturas"  HtmlEncode="false"/>                                                              
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
