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
    }
}