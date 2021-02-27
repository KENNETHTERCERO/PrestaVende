using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerreteriaSolucion.CLASES
{
    class cs_producto
    {
        private cs_connection_ferremas connection = new cs_connection_ferremas();
        private SqlCommand command = new SqlCommand();

        public DataTable getDataProductSpecific(ref string error, string codigo_producto)
        {
            try
            {
                DataTable returnTable = new DataTable("data");
                connection.connection.Open();
                command.Connection = connection.connection;
                
                command.CommandText = "SELECT " +
                                        "pr.id_producto AS [ID], " +
                                        "sub.descripcion + ' ' + pr.descripcion AS [DESCRIPCION PRODUCTO], " +
                                        "pr.marca_producto AS [MARCA], " +
                                        "pr.codigo_producto AS [CODIGO], " +
                                        "inv.stock , " +
                                        "inv.precio_unitario " +
                                        "FROM tbl_producto AS pr " +
                                        "INNER JOIN tbl_inventario AS inv ON inv.id_producto = pr.id_producto " +
                                        "INNER JOIN tbl_subcategoria AS sub ON sub.id_subcategoria = pr.id_subcategoria " +
                                        "WHERE pr.estado = 1 " +
                                        $"AND pr.codigo_producto = '{codigo_producto}'";
                
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

        public DataTable getDataProductSpecificID(ref string error, string id_producto)
        {
            try
            {
                DataTable returnTable = new DataTable("data");
                connection.connection.Open();
                command.Connection = connection.connection;

                command.CommandText = "SELECT " +
                                        "pr.id_producto AS [ID], " +
                                        "sub.descripcion + ' ' + pr.descripcion AS [DESCRIPCION PRODUCTO], " +
                                        "pr.marca_producto AS [MARCA], " +
                                        "pr.codigo_producto AS [CODIGO], " +
                                        "inv.stock , " +
                                        "inv.precio_unitario " +
                                        "FROM tbl_producto AS pr " +
                                        "INNER JOIN tbl_inventario AS inv ON inv.id_producto = pr.id_producto " +
                                        "INNER JOIN tbl_subcategoria AS sub ON sub.id_subcategoria = pr.id_subcategoria " +
                                        "WHERE pr.estado = 1 " +
                                        $"AND pr.id_producto = '{id_producto}' " +
                                        "AND inv.stock > 0";

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

        public DataTable getAllProducts(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("data");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id, 'SELECCIONAR' AS descripcion UNION SELECT id_producto AS id, cat.descripcion + ' ' + sub.descripcion + ' ' + pro.marca_producto  + ' ' + pro.descripcion AS descripcion " +
                                      "FROM tbl_producto AS pro INNER JOIN tbl_subcategoria AS sub ON sub.id_subcategoria = pro.id_subcategoria INNER JOIN tbl_categoria AS cat ON cat.id_categoria = sub.id_categoria WHERE pro.estado = 1";
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

        public AutoCompleteStringCollection LoadAutoComplete()
        {
            string error = "";
            DataTable dt = getAllProducts(ref error);

            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();

            foreach (DataRow row in dt.Rows)
            {
                stringCol.Add(Convert.ToString(row["descripcion"]));
            }

            return stringCol;
        }

        public DataTable getProductToMaintenance(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("dat");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT pro.id_producto, sub.id_subcategoria, sub.descripcion, pro.descripcion, pro.marca_producto, pro.codigo_producto, (CASE WHEN pro.estado = 1 THEN 'ACTIVO' WHEN pro.estado = 0 THEN 'INACTIVO' END), pro.estado FROM tbl_producto AS pro INNER JOIN tbl_subcategoria AS sub ON sub.id_subcategoria = pro.id_subcategoria";
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

        public string getMaxProducto(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_producto), 0) + 1 AS id FROM tbl_producto";
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

        public bool insertProducto(ref string error, string id_subcategoria, string descripcion, string marca_producto, string estado, string codigo_producto, string cantidad, string precio_unitario, string precio_costo)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_producto (id_subcategoria, descripcion, marca_producto, estado, codigo_producto, contador_productos) " +
                                                         "VALUES(@id_subcategoria, @descripcion, @marca_producto, @estado, @codigo_producto, 1)";
                command.Parameters.AddWithValue("@id_subcategoria", id_subcategoria);
                command.Parameters.AddWithValue("@descripcion", descripcion);
                command.Parameters.AddWithValue("@marca_producto", marca_producto);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@codigo_producto", codigo_producto);

                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    string id_producto = "";
                    command.CommandText = "SELECT ISNULL(MAX(id_producto), 0) FROM tbl_producto";
                    id_producto = command.ExecuteScalar().ToString();
                    if (insertInventario(ref error, id_producto, cantidad, precio_unitario, precio_costo))
                    {
                        command.Transaction.Commit();
                        return true;
                    }
                    else
                    {
                        throw new SystemException("ERROR AGREGANDO A INVENTARIO");
                    }
                }
                else
                {
                    throw new SystemException("NO SE PUDO AGREGAR PRODUCTO");
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

        private bool insertInventario(ref string error, string id_inventario, string cantidad, string precio_unitario, string precio_costo)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_inventario (id_producto, stock, precio_unitario, estado) " +
                                                           "VALUES(@id_productoInv, 0, 0, 1)";
                command.Parameters.AddWithValue("@id_productoInv", id_inventario);
                command.Parameters.AddWithValue("@cantidad", cantidad);
                command.Parameters.AddWithValue("@precio_unitario", precio_unitario);
                command.Parameters.AddWithValue("@precio_costo", precio_costo);
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        public bool updateProducto(ref string error, string id_subcategoria, string descripcion, string marca_producto, string estado, string codigo_producto, string id_producto)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_producto SET id_subcategoria = @id_subcategoria, descripcion = @descripcion, marca_producto = @marca_producto, estado = @estado, codigo_producto = @codigo_producto WHERE id_producto = @id_producto";
                command.Parameters.AddWithValue("@id_subcategoria", id_subcategoria);
                command.Parameters.AddWithValue("@descripcion", descripcion);
                command.Parameters.AddWithValue("@marca_producto", marca_producto);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@codigo_producto", codigo_producto);
                command.Parameters.AddWithValue("@id_producto", id_producto);
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("NO SE PUDO AGREGAR PRODUCTO");
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
