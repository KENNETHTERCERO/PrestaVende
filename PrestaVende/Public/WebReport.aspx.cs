using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WebReport : System.Web.UI.Page
    {
        private CLASS.cs_reporteria report = new CLASS.cs_reporteria();
        private CLASS.cs_prestamo cs_prestamo = new CLASS.cs_prestamo();
        private CLASS.cs_factura cs_factura = new CLASS.cs_factura();
        private CLASS.cs_caja cs_caja = new CLASS.cs_caja();
        private CLASS.cs_manejo_inventario cs_manejo_inventario = new CLASS.cs_manejo_inventario();
        private CLASS.cs_reporteria cs_reporteria = new CLASS.cs_reporteria();
        private CLASS.cs_liquidacion cs_liquidacion = new CLASS.cs_liquidacion();
        private CLASS.cs_traslado cs_traslado = new CLASS.cs_traslado();
        private CLASS.cs_Empresa cs_empresa = new CLASS.cs_Empresa();
        private string error = "";        

        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 tipo_reporte = 0;
            tipo_reporte = Convert.ToInt32(Request.QueryString.Get("tipo_reporte"));
            managedReport(tipo_reporte);
        }

        private void managedReport(Int32 tipo_reporte)
        {
            string nombre_reporte = "";
            bool reporte_adicional = false;
            string nombre_reporte_adicional = "";
            int opcion_case = 0;

            DataTable datosReporte = report.getReportData(ref error, report.getTransactionType(tipo_reporte), this.Session["id_empresa"].ToString());

            if (datosReporte.Rows.Count > 0)
            {
                nombre_reporte = datosReporte.Rows[0]["nombre_reporte"].ToString();
                reporte_adicional = bool.Parse(datosReporte.Rows[0]["reporte_adicional"].ToString());
                nombre_reporte_adicional = datosReporte.Rows[0]["nombre_reporte_adicional"].ToString();
                opcion_case = int.Parse(datosReporte.Rows[0]["opcion_case"].ToString());
            }

            switch (tipo_reporte)
            {
                case 1://Impresion contrato
                case 12:                    
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportAgreement", report.createLinkReport(nombre_reporte + "&id_sucursal=" + this.Session["id_sucursal"].ToString() + "&numero_prestamo=" + this.Request.QueryString.Get("numero_prestamo").ToString()), true);
                    if (reporte_adicional)
                        managedReport(opcion_case);
                    break;
                case 2://Impresion factura                     
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportBill", report.createLinkReport(nombre_reporte + "&id_factura=" + this.Request.QueryString.Get("id_factura").ToString()), true);
                    if (reporte_adicional)
                        managedReport(opcion_case);
                    break;
                case 3:
                case 10: //Impresion estado de cuenta.                    
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportAccountStatus", report.createLinkReport(nombre_reporte + "&numero_prestamo=" + this.Request.QueryString.Get("numero_prestamo").ToString() + "&id_sucursal=" + this.Session["id_sucursal"].ToString()), true);
                    if (reporte_adicional)
                        managedReport(opcion_case);
                    break;
                case 4:
                case 11://Impresion etiqueta
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportTagAgreement", report.createLinkReport(nombre_reporte + "&numero_prestamo=" + this.Request.QueryString.Get("numero_prestamo").ToString() + "&id_sucursal=" + this.Session["id_sucursal"].ToString()), true);
                    if (reporte_adicional)
                        managedReport(opcion_case);
                    break;
                case 5: //Impresion Recibo
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportReceipt", report.createLinkReport(nombre_reporte + "&id_recibo=" + this.Request.QueryString.Get("id_recibo").ToString() + "&id_sucursal=" + this.Session["id_sucursal"].ToString()), true);
                    if (reporte_adicional)
                        managedReport(opcion_case);
                    break;
                case 6:
                case 13: //Reporte Abonos y Cancelaciones
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportBondCancel", report.createLinkReport("AbonosCancelacion&fecha_inicio=" + this.Request.QueryString.Get("fecha_inicio") + "&fecha_fin=" + this.Request.QueryString.Get("fecha_fin") + "&id_sucursal=" + this.Session["id_sucursal"].ToString() + "&tipo_transaccion=" + this.Request.QueryString.Get("transaccion")), true);
                    break;
                case 7: //Reporte estado de cuenta caja.
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportBondCancel", report.createLinkReport("EstadoCuentaCaja&id_sucursal=" + this.Request.QueryString.Get("id_sucursal") + "&id_caja=" + this.Request.QueryString.Get("id_caja") + "&fechaInicio=" + this.Request.QueryString.Get("fecha_inicio") + "&fecha_final=" + this.Request.QueryString.Get("fecha_fin")), true);
                    break;
                case 8://Reporte de Anexo Contrato
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportAnnexed", report.createLinkReport(nombre_reporte + "&id_sucursal=" + this.Session["id_sucursal"].ToString() + "&numero_prestamo=" + this.Request.QueryString.Get("numero_prestamo").ToString()), true);
                    break;
                case 9: //Inventario disponible sucursal
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportInventory", report.createLinkReport("InventarioSucursal&id_sucursal=" + this.Request.QueryString.Get("id_sucursal")), true);
                    break;
                case 14: //Reporte de facturas detallado
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportBills", report.createLinkReport("Facturas&fecha_inicio=" + this.Request.QueryString.Get("fecha_inicio") + "&fecha_fin=" + this.Request.QueryString.Get("fecha_fin") + "&id_sucursal=" + this.Session["id_sucursal"].ToString() + "&tipo_factura=" + this.Request.QueryString.Get("tipo_factura")), true);
                    break;
                case 16: //reporte de prestamos por fechas.
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportAgreements", report.createLinkReport("Prestamos&id_sucursal=" + this.Session["id_sucursal"].ToString() + "&fecha_inicio=" + this.Request.QueryString.Get("fecha_inicio") + "&fecha_fin=" + this.Request.QueryString.Get("fecha_fin")), true);
                    break;
                case 17://reporte de liquidaciones.
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "scriptReportLiquidation", report.createLinkReport("Liquidaciones&id_sucursal=" + this.Session["id_sucursal"].ToString() + "&fecha_inicio=" + this.Request.QueryString.Get("fecha_inicio") + "&fecha_fin=" + this.Request.QueryString.Get("fecha_fin")), true);
                    break;
                default:

                    break;
            }

            if (Convert.ToInt32(tipo_reporte) == 1 || Convert.ToInt32(tipo_reporte) == 12)//Contrato
            {
                
            }
            else if (Convert.ToInt32(tipo_reporte) == 8)//Reporte ventas detallado.
            { //no se usa este numero, esta disponible.
            }
            else if (Convert.ToInt32(tipo_reporte) == 15)//Reporte Ingresos y Egresos RIE
            {
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                string id_empresa = Request.QueryString.Get("id_empresa");
                string fecha_inicio = Request.QueryString.Get("fecha_inicio");
                string fecha_fin = Request.QueryString.Get("fecha_fin");


                DataSet dsReporteIE = new DataSet("DSReporteRIE");


                dsReporteIE = cs_reporteria.getReporteRIE(ref error, id_sucursal, id_empresa, fecha_inicio, fecha_fin);

                if (dsReporteIE.Tables.Count  <= 0)
                {
                    error = "Error obteniendo datos de Ingresos y Egresos." + error;
                    throw new Exception("");
                }
                else
                {
                    try
                    {
                        Reports.CRReporteRIE reporteIE = new Reports.CRReporteRIE();
                        reporteIE.Load(Server.MapPath("~/Reports/CRReporteRIE.rpt"));
                        reporteIE.SetDataSource(dsReporteIE);
                        CrystalReportViewer1.ReportSource = reporteIE;//document;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();
                        string tipo = this.Request.QueryString.Get("tipo");
                        if (tipo == "excel")
                        {
                            reporteIE.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, Response, false, "RIE");
                        }
                        else
                        {
                            reporteIE.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "RIE");
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }
                }
            }
            else if (Convert.ToInt32(tipo_reporte) == 18)//18 reporte de prestamos vencidos.
            {
                DataTable ReporteContrato = new DataTable("ReporteContratos");
                string id_sucursal = this.Request.QueryString.Get("id_sucursal");
                string fecha_inicio = this.Request.QueryString.Get("fecha_inicio");
                string fecha_fin = this.Request.QueryString.Get("fecha_fin");

                cs_prestamo = new CLASS.cs_prestamo();
                ReporteContrato = cs_prestamo.getDataPrestamosVencidos(ref error, id_sucursal, fecha_inicio, fecha_fin);
                if (ReporteContrato.Rows.Count <= 0)
                {
                    error = "Error obteniendo datos de contrato." + error;
                    throw new Exception("");
                }
                else
                {
                    try
                    {
                        Reports.CRReporteVencidos ReportePrestamosVencidos = new Reports.CRReporteVencidos();
                        ReportePrestamosVencidos.Load(Server.MapPath("~/Reports/CRReporteVencidos.rpt"));
                        ReportePrestamosVencidos.SetDataSource(ReporteContrato);
                        CrystalReportViewer1.ReportSource = ReportePrestamosVencidos;//document;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();
                        string tipo = this.Request.QueryString.Get("tipo");
                        if (tipo == "excel")
                        {
                            ReportePrestamosVencidos.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, Response, false, "Sucursal No." + id_sucursal);
                        }
                        else
                        {
                            ReportePrestamosVencidos.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Sucursal No." + id_sucursal);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }

                }
            }
            else if (Convert.ToInt32(tipo_reporte) == 19)//Boleta de traslado.
            {
                string id_traslado_encabezado = Request.QueryString.Get("id_traslado_encabezado");


                DataTable dtBoletaTraslado = new DataTable("dtBoleta");


                dtBoletaTraslado = cs_traslado.ObtenerDatosBoletaTraslado(ref error, id_traslado_encabezado);

                if (dtBoletaTraslado.Rows.Count <= 0)
                {
                    error = "Error obteniendo datos de inventario." + error;
                    throw new Exception("");
                }
                else
                {
                    try
                    {
                        Reports.CRBoletaTraslado boletaTraslado = new Reports.CRBoletaTraslado();
                        boletaTraslado.Load(Server.MapPath("~/Reports/CRBoletaTraslado.rpt"));
                        boletaTraslado.SetDataSource(dtBoletaTraslado);
                        CrystalReportViewer1.ReportSource = boletaTraslado;//document;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();
                        string tipo = this.Request.QueryString.Get("tipo");
                        if (tipo == "excel")
                        {
                            boletaTraslado.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, Response, false, "BoletaTrasladoIDTraslado" + id_traslado_encabezado);
                        }
                        else
                        {
                            boletaTraslado.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "BoletaTrasladoIDTraslado" + id_traslado_encabezado);
                        }

                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }

                }
            }

            if (cs_empresa.getShowReportViewer(ref error, Session["id_sucursal"].ToString()) > 0)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
            }
        }
    }
}