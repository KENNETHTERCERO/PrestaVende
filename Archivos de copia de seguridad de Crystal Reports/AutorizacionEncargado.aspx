<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutorizacionEncargado.aspx.cs" Inherits="PrestaVende.Public.AutorizacionEncargado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
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
    <div class="container">
        <div>
            <div runat="server" id="divWarning" visible="false" class="alert alert-warning alert-dismissable fade in">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>
                    <asp:Label runat="server" ID="lblWarning" Text=""></asp:Label></strong>
            </div>

            <div runat="server" id="divError" visible="false" class="alert alert-danger alert-dismissable fade in">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>
                    <asp:Label runat="server" ID="lblError" Text=""></asp:Label></strong>
            </div>


        </div>
        <div>
            <center>
            <form id="form_login" runat="server">
                <table>
                    <tr>
                        <td>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:TextBox runat="server" ID="txtUser" PlaceHolder="ejemplo.prestavende" MaxLength="50" class="form-control"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                <asp:TextBox runat="server" ID="txtPassword" type="password" class="form-control" placeholder="Password"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnAutorizar" Text="Autorizar" class="btn btn-primary"/>
                        </td>
                    </tr>
                </table>
            </form>
                </center>
        </div>
    </div>
</body>
</html>