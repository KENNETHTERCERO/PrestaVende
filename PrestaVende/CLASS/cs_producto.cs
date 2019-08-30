using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_producto
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public cs_producto()
        {

        }

        public DataTable getProducto(ref string error, string id_subcategoria)
        {
            try
            {
                DataTable returnTable = new DataTable("subCategorias");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_producto, 'SELECCIONAR' AS producto UNION " +
                                      "SELECT id_producto, producto FROM tbl_producto WHERE id_sub_categoria = @id_sub_categoria AND estado = 1";
                command.Parameters.AddWithValue("@id_sub_categoria", id_subcategoria);
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