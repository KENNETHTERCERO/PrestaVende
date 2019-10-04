using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_manejo_medios
    {
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public DataTable getCategoriaMedio()
        {
            try
            {
                DataTable returnTable = new DataTable("categoriaMedio");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_categoria_medio, 'SELECCIONAR' categoria_medio UNION " +
                                        "SELECT id_categoria_medio, categoria_medio From tbl_categoria_medio WHERE estado = 1";
                returnTable.Load(command.ExecuteReader());
                return returnTable;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public DataTable getSubCategoriaMedio(string id_categoria)
        {
            try
            {
                DataTable returnTable = new DataTable("subcategoriaMedio");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 id_subcategoria_medio, 'SELECCIONAR' subcategoria_medio UNION " +
                                        "SELECT id_subcategoria_medio, subcategoria_medio FROM tbl_subcategoria_medio WHERE id_subcategoria_medio = @id_subcategoria_medio";
                command.Parameters.AddWithValue("@id_subcategoria_medio", id_categoria);
                returnTable.Load(command.ExecuteReader());
                return returnTable;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.connection.Close();
            }
        }
    }
}