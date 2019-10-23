<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecepcionCaja.aspx.cs" Inherits="PrestaVende.Public.RecepcionCaja" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recepcion caja</title>
    <link rel="shortcut icon" type="image/x-icon" href="Images/imagenlogo.ico" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <style>
        .container {
            position: relative;
            margin: 0 auto;
            text-align: center;
            margin-top: 13%;
        }
    </style>
</head>
<body>
    <div>
        <div runat="server" id="divWarning" visible="false" class="alert alert-warning alert-dismissable fade in">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <strong><asp:Label runat="server" ID="lblWarning" Text=""></asp:Label></strong>
        </div>

        <div runat="server" id="divError" visible="false" class="alert alert-danger alert-dismissable fade in">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <strong><asp:Label runat="server" ID="lblError" Text=""></asp:Label></strong>
        </div>
    </div>
        <div>
            <center>
            <form id="form_login" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblCajaAsignada" Text="Caja asignada: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblNumeroCajaAsignada" Text="Caja 1 "></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblMontoAsignado" Text="Monto asignado: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMontoRecepcion" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblCheckRecibir" Text="Recibir caja: "></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkRecibir" ></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnCancelarRecepcion" Text="CANCELAR RECEPCION" class="btn btn-warning" OnClick="btnCancelarRecepcion_Click" />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnRecibirAsignacion" Text="RECIBIR CAJA" class="btn btn-primary" OnClick="btnRecibirAsignacion_Click" />
                        </td>
                    </tr>
                </table>
            </form>
                </center>
        </div>
</body>
</html>
