using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_reporteria
    {
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public DataSet getReporteRIE(ref string error, string id_sucursal, string id_empresa, string fecha_inicio, string fecha_fin)
        {

            DataSet ds = new DataSet();

            try
            {               

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();

                SqlDataAdapter adapter;


                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_reporte_ingresos_egresos";
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                command.Parameters.AddWithValue("@id_empresa", id_empresa);
                command.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
                command.Parameters.AddWithValue("@fecha_fin", fecha_fin);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

                ds.DataSetName = "DSReporteRIE";

                ds.Tables[0].TableName = "dtReporteIE";
                ds.Tables[1].TableName = "dtAgencia";
                ds.Tables[2].TableName = "dtTransacciones";
                ds.Tables[3].TableName = "dtGarantias";
                ds.Tables[4].TableName = "dtValoresCaja";
            }catch(Exception ex)
            {
                error = "Error al consultar reporte: " + ex.ToString();
            }
            finally
            {
                connection.connection.Close();
            }            


            return ds;

        }

        public DataTable getReportData(ref string error, string id_tipo_transaccion, string id_empresa)
        {            
            DataTable dt = new DataTable();            

            try
            {

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();                

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CargarReporte";                
                command.Parameters.AddWithValue("@id_empresa", id_empresa);                
                command.Parameters.AddWithValue("@id_tipo_transaccion", id_tipo_transaccion);

                dt.Load(command.ExecuteReader());
                
                if (dt.Rows.Count == 0)
                    error = "Error no existe nombre del reporte";
                

            }
            catch (Exception ex)
            {
                error = "Error al consultar reporte: " + ex.ToString();
            }
            finally
            {
                connection.connection.Close();
            }

            return dt;
        }

        public string getTransactionType(Int32 opcionCase)
        {
            string transactionType = "";

            switch (opcionCase)
            {
                case 1://Impresion contrato
                case 12:
                    transactionType = "7";
                    break;
                case 2://Impresion factura
                    transactionType = "8";
                    break;                
                case 10: //Impresion estado de cuenta.
                    transactionType = "17";
                    break;
                case 11://Impresion etiqueta
                    transactionType = "18";
                    break;
                case 5: //Impresion Recibo
                    transactionType = "19";
                    break;
                case 8://Reporte de Anexo Contrato
                    transactionType = "20";
                    break;
            }

            return transactionType;
        }

        public string createLinkReport(string parameters)
        {
            try
            {
                string scriptReport = "window.open('http://sql5090.site4now.net/ReportServer/Pages/ReportViewer.aspx?%2fgtsa2019-002%2f" + parameters + "&rc:Parameters=False');";
                return scriptReport;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}