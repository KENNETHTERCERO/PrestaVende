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
        private CLASS.cs_factura cs_factura = new CLASS.cs_factura();
        private CLASS.cs_caja cs_caja = new CLASS.cs_caja();
        private string error = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string tipo_reporte = "";
            tipo_reporte = Request.QueryString.Get("tipo_reporte");
            managedReport(tipo_reporte);
        }

        private void managedReport(string tipo_reporte)
        {
            if (Convert.ToInt32(tipo_reporte) == 1)//Contrato
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
                        Reports.CRContratoGeneral prestamoGeneral = new Reports.CRContratoGeneral();
                        prestamoGeneral.Load(Server.MapPath("~/Reports/CRContratoGeneral.rpt"));
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
            }
            else if (Convert.ToInt32(tipo_reporte) == 2) //2 factura
            {
                DataTable factura = new DataTable("factura");
                string id_factura = Request.QueryString.Get("id_factura");
                factura = cs_factura.ObtenerFactura(ref error, id_factura);

                if (factura.Rows.Count <= 0)
                {
                    error = "Error obteniendo datos de contrato." + error;
                    throw new Exception("");
                }
                else
                {
                    try
                    {
                        Reports.CRFacturaIntereses ReporteFactura = new Reports.CRFacturaIntereses();

                        ReporteFactura.Load(Server.MapPath("~/Reports/CRFacturaIntereses.rpt"));
                        ReporteFactura.SetDataSource(factura);
                        CrystalReportViewer1.ReportSource = ReporteFactura;//document;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();
                        ReporteFactura.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }

                }

            }
            else if (Convert.ToInt32(tipo_reporte) == 3)//3 estado de cuenta prestamo
            {
                DataTable estadoCuenta = new DataTable("estadoCuentaPrestamo");
                string numero_prestamo = Request.QueryString.Get("numero_prestamo");
                estadoCuenta = cs_prestamo.GetEstadoCuentaPrestamoEncabezado(ref error, numero_prestamo);
                if (estadoCuenta.Rows.Count <= 0)
                {
                    error = "Error obteniendo datos de contrato." + error;
                    throw new Exception("");
                }
                else
                {
                    try
                    {
                        DataTable estadoCuentaDetalle = new DataTable("estadoCuentaPrestamoDetalle");
                        estadoCuenta = cs_prestamo.GetEstadoCuentaPrestamoEncabezado(ref error, numero_prestamo);
                        DataTable proyeccion = new DataTable("dtProyeccionInteres");
                        proyeccion = cs_prestamo.getDTProyeccion(ref error);

                        Reports.CREstadoCuentaPrestamo EstadoCuentaPrestamo = new Reports.CREstadoCuentaPrestamo();

                        EstadoCuentaPrestamo.Load(Server.MapPath("~/Reports/CREstadoCuentaPrestamo.rpt"));
                        EstadoCuentaPrestamo.Subreports[0].SetDataSource(proyeccion);
                        EstadoCuentaPrestamo.SetDataSource(estadoCuenta);
                        CrystalReportViewer1.ReportSource = EstadoCuentaPrestamo;//document;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();
                        EstadoCuentaPrestamo.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Estado de cuenta No." + numero_prestamo);
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }

                }
            }
            else if (Convert.ToInt32(tipo_reporte) == 4)//4 etiqueta prestamo
            {
                DataTable contrato = new DataTable("estadoCuentaPrestamo");
                string numero_prestamo = Request.QueryString.Get("numero_prestamo");
                contrato = cs_prestamo.GetDataEtiquetaPrestamo(ref error, numero_prestamo);
                if (contrato.Rows.Count <= 0)
                {
                    error = "Error obteniendo datos de contrato." + error;
                    throw new Exception("");
                }
                else
                {
                    try
                    {
                        Reports.CREtiquetaPrestamo Etiquetaprestamo = new Reports.CREtiquetaPrestamo();
                        Etiquetaprestamo.Load(Server.MapPath("~/Reports/CREtiquetaPrestamo.rpt"));
                        Etiquetaprestamo.SetDataSource(contrato);
                        CrystalReportViewer1.ReportSource = Etiquetaprestamo;//document;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();
                        Etiquetaprestamo.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Etiqueta No." + numero_prestamo);
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }

                }
            }
            else if (Convert.ToInt32(tipo_reporte) == 5)//5 impresion de recibo.
            {
                DataTable factura = new DataTable("DtDatos");
                string id_factura = Request.QueryString.Get("id_factura");
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                factura = cs_factura.ObtenerFacturaRecibo(ref error, id_factura, id_sucursal);

                if (factura.Rows.Count <= 0)
                {
                    error = "Error obteniendo datos de contrato." + error;
                    throw new Exception("");
                }
                else
                {
                    try
                    {
                        Reports.CRFacturaRecibo ReporteFactura = new Reports.CRFacturaRecibo();

                        ReporteFactura.Load(Server.MapPath("~/Reports/CRFacturaRecibo.rpt"));
                        ReporteFactura.SetDataSource(factura);
                        CrystalReportViewer1.ReportSource = ReporteFactura;//document;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();
                        ReporteFactura.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }

                }
            }
            else if (Convert.ToInt32(tipo_reporte) == 6)//Reporte abono capital.
            {
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                string fecha_inicio = Request.QueryString.Get("fecha_inicio");
                string fecha_fin = Request.QueryString.Get("fecha_fin");
            }
            else if (Convert.ToInt32(tipo_reporte) == 7)//Reporte Estado de Cuenta Caja
            {
                DataTable EstadoCuenta = new DataTable("DtDatos");
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                string fecha_inicio = Request.QueryString.Get("fecha_inicio");
                string fecha_fin = Request.QueryString.Get("fecha_fin");
                string id_caja = Request.QueryString.Get("id_caja");

                EstadoCuenta = cs_caja.ObtenerEstadoCuenta(ref error, id_sucursal, fecha_inicio, fecha_fin, id_caja);
                if (EstadoCuenta.Rows.Count <= 0)
                {
                    error = "Error obteniendo datos del estado de cuenta." + error;
                    throw new Exception("");
                }
                else
                {
                    try
                    {
                        Reports.CREtiquetaPrestamo Etiquetaprestamo = new Reports.CREtiquetaPrestamo();
                        Etiquetaprestamo.Load(Server.MapPath("~/Reports/CREtiquetaPrestamo.rpt"));
                        Etiquetaprestamo.SetDataSource(contrato);
                        CrystalReportViewer1.ReportSource = Etiquetaprestamo;//document;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();
                        Etiquetaprestamo.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Etiqueta No." + numero_prestamo);
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }

                }
            }
        }
    }
}