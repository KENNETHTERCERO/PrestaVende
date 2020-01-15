using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_marca
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public cs_marca()
        {
        }

        public DataTable getMarca(ref string error)
        {
            try
            {
                DataTable dtMarca = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_marca, 'SELECCIONAR' AS marca UNION " + 
                                      "SELECT id_marca, marca FROM tbl_marca WHERE estado = 1 ORDER BY marca ASC";
                dtMarca.Load(command.ExecuteReader());
                return dtMarca;
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