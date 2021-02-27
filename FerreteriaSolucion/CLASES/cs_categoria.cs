using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaSolucion.CLASES
{
    class cs_categoria
    {
        private cs_connection_ferremas connection = new cs_connection_ferremas();
        private SqlCommand command = new SqlCommand();

        public DataTable getCategorias(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("tab");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT *, (CASE WHEN estado = 1 THEN 'ACTIVO' WHEN estado = 0 THEN 'INACTIVO' END) AS estado_letras FROM tbl_categoria";
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

        public string getMaxIdCategoria(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_categoria), 1) AS id FROM tbl_categoria";
                return command.ExecuteScalar().ToString();
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

        public DataTable estadosCategoria()
        {
            try
            {
                DataTable returnTable = new DataTable("dat");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id, 'INACTIVA' AS descripcion UNION SELECT 1 AS id, 'ACTIVA' AS descripcion";
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

        public bool insertCategoria(ref string error, string descripcion, string estado)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_categoria (descripcion, estado) " +
                                        "VALUES(@descripcion, @estado)";
                command.Parameters.AddWithValue("@descripcion", descripcion);
                command.Parameters.AddWithValue("@estado", estado);
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    error = "NO SE PUDO AGREGAR CATEGORIA.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                command.Transaction.Rollback();
                return false;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public bool updateCategoria(ref string error, string descripcion, string estado, string id_categoria)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_categoria SET descripcion = @descripcion, estado = @estado WHERE id_categoria = @id_categoria";
                command.Parameters.AddWithValue("@descripcion", descripcion);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@id_categoria", id_categoria);
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    error = "NO SE PUDO ACTUALIZAR CATEGORIA.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                command.Transaction.Rollback();
                return false;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public DataTable getCategoriasCMB(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("dat");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_categoria, 'SELECCIONAR' AS descripcion UNION SELECT id_categoria, descripcion From tbl_categoria WHERE estado = 1";
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
