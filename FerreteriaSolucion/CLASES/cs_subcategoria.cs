using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaSolucion.CLASES
{
    class cs_subcategoria
    {
        private cs_connection_ferremas connection = new cs_connection_ferremas();
        private SqlCommand command = new SqlCommand();

        public DataTable estadosSubCategoria(ref string error)
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
                error = ex.ToString();
                return null;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public string getMaxIdSubCategoria(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_subcategoria), 1) AS id FROM tbl_subcategoria";
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

        public DataTable getSubCategorias(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "select sub.id_subcategoria, cat.id_categoria, cat.descripcion, sub.descripcion, (CASE WHEN sub.estado = 1 THEN 'ACTIVO' WHEN sub.estado = 0 THEN 'INACTIVO' END) AS estado, sub.estado AS state from tbl_subcategoria AS sub INNER JOIN tbl_categoria AS cat ON cat.id_categoria = sub.id_categoria";
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

        public DataTable getSubCategoriasCMB(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("data");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id, 'SELECCIONAR' AS descripcion UNION SELECT sub.id_subcategoria AS id, cat.descripcion + ' ' + sub.descripcion FROM tbl_subcategoria AS sub INNER JOIN tbl_categoria AS cat ON cat.id_categoria = sub.id_categoria WHERE sub.estado <> 0";
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

        public bool insertCategoria(ref string error, string descripcion, string estado, string id_categoria)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_subcategoria (id_categoria, descripcion, estado) " +
                                        "VALUES(@id_categoria, @descripcion, @estado)";
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
                    throw new SystemException( "NO SE PUDO AGREGAR CATEGORIA.");
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public bool updateCategoria(ref string error, string descripcion, string estado, string id_sub_categoria, string id_categoria)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_subcategoria SET descripcion = @descripcion, estado = @estado, id_categoria = @id_categoria WHERE id_subcategoria = @id_subcategoria";
                command.Parameters.AddWithValue("@descripcion", descripcion);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@id_subcategoria", id_sub_categoria);
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
    }
}
