using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace PrestaVende.CLASS
{
    public class cs_asignacion_caja
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        #region constantes

        private string[] TipoCajaGeneral = { "1", "4" };

        //ESTADOS CAJA
        private string AsignacionCaja = "2";
        private string CajaRecibida = "3";
        private string Incremento = "7";
        private string Decremento = "8";

        #endregion

        #region combos

        public DataTable getCaja(ref string error)
        {
            try
            {
                DataTable Caja = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_caja, 'SELECCIONAR' AS nombre_caja UNION " +
                                      "SELECT id_caja, nombre_caja FROM tbl_caja WHERE id_sucursal = @id_sucursal";
                command.Parameters.AddWithValue("@id_sucursal", HttpContext.Current.Session["id_sucursal"].ToString());
                Caja.Load(command.ExecuteReader());

                return Caja;
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

        public DataTable getEstadosCajas(ref string error, string id_caja)
        {
            try
            {
                DataTable EstadosCaja = new DataTable();
                //string DtEstadosValidacion = new DataTable();
                string id_tipo_caja_seleccionada = "";
                connection.connection.Open();
                command.Parameters.Clear();
                command.Connection = connection.connection;


               //OBTENIENDO TIPO DE CAJA
                command.CommandText = "select id_tipo_caja from tbl_caja where id_caja = @id_caja";
                command.Parameters.AddWithValue("@id_caja", id_caja);
                id_tipo_caja_seleccionada = command.ExecuteScalar().ToString();

                //SI LA CAJA YA ESTA RECIBIDA DEBE MOSTRAR INCREMENTO Y DECREMENTO

                        //SI LA CAJA ESTA CERRADA NO DEBE APARECER EL ESTADO DE CIERRE
                        command.CommandText = "SELECT 0 AS id_estado_caja, 'SELECCIONAR' AS estado_caja UNION "
                                         + " select ec.id_estado_caja, ec.estado_caja from tbl_estado_caja ec "
                                         + "    inner "
                                         + "        join tbl_asociacion_estado_tipo_caja aetc "
                                         + "      on ec.id_estado_caja = aetc.id_estado_caja "
                                         + "  inner "
                                         + "        join tbl_tipo_caja tc "
                                         + "      on aetc.id_tipo_caja = tc.id_tipo_caja "
                                         + "     where tc.id_tipo_caja = @id_tipo_caja ";
                command.Parameters.AddWithValue("@id_tipo_caja", id_tipo_caja_seleccionada);
                EstadosCaja.Load(command.ExecuteReader());
                return EstadosCaja;
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

        public DataTable getEstadoCajaPorRol(ref string error, string id_caja, ref string id_usuario_caja_asignada)
        {
            try
            {
                DataTable EstadoCaja = new DataTable();
                DataTable DtEstadosValidacion = new DataTable();
                string id_tipo_caja_seleccionada = "", id_estado_caja = "", queryString = "";
                connection.connection.Open();
                command.Parameters.Clear();
                command.Connection = connection.connection;

                
                //SI LA CAJA ESTA CERRADA NO DEBE APARECER EL ESTADO DE CIERRE
                
                queryString = "SELECT TOP 1 caj.id_estado_caja, caj.id_tipo_caja, asi.id_usuario_asignado " +
                                        "FROM tbl_caja AS caj " +
                                        "INNER JOIN tbl_asignacion_caja AS asi ON asi.id_caja = caj.id_caja AND asi.id_estado_caja = 2 AND asi.id_usuario_asignado <> 0 " +
                                        "WHERE caj.id_caja = @id_caja " +
                                        "ORDER BY asi.fecha_creacion DESC";
                command.CommandText = queryString;
                command.Parameters.AddWithValue("@id_caja", id_caja);
                DtEstadosValidacion.Load(command.ExecuteReader());

                //SI LA CAJA YA ESTA RECIBIDA DEBE MOSTRAR INCREMENTO Y DECREMENTO
                if (DtEstadosValidacion.Rows.Count > 0)
                {
                    foreach (DataRow item in DtEstadosValidacion.Rows)
                    {
                        if ((item[0].ToString() == CajaRecibida) || (item[0].ToString() == Incremento) || (item[0].ToString() == Decremento)) //CAJA RECIBIDA
                        {
                            //SI LA CAJA ESTA ASIGNADA ESTOS ESTADOS NO DEBEN OCULTARSE
                            Incremento = "0";
                            Decremento = "0";
                        }
                        else if (item[0].ToString() == "1")
                        {
                            AsignacionCaja = "0";
                        }
                        id_estado_caja = item[0].ToString();
                        id_tipo_caja_seleccionada = item[1].ToString();
                        id_usuario_caja_asignada = item[2].ToString();
                    }
                }

                string querySelect = "SELECT 0 AS id_estado_caja, 'SELECCIONAR' AS estado_caja UNION " +
                            "select ec.id_estado_caja, ec.estado_caja " +
                            "from tbl_estado_caja ec " +
                            "inner join tbl_asociacion_estado_tipo_caja aetc on ec.id_estado_caja = aetc.id_estado_caja " +
                            "inner join tbl_tipo_caja tc on aetc.id_tipo_caja = tc.id_tipo_caja ";

                if (id_estado_caja.Equals("3"))
                {
                    if (Convert.ToInt32(HttpContext.Current.Session["id_rol"]) == 3 || Convert.ToInt32(HttpContext.Current.Session["id_rol"]) == 4)
                    {
                        querySelect = querySelect + " where tc.id_tipo_caja = @id_tipo_caja and ec.id_estado_caja in (4, 7, 8)";
                    }
                    else if (Convert.ToInt32(HttpContext.Current.Session["id_rol"]) == 5)
                    {
                        querySelect = querySelect + " where tc.id_tipo_caja = @id_tipo_caja and ec.id_estado_caja in (4, 7, 8)";
                    }
                    else
                    {
                        querySelect = querySelect + " where tc.id_tipo_caja = @id_tipo_caja";
                    }
                }
                else
                {
                    querySelect = querySelect + " where tc.id_tipo_caja in (1, 2)";
                }

                command.CommandText = querySelect;

                command.Parameters.AddWithValue("@id_tipo_caja", id_tipo_caja_seleccionada);
                command.Parameters.AddWithValue("@Asignacion", AsignacionCaja);
                EstadoCaja.Load(command.ExecuteReader());
                return EstadoCaja;
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

        //VALIDANDO ESTADO DE LA ASIGNACION PARA NO RECIBIR MAS DE UNA VEZ
        public string getValidandoEstadoAsignacion(ref string error, string id_asignacion)
        {
            try
            {
                connection.connection.Open();
                command.Parameters.Clear();
                command.Connection = connection.connection;

                command.CommandText = "select estado_asignacion from tbl_asignacion_caja where id_asignacion_caja = @id_asignacion_caja";
                command.Parameters.AddWithValue("@id_asignacion_caja", id_asignacion);

                return command.ExecuteScalar().ToString(); ;
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

        public string getIDCajaAsignada(ref string error)
        {
            try
            {
                string id_caja = "";
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT caj.id_caja " +
                                        "FROM tbl_caja AS caj " +
                                        "INNER JOIN tbl_asignacion_caja AS asi ON asi.id_caja = caj.id_caja AND asi.id_estado_caja = 2 AND asi.id_usuario_asignado <> 0 " +
                                        "WHERE asi.id_usuario_asignado = @id_usuario " +
                                        "ORDER BY asi.fecha_creacion DESC";
                command.Parameters.AddWithValue("@id_usuario", Convert.ToInt32(HttpContext.Current.Session["id_usuario"]));
                id_caja = command.ExecuteScalar().ToString();

                return id_caja;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return "0";
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public decimal getSaldoCajaValidacion(ref string error, int id_caja)
        {
            try
            {
                error = "";
                decimal monto_return = 0;
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "select saldo From tbl_caja where id_caja = @id_caja";
                command.Parameters.AddWithValue("@id_caja", id_caja);
                monto_return = Convert.ToDecimal(command.ExecuteScalar().ToString());
                return monto_return;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return 0;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public DataTable getUsuarioAsignado(ref string error)
        {
            try
            {
                DataTable EstadoAreaEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT 0 AS id_usuario, 'SELECCIONAR' AS usuario UNION " + 
                                      "SELECT id_usuario, usuario from tbl_usuario WHERE id_rol in (3, 4, 5) AND id_sucursal = @id_sucursal";
                command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
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

        public DataTable getAsignacionCaja(ref string error)
        {
            try
            {
                DataTable AsignacionAreaEmpresa = new DataTable();
                string queryString = "";
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();

                queryString = "SELECT TOP 30 " +
                                            "asi.id_asignacion_caja, " +
                                            "asi.id_caja, " +
                                            "caj.nombre_caja, " +
                                            "est.estado_caja, " +
                                            "asi.monto, " +
                                            "(CASE WHEN asi.estado_asignacion = 0 THEN 'PENDIENTE RECEPCION' ELSE 'RECIBIDO' END) estado_asignacion, " +
                                            "UPPER(asi.usuario_asigna) usuario_asigna, " +
                                            "UPPER(us.usuario) AS usuario_asignado " +
                                        "FROM tbl_asignacion_caja AS asi " +
                                        "INNER JOIN tbl_caja AS caj ON caj.id_caja = asi.id_caja " +
                                        "INNER JOIN tbl_estado_caja AS est ON est.id_estado_caja = asi.id_estado_caja " +
                                        "INNER JOIN tbl_usuario AS us ON us.id_usuario = id_usuario_asignado ";
                if (Convert.ToInt32(HttpContext.Current.Session["id_rol"]) == 5)
                {
                    queryString = queryString + "WHERE caj.id_sucursal = @id_sucursal AND asi.id_usuario_asignado = @id_usuario " +
                                        "ORDER BY asi.fecha_creacion DESC";
                }
                else
                {
                    queryString = queryString + "WHERE caj.id_sucursal = @id_sucursal " +
                                        "ORDER BY asi.fecha_creacion DESC";
                }

                command.CommandText = queryString;
                command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
                command.Parameters.AddWithValue("@id_usuario", Convert.ToInt32(HttpContext.Current.Session["id_usuario"]));
                AsignacionAreaEmpresa.Load(command.ExecuteReader());
                return AsignacionAreaEmpresa;
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

        public string getIDMaxAsignacionCaja(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_asignacion_caja), 0) +1 FROM tbl_asignacion_caja";
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

        public bool insertAsignacionCaja(ref string error, string id_asignacion_caja, string id_caja, string id_estado_caja, string monto, string estado_asignacion,
                                            string fecha_creacion, string fecha_modificacion, string usuario_asigna, string id_usuario_asignado, Boolean BlnRecibir,
                                            string id_asignacion_recibida, ref int IntCajaActual)
        {
            try
            {
                DataTable DtDatosAsignacionCaja = new DataTable();
                DataTable DtTipoCaja = new DataTable();
                DataTable DtCajaGeneral = new DataTable();
                DataTable DtOperadionMatematica = new DataTable();
                string EstadoCajaOperacion = "";
                int rowsUpdated = 0;
                string id_tipo_caja = "";
                string id_caja_general = "";

                command = new SqlCommand();

                command.Connection = connection.connection;
                connection.connection.Open();
                command.Parameters.Clear();
                command.Transaction = connection.connection.BeginTransaction();

                //OBTENIENDO TIPO DE CAJA
                command.Parameters.Clear();
                command.CommandText = "select id_tipo_caja from tbl_caja where id_caja = @id_caja";
                command.Parameters.AddWithValue("@id_caja", id_caja);
                DtTipoCaja.Load(command.ExecuteReader());

                if (DtTipoCaja.Rows.Count > 0)
                {
                    foreach (DataRow item in DtTipoCaja.Rows)
                    {
                        id_tipo_caja = item[0].ToString();
                    }
                }

                command.Parameters.Clear();
                command.CommandText = " INSERT INTO tbl_asignacion_caja (id_caja, id_estado_caja, monto, estado_asignacion, fecha_creacion, fecha_modificacion, usuario_asigna, id_usuario_asignado) "
                                     + "       VALUES(@id_caja, @id_estado_caja, @monto, @estado_asignacion, GETDATE(), GETDATE(), @usuario_asigna, @id_usuario_asignado)";

                EstadoCajaOperacion = id_estado_caja;
                command.Parameters.AddWithValue("@id_estado_caja", id_estado_caja);
                command.Parameters.AddWithValue("@id_usuario_asignado", id_usuario_asignado);
                command.Parameters.AddWithValue("@estado_asignacion", "0");
                command.Parameters.AddWithValue("@id_caja", id_caja);
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@usuario_asigna", HttpContext.Current.Session["usuario"].ToString());
                command.ExecuteNonQuery();

                //SE OBTINE ID DE CAJA GENERAL PARA REALIZAR LA SUMATORIA/RESTA
                command.Parameters.Clear();
                command.CommandText = "SELECT id_caja FROM tbl_caja WHERE id_sucursal = @id_sucursal and id_tipo_caja = 1 and estado = 1 ";
                command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
                id_caja_general = command.ExecuteScalar().ToString();
                
                if (id_estado_caja == AsignacionCaja || id_estado_caja == Incremento) //ESTADO ASIGNACION, SI SE ASIGNA LA CAJA EL MONTO DE ASIGNACION ES EL SALDO DE LA CAJA ACTUAL
                {
                    //REALIZANDO ACTUALIZACION DE SALDO PARA LA CAJA TRANSACCIONAL
                    if (id_estado_caja == Incremento)
                    {
                        id_estado_caja = "3";
                        rowsUpdated = 1;
                    }
                    else
                    {
                        id_estado_caja = "2";
                        command.Parameters.Clear();
                        EstadoCajaOperacion = id_estado_caja;
                        command.CommandText = " UPDATE tbl_caja SET saldo = @SaldoAsignado, id_estado_caja = @id_estado_caja where id_caja = @id_caja ";
                        command.Parameters.AddWithValue("@SaldoAsignado", monto);
                        command.Parameters.AddWithValue("@id_caja", id_caja);
                        command.Parameters.AddWithValue("@id_estado_caja", id_estado_caja);
                        rowsUpdated = command.ExecuteNonQuery();
                    }

                    if (rowsUpdated <= 0)
                    {
                        command.Transaction.Rollback();
                        return false;
                    }

                    if (id_tipo_caja != "1" && id_tipo_caja != "4")
                    {
                        //REALIZANDO ACTUALIZACION DE SALDO PARA LA CAJA GENERAL
                        if (id_estado_caja != "8")
                        {
                            command.Parameters.Clear();
                            command.CommandText = " UPDATE tbl_caja SET SALDO = SALDO - @SaldoAsignado where id_caja = @id_caja ";
                            command.Parameters.AddWithValue("@SaldoAsignado", monto);
                            command.Parameters.AddWithValue("@id_caja", id_caja_general);
                            rowsUpdated = command.ExecuteNonQuery();

                            if (rowsUpdated <= 0)
                            {
                                command.Transaction.Rollback();
                                return false;
                            }
                        }
                        else if (id_estado_caja == "8")
                        {
                            command.Parameters.Clear();
                            command.CommandText = " UPDATE tbl_caja SET SALDO = SALDO + @SaldoAsignado where id_caja = @id_caja ";
                            command.Parameters.AddWithValue("@SaldoAsignado", monto);
                            command.Parameters.AddWithValue("@id_caja", id_caja_general);
                            rowsUpdated = command.ExecuteNonQuery();

                            if (rowsUpdated <= 0)
                            {
                                command.Transaction.Rollback();
                                return false;
                            }
                        }
                    }
                }

                command.Transaction.Commit();
                return true;

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

        public DataTable getDatosAsignacionCaja(ref string error, string id_asignacion_caja)
        {
            try
            {
                DataTable EstadoAreaEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT a.*,b.saldo " +
                                        "FROM tbl_asignacion_caja a " +
                                        "INNER JOIN tbl_caja b " +
                                        "on a.id_caja = b.id_caja where id_asignacion_caja = @id_asignacion_caja";
                command.Parameters.AddWithValue("@id_asignacion_caja", id_asignacion_caja);
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

        public DataTable getAsignacionCaja(ref string error, string id_asignacion_caja)
        {
            try
            {
                DataTable dtAsignacionCaja = new DataTable("AsignacionCaja");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT " +
                                            "asi.id_asignacion_caja, " +
                                            "asi.id_caja,            " +
                                            "asi.id_estado_caja,     " +
                                            "caj.saldo AS monto,     " +
                                            "asi.estado_asignacion,  " +
                                            "asi.fecha_creacion,     " +
                                            "caj.nombre_caja,        " +
                                            "caj.puede_vender        " +
                                        "FROM                        " +
                                        "tbl_asignacion_caja AS asi  " +
                                        "INNER JOIN tbl_caja AS caj ON caj.id_caja = asi.id_caja " +
                                        "WHERE asi.estado_asignacion = 0 AND asi.id_asignacion_caja = @id_asignacion_caja";
                command.Parameters.AddWithValue("@id_asignacion_caja", id_asignacion_caja);
                dtAsignacionCaja.Load(command.ExecuteReader());
                return dtAsignacionCaja;
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

        public bool recibirAsignacionCaja(ref string error, string id_asignacion_caja, string monto)
        {
            try
            {
                int rowsUpdated = 0;
                DataTable DtEstados = new DataTable();

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();


                command.CommandText = "UPDATE tbl_asignacion_caja SET estado_asignacion = 1 WHERE id_asignacion_caja = @id_asignacion_caja";
                command.Parameters.AddWithValue("@id_asignacion_caja", id_asignacion_caja);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de asignacion de caja.");
                }
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_caja SET id_estado_caja = 3, saldo = @monto_caja WHERE id_caja = @id_caja";
                command.Parameters.AddWithValue("@id_caja", Convert.ToInt32(HttpContext.Current.Session["id_caja"]));
                command.Parameters.AddWithValue("@monto_caja", monto);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de caja.");
                }

                //command.Parameters.Clear();
                //command.CommandText = "UPDATE tbl_caja SET saldo = saldo - @monto_caja_general WHERE id_sucursal = @id_sucursal AND id_tipo_caja = 1 AND id_estado_caja = 2";
                //command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
                //command.Parameters.AddWithValue("@monto_caja_general", monto);
                //rowsUpdated = command.ExecuteNonQuery();

                //if (rowsUpdated <= 0)
                //{
                //    throw new SystemException("Error actualizando el saldo de la caja general.");
                //}

                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_usuario SET caja_asignada = 1 WHERE id_usuario = @id_usuario";
                command.Parameters.AddWithValue("@id_usuario", Convert.ToInt32(HttpContext.Current.Session["id_usuario"]));
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de usuario en la recepcion.");
                }

                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja, monto, estado_transaccion, fecha_transaccion, usuario, movimiento_saldo, id_sucursal) " +
                                        "VALUES(5, @id_caja_transaccion, @monto, 1, GETDATE(), @usuario, @movimiento_saldo, @id_sucursal)";
                command.Parameters.AddWithValue("@id_caja_transaccion", Convert.ToInt32(HttpContext.Current.Session["id_caja"]));
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@usuario", HttpContext.Current.Session["usuario"].ToString());
                command.Parameters.AddWithValue("@movimiento_saldo", monto);
                command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de usuario en la recepcion.");
                }

                command.Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                error = ex.ToString();
                return false;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public bool recibirCierreCaja(ref string error, string id_asignacion_caja, string monto, string id_caja, string id_usuario)
        {
            try
            {
                int rowsUpdated = 0;
                DataTable DtEstados = new DataTable();

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();

                command.CommandText = "UPDATE tbl_asignacion_caja SET estado_asignacion = 1 WHERE id_asignacion_caja = @id_asignacion_caja";
                command.Parameters.AddWithValue("@id_asignacion_caja", id_asignacion_caja);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de asignacion de caja.");
                }
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_caja SET id_estado_caja = 1, saldo = saldo - @monto_caja WHERE id_caja = @id_caja";
                command.Parameters.AddWithValue("@id_caja", id_caja);
                command.Parameters.AddWithValue("@monto_caja", monto);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de caja.");
                }

                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_caja SET saldo = saldo + @monto_caja_general WHERE id_sucursal = @id_sucursal AND id_tipo_caja = 1 AND id_estado_caja = 2";
                command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
                command.Parameters.AddWithValue("@monto_caja_general", monto);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando el saldo de la caja general.");
                }

                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_usuario SET caja_asignada = 0 WHERE id_usuario = @id_usuario";
                command.Parameters.AddWithValue("@id_usuario", id_usuario);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de usuario en la recepcion.");
                }

                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja, monto, estado_transaccion, fecha_transaccion, usuario, movimiento_saldo, id_sucursal) " +
                                        "VALUES(6, @id_caja_transaccion, @monto, 1, GETDATE(), @usuario, @movimiento_saldo, @id_sucursal)";
                command.Parameters.AddWithValue("@id_caja_transaccion", id_caja);
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@usuario", Convert.ToString(HttpContext.Current.Session["usuario"]));
                command.Parameters.AddWithValue("@movimiento_saldo", monto);
                command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla transaccion en la recepcion.");
                }

                command.Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                error = ex.ToString();
                return false;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public bool recibirIncrementoCapitalCaja(ref string error, string id_asignacion_caja, string monto, string id_caja, string id_usuario)
        {
            try
            {
                int rowsUpdated = 0;
                DataTable DtEstados = new DataTable();

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();


                command.CommandText = "UPDATE tbl_asignacion_caja SET estado_asignacion = 1 WHERE id_asignacion_caja = @id_asignacion_caja";
                command.Parameters.AddWithValue("@id_asignacion_caja", id_asignacion_caja);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de asignacion de caja.");
                }

                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja, monto, estado_transaccion, fecha_transaccion, usuario, movimiento_saldo, id_sucursal) " +
                                        "VALUES(15, @id_caja_transaccion, @monto, 1, GETDATE(), @usuario, (SELECT saldo + @movimiento_saldo FROM tbl_caja WHERE id_caja = @id_caja_transaccion), @id_sucursal)";
                command.Parameters.AddWithValue("@id_caja_transaccion", id_caja);
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@usuario", HttpContext.Current.Session["usuario"].ToString());
                command.Parameters.AddWithValue("@movimiento_saldo", monto);
                command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de usuario en la recepcion.");
                }

                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_caja SET saldo = saldo + @monto_caja WHERE id_caja = @id_caja";
                command.Parameters.AddWithValue("@id_caja", id_caja);
                command.Parameters.AddWithValue("@monto_caja", monto);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de caja.");
                }

                command.Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                error = ex.ToString();
                return false;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public bool recibirDecrementoCapitalCaja(ref string error, string id_asignacion_caja, string monto, string id_caja, string id_usuario)
        {
            try
            {
                int rowsUpdated = 0;
                DataTable DtEstados = new DataTable();

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();


                command.CommandText = "UPDATE tbl_asignacion_caja SET estado_asignacion = 1 WHERE id_asignacion_caja = @id_asignacion_caja";
                command.Parameters.AddWithValue("@id_asignacion_caja", id_asignacion_caja);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de asignacion de caja.");
                }

                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja, monto, estado_transaccion, fecha_transaccion, usuario, movimiento_saldo, id_sucursal) " +
                                        "VALUES(15, @id_caja_transaccion, @monto, 1, GETDATE(), @usuario, (SELECT saldo - @movimiento_saldo FROM tbl_caja WHERE id_caja = @id_caja_transaccion), @id_sucursal)";
                command.Parameters.AddWithValue("@id_caja_transaccion", id_caja);
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@usuario", HttpContext.Current.Session["usuario"].ToString());
                command.Parameters.AddWithValue("@movimiento_saldo", monto);
                command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de usuario en la recepcion.");
                }

                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_caja SET saldo = saldo - @monto_caja WHERE id_caja = @id_caja";
                command.Parameters.AddWithValue("@id_caja", id_caja);
                command.Parameters.AddWithValue("@monto_caja", monto);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de caja.");
                }

                command.Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                error = ex.ToString();
                return false;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public string getIdCaja(string id_asignacion_caja)
        {
            try
            {
                DataTable EstadoAreaEmpresa = new DataTable();
                command.Connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "select id_caja from tbl_asignacion_caja where id_asignacion_caja = @id_asignacion_caja";
                command.Parameters.AddWithValue("@id_asignacion_caja", id_asignacion_caja);
                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        #endregion
    }
}