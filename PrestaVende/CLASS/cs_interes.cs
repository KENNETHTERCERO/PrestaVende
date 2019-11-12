using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_interes
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public cs_interes()
        {

        }

        public DataTable getInteres(ref string error)
        {
            try
            {
                DataTable dtInteres = new DataTable("intereses");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_interes, 'SELECCIONAR' AS interes UNION " + 
                                      "SELECT id_interes, interes FROM tbl_interes WHERE estado = 1";
                dtInteres.Load(command.ExecuteReader());
                return dtInteres;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public string getIdInteres(ref string error, string monto)
        {
            try
            {
                string id_interes = "";
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "select top 1 ISNULL(id_plan_prestamo, 0) From tbl_plan_prestamo WHERE @monto between minimo AND maximo";
                command.Parameters.AddWithValue("@monto", monto);
                id_interes = command.ExecuteScalar().ToString();
                return id_interes;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return "";
            }
            finally
            {
                connection.connection.Close();
            }
        }
    }
}