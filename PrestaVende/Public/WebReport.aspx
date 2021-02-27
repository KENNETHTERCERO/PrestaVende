<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebReport.aspx.cs" Inherits="PrestaVende.Public.WebReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="True" />
        <div runat="server" class="errorcss" id="errorcss">
            <p>
                <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
            </p>
        </div>
    <script language="javascript">
        function printreport() {
            var viewerreference = $find("CrystalReportViewer1");

            var stillonloadstate = clientviewer.get_isloading();

            if (!stillonloadstate) {
                var reportarea = viewerreference.get_reportareacontenttype();
                if (reportarea == microsoft.reporting.webformsclient.reportareacontent.reportpage) {
                    $find("CrystalReportViewer1").invokeprintdialog();
                }
            }
        } 
    </script>

    <script type="text/javascript">
        onload = function() {
            window.moveto(0, 0);
            window.resizeto(screen.availwidth, screen.availheight);
        }
    </script>

    <object id="impr" classid="clsid:1663ed61-23eb-11d2-b92f-008048fdd814" codebase="scriptx.cab#version=6,1,429,14">
    </object>

    <script>
        impr.printing.header = ""
        impr.printing.footer = ""
        impr.printing.topmargin = 0
        impr.printing.bottommargin = 0
        impr.printing.leftmargin = 0
        impr.printing.rightmargin = 0
    </script>

    <script language="javascript">

        function imprimir() {
            window.print()
        }

    </script>
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
    </div>
    </form>
</body>
</html>

