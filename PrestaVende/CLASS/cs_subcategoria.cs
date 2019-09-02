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


        public string getIDMaxSubCategoria(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_sub_categoria), 0) +1 FROM tbl_subcategoria";
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
                DataTable SubCategoria = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = " select id_sub_categoria, "
                                    + "        c.categoria,      "
                                    + " 	   sc.sub_categoria, "
                                    + " 	   (CASE WHEN sc.estado = 1 THEN 'ACTIVO' WHEN sc.estado = 0 THEN 'INACTIVO' ELSE 'OTRO ESTADO' END) AS estado "
                                    + " from tbl_subcategoria sc "
                                    + "     inner join tbl_categoria c "
                                    + " on sc.id_categoria = c.id_categoria  ";
                SubCategoria.Load(command.ExecuteReader());
                return SubCategoria;
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
                DataTable Categoria = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_categoria, 'SELECCIONAR' AS categoria UNION SELECT id_categoria, UPPER(categoria) From tbl_categoria WHERE estado = 1";
                Categoria.Load(command.ExecuteReader());
                return Categoria;
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

        public DataTable getEstadoSubCategoria(ref string error)
        {
            try
            {
                DataTable EstadoAreaEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id, 'INACTIVO' AS estado UNION SELECT 1 AS id, 'ACTIVO' AS estado";
                EstadoAreaEmpresa.Load(command.ExecuteReader());
                return EstadoAreaEmpresa;
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

        public DataTable getObtieneDatosModificar(ref string error, string id_sub_categoria)
        {
            try
            {
                DataTable DatosSubCategoria = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = " SELECT id_sub_categoria, id_categoria, sub_categoria, estado FROM tbl_subcategoria WHERE id_sub_categoria = @id_sub_categoria ";
                command.Parameters.AddWithValue("@id_sub_categoria", id_sub_categoria);
                DatosSubCategoria.Load(command.ExecuteReader());
                return DatosSubCategoria;
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

        public bool insertSubCategoria(ref string error, string id_sub_categoria, string id_categoria, string sub_categoria, string estado, string fecha_creacion)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = " INSERT INTO tbl_subcategoria (id_sub_categoria, id_categoria, sub_categoria ,estado, fecha_creacion) "
                                    + " VALUES(@id_sub_categoria, @id_categoria, @sub_categoria, @estado, @fecha_creacion) ";
                command.Parameters.AddWithValue("@id_sub_categoria", id_sub_categoria);
                command.Parameters.AddWithValue("@id_categoria", id_categoria);
                command.Parameters.AddWithValue("@sub_categoria", sub_categoria);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@fecha_creacion", fecha_creacion);
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo insertar la sub categoria, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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

        public bool updateSubCategoria(ref string error, string[] datos)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_subcategoria SET estado = @estado WHERE id_sub_categoria = @id_sub_categoria";
                command.Parameters.AddWithValue("@id_sub_categoria", datos[0]);
                command.Parameters.AddWithValue("@estado", datos[3]);
                
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