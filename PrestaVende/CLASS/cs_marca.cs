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

        public DataTable getMarcas(ref string error)
        {
            try
            {
                DataTable dtMarca = new DataTable("marcas");

                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT TOP (1000) [id_marca] " +
                                      ",[marca]" +
                                      ",[estado]" +
                                      ",[fecha_creacion]" +
                                      "  FROM [tbl_marca]";
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

        public int validaMarca(ref string error, string marca)
        {
            try
            {
                int numberReturn = 0;
                string valida = "";
                command = new SqlCommand();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(COUNT(id_marca), 0) id_marca FROM tbl_marca WHERE marca LIKE '%" + marca + "%'";
                numberReturn = Convert.ToInt32(command.ExecuteScalar().ToString());

                return numberReturn;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return 999999;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public int insertMarca(ref string error, string marca)
        {
            try
            {
                int numberReturn = 0;
                command = new SqlCommand();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "INSERT INTO tbl_marca (marca, estado, fecha_creacion, fecha_modificacion) " +
                                        "VALUES(@marca, 1, GETDATE(), GETDATE())";
                command.Parameters.AddWithValue("@marca", marca);
                numberReturn = command.ExecuteNonQuery();

                return numberReturn;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return 999999;
            }
            finally
            {
                connection.connection.Close();
            }
        }
    }
}