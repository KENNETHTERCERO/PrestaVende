<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="WFBusquedaClienteFacturacion.aspx.cs" Inherits="PrestaVende.Public.WFBusquedaClienteFacturacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script>
    function button_click(objTextBox,objBtnID)
    {
        if(window.event.keyCode==13)
        {
            document.getElementById(objBtnID).focus();
            document.getElementById(objBtnID).click();
        }
    }
</script>--%>

    <script type="text/javascript">
        function doClick(buttonName,e)
        {
            //the purpose of this function is to allow the enter key to 
            //point to the correct button to click.
            var key;

             if(window.event)
                  key = window.event.keyCode;     //IE
             else
                  key = e.which;     //firefox

            if (key == 13)
            {
                //Get the button the user wants to have clicked
                var btn = document.getElementById(buttonName);
                if (btn != null)
                { //If we find the button click it
                    btn.click();
                    event.keyCode = 0
                }
            }
       }
    </script>
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
                                <asp:Button ID="btnBack" runat="server" Width="200px" Text="Regresar" class="btn btn-default" Visible="true" OnClick="btnBack_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnCreateClient" runat="server" Width="200px" Text="Crear cliente" class="btn btn-primary" Visible="true" OnClick="btnCreateClient_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnGuardarUsuario" runat="server" Width="200px" Text="Guardar cliente" class="btn btn-info" Visible="false" OnClick="btnGuardarUsuario_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnAtras" runat="server" Width="200px" Text="Cancelar" class="btn btn-warning" Visible="false" OnClick="btnAtras_Click" />
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
                                    <asp:Panel id="panSearch" runat="server" DefaultButton="btnBuscarCliente">
                                        <table>
                                            <tr>
                                                <td>
                                                    <h1>Búsqueda de cliente&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBusquedaCliente" runat="server" class="form-control" Width="300px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnBuscarCliente" runat="server" class="btn btn-success" Text="Buscar cliente" OnClick="btnBuscarCliente_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
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
                                                    <asp:GridView ID="gvCliente" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None" OnRowCommand="gvCliente_RowCommand" OnSelectedIndexChanged="gvCliente_SelectedIndexChanged" >
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-success" FooterStyle-BackColor="#ff9a32" CommandName="crear" HeaderText="<center>Crear<br/>Factura </center>" Text="->" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                            <asp:ButtonField ButtonType="Button" Visible="false" ControlStyle-CssClass="btn btn-success" FooterStyle-BackColor="#ff9a32" CommandName="editar" HeaderText="<center>Editar<br/>cliente</center>" Text="<>" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="id_cliente" HeaderText="<center>ID</center>" SortExpression="id_cliente" HtmlEncode="false"/>
                                                            <asp:BoundField DataField="DPI" HeaderText="<center>DPI</center>" SortExpression="DPI" HtmlEncode="false" />
                                                            <asp:BoundField DataField="nit" HeaderText="<center>NIT</center>" SortExpression="nit" HtmlEncode="false" />
                                                            <asp:BoundField DataField="nombre_completo" HeaderText="<center>NOMBRE</center>" SortExpression="nombre_completo" HtmlEncode="false" />
                                                            <asp:BoundField DataField="direccion" HeaderText="<center>DIRECCION</center>" SortExpression="direecion"  HtmlEncode="false"/>
                                                            <asp:BoundField DataField="estadoLetras" HeaderText="<center>Estado</center>" SortExpression="estadoLetras"  HtmlEncode="false"/>
                                                            <asp:BoundField DataField="estado" HeaderText="<center>IES</center>" SortExpression="estado" HtmlEncode="false" Visible="true"/>
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

