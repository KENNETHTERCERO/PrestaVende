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
        private CLASS.cs_manejo_inventario cs_manejo_inventario = new CLASS.cs_manejo_inventario();
        private CLASS.cs_reporteria cs_reporteria = new CLASS.cs_reporteria();

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
                contrato = cs_prestamo.GetContrato(ref error, numero_prestamo, Session["id_sucursal"].ToString());
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
                DataTable proyeccion = new DataTable("proyeccion");

                string id_factura = Request.QueryString.Get("id_factura");
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                string numero_contrato = Request.QueryString.Get("numero_contrato");
                factura = cs_factura.ObtenerFactura(ref error, id_factura);
                proyeccion = cs_prestamo.getValorProximoPago(ref error, numero_contrato);

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
                        ReporteFactura.Subreports[0].SetDataSource(proyeccion);
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
                estadoCuenta = cs_prestamo.GetEstadoCuentaPrestamoEncabezado(ref error, numero_prestamo, Session["id_sucursal"].ToString());
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
                        estadoCuenta = cs_prestamo.GetEstadoCuentaPrestamoEncabezado(ref error, numero_prestamo, Session["id_sucursal"].ToString());
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
                contrato = cs_prestamo.GetDataEtiquetaPrestamo(ref error, numero_prestamo, Session["id_sucursal"].ToString());
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
                DataTable factura = new DataTable("dtRecibo");
                string id_recibo = Request.QueryString.Get("id_recibo");
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                factura = cs_factura.ObtenerRecibo(ref error, id_recibo, id_sucursal);

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

                DataTable AbonosCapital = new DataTable("AbonosCapital");
                AbonosCapital = cs_prestamo.GetDataReporteAbono(ref error, fecha_inicio, fecha_fin, id_sucursal, "9");

                Reports.CRAbonosCapital ReporteAbonos = new Reports.CRAbonosCapital();

                ReporteAbonos.Load(Server.MapPath("~/Reports/CRAbonosCapital.rpt"));
                ReporteAbonos.SetDataSource(AbonosCapital);
                CrystalReportViewer1.ReportSource = ReporteAbonos;//document;
                CrystalReportViewer1.DataBind();
                CrystalReportViewer1.RefreshReport();
                ReporteAbonos.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");

            }
            else if (Convert.ToInt32(tipo_reporte) == 7)//Reporte Estado de Cuenta Caja
            {
                DataTable EstadoCuenta = new DataTable("DtDatos");
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                string fecha_inicio = Request.QueryString.Get("fecha_inicio");
                string fecha_fin = Request.QueryString.Get("fecha_fin");
                string id_caja = Request.QueryString.Get("id_caja");

                EstadoCuenta = cs_caja.ObtenerEstadoCuenta(ref error, id_sucursal, id_caja, fecha_inicio, fecha_fin);
                if (EstadoCuenta.Rows.Count <= 0)
                {
                    error = "Error obteniendo datos del estado de cuenta." + error;
                    throw new Exception("");
                }
                else
                {
                    try
                    {
                        Reports.CRReporteEstadoDeCuentaCaja CRReporteEstadoDeCuentaCaja = new Reports.CRReporteEstadoDeCuentaCaja();
                        CRReporteEstadoDeCuentaCaja.Load(Server.MapPath("~/Reports/CRReporteEstadoDeCuentaCaja.rpt"));
                        CRReporteEstadoDeCuentaCaja.SetDataSource(EstadoCuenta);
                        CrystalReportViewer1.ReportSource = CRReporteEstadoDeCuentaCaja;//document;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();
                        CRReporteEstadoDeCuentaCaja.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Estado de cuenta caja de " + fecha_inicio + " a " + fecha_fin);
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }
                }
            }
            else if (Convert.ToInt32(tipo_reporte) == 8)//Reporte cancelaciones.
            {
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                string fecha_inicio = Request.QueryString.Get("fecha_inicio");
                string fecha_fin = Request.QueryString.Get("fecha_fin");
            }
            else if (Convert.ToInt32(tipo_reporte) == 9)//Reporte inventario disponible.
            {
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                

                DataTable inventario = new DataTable("dtInventario");


                inventario = cs_manejo_inventario.getInventarioDisponible(ref error,  id_sucursal);

                if (inventario.Rows.Count <= 0)
                {
                    error = "Error obteniendo datos de inventario." + error;
                    throw new Exception("");
                }
                else
                {
                    try
                    {
                        Reports.CRInventarioSucursal inventarioSucursal = new Reports.CRInventarioSucursal();
                        inventarioSucursal.Load(Server.MapPath("~/Reports/CRInventarioSucursal.rpt"));
                        inventarioSucursal.SetDataSource(inventario);
                        CrystalReportViewer1.ReportSource = inventarioSucursal;//document;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.RefreshReport();
                        inventarioSucursal.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }

                }

            }
            else if (Convert.ToInt32(tipo_reporte) == 10)//10 estado de cuenta prestamo reimpresion.
            {
                DataTable estadoCuenta = new DataTable("estadoCuentaPrestamo");
                string numero_prestamo = Request.QueryString.Get("numero_prestamo");
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                estadoCuenta = cs_prestamo.GetEstadoCuentaPrestamoEncabezado(ref error, numero_prestamo, id_sucursal);
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
                        estadoCuenta = cs_prestamo.GetEstadoCuentaPrestamoEncabezado(ref error, numero_prestamo, id_sucursal);
                        DataTable proyeccion = new DataTable("dtProyeccionInteres");
                        proyeccion = cs_prestamo.getDTProyeccion(ref error, numero_prestamo, id_sucursal);

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
            else if (Convert.ToInt32(tipo_reporte) == 11)//11 etiqueta prestamo reimpresion.
            {
                DataTable contrato = new DataTable("estadoCuentaPrestamo");
                string numero_prestamo = Request.QueryString.Get("numero_prestamo");
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                contrato = cs_prestamo.GetDataEtiquetaPrestamo(ref error, numero_prestamo, id_sucursal);
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
            else if (Convert.ToInt32(tipo_reporte) == 12)//12 Contrato reimpresion.
            {
                DataTable contrato = new DataTable("contrato");
                string numero_prestamo = Request.QueryString.Get("numero_prestamo");
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                contrato = cs_prestamo.GetContrato(ref error, numero_prestamo, id_sucursal);
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
            else if (Convert.ToInt32(tipo_reporte) == 13)//Reporte cancelaciones.
            {
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                string fecha_inicio = Request.QueryString.Get("fecha_inicio");
                string fecha_fin = Request.QueryString.Get("fecha_fin");

                DataTable Cancelacion = new DataTable("Cancelaciones");
                Cancelacion = cs_prestamo.GetDataReporteAbono(ref error, fecha_inicio, fecha_fin, id_sucursal, "10");

                Reports.CRCancelacion ReporteCancelacion = new Reports.CRCancelacion();

                ReporteCancelacion.Load(Server.MapPath("~/Reports/CRCancelacion.rpt"));
                ReporteCancelacion.SetDataSource(Cancelacion);
                CrystalReportViewer1.ReportSource = ReporteCancelacion;//document;
                CrystalReportViewer1.DataBind();
                CrystalReportViewer1.RefreshReport();
                ReporteCancelacion.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");

            }
            else if (Convert.ToInt32(tipo_reporte) == 14)//Reporte facturas.
            {
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                string fecha_inicio = Request.QueryString.Get("fecha_inicio");
                string fecha_fin = Request.QueryString.Get("fecha_fin");

                DataTable Facturas = new DataTable("Facturas");
                Facturas = cs_prestamo.GetDataReporteFacturas(ref error, fecha_inicio, fecha_fin, id_sucursal);

                Reports.CRFacturas ReporteFacturas = new Reports.CRFacturas();

                ReporteFacturas.Load(Server.MapPath("~/Reports/CRFacturas.rpt"));
                ReporteFacturas.SetDataSource(Facturas);
                CrystalReportViewer1.ReportSource = ReporteFacturas;//document;
                CrystalReportViewer1.DataBind();
                CrystalReportViewer1.RefreshReport();
                ReporteFacturas.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");

            }
            else if (Convert.ToInt32(tipo_reporte) == 15)//Reporte Ingresos y Egresos RIE
            {
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                string id_empresa = Request.QueryString.Get("id_empresa");


                DataSet dsReporteIE = new DataSet("DSReporteRIE");


                dsReporteIE = cs_reporteria.getReporteRIE(ref error, id_sucursal, id_empresa);

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
                        reporteIE.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }

                }

            }
            else if (Convert.ToInt32(tipo_reporte) == 16)//16 reporte de prestamos por fechas.
            {
                DataTable contrato = new DataTable("ReporteContratos");
                string id_sucursal = Request.QueryString.Get("numero_prestamo");
                string fecha_inicio = Request.QueryString.Get("id_sucursal");
                string fecha_fin = "";
                contrato = cs_prestamo.getDataPrestamosPorFecha(ref error, fecha_inicio, fecha_fin, id_sucursal);
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
                        prestamoGeneral.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Sucursal No." + id_sucursal);
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                    }

                }
            }
            else if (Convert.ToInt32(tipo_reporte) == 17)//17 reporte de liquidaciones.
            {
                DataTable contrato = new DataTable("contrato");
                string numero_prestamo = Request.QueryString.Get("numero_prestamo");
                string id_sucursal = Request.QueryString.Get("id_sucursal");
                contrato = cs_prestamo.GetContrato(ref error, numero_prestamo, id_sucursal);
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
        }
    }
}