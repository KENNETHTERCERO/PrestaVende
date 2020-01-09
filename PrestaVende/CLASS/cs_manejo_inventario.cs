using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_manejo_inventario
    {
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public DataTable getArticulos(ref string error, string numero_prestamo)
        {
            try
            {
                DataTable dtArticulos = new DataTable("dtArticulos");

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec sp_obtiene_producto_venta @id_sucursal, @numero_prestamo";
                command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
                dtArticulos.Load(command.ExecuteReader());
                return dtArticulos;
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

        public DataTable getArticuloEspecifico(ref string error, string numero_prestamo, string id_inventario)
        {
            try
            {
                DataTable dtArticulos = new DataTable("dtArticulos");

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec sp_obtiene_producto_venta_detalle @id_sucursal, @numero_prestamo, @id_inventario";
                command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
                command.Parameters.AddWithValue("@id_inventario", id_inventario);
                dtArticulos.Load(command.ExecuteReader());
                return dtArticulos;
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

        public bool GuardarFactura(ref string error, DataTable detalleFactura, string[] encabezado, ref int id_factura_encabezado)
        {
            try
            {
                string numero_factura = "";

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.Transaction = connection.connection.BeginTransaction();

                command.CommandText = "SELECT correlativo + 1 FROM tbl_serie WHERE id_sucursal = @id_sucursal and id_serie = @id_serie";
                command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
                command.Parameters.AddWithValue("@id_serie", encabezado[0]);
                numero_factura = command.ExecuteScalar().ToString();

                id_factura_encabezado = insert_factura_encabezado(ref error, encabezado);

                if (id_factura_encabezado > 0)
                {
                    if (insert_factura_detalle(ref error, detalleFactura, id_factura_encabezado.ToString()))
                    {
                        if (insert_transaccion(ref error, encabezado[3].ToString(), numero_factura, encabezado[0]))
                        {
                            if (update_saldo_caja(ref error, encabezado[3].ToString()))
                            {
                                if (update_Inventario(ref error, detalleFactura, encabezado[0], numero_factura))
                                {
                                    if (update_correlativo_serie(ref error, encabezado[0], numero_factura))
                                    {
                                        command.Transaction.Commit();
                                        return true;
                                    }
                                    else
                                        throw new Exception("No se pudo actualizar el correlativo de factura. " + error);
                                }
                                else
                                    throw new Exception("No se pudo actualizar el inventario. " + error);
                            }
                            else
                                throw new Exception("No se pudo actualizar el saldo de la caja. " + error);
                        }
                        else
                            throw new Exception("No se pudo insertar transaccion de la factura. " + error);
                    }
                    else
                        throw new Exception("No se pudo insertar detalle de factura. " + error);
                }
                else
                    throw new Exception("No se pudo insertar encabezado de factura. " + error);
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

        private int insert_factura_encabezado(ref string error, string[] datosEnc)
        {
            try
            {
                int insert = 0;
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_factura_encabezado ([id_serie],[numero_factura],[id_cliente],[total_factura],[sub_total_factura],[iva_total_factura],[id_tipo_transaccion],[id_caja],[numero_prestamo],[monto_abono_capital],[monto_cancelacion],[estado_factura], fecha_creacion) " +
                                            "VALUES( " +
                                            "@id_serie,             " +
                                            "@numero_factura,       " +
                                            "@id_cliente,           " +
                                            "@total_factura,        " +
                                            "@sub_total_factura,    " +
                                            "@iva_total_factura,    " +
                                            "@id_tipo_transaccion,  " +
                                            "@id_caja,              " +
                                            "@numero_prestamo,      " +
                                            "@monto_abono_capital,  " +
                                            "@monto_cancelacion,    " +
                                            "@estado, " +
                                            "GETDATE())";
                command.Parameters.AddWithValue("@id_serie", datosEnc[0]);
                command.Parameters.AddWithValue("@numero_factura", datosEnc[1]);
                command.Parameters.AddWithValue("@id_cliente", datosEnc[2]);
                command.Parameters.AddWithValue("@total_factura", datosEnc[3]);
                command.Parameters.AddWithValue("@sub_total_factura", datosEnc[4]);
                command.Parameters.AddWithValue("@iva_total_factura", datosEnc[5]);
                command.Parameters.AddWithValue("@id_tipo_transaccion", datosEnc[6]);
                command.Parameters.AddWithValue("@id_caja", datosEnc[7]);
                command.Parameters.AddWithValue("@numero_prestamo", datosEnc[8]);
                command.Parameters.AddWithValue("@monto_abono_capital", datosEnc[9]);
                command.Parameters.AddWithValue("@monto_cancelacion", datosEnc[10]);
                command.Parameters.AddWithValue("@estado", "1");
                insert = Convert.ToInt32(command.ExecuteNonQuery());
                if (insert > 0)
                {
                    command.CommandText = "SELECT MAX(id_factura_encabezado) fROM tbl_factura_encabezado WHERE id_serie = @id_serie AND numero_factura = @numero_factura";
                    insert = Convert.ToInt32(command.ExecuteScalar().ToString());
                    return insert;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return 0;
            }
        }

        private bool insert_factura_detalle(ref string error, DataTable detalle, string id_factura_encabezado)
        {
            try
            {
                string comando = "";
                int inserts = 0;
                foreach (DataRow item in detalle.Rows)
                {
                    comando = "INSERT INTO tbl_factura_detalle (id_factura_encabezado, numero_prestamo, numero_linea, cantidad, precio, total_fila, sub_total_fila, iva_fila, bien_servicio, descripcion_detalle) " +
                        $"VALUES({id_factura_encabezado}, {item["numero_prestamo"].ToString()}, {item["numero_linea"].ToString()}, 1, {item["valor"].ToString()}, {item["valor"].ToString()}" +
                        $",{item["subTotal"].ToString()}, {item["IVA"].ToString()}, 'B', '{item["caracteristicas"].ToString()}')";

                    command.CommandText = comando;
                    inserts += command.ExecuteNonQuery();
                }

                if (inserts != detalle.Rows.Count)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        private bool insert_transaccion(ref string error, string monto, string numero_factura, string id_serie)
        {
            try
            {
                command.Parameters.Clear();
                int insert = 0;
                command.CommandText = "INSERT INTO tbl_transaccion (id_tipo_transaccion,    id_caja,    monto,  estado_transaccion, fecha_transaccion,      usuario,            movimiento_saldo,                                             numero_factura,   id_serie,   id_sucursal) " +
                                                           "VALUES(@id_tipo_transaccion,    @id_caja,   @monto,         1,              GETDATE(),      @usuario_transaccion, (SELECT saldo + @monto FROM tbl_caja WHERE id_caja = @id_caja), @numero_factura,  @id_serie, @id_sucursal)";
                command.Parameters.AddWithValue("@id_tipo_transaccion", "13");
                command.Parameters.AddWithValue("@id_caja", cs_usuario.id_caja);
                command.Parameters.AddWithValue("@id_serie", id_serie);
                command.Parameters.AddWithValue("@numero_factura", numero_factura);
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@usuario_transaccion", cs_usuario.usuario);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
                insert = command.ExecuteNonQuery();

                if (insert > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        private bool update_saldo_caja(ref string error, string monto)
        {
            try
            {
                int update = 0;
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_caja SET saldo = saldo + @monto_update WHERE id_caja = @id_caja_update";
                command.Parameters.AddWithValue("@monto_update", monto);
                command.Parameters.AddWithValue("@id_caja_update", cs_usuario.id_caja);

                update = command.ExecuteNonQuery();
                if (update > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        private bool update_Inventario(ref string error, DataTable detalle, string id_serie, string numero_factura)
        {
            try
            {
                string comando = "";
                int inserts = 0;
                foreach (DataRow item in detalle.Rows)
                {
                    comando = $"UPDATE tbl_inventario SET estado_inventario = 1, fecha_vendido = GETDATE(), id_serie = {id_serie}, numero_factura = {numero_factura} " + 
                                $"WHERE id_inventario = {item["id_inventario"].ToString()}";

                    command.CommandText = comando;
                    inserts += command.ExecuteNonQuery();
                }

                if (inserts != detalle.Rows.Count)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        private bool update_correlativo_serie(ref string error, string id_serie, string numero_factura)
        {
            try
            {
                int update = 0;
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_serie SET correlativo = @numero_factura, " +
                                      "estado = CASE WHEN @numero_factura >= numero_de_facturas THEN  0 ELSE estado END " +
                                      "WHERE id_sucursal = @id_sucursal AND id_serie = @id_serie";
                command.Parameters.AddWithValue("@id_serie", id_serie);
                command.Parameters.AddWithValue("@numero_factura", numero_factura);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);

                update = command.ExecuteNonQuery();
                if (update > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }
    }
}