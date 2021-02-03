using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using Microsoft.Reporting.WebForms;

namespace PrestaVende.Public
{
    public partial class Report : System.Web.UI.Page
    {
        private string usuarioReportingServices;
        private string passwordReportingServices;
        private string carpetaReportes;



        public string PasswordReportingServices
        {
            get
            {
                return passwordReportingServices;
            }

            set
            {
                passwordReportingServices = value;
            }
        }

        public string CarpetaReportes
        {
            get
            {
                return carpetaReportes;
            }

            set
            {
                carpetaReportes = value;
            }
        }

        public string UsuarioReportingServices
        {
            get
            {
                return usuarioReportingServices;
            }

            set
            {
                usuarioReportingServices = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string tipo_reporte = "";
                tipo_reporte = Request.QueryString.Get("tipo_reporte");
                setCredential();
                managedReport(tipo_reporte);
            }
        }

        private void setCredential()
        {
            UsuarioReportingServices = "gtsa2019-002";
            PasswordReportingServices = "Tercero#3";
            CarpetaReportes = "/gtsa2019-002/";
        }

        private void managedReport(string tipo_reporte)
        {
            try
            {
                string reporte = "";
                List<ReportParameter> paramList = new List<ReportParameter>();
                if (tipo_reporte == "6")
                {
                    //string id_sucursal = Request.QueryString.Get("id_sucursal");
                    //string fecha_inicio = Request.QueryString.Get("fecha_inicio");
                    //string fecha_fin = Request.QueryString.Get("fecha_fin");
                    //string id_transaccion = Request.QueryString.Get("transaccion");

                    paramList.Add(new ReportParameter("fecha_inicio", Request.QueryString.Get("fecha_inicio"), false));
                    paramList.Add(new ReportParameter("fecha_fin", Request.QueryString.Get("fecha_fin"), false));
                    paramList.Add(new ReportParameter("id_sucursal", Request.QueryString.Get("id_sucursal"), false));
                    paramList.Add(new ReportParameter("transaccion", Request.QueryString.Get("transaccion"), false));
                    reporte = "CRAbonosCapital";
                }
                
                ReportViewer1.Width = 800;
                ReportViewer1.Height = 600;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(UsuarioReportingServices, PasswordReportingServices, "SSRS Server"); // e.g.: ("demo-001", "123456789", "sql5090")
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://sql5090.site4now.net/ReportServer");
                ReportViewer1.ServerReport.ReportPath = CarpetaReportes + reporte; //e.g.: /demo-001/test
                ReportViewer1.ServerReport.SetParameters(paramList);
                ReportViewer1.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                showError(ex.ToString());
            }
        }

        private bool showWarning(string warning)
        {
            divWarning.Visible = true;
            lblWarning.Controls.Add(new LiteralControl(string.Format("<span style='color:Orange'>{0}</span>", warning)));
            return true;
        }

        private bool showError(string error)
        {
            divError.Visible = true;
            lblError.Controls.Add(new LiteralControl(string.Format("<span style='color:Red'>{0}</span>", error)));
            return true;
        }
    }

    public class CustomReportCredentials : IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
}