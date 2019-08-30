using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_subcategoria
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public cs_subcategoria()
        {

        }

        public DataTable getSubCategoria(ref string error, string id_categoria)
        {
            try
            {
                DataTable returnTable = new DataTable("subCategorias");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_sub_categoria, 'SELECCIONAR' AS sub_categoria UNION " +
                                      "SELECT id_sub_categoria, sub_categoria FROM tbl_subcategoria " +
                                            "WHERE id_categoria = @id_categoria AND estado = 1";
                command.Parameters.AddWithValue("@id_categoria", id_categoria);
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