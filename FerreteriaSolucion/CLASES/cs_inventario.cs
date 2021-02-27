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
    class cs_inventario
    {
        private cs_connection_ferremas connection = new cs_connection_ferremas();
        private SqlCommand command = new SqlCommand();

        public DataTable getReporteInventarioActual(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("dat");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "exec sp_reporte_inventario_actual";
                returnTable.Load(command.ExecuteReader());
                return returnTable;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.connection.Close();
            }
        }


        public DataTable getInventarioGrid(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT inv.id_inventario, cat.descripcion, sub.descripcion, pro.descripcion, pro.marca_producto, pro.codigo_producto, inv.stock " +
                                        "FROM tbl_inventario AS inv " +
                                        "INNER JOIN tbl_producto AS pro ON pro.id_producto = inv.id_producto " +
                                        "INNER JOIN tbl_subcategoria AS sub ON sub.id_subcategoria = pro.id_subcategoria " +
                                        "INNER JOIN tbl_categoria AS cat ON cat.id_categoria = sub.id_categoria " +
                                        "WHERE inv.stock > 0";
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

        public DataTable getProductosToCMB(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT pro.id_producto AS id, pro.codigo_producto + ' ' + cat.descripcion + ' ' + sub.descripcion + ' ' + pro.descripcion + ' ' + pro.marca_producto AS descripcion " +
                                        "FROM tbl_inventario AS inv " +
                                        "INNER JOIN tbl_producto AS pro ON pro.id_producto = inv.id_producto " +
                                        "INNER JOIN tbl_subcategoria AS sub ON sub.id_subcategoria = pro.id_subcategoria " +
                                        "INNER JOIN tbl_categoria AS cat ON cat.id_categoria = sub.id_categoria";
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
            DataTable dt = getProductosToCMB(ref error);

            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();

            foreach (DataRow row in dt.Rows)
            {
                stringCol.Add(Convert.ToString(row["descripcion"]));
            }

            return stringCol;
        }

        public bool updateInventario(ref string error, string cantidad, string precio_unitario, string id_producto, string precio_costo)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();

                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_inventario SET stock = stock + @cantidad, precio_unitario = @precio_unitario, precio_costo = @precio_costo WHERE id_producto = @id_producto";
                command.Parameters.AddWithValue("@cantidad", cantidad);
                command.Parameters.AddWithValue("@precio_unitario", precio_unitario);
                command.Parameters.AddWithValue("@id_producto", id_producto);
                command.Parameters.AddWithValue("@precio_costo", precio_costo);
                if (int.Parse(command.ExecuteNonQuery().ToString())> 0)
                {
                    if (insertLogInventario(ref error, id_producto, cantidad, precio_unitario))
                    {
                        command.Transaction.Commit();
                        return true;
                    }
                    else
                    {
                        throw new SystemException("ERROR AGREGANDO LOG DE INVENTARIO " + error);
                    }
                }
                else
                {
                    throw new SystemException("ERROR AGREGANDO ARTICULOS DE INVENTARIO " + error);
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

        public bool insertLogInventario(ref string error, string id_producto, string cantidad, string precio_unitario)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_log_inventario (id_producto, id_tipo_transaccion, cantidad, fecha_transaccion) " +
                                                               "VALUES(@id_productoLog, 1, @cantidadLog, GETDATE())";
                command.Parameters.AddWithValue("@cantidadLog", cantidad);
                command.Parameters.AddWithValue("@id_productoLog", id_producto);

                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    throw new SystemException("ERROR INSERTANDO LOG DE INVENTARIO");
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }
    }
}
