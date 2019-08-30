using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_kilataje
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public cs_kilataje()
        {

        }

        public DataTable getKilataje(ref string error)
        {
            try
            {
                DataTable dtKilataje = new DataTable("kilataje");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_kilataje, 'SELECCIONAR' AS kilataje UNION " + 
                                      "SELECT id_kilataje, kilataje FROM tbl_kilataje WHERE estado = 1";
                dtKilataje.Load(command.ExecuteReader());
                return dtKilataje;
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
    }
}