﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_factura
    {
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public cs_factura()
        {
            
        }

        public DataTable ObtenerFacturas(ref string error, string id_prestamo)
        {
            DataTable dtReturnFacturas = new DataTable("dtFacturas");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select isnull(se.serie,'Creacion') serie,isnull(cast(numero_factura as varchar(50)),'Prestamo') numero_factura, " +
                                        "tr.numero_prestamo,cast(fecha_transaccion as date) fecha_transaccion " +
                                        "from tbl_transaccion tr " +
                                        "inner join tbl_prestamo_encabezado pre on pre.numero_prestamo = tr.numero_prestamo " +
                                        "left join tbl_serie se on se.id_serie = tr.id_serie " +
                                        "where pre.id_prestamo_encabezado = @id_prestamo";
                command.Parameters.AddWithValue("@id_prestamo", id_prestamo);
                dtReturnFacturas.Load(command.ExecuteReader());
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
            return dtReturnFacturas;
        }

        public DataTable ObtenerFactura(ref string error, string id_factura)
        {
            DataTable dtReturnFacturas = new DataTable("dtFacturas");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "EXEC sp_obtiene_factura_impresion @id_factura";
                command.Parameters.AddWithValue("@id_factura", id_factura);
                dtReturnFacturas.Load(command.ExecuteReader());
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
            return dtReturnFacturas;
        }

        public DataTable ObtenerFacturaRecibo(ref string error, string id_factura, string id_sucursal)
        {
            DataTable dtReturnFacturas = new DataTable("dtFacturas");
            DataSet ds = new DataSet();

            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();

                SqlDataAdapter adapter;
                SqlParameter param;
                SqlParameter paramS;

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_ConsultaImpresionRecibo";

                param = new SqlParameter("@IdFactura", id_factura);                
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.Int16;
                command.Parameters.Add(param);

                paramS = new SqlParameter("@IdSucursal", id_sucursal);
                paramS.Direction = ParameterDirection.Input;
                paramS.DbType =  DbType.Int16;                
                command.Parameters.Add(paramS);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

                dtReturnFacturas = ds.Tables[0];
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
            return dtReturnFacturas;
        }

        public DataSet ObtenerDetalleFacturas(ref string error, string id_prestamo)
        {
            //DataTable dtReturnFacturas = new DataTable("dtDetalleFacturas");
            DataSet ds = new DataSet();
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();

                SqlDataAdapter adapter;
                SqlParameter param;                

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CalculoCargosPrestamo";

                param = new SqlParameter("@id_encabezado_prestamo", id_prestamo);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

                //command.CommandText = "exec SP_CalculoCargosPrestamo @id_prestamo";
                //command.Parameters.AddWithValue("@id_prestamo", id_prestamo);
                //dtReturnFacturas.Load(command.ExecuteReader());


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
            return ds;
        }

        public string GuardarFactura(ref string error, DataSet DatosFactura, string id_serie, string id_cliente, string id_tipo_transaccion, int id_caja, string numero_prestamo, string abono)
        {
            bool Resultado = false;
            int id_factura_encabezado = 0;

            try
            {                
                string numero_factura = "";
                string fecha_proximo_pago = "";
                string fecha_ultimo_pago = "";
                string[] encabezado = new string[12];

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.Transaction = connection.connection.BeginTransaction();

                command.CommandText = "SELECT correlativo + 1 FROM tbl_serie WHERE id_sucursal = @id_sucursal and id_serie = @id_serie";
                command.Parameters.AddWithValue("@id_sucursal", (int)HttpContext.Current.Session["id_sucursal"]);
                command.Parameters.AddWithValue("@id_serie", id_serie);
                numero_factura = command.ExecuteScalar().ToString();

                encabezado[0] = id_serie;
                encabezado[1] = numero_factura; 
                encabezado[2] = id_cliente;
                encabezado[3] = DatosFactura.Tables[1].Rows[0]["Total"].ToString().Replace(",", ".");
                encabezado[4] = DatosFactura.Tables[1].Rows[0]["SubTotal"].ToString().Replace(",", ".");
                encabezado[5] = DatosFactura.Tables[1].Rows[0]["IVA"].ToString().Replace(",", ".");
                encabezado[6] = id_tipo_transaccion;
                encabezado[7] = id_caja.ToString();
                encabezado[8] = numero_prestamo;
                encabezado[9] = (id_tipo_transaccion == "9") ? abono : "0"; 
                encabezado[10] = (id_tipo_transaccion == "10") ? abono : "0";
                fecha_proximo_pago = DatosFactura.Tables[0].Rows[0]["calculo_fecha_proximo_pago"].ToString();
                fecha_ultimo_pago = DatosFactura.Tables[0].Rows[0]["calculo_fecha_ultimo_pago"].ToString();

                id_factura_encabezado = insert_factura_encabezado(ref error, encabezado);

                if (id_factura_encabezado > 0)
                {
                    if (insert_factura_detalle(ref error, DatosFactura.Tables[0], id_factura_encabezado.ToString(), numero_prestamo))
                    {
                        if (insert_transaccion(ref error, encabezado[3].ToString(), numero_prestamo, id_caja.ToString(), id_tipo_transaccion, numero_factura, id_serie, abono))
                        {
                            if (update_saldo_caja(ref error, encabezado[3].ToString(), abono))
                            {
                                if (update_prestamo(ref error, numero_prestamo, abono, id_tipo_transaccion,fecha_proximo_pago,fecha_ultimo_pago))
                                {
                                    if (update_correlativo_serie(ref error, id_serie, numero_factura))
                                    {
                                        if (id_tipo_transaccion == "9" || id_tipo_transaccion == "10")
                                        {
                                            if (id_tipo_transaccion == "9")
                                            {
                                                if (insert_abono(ref error, numero_prestamo, numero_factura, id_serie, abono))
                                                {
                                                    command.Transaction.Commit();
                                                    Resultado = true;
                                                }
                                                else
                                                    throw new Exception("No se pudo ingresar el abono de factura.");
                                            }
                                            else
                                            {
                                                if (insert_cancelacion(ref error, numero_prestamo, numero_factura, id_serie, abono))
                                                {
                                                    command.Transaction.Commit();
                                                    Resultado = true;
                                                }
                                                else
                                                    throw new Exception("No se pudo ingresar la cancelacion.");
                                            }
                                        }
                                        else {
                                            command.Transaction.Commit();
                                            Resultado = true;
                                        }
                                    }
                                    else
                                        throw new Exception("No se pudo actualizar el correlativo de factura.");
                                }
                                else
                                    throw new Exception("No se pudo actualizar el prestamo.");
                            }
                            else
                                throw new Exception("No se pudo actualizar el saldo de la caja.");                            
                        } 
                        else
                            throw new Exception("No se pudo insertar transaccion de la factura.");
                    }
                    else
                        throw new Exception("No se pudo insertar detalle de factura.");
                }
                else
                    throw new Exception("No se pudo insertar encabezado de factura.");
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                Resultado = false;
                command.Transaction.Rollback();
                return string.Empty;
            }
            finally
            {
                connection.connection.Close();
            }
            
            return Convert.ToString(id_factura_encabezado);
        }

        private int insert_factura_encabezado(ref string error, string[] datosEnc)
        {
            try
            {
                int insert = 0;
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_factura_encabezado ([id_serie],[numero_factura],[id_cliente],[total_factura],[sub_total_factura],[iva_total_factura],[id_tipo_transaccion],[id_caja],[numero_prestamo],[monto_abono_capital],[monto_cancelacion],[estado_factura], fecha_creacion) " +
                                            "VALUES( " +
                                            "@id_serie_enc,             " +
                                            "@numero_factura_enc,       " +
                                            "@id_cliente_enc,           " +
                                            "@total_factura_enc,        " +
                                            "@sub_total_factura_enc,    " +
                                            "@iva_total_factura_enc,    " +
                                            "@id_tipo_transaccion_enc,  " +
                                            "@id_caja_enc,              " +
                                            "@numero_prestamo_enc,      " +
                                            "@monto_abono_capital_enc,  " +
                                            "@monto_cancelacion_enc,    " +
                                            "@estado_enc, " +
                                            "GETDATE())";
                command.Parameters.AddWithValue("@id_serie_enc", datosEnc[0]);
                command.Parameters.AddWithValue("@numero_factura_enc", datosEnc[1]);
                command.Parameters.AddWithValue("@id_cliente_enc", datosEnc[2]);
                command.Parameters.AddWithValue("@total_factura_enc", datosEnc[3]);
                command.Parameters.AddWithValue("@sub_total_factura_enc", datosEnc[4]);
                command.Parameters.AddWithValue("@iva_total_factura_enc", datosEnc[5]);
                command.Parameters.AddWithValue("@id_tipo_transaccion_enc", datosEnc[6]);
                command.Parameters.AddWithValue("@id_caja_enc", datosEnc[7]);
                command.Parameters.AddWithValue("@numero_prestamo_enc", datosEnc[8]);
                command.Parameters.AddWithValue("@monto_abono_capital_enc", datosEnc[9]);
                command.Parameters.AddWithValue("@monto_cancelacion_enc", datosEnc[10]);
                command.Parameters.AddWithValue("@estado_enc", "1");
                insert = Convert.ToInt32(command.ExecuteNonQuery());
                if (insert > 0)
                {
                    command.CommandText = "SELECT MAX(id_factura_encabezado) fROM tbl_factura_encabezado WHERE id_serie = @id_serie_enc AND numero_factura = @numero_factura_enc";
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

        private bool insert_factura_detalle(ref string error, DataTable detalle, string id_factura_encabezado, string numero_prestamo)
        {
            try
            {
                string comando = "";
                int inserts = 0;
                foreach (DataRow item in detalle.Rows)
                {
                    comando = "INSERT INTO tbl_factura_detalle (id_factura_encabezado, numero_prestamo, numero_linea, cantidad, precio, total_fila, sub_total_fila, iva_fila, bien_servicio, descripcion_detalle) " +
                        $"VALUES({id_factura_encabezado}, {numero_prestamo}, {(inserts + 1).ToString()}, {item["Cantidad"].ToString()}, {item["Precio"].ToString()}, {item["SubTotal"].ToString()}" +
                        $",{(decimal.Parse(item["SubTotal"].ToString()) - decimal.Parse(item["IVA"].ToString())).ToString()}, {item["IVA"].ToString()}, 'S', '{item["cargo"].ToString()}')";


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

        private bool insert_abono(ref string error, string numero_prestamo, string numero_factura, string id_serie, string abono)
        {
            try
            {
                int insert = 0;
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_abono_a_capital (id_sucursal,numero_prestamo,monto_abono,id_serie,numero_factura,estado_abono,fecha_abono) " +
                                                                "VALUES(@id_sucursal_abono, @numero_prestamo_abono, @abono_abono, @id_serie_abono, @numero_factura_abono, 1, GETDATE())";
                command.Parameters.AddWithValue("@id_sucursal_abono", (int)HttpContext.Current.Session["id_sucursal"]);
                command.Parameters.AddWithValue("@numero_prestamo_abono", numero_prestamo);
                command.Parameters.AddWithValue("@abono_abono", abono);
                command.Parameters.AddWithValue("@id_serie_abono", id_serie);
                command.Parameters.AddWithValue("@numero_factura_abono", numero_factura);
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

        private bool insert_cancelacion(ref string error, string numero_prestamo, string numero_factura, string id_serie, string abono)
        {
            try
            {
                command.Parameters.Clear();
                int insert = 0;
                command.CommandText = "INSERT INTO tbl_cancelacion (id_sucursal,numero_prestamo,monto_cancelacion,id_serie,numero_factura,estado_cancelacion,fecha_cancelacion) " +
                                                                "VALUES(@id_sucursal_cancelacion, @numero_prestamo_cancelacion, @abono_cancelacion, @id_serie_cancelacion, @numero_factura_cancelacion, 1, GETDATE())";
                command.Parameters.AddWithValue("@id_sucursal_cancelacion", (int)HttpContext.Current.Session["id_sucursal"]);
                command.Parameters.AddWithValue("@numero_prestamo_cancelacion", numero_prestamo);
                command.Parameters.AddWithValue("@abono_cancelacion", abono);
                command.Parameters.AddWithValue("@id_serie_cancelacion", id_serie);
                command.Parameters.AddWithValue("@numero_factura_cancelacion", numero_factura);
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

        private bool insert_transaccion(ref string error, string monto, string numero_prestamo, string id_caja, string id_tipo_transaccion, string numero_factura, string id_serie, string abono)
        {
            try
            {
                command.Parameters.Clear();
                int insert = 0;
                command.CommandText = "INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja, monto, numero_prestamo, estado_transaccion, fecha_transaccion, usuario, movimiento_saldo, numero_factura, id_serie, id_sucursal) " +
                                                                "VALUES(@id_tipo_transaccion, @id_caja_transaccion, @monto_transaccion, @numero_prestamo_transaccion, 1, GETDATE(), @usuario_transaccion, " +
                                                                "(SELECT saldo + @monto_transaccion FROM tbl_caja WHERE id_caja = @id_caja_transaccion), @numero_factura_transaccion, @id_serie_transaccion, @id_sucursal_transaccion)";
                command.Parameters.AddWithValue("@id_tipo_transaccion", "8");
                command.Parameters.AddWithValue("@id_caja_transaccion", id_caja);
                command.Parameters.AddWithValue("@id_serie_transaccion", id_serie);
                command.Parameters.AddWithValue("@numero_factura_transaccion", numero_factura);
                command.Parameters.AddWithValue("@monto_transaccion", monto);
                command.Parameters.AddWithValue("@numero_prestamo_transaccion", numero_prestamo);
                command.Parameters.AddWithValue("@usuario_transaccion", (string)HttpContext.Current.Session["usuario"]);
                command.Parameters.AddWithValue("@id_sucursal_transaccion", (int)HttpContext.Current.Session["id_sucursal"]);
                insert = command.ExecuteNonQuery();

                if (insert > 0)
                {
                    if (id_tipo_transaccion == "9" || id_tipo_transaccion == "10")
                    {
                        command.Parameters.Clear();
                        int insert2 = 0;
                        command.CommandText = "INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja, monto, numero_prestamo, estado_transaccion, fecha_transaccion, usuario, movimiento_saldo, numero_factura, id_serie, id_sucursal) " +
                                                                        "VALUES(@id_tipo_transaccion, @id_caja_transaccion, @abono_transaccion, @numero_prestamo_transaccion, 1, GETDATE(), @usuario_transaccion, " +
                                                                        "(SELECT saldo + @abono_transaccion FROM tbl_caja WHERE id_caja = @id_caja_transaccion), @numero_factura_transaccion, @id_serie_transaccion, @id_sucursal_transaccion)";
                        command.Parameters.AddWithValue("@id_tipo_transaccion", id_tipo_transaccion);
                        command.Parameters.AddWithValue("@id_caja_transaccion", id_caja);
                        command.Parameters.AddWithValue("@id_serie_transaccion", id_serie);
                        command.Parameters.AddWithValue("@numero_factura_transaccion", numero_factura);
                        command.Parameters.AddWithValue("@abono_transaccion", abono);
                        command.Parameters.AddWithValue("@monto_transaccion", monto);
                        command.Parameters.AddWithValue("@numero_prestamo_transaccion", numero_prestamo);
                        command.Parameters.AddWithValue("@usuario_transaccion", (string)HttpContext.Current.Session["usuario"]);
                        command.Parameters.AddWithValue("@id_sucursal_transaccion", (int)HttpContext.Current.Session["id_sucursal"]);
                        insert2 = command.ExecuteNonQuery();

                        if (insert2 > 0)
                            return true;
                        else
                            return false;
                    }
                    else
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

        private bool update_saldo_caja(ref string error, string monto, string abono)
        {
            try
            {
                int update = 0;
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_caja SET saldo = saldo + @monto_update + @abono_update WHERE id_caja = @id_caja_update";
                command.Parameters.AddWithValue("@monto_update", monto);
                command.Parameters.AddWithValue("@abono_update", abono);
                command.Parameters.AddWithValue("@id_caja_update", (int)HttpContext.Current.Session["id_caja"]);

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

        private bool update_prestamo(ref string error, string numero_prestamo, string abono, string id_tipo_transaccion, string fecha_proximo_pago, string fecha_ultimo_pago)
        {
            try
            {
                string estado_prestamo = (id_tipo_transaccion == "10") ? "2" : "1";

                int update = 0;
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_prestamo_encabezado SET fecha_proximo_pago = CONVERT(DATE,@fecha_proximo_pago_update,103), " +
                                      "fecha_modificacion_prestamo = GETDATE(),fecha_ultimo_pago = CONVERT(DATE,@fecha_ultimo_pago_update,103), saldo_prestamo = saldo_prestamo - @abono_update, " +
                                      "estado_prestamo = @estado_prestamo_update " +
                                      "WHERE id_sucursal = @id_sucursal_update AND numero_prestamo = @numero_prestamo_update";
                command.Parameters.AddWithValue("@numero_prestamo_update", numero_prestamo);
                command.Parameters.AddWithValue("@id_sucursal_update", (int)HttpContext.Current.Session["id_sucursal"]);
                command.Parameters.AddWithValue("@abono_update", abono);
                command.Parameters.AddWithValue("@estado_prestamo_update", estado_prestamo);
                command.Parameters.AddWithValue("@fecha_proximo_pago_update", fecha_proximo_pago);
                command.Parameters.AddWithValue("@fecha_ultimo_pago_update", fecha_ultimo_pago);

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

        private bool update_correlativo_serie(ref string error, string id_serie, string numero_factura)
        {
            try
            {
                int update = 0;
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_serie SET correlativo = @numero_factura_updatecor, " +
                                      "estado = CASE WHEN @numero_factura_updatecor >= numero_de_facturas THEN  0 ELSE estado END " +
                                      "WHERE id_sucursal = @id_sucursal_updatecor AND id_serie = @id_serie_updatecor";
                command.Parameters.AddWithValue("@id_serie_updatecor", id_serie);
                command.Parameters.AddWithValue("@numero_factura_updatecor", numero_factura);
                command.Parameters.AddWithValue("@id_sucursal_updatecor", (int)HttpContext.Current.Session["id_sucursal"]);

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
