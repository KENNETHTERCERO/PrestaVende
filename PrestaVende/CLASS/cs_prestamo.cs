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
        public static string id_interes_proyeccion = "", monto_proyeccion = "", id_plan_prestamo_proyeccion = "";
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public bool guardar_prestamo(ref string error, string[] encabezado, DataTable detalle, string tipo_prenda, ref string numero_prestamo_guardado)
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
                                if (update_casilla(ref error, encabezado[8]))
                                {
                                    if (update_sucursal_correlativo_prestamo(ref error))
                                    {
                                        numero_prestamo_guardado = getNumeroPrestamoGuardado(ref error);
                                        if (!numero_prestamo.Equals("error") || !numero_prestamo_guardado.Equals(""))
                                        {
                                            command.Transaction.Commit();
                                            return true;
                                        }
                                        else
                                        {
                                            throw new Exception("No se pudo obtener el numero de prestamo guardado.");
                                        }
                                    }
                                    else
                                        throw new Exception("No se pudo actualizar el correlativo de pretamos.");
                                }
                                else
                                    throw new Exception("No se pudo actualizar la casilla.");
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
                string fecha_modificada = "";
                fecha_modificada = Convert.ToDateTime(datosEnc[3].ToString()).Year.ToString() + "/" + Convert.ToDateTime(datosEnc[3].ToString()).Month.ToString() + "/" + Convert.ToDateTime(datosEnc[3].ToString()).Day.ToString();
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_prestamo_encabezado (id_sucursal, id_cliente, numero_prestamo, total_prestamo, fecha_creacion_prestamo, estado_prestamo, fecha_proximo_pago, saldo_prestamo, usuario, id_plan_prestamo, id_interes, id_casilla, fecha_ultimo_pago, fecha_modificacion_prestamo, avaluo_original) " +
                                            "VALUES( " +
                                            "@id_sucursal_enc,         " +
                                            "@id_cliente,              " +
                                            "@numero_prestamo,         " +
                                            "@total_prestamo,          " +
                                            "GETDATE(),                " +
                                            "@estado_prestamo,         " +
                                            "CAST(@fecha_proximo_pago AS date),      " +
                                            "@saldo_prestamo,          " +
                                            "@usuario,                 " +
                                            "@id_plan_prestamo,        " +
                                            "@id_interes,              " +
                                            "@id_casilla, " +
                                            "GETDATE()," +
                                            "GETDATE(), " +
                                            "@avaluo_original )";
                command.Parameters.AddWithValue("@id_sucursal_enc", cs_usuario.id_sucursal);
                command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
                command.Parameters.AddWithValue("@id_cliente", datosEnc[0]);
                command.Parameters.AddWithValue("@total_prestamo", datosEnc[1]);
                command.Parameters.AddWithValue("@estado_prestamo", datosEnc[2]);
                command.Parameters.AddWithValue("@fecha_proximo_pago", fecha_modificada);
                command.Parameters.AddWithValue("@saldo_prestamo", datosEnc[4]);
                command.Parameters.AddWithValue("@usuario", datosEnc[5]);
                command.Parameters.AddWithValue("@id_plan_prestamo", datosEnc[6]);
                command.Parameters.AddWithValue("@id_interes", datosEnc[7]);
                command.Parameters.AddWithValue("@id_casilla", datosEnc[8]);
                command.Parameters.AddWithValue("@avaluo_original", datosEnc[9]);
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
                command.CommandText = "INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja, monto, numero_prestamo, estado_transaccion, fecha_transaccion, usuario, movimiento_saldo, id_sucursal) " +
                                                                "VALUES(7, @id_caja, @monto, @numero_prestamo_transaccion, 1, GETDATE(), @usuario_transaccion, (SELECT saldo - @monto FROM tbl_caja WHERE id_caja = @id_caja), @id_sucursal_transaccion)";
                command.Parameters.AddWithValue("@id_caja", cs_usuario.id_caja);
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@numero_prestamo_transaccion", numero_prestamo);
                command.Parameters.AddWithValue("@usuario_transaccion", cs_usuario.usuario);
                command.Parameters.AddWithValue("@id_sucursal_transaccion", cs_usuario.id_sucursal);
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

        private bool update_casilla(ref string error, string id_casilla)
        {
            try
            {
                int update = 0;
                command.CommandText = "UPDATE tbl_casilla SET estado = 1 WHERE id_casilla = @id_casilla";
                command.Parameters.AddWithValue("@@id_casilla", id_casilla);

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

        private bool update_sucursal_correlativo_prestamo(ref string error)
        {
            try
            {
                int update = 0;
                command.CommandText = "UPDATE tbl_sucursal SET correlativo_prestamo = correlativo_prestamo + 1 WHERE id_sucursal = @id_sucursal";
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

        public string getNumeroPrestamoGuardado(ref string error)
        {
            try
            {
                string numero_prestamo = "";
                command.Parameters.Clear();
                command.CommandText = "SELECT correlativo_prestamo FROM tbl_sucursal WHERE id_sucursal = @id_sucursalGuardado";
                command.Parameters.AddWithValue("@id_sucursalGuardado", CLASS.cs_usuario.id_sucursal);
                numero_prestamo = command.ExecuteScalar().ToString();
                return numero_prestamo;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return "error";
            }
        }

        public string getMaxNumeroPrestamo(ref string error)
        {
            try
            {
                string numero_prestamo = "";

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.Transaction = connection.connection.BeginTransaction();
                command.CommandText = "SELECT correlativo_prestamo + 1 FROM tbl_sucursal WHERE id_sucursal = @id_sucursal";
                command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
                numero_prestamo = command.ExecuteScalar().ToString();
                return numero_prestamo;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return "error";
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public DataTable getCountPrestamosActivos(ref string error, string id_cliente)
        {
            try
            {
                DataTable dtPrestamosLiquidados = new DataTable("prestamosLiquidados");

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.Transaction = connection.connection.BeginTransaction();
                command.CommandText = "SELECT COUNT(id_sucursal), SUM(total_prestamo) FROM  tbl_prestamo_encabezado WHERE estado_prestamo = 1 AND id_cliente = @id_cliente";
                command.Parameters.AddWithValue("@id_cliente", id_cliente);
                dtPrestamosLiquidados.Load(command.ExecuteReader());
                return dtPrestamosLiquidados;
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

        public DataTable getTipo(ref string error)
        {
            try
            {
                DataTable dtTipo = new DataTable("dtTipo");

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.Transaction = connection.connection.BeginTransaction();
                command.CommandText = "select 0 AS id_tipo, 'SELECCIONAR' AS opcion UNION " +
                                        "select 1 AS id_tipo, 'PRESTAMO' AS opcion UNION " +
                                        "select 2 AS id_tipo, 'COMPRA' AS opcion";
                dtTipo.Load(command.ExecuteReader());
                return dtTipo;
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

        public DataTable getCountPrestamosCancelados(ref string error, string id_cliente)
        {
            try
            {
                DataTable dtPrestamosLiquidados = new DataTable("prestamosCancelados");

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.Transaction = connection.connection.BeginTransaction();
                command.CommandText = "SELECT COUNT(id_sucursal), SUM(total_prestamo) FROM  tbl_prestamo_encabezado WHERE estado_prestamo = 2 AND id_cliente = @id_cliente";
                command.Parameters.AddWithValue("@id_cliente", id_cliente);
                dtPrestamosLiquidados.Load(command.ExecuteReader());
                return dtPrestamosLiquidados;
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

        public DataTable getCountPrestamosLiquidados(ref string error, string id_cliente)
        {
            try
            {
                DataTable dtPrestamosLiquidados = new DataTable("prestamosLiquidados");

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.Transaction = connection.connection.BeginTransaction();
                command.CommandText = "SELECT COUNT(id_sucursal), SUM(total_prestamo) FROM  tbl_prestamo_encabezado WHERE estado_prestamo = 3 AND id_cliente = @id_cliente";
                command.Parameters.AddWithValue("@id_cliente", id_cliente);
                dtPrestamosLiquidados.Load(command.ExecuteReader());
                return dtPrestamosLiquidados;
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

        public DataTable ObtenerPrestamos(ref string error, string id_cliente)
        {
            DataTable dtReturnPrestamos = new DataTable("dtPrestamos");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select pre.id_prestamo_encabezado,pre.numero_prestamo,suc.sucursal,pre.total_prestamo,CONVERT(VARCHAR(10), pre.fecha_creacion_prestamo, 103) AS fecha_creacion_prestamo, " +
                                            "CONVERT(VARCHAR(10), pre.fecha_proximo_pago, 103) AS fecha_proximo_pago,pre.saldo_prestamo,pla.plan_prestamo, " +
                                            "(select top 1 cat.categoria                            " +
                                                "From tbl_prestamo_encabezado AS enc                                                                " +
                                                "INNER JOIN tbl_prestamo_detalle AS det ON det.id_prestamo_encabezado = enc.id_prestamo_encabezado  " +
                                                "INNER JOIN tbl_producto AS pro ON pro.id_producto = det.id_producto                                " +
                                                "INNER JOIN tbl_subcategoria AS sub ON sub.id_sub_categoria = pro.id_sub_categoria                  " +
                                                "INNER JOIN tbl_categoria AS cat ON cat.id_categoria = sub.id_sub_categoria                         " +
                                                "WHERE enc.numero_prestamo = pre.numero_prestamo AND enc.id_sucursal = pre.id_sucursal) AS garantia " +
                                            "from tbl_prestamo_encabezado pre " +
                                            "inner join tbl_sucursal suc on suc.id_sucursal = pre.id_sucursal " +
                                            "inner join tbl_plan_prestamo pla on pla.id_plan_prestamo = pre.id_plan_prestamo " +
                                            "inner join tbl_cliente cli on cli.id_cliente = pre.id_cliente " +
                                            "where pre.estado_prestamo = 1 " +
                                            "AND pre.id_sucursal = " + cs_usuario.id_sucursal + " " +
                                            "and pre.id_cliente = " + id_cliente;
                dtReturnPrestamos.Load(command.ExecuteReader());
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
            return dtReturnPrestamos;
        }

        public DataTable ObtenerPrestamoEspecifico(ref string error, string id_prestamo)
        {
            DataTable dtReturnClient = new DataTable("dtReturnPrestamo");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select pre.id_prestamo_encabezado,pre.numero_prestamo,cli.primer_nombre,cli.segundo_nombre,cli.primer_apellido,cli.segundo_apellido, pre.id_cliente, " +
                                       "pre.saldo_prestamo,(c.factor * 100) AS factor,pre.total_prestamo from tbl_prestamo_encabezado pre " +
                                       "inner join tbl_cliente cli on cli.id_cliente = pre.id_cliente " +
                                       "inner join tbl_sucursal su on su.id_sucursal = pre.id_sucursal " +
                                       "inner join tbl_cargo c on c.id_interes = pre.id_interes and c.id_empresa = su.id_empresa " +
                                       "where pre.estado_prestamo = 1 and pre.id_prestamo_encabezado = @id_prestamo";
                command.Parameters.AddWithValue("@id_prestamo", id_prestamo);
                dtReturnClient.Load(command.ExecuteReader());
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
            return dtReturnClient;
        }

        public DataTable GetContrato(ref string error, string id_prestamo)
        {
            DataTable dtContrato = new DataTable("dtContrato");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec sp_contrato_prestamo @id_sucursal, @id_prestamo";
                command.Parameters.AddWithValue("@id_prestamo", id_prestamo);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
                dtContrato.Load(command.ExecuteReader());
                return dtContrato;
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

        public DataTable GetDataEtiquetaPrestamo(ref string error, string numero_prestamo)
        {
            DataTable dtEtiqueta = new DataTable("dtEtiqueta");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec sp_imprime_etiqueta @id_sucursal, @numero_prestamo";
                command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
                dtEtiqueta.Load(command.ExecuteReader());
                return dtEtiqueta;
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

        public DataTable getDTProyeccion(ref string error)
        {
            DataTable dtContrato = new DataTable("dtProyeccion");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec SP_proyeccion_intereses @id_interes, @monto, @id_plan_prestamo";
                command.Parameters.AddWithValue("@id_interes", cs_prestamo.id_interes_proyeccion);
                command.Parameters.AddWithValue("@monto", cs_prestamo.monto_proyeccion);
                command.Parameters.AddWithValue("@id_plan_prestamo", cs_prestamo.id_plan_prestamo_proyeccion);
                dtContrato.Load(command.ExecuteReader());
                return dtContrato;
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

        public DataTable GetEstadoCuentaPrestamoEncabezado(ref string error, string numero_prestamo)
        {
            DataTable dtEstadoCuentaEncabezado = new DataTable("EstadoCuentaEncabezado");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec sp_estado_cuenta_prestamo_encabezado @id_sucursal, @numero_prestamo";
                command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
                dtEstadoCuentaEncabezado.Load(command.ExecuteReader());
                return dtEstadoCuentaEncabezado;
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

        public DataTable GetEstadoCuentaPrestamoDetalle(ref string error, string numero_prestamo)
        {
            DataTable dtEstadoCuentaDetalle = new DataTable("EstadoCuentaDetalle");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec sp_estado_cuenta_prestamo_detalle @id_sucursal, @numero_prestamo";
                command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
                dtEstadoCuentaDetalle.Load(command.ExecuteReader());
                return dtEstadoCuentaDetalle;
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

        public DataTable GetDetallePrestamo(ref string error, string numero_prestamo)
        {
            DataTable dtDetallePrestamo = new DataTable("DetallePrestamo");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT PD.numero_prestamo, P.id_producto idproducto,P.producto, PD.cantidad, PD.valor, CONVERT(BIT,ISNULL(PD.retirada, 0)) retirada FROM tbl_prestamo_detalle PD " +
                                      "INNER JOIN tbl_producto P ON P.id_producto = PD.id_producto " +
                                      "WHERE PD.id_prestamo_encabezado = @id_prestamo AND PD.id_sucursal = @id_sucursal";
                command.Parameters.AddWithValue("@id_prestamo", numero_prestamo);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
                dtDetallePrestamo.Load(command.ExecuteReader());                
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

            return dtDetallePrestamo;
        }

        public DataTable GetDatosRetiro(ref string error, string numero_prestamo)
        {
            DataTable dtDetallePrestamo = new DataTable("DetallePrestamo");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select (p.total_prestamo - p.saldo_prestamo) ValorCancelado,  " +
                                      "isnull((select sum(valor) from tbl_prestamo_detalle dp where dp.retirada = 1 and dp.id_prestamo_encabezado = p.id_prestamo_encabezado),0) ValorRetirado " +
                                      "from tbl_prestamo_encabezado p where p.id_prestamo_encabezado = @id_prestamo and p.id_sucursal = @id_sucursal";
                command.Parameters.AddWithValue("@id_prestamo", numero_prestamo);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
                dtDetallePrestamo.Load(command.ExecuteReader());
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

            return dtDetallePrestamo;
        }
    }
}