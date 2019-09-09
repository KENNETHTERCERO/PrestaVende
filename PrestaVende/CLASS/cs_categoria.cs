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

        public string getIDMaxCategoria(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_categoria), 0) +1 FROM tbl_categoria";
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

        public DataTable getCategoriaComboBox(ref string error)
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

        public DataTable getCategoria(ref string error)
        {
            try
            {
                DataTable AreaEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = " SELECT id_categoria, categoria, (CASE WHEN estado = 1 THEN 'ACTIVO' WHEN estado = 0 THEN 'INACTIVO' ELSE 'OTRO ESTADO' END) AS estado FROM tbl_categoria ";
                AreaEmpresa.Load(command.ExecuteReader());
                return AreaEmpresa;
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

        public DataTable getEstadoCategoria(ref string error)
        {
            try
            {
                DataTable EstadoCategoria = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id, 'INACTIVO' AS estado UNION SELECT 1 AS id, 'ACTIVO' AS estado";
                EstadoCategoria.Load(command.ExecuteReader());
                return EstadoCategoria;
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

        public DataTable getObtieneDatosModificar(ref string error, string id_categoria)
        {
            try
            {
                DataTable DatosCategoria = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT id_categoria, categoria, estado FROM tbl_categoria WHERE id_categoria = @id_categoria ";
                command.Parameters.AddWithValue("@id_categoria", id_categoria);
                DatosCategoria.Load(command.ExecuteReader());
                return DatosCategoria;
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

        public bool insertCategoria(ref string error, string id_categoria, string categoria, string estado, string fecha_creacion)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = " INSERT INTO tbl_categoria (categoria, estado, fecha_creacion) "
                                    + " VALUES(@categoria, @estado, @fecha_creacion) ";
                command.Parameters.AddWithValue("@id_categoria", id_categoria);
                command.Parameters.AddWithValue("@categoria", categoria);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@fecha_creacion", fecha_creacion);
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo insertar la categoria, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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

        public bool updateCategoria(ref string error, string[] datos)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_categoria SET estado = @estado WHERE id_categoria = @id_categoria";
                command.Parameters.AddWithValue("@id_categoria", datos[0]);
                command.Parameters.AddWithValue("@estado", datos[2]);
                
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo actualizar, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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