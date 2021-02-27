<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProyeccionInteres.aspx.cs" Inherits="PrestaVende.Public.ProyeccionInteres" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvProyeccion" runat="server" Width="100%" CssClass="footable" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="id_semana" HeaderText="<center>ID</center>" SortExpression="id_semana" HtmlEncode="false"/>
                <asp:BoundField DataField="semana" HeaderText="<center>Semana</center>" SortExpression="semana" HtmlEncode="false"/>
                <asp:BoundField DataField="fechas" HeaderText="<center>Fechas</center>" SortExpression="fechas" HtmlEncode="false"/>
                <asp:BoundField DataField="intereses" HeaderText="<center>Intereses</center>" SortExpression="intereses" HtmlEncode="false"/>
                <asp:BoundField DataField="totalACancelar" HeaderText="<center>A Cancelar</center>" SortExpression="totalACancelar" HtmlEncode="false"/>
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" />
            <HeaderStyle BackColor="#016965" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#ff9a32" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
