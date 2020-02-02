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

        public DataSet getReporteRIE(ref string error, string id_sucursal, string id_empresa)
        {

            DataSet ds = new DataSet();

            connection.connection.Open();
            command.Connection = connection.connection;
            command.Parameters.Clear();

            SqlDataAdapter adapter;
            

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_reporte_ingresos_egresos";
            command.Parameters.AddWithValue("@id_sucursal", 1);
            command.Parameters.AddWithValue("@id_empresa", 1);

            adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);

            ds.DataSetName = "DSReporteRIE";

            ds.Tables[0].TableName = "dtReporteIE";
            ds.Tables[1].TableName = "dtTransacciones";
            ds.Tables[2].TableName = "dtGarantias";
            ds.Tables[3].TableName = "dtValoresCaja";


            return ds;

        }
    }
}