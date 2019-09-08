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

        public string getIDMaxProducto(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_producto), 0) +1 FROM tbl_producto";
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

        public DataTable getProducto(ref string error)
        {
            try
            {
                DataTable Producto = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = " SELECT p.id_producto, "
                                        + "        c.categoria, "
                                        + " 	   sc.sub_categoria, "
                                        + " 	   p.producto, "
                                        + " 	   p.precio_sugerido, "
                                        + " 	   (CASE WHEN p.estado = 1 THEN 'ACTIVO' WHEN p.estado = 0 THEN 'INACTIVO' ELSE 'OTRO ESTADO' END) AS estado "
                                        + " FROM tbl_producto p "
                                        + "     INNER JOIN tbl_subcategoria sc "
                                        + "         ON sc.id_sub_categoria = p.id_sub_categoria "
                                        + "     INNER JOIN tbl_categoria c "
                                        + "         ON c.id_categoria = sc.id_categoria ";
                Producto.Load(command.ExecuteReader());
                return Producto;
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
                DataTable categoria = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_categoria, 'SELECCIONAR' AS categoria UNION SELECT id_categoria, UPPER(categoria) From tbl_categoria WHERE estado = 1";
                categoria.Load(command.ExecuteReader());
                return categoria;
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

        public DataTable getSubCategoria(ref string error, string id_categoria)
        {
            try
            {
                DataTable getSubCategoria = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_sub_categoria, 'SELECCIONAR' AS sub_categoria UNION SELECT id_sub_categoria, UPPER(sub_categoria) From tbl_subcategoria WHERE estado = 1 and id_categoria = @id_categoria";
                command.Parameters.AddWithValue("@id_categoria", id_categoria);
                getSubCategoria.Load(command.ExecuteReader());
                return getSubCategoria;
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

        public string getNombreCategoria(ref string error, string id_sub_categoria)
        {
            try
            {
                DataTable getNombreCategoria = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = " SELECT DISTINCT(c.id_categoria) FROM tbl_subcategoria s  "
                                        + " INNER JOIN tbl_categoria c "
                                        + "     ON s.id_categoria = c.id_categoria "
                                        + " WHERE s.id_sub_categoria = @id_sub_categoria ";
                command.Parameters.AddWithValue("@id_sub_categoria", id_sub_categoria);
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

        public DataTable getEstadoProducto(ref string error)
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

        public DataTable getObtieneDatosModificar(ref string error, string id_producto)
        {
            try
            {
                DataTable DatosProducto = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT id_producto, id_sub_categoria, producto, precio_sugerido, estado FROM tbl_producto WHERE id_producto = @id_producto";
                command.Parameters.AddWithValue("@id_producto", id_producto);
                DatosProducto.Load(command.ExecuteReader());
                return DatosProducto;
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

        public bool insertProducto(ref string error, string id_sub_categoria, string producto, string precio_sugerido, string estado)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_producto (id_sub_categoria, producto, precio_sugerido, estado) "
                                     + " VALUES(@id_sub_categoria, @producto, @precio_sugerido, @estado)";
                command.Parameters.AddWithValue("@id_sub_categoria", id_sub_categoria);
                command.Parameters.AddWithValue("@producto", producto);
                command.Parameters.AddWithValue("@precio_sugerido", precio_sugerido);
                command.Parameters.AddWithValue("@estado", estado);
               
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo insertar el producto, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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

        public bool updateProducto(ref string error, string[] datos)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_producto SET precio_sugerido = @precio_sugerido, estado = @estado WHERE id_producto = @id_producto";
                command.Parameters.AddWithValue("@id_producto", datos[0]);
                command.Parameters.AddWithValue("@precio_sugerido", datos[1]);
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