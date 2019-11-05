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
        private CLASS.cs_prestamo cs_prestamo = new CLASS.cs_prestamo();
        private string error = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string tipo_reporte = "";
            tipo_reporte = Request.QueryString.Get("tipo_reporte");
            managedReport(tipo_reporte);
        }

        private void managedReport(string tipo_reporte)
        {
            if (Convert.ToInt32(tipo_reporte) == 1)//1 reporte de packing list en pdf
            {
                DataTable contrato = new DataTable("contrato");
                string numero_prestamo = Request.QueryString.Get("numero_prestamo");
                contrato = cs_prestamo.GetContrato(ref error, numero_prestamo);
                if (contrato.Rows.Count <= 0)
                {
                    error = "Error obteniendo datos de contrato." + error;
                    throw new Exception("");
                }
                else
                {
                    try
                    {
                        //DataTable resumenPacking = new DataTable();
                        Reports.CRContratoGeneral prestamoGeneral = new Reports.CRContratoGeneral();

                        //resumenPacking = pack.getDataResumePackingList(ref error, numero_recoleccion);
                        prestamoGeneral.Load(Server.MapPath("~/Reports/CRContratoGeneral.rpt"));
                        //prestamoGeneral.Subreports[0].SetDataSource(resumenPacking);
                        prestamoGeneral.SetDataSource(contrato);
                        CrystalReportViewer1.ReportSource = prestamoGeneral;//document;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();
                        prestamoGeneral.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Contrato No." + numero_prestamo);
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }
                    
                }
            }//finaliza if de packing list en pdf
             //    else if (Convert.ToInt32(tipo_reporte) == 2) //2 reporte de etiquetas en pdf
             //    {
             //        etiquetas = new CLASS.cs_etiquetas();
             //        string numero_recoleccion = Request.QueryString.Get("numero_recoleccion");
             //        //ReportDocument rpt_izquierda = new ReportDocument();
             //        //ReportDocument rpt_derecha = new ReportDocument();
             //        ReportDocument rpt_all_etiquetas = new ReportDocument();
             //        DataTable dt_rpt_izquierda = new DataTable("dt_izquierda");
             //        DataTable dt_rpt_derecha = new DataTable("dt_derecha");
             //        rpt_all_etiquetas.Load(Server.MapPath("~/Public/Vista/Reports/rpt_all_etiquetas.rpt"));
             //        dt_rpt_izquierda = etiquetas.getDataEtiqueta(ref error, numero_recoleccion, "1");
             //        dt_rpt_derecha = etiquetas.getDataEtiqueta(ref error, numero_recoleccion, "2");
             //        rpt_all_etiquetas.Subreports[0].SetDataSource(dt_rpt_derecha);
             //        rpt_all_etiquetas.Subreports[1].SetDataSource(dt_rpt_izquierda);
             //        CrystalReportViewer1.ReportSource = rpt_all_etiquetas;
             //        rpt_all_etiquetas.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Etiquetas de recoleccion No." + numero_recoleccion);
             //    }//finalizareporte de etiquetas en pdf
             //    else if (Convert.ToInt32(tipo_reporte) == 3)//3 reporte de packing list en excel
             //    {
             //        pack = new CLASS.cs_packing_list();
             //        DataTable packing = new DataTable("packing");
             //        string numero_recoleccion = Request.QueryString.Get("numero_recoleccion");
             //        packing = pack.getAllDataPackingList(ref error, numero_recoleccion);
             //        if (packing.Rows.Count <= 0)
             //        {
             //            error = "Error obteniendo packing list" + error;
             //            throw new Exception("");
             //        }
             //        else
             //        {
             //            DataTable resumenPacking = new DataTable();
             //            Reports.rpt_packing_list document = new Reports.rpt_packing_list();
             //            document.Load(Server.MapPath("~/Public/Vista/Reports/rpt_packing_list.rpt"));
             //            resumenPacking = pack.getDataResumePackingList(ref error, numero_recoleccion);
             //            document.Subreports[0].SetDataSource(resumenPacking);
             //            document.SetDataSource(packing);
             //            CrystalReportViewer1.ReportSource = document;
             //            CrystalReportViewer1.DataBind();
             //            CrystalReportViewer1.RefreshReport();
             //            document.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Packing list de recoleccion No." + numero_recoleccion);
             //        }
             //    }//Finaliza reporte de packint list en excel
             //    else if (Convert.ToInt32(tipo_reporte) == 4)//Reporte pedidos en pdf
             //    {
             //        pedido = new CLASS.cs_reportes_pedido();
             //        string numero_pedido = Request.QueryString.Get("numero_pedido");
             //        ReportDocument rpt_pedido = new ReportDocument();
             //        DataTable dt_pedido = new DataTable("pedido");
             //        rpt_pedido.Load(Server.MapPath("~/Public/Vista/Reports/rpt_pedido.rpt"));
             //        dt_pedido = pedido.getAllDataPedido(ref error, numero_pedido);
             //        rpt_pedido.SetDataSource(dt_pedido);
             //        CrystalReportViewer1.ReportSource = rpt_pedido;
             //        rpt_pedido.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Pedido No." + numero_pedido);
             //    }
             //    else if (Convert.ToInt32(tipo_reporte) == 5)//Reporte pedidos en excel
             //    {
             //        pedido = new CLASS.cs_reportes_pedido();
             //        string numero_pedido = Request.QueryString.Get("numero_pedido");
             //        ReportDocument rpt_pedido = new ReportDocument();
             //        DataTable dt_pedido = new DataTable("pedido");
             //        rpt_pedido.Load(Server.MapPath("~/Public/Vista/Reports/rpt_pedido.rpt"));
             //        dt_pedido = pedido.getAllDataPedido(ref error, numero_pedido);
             //        rpt_pedido.SetDataSource(dt_pedido);
             //        CrystalReportViewer1.ReportSource = rpt_pedido;
             //        rpt_pedido.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook, Response, false, "Pedido No." + numero_pedido);
             //    }
             //    else if (Convert.ToInt32(tipo_reporte) == 6)//Reporte etiquetas gramas
             //    {
             //        etiquetas = new CLASS.cs_etiquetas();
             //        string numero_recoleccion = Request.QueryString.Get("numero_recoleccion");
             //        //ReportDocument rpt_izquierda = new ReportDocument();
             //        //ReportDocument rpt_derecha = new ReportDocument();
             //        ReportDocument rpt_all_etiquetas_gramas = new ReportDocument();
             //        DataTable dt_rpt_izquierda_gramas = new DataTable("dt_izquierda_gramas");
             //        DataTable dt_rpt_derecha_gramas = new DataTable("dt_derecha_gramas");
             //        rpt_all_etiquetas_gramas.Load(Server.MapPath("~/Public/Vista/Reports/rpt_all_etiquetas_gramas.rpt"));
             //        dt_rpt_izquierda_gramas = etiquetas.getDataEtiquetasGramas(ref error, numero_recoleccion, "1");
             //        dt_rpt_derecha_gramas = etiquetas.getDataEtiquetasGramas(ref error, numero_recoleccion, "2");
             //        rpt_all_etiquetas_gramas.Subreports[0].SetDataSource(dt_rpt_derecha_gramas);
             //        rpt_all_etiquetas_gramas.Subreports[1].SetDataSource(dt_rpt_izquierda_gramas);
             //        CrystalReportViewer1.ReportSource = rpt_all_etiquetas_gramas;
             //        rpt_all_etiquetas_gramas.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Etiquetas de recoleccion No." + numero_recoleccion);
             //    }
        }
    }
}