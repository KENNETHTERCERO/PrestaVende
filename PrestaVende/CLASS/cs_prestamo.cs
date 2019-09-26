using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_prestamo
    {
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public bool guardar_prestamo(ref string error, string[] encabezado, DataTable detalle, string tipo_prenda)
        {
            try
            {
                string numero_prestamo = "", monto = "";
                int id_prestamo_encabezado = 0;
               
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.Transaction = connection.connection.BeginTransaction();
                command.CommandText = "SELECT correlativo_prestamo + 1 FROM tbl_sucursal WHERE id_sucursal = @id_sucursal";
                command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
                numero_prestamo = command.ExecuteScalar().ToString();
                id_prestamo_encabezado = insert_prestamo_encabezado(ref error, encabezado, numero_prestamo);

                monto = encabezado[1];

                if (id_prestamo_encabezado > 0)
                {
                    if (insert_prestamo_detalle(ref error, detalle, id_prestamo_encabezado.ToString(), numero_prestamo, tipo_prenda))
                    {
                        if (insert_transaccion(ref error, monto, numero_prestamo))
                        {
                            if (update_saldo_caja(ref error, monto))
                            {
                                command.Transaction.Commit();
                                return true;
                            }
                            else
                                throw new Exception("No se pudo actualizar el saldo de la caja.");
                        }
                        else
                            throw new Exception("No se pudo insertar transaccion de prestamo.");
                    }
                    else
                        throw new Exception("No se pudo insertar detalle de prestamo.");
                }
                else
                    throw new Exception("No se pudo insertar encabezado de prestamo.");
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

        private int insert_prestamo_encabezado(ref string error, string[] datosEnc, string numero_prestamo)
        {
            try
            {
                int insert = 0;
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_prestamo_encabezado (id_sucursal, id_cliente, numero_prestamo, total_prestamo, fecha_creacion_prestamo, estado_prestamo, fecha_proximo_pago, saldo_prestamo, usuario, id_plan_prestamo, id_interes, id_casilla) " +
                                            "VALUES( " +
                                            "@id_sucursal_enc,         "+  
                                            "@id_cliente,              "+
                                            "@numero_prestamo,         " +
                                            "@total_prestamo,          " +
                                            "GETDATE(),                "+
                                            "@estado_prestamo,         "+
                                            "@fecha_proximo_pago,      "+
                                            "@saldo_prestamo,          "+
                                            "@usuario,                 "+
                                            "@id_plan_prestamo,        "+
                                            "@id_interes,              "+
                                            "@id_casilla )";
                command.Parameters.AddWithValue("@id_sucursal_enc",     cs_usuario.id_sucursal);
                command.Parameters.AddWithValue("@numero_prestamo",     numero_prestamo);
                command.Parameters.AddWithValue("@id_cliente",          datosEnc[0]);
                command.Parameters.AddWithValue("@total_prestamo",      datosEnc[1]);
                command.Parameters.AddWithValue("@estado_prestamo",     datosEnc[2]);
                command.Parameters.AddWithValue("@fecha_proximo_pago",  datosEnc[3]);
                command.Parameters.AddWithValue("@saldo_prestamo",      datosEnc[4]);
                command.Parameters.AddWithValue("@usuario",             datosEnc[5]);
                command.Parameters.AddWithValue("@id_plan_prestamo",    datosEnc[6]);
                command.Parameters.AddWithValue("@id_interes",          datosEnc[7]);
                command.Parameters.AddWithValue("@id_casilla",          datosEnc[8]);
                insert = Convert.ToInt32(command.ExecuteNonQuery());
                if (insert > 0)
                {
                    command.CommandText = "SELECT MAX(id_prestamo_encabezado) fROM tbl_prestamo_encabezado WHERE id_sucursal = @id_sucursal_enc";
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

        private bool insert_prestamo_detalle(ref string error, DataTable detalle, string id_prestamo_encabezado, string numero_prestamo, string tipo_prenda)
        {
            try
            {
                string comando = "";
                int inserts = 0;
                foreach (DataRow item in detalle.Rows)
                {
                    if (tipo_prenda.Equals("1"))
                    {
                        comando = "INSERT INTO tbl_prestamo_detalle (id_prestamo_encabezado, id_sucursal, numero_prestamo, id_producto,     numero_linea,               peso,       id_kilataje,            cantidad,           valor,          id_marca,           caracteristicas,            peso_descuento,         peso_con_descuento) " +
                              $"VALUES({id_prestamo_encabezado}, {cs_usuario.id_sucursal}, {numero_prestamo}, {item["id_producto"].ToString()}, {item["numero_linea"].ToString()}, {item["peso"].ToString()}, {item["id_kilataje"].ToString()}, 1, {item["valor"].ToString()}, 0, '{item["caracteristicas"].ToString()}', {item["descuento"].ToString()}, {item["pesoReal"].ToString()})";
                    }
                    else
                    {
                        comando = "INSERT INTO tbl_prestamo_detalle (id_prestamo_encabezado, id_sucursal, numero_prestamo, id_producto,     numero_linea,               peso,       id_kilataje,            cantidad,           valor,          id_marca,           caracteristicas, peso_descuento, peso_con_descuento) " +
                              $"VALUES({id_prestamo_encabezado}, {cs_usuario.id_sucursal}, {numero_prestamo}, {item["id_producto"].ToString()}, {item["numero_linea"].ToString()}, 0, 0, 1, {item["valor"].ToString()}, {item["id_marca"].ToString()}, '{item["caracteristicas"].ToString()}', 0, 0)";
                    }
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

        private bool insert_transaccion(ref string error, string monto, string numero_prestamo)
        {
            try
            {
                int insert = 0;
                command.CommandText = "INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja, monto, numero_prestamo, estado_transaccion, fecha_transaccion, usuario, movimiento_saldo) " +
                                                                "VALUES(7, @id_caja, @monto, @numero_prestamo_transaccion, 1, GETDATE(), @usuario_transaccion, (SELECT saldo - @monto FROM tbl_caja WHERE id_caja = @id_caja))";
                command.Parameters.AddWithValue("@id_caja",         cs_usuario.id_caja);
                command.Parameters.AddWithValue("@monto",           monto);
                command.Parameters.AddWithValue("@numero_prestamo_transaccion", numero_prestamo);
                command.Parameters.AddWithValue("@usuario_transaccion",         cs_usuario.usuario);
                insert = command.ExecuteNonQuery();
                if (insert > 0)
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

        private bool update_saldo_caja(ref string error, string monto)
        {
            try
            {
                int update = 0;
                command.CommandText = "UPDATE tbl_caja SET saldo = saldo - @monto_update WHERE id_caja = @id_caja_update";
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

        public DateTime fecha_proximo_pago(ref string error, string plan)
        {
            try
            {
                DateTime returnDate = DateTime.Now;
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "select dbo.fecha_proximo_pago(@fecha, @plan)";
                command.Parameters.AddWithValue("@fecha", returnDate);
                command.Parameters.AddWithValue("@plan", plan);
                returnDate = Convert.ToDateTime(command.ExecuteScalar().ToString());
                return returnDate;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return DateTime.Now;
            }
            finally
            {
                connection.connection.Close();
            }
        }
    }
}