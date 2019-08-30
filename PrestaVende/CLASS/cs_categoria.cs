using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_categoria
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public cs_categoria()
        {
        }

        public DataTable getCategoria(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("categorias");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 as id_categoria, 'SELECCIONAR' AS categoria UNION " +
                                      "SELECT id_categoria, categoria From tbl_categoria WHERE estado = 1";
                returnTable.Load(command.ExecuteReader());
                return returnTable;
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