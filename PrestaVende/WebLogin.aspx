﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebLogin.aspx.cs" Inherits="PrestaVende.WebLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Presta Vende</title>
    <link rel="shortcut icon" type="image/x-icon" href="Images/imagenlogo.ico" />
    <script type="text/javascript">

        $(document).ready(function () {

            window.setTimeout(function () {
                $(".alert").fadeTo(1000, 0).slideUp(1000, function () {
                    $(this).remove();
                });
            }, 5000);
        });
    </script>
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
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="True" />
                <div>
                    <asp:Image runat="server" ID="imageLogo" AutoPostBack="True" Width="300px" Height ="150px"></asp:Image>
                </div>
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
                        <td align="right">
                            <asp:Button runat="server" ID="btnIngresar" Text="Ingresar" class="btn btn-primary" OnClick="btnIngresar_Click" />
                        </td>
                    </tr>
                </table>
            </form>
                </center>
        </div>
    </div>
</body>
</html>
