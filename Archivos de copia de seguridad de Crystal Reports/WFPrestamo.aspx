<%@ Page Title="" Language="C#" MasterPageFile="~/Public/PrestaVende.Master" AutoEventWireup="true" CodeBehind="WFPrestamo.aspx.cs" Inherits="PrestaVende.Public.WFPrestamo" %>
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
    </style>
    <script src="JS/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function doClick(buttonName, e) {
            //the purpose of this function is to allow the enter key to 
            //point to the correct button to click.
            var key;

            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 13) {
                //Get the button the user wants to have clicked
                var btn = document.getElementById(buttonName);
                if (btn != null) { //If we find the button click it
                    btn.click();
                    event.keyCode = 0
                }
            }
        }
     <%--   function EnterEvent(e) 
        {
            if ((e.keyCode === 13)) {
                    //alert("1");
                    var btnName = $get("<%=btnBuscarPorCodigo.ClientID%>").name;
                    //alert("2");
                    __doPostBack(btnName, "");
                    //alert("3");
                    e.preventDefault();
                    //alert("4");
                    return false;
                }
        }--%>
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
                                <asp:Button ID="btnBack" runat="server" Width="200px" Text="Regresar" CssClass="btn btn-default" Visible="true" OnClick="btnBack_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnGuardarPrestamo" runat="server" Width="200px" Text="Guardar préstamo" CssClass="btn btn-primary" Visible="true" OnClick="btnGuardarPrestamo_Click"/>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnGuardarAvaluo" runat="server" Width="200px" Text="Guardar avaluo" CssClass="btn btn-info" Visible="false" />
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnProyeccion" runat="server" Width="200px" Text="Proyeccion" CssClass="btn btn-danger" Visible="true" OnClick="btnProyeccion_Click" />
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnVerEstadoDeCuenta" runat="server" Width="200px" Text="Cancelar" CssClass="btn btn-warning" Visible="true" />
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
                                                    <td>
                                                        <asp:Label ID="lblCasilla" runat="server" Text="CASILLA"></asp:Label>
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
                                                    <td>
                                                        <asp:DropDownList ID="ddlCasilla" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                                    <td>
                                                        <div>
                                                                    <asp:ImageButton ID="imgBtnBuscaSubCliente" runat="server" 
                                                                        AlternateText="Busca Sub cliente" 
                                                                        ImageUrl="~/Public/image/candado.png"
                                                                        Height="50" Width="50" Visible="true"/>
                                                                </div>
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
                                                    <td>
                                                        <div>
                                                            <%--aqui va la parte de modal windows--%>
                                                            <asp:Panel ID="panelModalSubCliente" class="modalPopup" runat="server" Style="display: none; width:400px; height:400px; overflow:scroll; resize: vertical;" align="center">
                                                                <div class="header">
                                                                    Autorización encargado
                                                                </div>
                                                                <div class="body">
                                                                    <iframe style="width: 300px; height: 300px;" id="Iframe1" src="AutorizacionEncargado.aspx" runat="server"></iframe>
                                                                </div>
                                                                <div class="footer" align="center">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="btnCancelMSubCliente" runat="server" Text="Cancelar" class="no" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnAceptMSubCliente" runat="server" Text="Aceptar" class="yes" OnClick="btnAcept_Click"/>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </asp:Panel>
                                                            <ajaxToolkit:ModalPopupExtender 
                                                                ID="ModalPopupExtender2"
                                                                runat="server"
                                                                CancelControlID="btnCancelMSubCliente"
                                                                PopupControlID="panelModalSubCliente"
                                                                TargetControlID="imgBtnBuscaSubCliente"
                                                                BackgroundCssClass="modalBackground"
                                                                PopupDragHandleControlID="panelModalSubCliente"
                                                                Drag="true"
                                                                RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                DropShadow="false">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                        </div>
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
                                                        <asp:TextBox ID="txtPeso" runat="server" type="number" step="0.01" AutoPostBack="true" class="form-control" OnTextChanged="txtPeso_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPesoDescuento" type="number" AutoPostBack="true" step="0.01" runat="server" class="form-control" OnTextChanged="txtPesoDescuento_TextChanged"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtValor" type="number" step="0.01" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlIntereses" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success" Text="AGREGAR ARTICULO" OnClick="btnAgregar_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMontoARecalcular" type="number" step="0.01" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnRecalcularValorPrestamoTotal" runat="server" CssClass="btn btn-primary" Text="RECALCULAR" Visible="false" OnClick="btnRecalcularValorPrestamoTotal_Click"/>
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
                                                        ForeColor="#333333" GridLines="None" OnRowCommand="gvProductoJoya_RowCommand">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-danger" FooterStyle-BackColor="#ff9a32" CommandName="borrar" HeaderText="<center>X</center>" Text="X" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="id_producto"              HeaderText="<center>IDJ</center>"               SortExpression="id_producto"             HtmlEncode="false"/>
                                                            <asp:BoundField DataField="numero_linea"           HeaderText="<center>Linea</center>"             SortExpression="numero_linea"          HtmlEncode="false"/>
                                                            <asp:BoundField DataField="joya"            HeaderText="<center>Joya</center>"              SortExpression="articulo"       HtmlEncode="false"/>
                                                            <asp:BoundField DataField="kilataje"        HeaderText="<center>Kilataje</center>"          SortExpression="kilataje"       HtmlEncode="false" />
                                                            <asp:BoundField DataField="peso"            HeaderText="<center>Peso</center>"              SortExpression="peso"           HtmlEncode="false" />
                                                            <asp:BoundField DataField="descuento"       HeaderText="<center>Descuento</center>"         SortExpression="descuento"      HtmlEncode="false" />
                                                            <asp:BoundField DataField="pesoReal"        HeaderText="<center>Peso Real</center>"         SortExpression="pesoReal"       HtmlEncode="false" />
                                                            <asp:BoundField DataField="valor"           HeaderText="<center>Valor</center>"             SortExpression="valor"          HtmlEncode="false" />
                                                            <asp:BoundField DataField="caracteristicas" HeaderText="<center>Caracteristicas</center>"   SortExpression="caracteristicas" HtmlEncode="false"/>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#7C6F57" />
                                                        <FooterStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" />
                                                        <HeaderStyle BackColor="#016965" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#E3EAEB" />
                                                        <SelectedRowStyle BackColor="#ff9a32" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvProductoElectrodomesticos" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" 
                                                        ForeColor="#333333" GridLines="None" OnRowCommand="gvProductoElectrodomesticos_RowCommand">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-danger" FooterStyle-BackColor="#ff9a32" CommandName="borrar" HeaderText="<center>Borrar</center>" Text="X" >
                                                            <FooterStyle BackColor="#FF9A32" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="id_producto"         HeaderText="<center>ID</center>"                SortExpression="id_producto"        HtmlEncode="false"/>
                                                            <asp:BoundField DataField="numero_linea"               HeaderText="<center>Linea</center>"             SortExpression="numero_linea"          HtmlEncode="false"/>
                                                            <asp:BoundField DataField="producto"            HeaderText="<center>Producto</center>"          SortExpression="producto"           HtmlEncode="false" />
                                                            <asp:BoundField DataField="marca"               HeaderText="<center>Marca</center>"             SortExpression="marca"              HtmlEncode="false" />
                                                            <asp:BoundField DataField="valor"               HeaderText="<center>Valor</center>"             SortExpression="valor"              HtmlEncode="false" />
                                                            <asp:BoundField DataField="caracteristicas"     HeaderText="<center>Caracteristicas</center>"   SortExpression="caracteristicas"    HtmlEncode="false"/>
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
