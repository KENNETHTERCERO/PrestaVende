﻿using System;
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
        private int IntCajaActual = 0;

        //ESTADOS CAJA
        private string Disponible = "1";
        private string AsignacionCaja = "2";
        private string CajaRecibida = "3";
        private string CierreCaja = "4";
        private string CajaRecibidaGeneral = "6";
        private string Incremento = "7";
        private string Decremento = "8";
        private string AsignacionCajaGeneral = "";
        private string RecepcionCajaGeneral = "";

        //TIPOS TRANSACCIONES
        private int TransaccionAperturaCajaGeneral = 1;
        private int TransaccionCierreCajaGeneral = 2;
        private int TransaccionIncrementoCapitalCajaGeneral = 3;
        private int TransaccionDecrementoCapitalCajaGeneral = 4;
        private int TransaccionAperturaCapitalCajaTransaccionalPrestamo = 5;
        private int TransaccionCierreCapitalCajaTransaccionalPrestamo = 6;
        private int TransaccionRecepcionCajaGeneral = 14;
        private int TransaccionIncrementoCapitalCajaTransaccional = 15;
        private int TransaccionDecrementoCapitalCajaTransaccional = 16;    
        private int TransaccionAperturaCajaTransaccional = 17;
        private int TransaccionCierreCajaTransaccional = 18;

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
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
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
                
                queryString = "SELECT caj.id_estado_caja, caj.id_tipo_caja, asi.id_usuario_asignado " +
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
                    if (cs_usuario.id_rol == 3 || cs_usuario.id_rol == 3)
                    {
                        querySelect = querySelect + " where tc.id_tipo_caja = @id_tipo_caja and ec.id_estado_caja in (4, 7, 8)";
                    }
                    else if (cs_usuario.id_rol == 5)
                    {
                        querySelect = querySelect + " where tc.id_tipo_caja = @id_tipo_caja and ec.id_estado_caja in (4)";
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
                command.Parameters.AddWithValue("@id_usuario", CLASS.cs_usuario.id_usuario);
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
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
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
                if (cs_usuario.id_rol == 5)
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
                command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
                command.Parameters.AddWithValue("@id_usuario", CLASS.cs_usuario.id_usuario);
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

        public String getIDMaxAsignacionCaja(ref string error)
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
                int TipoTransaccion = 0;
                int rowsUpdated = 0;
                string id_tipo_caja = "";
                string id_caja_general = "";
                string operacion_matematica = "";
                Decimal SaldoActualCaja = 0;

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
                command.CommandText = " INSERT INTO tbl_asignacion_caja (id_caja, id_estado_caja, monto,"
                                     + "       estado_asignacion, fecha_creacion, fecha_modificacion, usuario_asigna, id_usuario_asignado)      "
                                     + "       VALUES(@id_caja, @id_estado_caja, @monto,                                   "
                                     + "       @estado_asignacion, @fecha_creacion, @fecha_modificacion, @usuario_asigna, @id_usuario_asignado)";

                if (BlnRecibir == true) //CUANDO LA CAJA SE RECIBE
                {
                    if (id_tipo_caja == "1")//CUANDO LA CAJA ES GENERAL
                    {
                        EstadoCajaOperacion = CajaRecibidaGeneral;
                        command.Parameters.AddWithValue("@id_estado_caja", CajaRecibidaGeneral);
                        command.Parameters.AddWithValue("@id_usuario_asignado", CLASS.cs_usuario.id_usuario);
                    }
                    else //CUANDO LA CAJA ES TRANSACCIONAL
                    {
                        EstadoCajaOperacion = CajaRecibida;
                        command.Parameters.AddWithValue("@id_estado_caja", CajaRecibida);
                        command.Parameters.AddWithValue("@id_usuario_asignado", CLASS.cs_usuario.id_usuario);
                    }
                    command.Parameters.AddWithValue("@estado_asignacion", "0");
                }
                else
                {
                    EstadoCajaOperacion = id_estado_caja;
                    command.Parameters.AddWithValue("@id_estado_caja", id_estado_caja);
                    command.Parameters.AddWithValue("@id_usuario_asignado", id_usuario_asignado);
                    command.Parameters.AddWithValue("@estado_asignacion", "0");
                }

                command.Parameters.AddWithValue("@id_caja", id_caja);
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@fecha_creacion", DateTime.Now);
                command.Parameters.AddWithValue("@fecha_modificacion", DateTime.Now);
                command.Parameters.AddWithValue("@usuario_asigna", CLASS.cs_usuario.usuario);
                command.ExecuteNonQuery();

               

                //SE OBTINE ID DE CAJA GENERAL PARA REALIZAR LA SUMATORIA/RESTA
                command.Parameters.Clear();
                command.CommandText = "SELECT id_caja FROM tbl_caja WHERE id_sucursal = @id_sucursal and id_tipo_caja = 1 and estado = 1 ";
                command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
                DtCajaGeneral.Load(command.ExecuteReader());

                if (DtCajaGeneral.Rows.Count > 0)
                {
                    foreach (DataRow item in DtCajaGeneral.Rows)
                    {
                        id_caja_general = item[0].ToString();
                    }
                }

                if (BlnRecibir == true) //CUANDO LA CAJA SE RECIBE
                {

                    if (id_estado_caja == CierreCaja)
                    {
                        //MODIFICANDO SALDO ACTUAL DE CAJA TRANSACCIONAL
                        command.Parameters.Clear();
                        EstadoCajaOperacion = CierreCaja;
                        command.CommandText = " UPDATE tbl_caja SET fecha_modificacion = GETDATE(), saldo = saldo - @saldo, id_estado_caja = @id_estado_caja WHERE id_caja = @id_caja";
                        command.Parameters.AddWithValue("@id_caja", id_caja);
                        command.Parameters.AddWithValue("@saldo", monto);
                        command.Parameters.AddWithValue("@id_estado_caja", CierreCaja);
                        rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated <= 0)
                        {
                            command.Transaction.Rollback();
                            return false;
                        }

                        //MODIFICANDO SALDO ACTUAL DE CAJA GENERAL
                        command.Parameters.Clear();
                        command.CommandText = " UPDATE tbl_caja SET fecha_modificacion = GETDATE(), saldo = saldo + @saldo WHERE id_caja = @id_caja";
                        command.Parameters.AddWithValue("@id_caja", id_caja_general);
                        command.Parameters.AddWithValue("@saldo", monto);
                        rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated <= 0)
                        {
                            command.Transaction.Rollback();
                            return false;
                        }

                        IntCajaActual = CLASS.cs_usuario.id_caja;
                        //OBTENIENDO CAJA POR USUARIO
                        command.CommandText = "select caja_asignada from tbl_usuario where id_usuario = @id_usuario";
                        command.Parameters.AddWithValue("@id_usuario", CLASS.cs_usuario.id_usuario);
                        IntCajaActual = Convert.ToInt32( command.ExecuteScalar().ToString());



                        //CUANDO SE CIERRA LA CAJA EL USUARIO YA NO DEBE TENER CAJA ASIGNADA
                        command.CommandText = "UPDATE tbl_usuario SET caja_asignada = 0 WHERE id_usuario = @id_usuario";
                        rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated <= 0)
                        {
                            command.Transaction.Rollback();
                            return false;
                        }
                    }
                    else if (id_estado_caja == AsignacionCaja) //ESTADO ASIGNACION, si se recibe la asignacion cambia a caja recibida
                    {
                        //MODIFICANDO SALDO ACTUAL DE CAJA TRANSACCIONAL
                        command.Parameters.Clear();
                        EstadoCajaOperacion = CajaRecibida;
                        command.CommandText = " UPDATE tbl_caja SET fecha_modificacion = @fecha_modificacion, id_estado_caja = @id_estado_caja WHERE id_caja = @id_caja";
                        command.Parameters.AddWithValue("@id_caja", id_caja);
                        command.Parameters.AddWithValue("@fecha_modificacion", DateTime.Now);
                        command.Parameters.AddWithValue("@id_estado_caja", "2");
                        rowsUpdated = command.ExecuteNonQuery();

                        //if (id_tipo_caja != "1" && id_tipo_caja != "4")
                        //{
                        //    command.Parameters.Clear();
                        //    command.CommandText = "select saldo from tbl_caja where id_caja = @id_caja";
                        //    command.Parameters.AddWithValue("@id_caja", id_caja_general);
                        //    SaldoActualCaja = Convert.ToDecimal(command.ExecuteScalar().ToString());


                        //    //INSERTANDO TRANSACCION
                        //    command.Parameters.Clear();
                        //    command.CommandText = " INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja ,monto, "
                        //                          + " estado_transaccion, fecha_transaccion, usuario, movimiento_saldo, id_sucursal) "
                        //                          + "  VALUES(@id_tipo_transaccion, @id_caja, @monto, "
                        //                          + " @estado_transaccion, @fecha_transaccion, @usuario, @movimiento_saldo, @id_sucursal) ";

                        //    command.Parameters.Clear();
                        //    TipoTransaccion = TransaccionDecrementoCapitalCajaGeneral;
                        //    command.Parameters.AddWithValue("@id_tipo_transaccion", TipoTransaccion);
                        //    command.Parameters.AddWithValue("@id_caja", id_caja_general);
                        //    command.Parameters.AddWithValue("@monto", monto);
                        //    command.Parameters.AddWithValue("@estado_transaccion", 1);
                        //    command.Parameters.AddWithValue("@fecha_transaccion", DateTime.Now);
                        //    command.Parameters.AddWithValue("@usuario", usuario_asigna);
                        //    command.Parameters.AddWithValue("@movimiento_saldo", (SaldoActualCaja));
                        //    command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
                        //    command.ExecuteNonQuery();
                        //}                        
                    }
                    else if (id_estado_caja == Incremento || id_estado_caja == Decremento) //ESTADO ASIGNACION, si se recibe la asignacion cambia a caja recibida
                    {
                        //INTENTO
                        //SE OBTIENE OPERACION MATEMATICA SEGUN EL ESTADO SELECCIONADO 
                        command.Parameters.Clear();
                        EstadoCajaOperacion = id_estado_caja;
                        command.CommandText = "SELECT operacion_matematica FROM tbl_estado_caja where id_estado_caja = @id_estado_caja";
                        command.Parameters.AddWithValue("@id_estado_caja", id_estado_caja);
                        DtOperadionMatematica.Load(command.ExecuteReader());

                        if (DtOperadionMatematica.Rows.Count > 0)
                        {
                            foreach (DataRow item in DtOperadionMatematica.Rows)
                            {
                                operacion_matematica = item[0].ToString();
                            }
                        }


                        //REALIZANDO ACTUALIZACION DE SALDO PARA LA CAJA TRANSACCIONAL DEPENDIENDO LA OPERACION MATEMATICA DE LOS ESTADOS QUE SE ASIGNEN
                        command.Parameters.Clear();

                        if (operacion_matematica == "+")
                        {
                            command.CommandText = " UPDATE tbl_caja SET SALDO = SALDO + @SaldoAsignado, id_estado_caja = @id_estado_caja where id_tipo_caja = @id_tipo_caja and id_caja = @id_caja";
                        }
                        else
                        {
                            command.CommandText = " UPDATE tbl_caja SET SALDO = SALDO - @SaldoAsignado, id_estado_caja = @id_estado_caja where id_tipo_caja = @id_tipo_caja and id_caja = @id_caja";
                        }
                        EstadoCajaOperacion = id_estado_caja;
                        command.Parameters.AddWithValue("@id_tipo_caja", id_tipo_caja);
                        command.Parameters.AddWithValue("@id_estado_caja", id_estado_caja);
                        command.Parameters.AddWithValue("@SaldoAsignado", monto);
                        command.Parameters.AddWithValue("@id_caja", id_caja);
                        rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated <= 0)
                        {
                            command.Transaction.Rollback();
                            return false;
                        }
                    }
                    //ACTUALIZANDO ESTADO ACTIVO A INACTIVO DE ASIGNACIÓN LA CUAL SE RECIBIRÁ
                    command.CommandText = "UPDATE tbl_asignacion_caja SET estado_asignacion = 0 WHERE id_asignacion_caja = @id_asignacion ";
                    command.Parameters.AddWithValue("@id_asignacion", id_asignacion_recibida);
                    rowsUpdated = command.ExecuteNonQuery();

                    if (rowsUpdated <= 0)
                    {
                        command.Transaction.Rollback();
                        return false;
                    }
                }
                else
                {
                    if (id_estado_caja == AsignacionCaja) //ESTADO ASIGNACION, SI SE ASIGNA LA CAJA EL MONTO DE ASIGNACION ES EL SALDO DE LA CAJA ACTUAL
                    {
                        //REALIZANDO ACTUALIZACION DE SALDO PARA LA CAJA TRANSACCIONAL
                        command.Parameters.Clear();
                        EstadoCajaOperacion = id_estado_caja;
                        command.CommandText = " UPDATE tbl_caja SET SALDO = @SaldoAsignado, id_estado_caja = @id_estado_caja where id_caja = @id_caja ";
                        command.Parameters.AddWithValue("@SaldoAsignado", monto);
                        command.Parameters.AddWithValue("@id_caja", id_caja);
                        command.Parameters.AddWithValue("@id_estado_caja", id_estado_caja);
                        rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated <= 0)
                        {
                            command.Transaction.Rollback();
                            return false;
                        }

                        if (id_tipo_caja != "1" && id_tipo_caja != "4")
                        {
                            //REALIZANDO ACTUALIZACION DE SALDO PARA LA CAJA GENERAL
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
                        
                    }
                    
                }

                if (BlnRecibir == true)
                {
                    //command.Parameters.Clear();
                    //command.CommandText = "select saldo from tbl_caja where id_caja = @id_caja";
                    //command.Parameters.AddWithValue("@id_caja", id_caja);
                    //SaldoActualCaja = Convert.ToDecimal(command.ExecuteScalar().ToString());


                    ////INSERTANDO TRANSACCION
                    //command.Parameters.Clear();
                    //command.CommandText = " INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja ,monto, "
                    //                      + " estado_transaccion, fecha_transaccion, usuario, movimiento_saldo, id_sucursal) "
                    //                      + "  VALUES(@id_tipo_transaccion, @id_caja, @monto, "
                    //                      + " @estado_transaccion, @fecha_transaccion, @usuario, @movimiento_saldo, @id_sucursal) ";


                    //if (EstadoCajaOperacion != "2")
                    //{

                    //    if (id_tipo_caja == "2") //Caja Transaccional Prestamos
                    //    {
                    //        if (EstadoCajaOperacion == "2")
                    //        {
                    //            TipoTransaccion = TransaccionAperturaCapitalCajaTransaccionalPrestamo;
                    //            SaldoActualCaja = Convert.ToDecimal(monto);
                    //        }
                    //        else
                    //        {
                    //            TipoTransaccion = TransaccionCierreCapitalCajaTransaccionalPrestamo;
                    //            SaldoActualCaja = SaldoActualCaja - Convert.ToDecimal(monto);
                    //        }
                    //    }
                    //    else if (id_tipo_caja == "1") //Caja General Sucursal
                    //    {
                    //        if (EstadoCajaOperacion == "2")
                    //        {
                    //            TipoTransaccion = TransaccionAperturaCajaGeneral;
                    //            SaldoActualCaja = Convert.ToDecimal(monto);
                    //        }
                    //        else if (EstadoCajaOperacion == "4")
                    //        {
                    //            TipoTransaccion = TransaccionCierreCajaGeneral;
                    //            SaldoActualCaja = SaldoActualCaja - Convert.ToDecimal(monto);
                    //        }
                    //        else if (EstadoCajaOperacion == "7")
                    //        {
                    //            TipoTransaccion = TransaccionIncrementoCapitalCajaGeneral;
                    //            SaldoActualCaja = SaldoActualCaja + Convert.ToDecimal(monto);
                    //        }
                    //        else
                    //        {
                    //            TipoTransaccion = TransaccionDecrementoCapitalCajaGeneral;
                    //            SaldoActualCaja = SaldoActualCaja - Convert.ToDecimal(monto);
                    //        }
                    //    }
                    //    else if (id_tipo_caja == "5") //Caja Transaccional 
                    //    {
                    //        if (EstadoCajaOperacion == "2")
                    //        {
                    //            TipoTransaccion = TransaccionAperturaCajaTransaccional;
                    //            SaldoActualCaja = Convert.ToDecimal(monto);
                    //        }
                    //        else if (EstadoCajaOperacion == "4")
                    //        {
                    //            TipoTransaccion = TransaccionCierreCajaTransaccional;
                    //            SaldoActualCaja = SaldoActualCaja - Convert.ToDecimal(monto);
                    //        }
                    //        else if (EstadoCajaOperacion == "7")
                    //        {
                    //            TipoTransaccion = TransaccionIncrementoCapitalCajaTransaccional;
                    //            SaldoActualCaja = SaldoActualCaja + Convert.ToDecimal(monto);
                    //        }
                    //        else
                    //        {
                    //            TipoTransaccion = TransaccionDecrementoCapitalCajaTransaccional;
                    //            SaldoActualCaja = SaldoActualCaja - Convert.ToDecimal(monto);
                    //        }
                    //    }

                    //    command.Parameters.Clear();
                    //    command.Parameters.AddWithValue("@id_tipo_transaccion", TipoTransaccion);
                    //    command.Parameters.AddWithValue("@id_caja", id_caja);
                    //    command.Parameters.AddWithValue("@monto", monto);
                    //    command.Parameters.AddWithValue("@estado_transaccion", 1);
                    //    command.Parameters.AddWithValue("@fecha_transaccion", DateTime.Now);
                    //    command.Parameters.AddWithValue("@usuario", usuario_asigna);
                    //    command.Parameters.AddWithValue("@movimiento_saldo", (SaldoActualCaja + Convert.ToDecimal(monto)));
                    //    command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
                    //    command.ExecuteNonQuery();
                    //}
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
                                            "asi.monto,              " +
                                            "asi.estado_asignacion,  " +
                                            "asi.fecha_creacion,     " +
                                            "caj.nombre_caja,        " +
                                            "caj.puede_vender        " +
                                        "FROM                        " +
                                        "tbl_asignacion_caja AS asi  " +
                                        "INNER JOIN tbl_caja AS caj ON caj.id_caja = asi.id_caja " +
                                        "WHERE asi.id_asignacion_caja = @id_asignacion_caja";
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
                command.Parameters.AddWithValue("@id_caja", CLASS.cs_usuario.id_caja);
                command.Parameters.AddWithValue("@monto_caja", monto);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de caja.");
                }

                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_caja SET saldo = saldo - @monto_caja_general WHERE id_sucursal = @id_sucursal AND id_tipo_caja = 1 AND id_estado_caja = 2";
                command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
                command.Parameters.AddWithValue("@monto_caja_general", monto);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando el saldo de la caja general.");
                }

                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_usuario SET caja_asignada = 1 WHERE id_usuario = @id_usuario";
                command.Parameters.AddWithValue("@id_usuario", CLASS.cs_usuario.id_usuario);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de usuario en la recepcion.");
                }

                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja, monto, estado_transaccion, fecha_transaccion, usuario, movimiento_saldo, id_sucursal) " +
                                        "VALUES(5, @id_caja_transaccion, @monto, 1, GETDATE(), @usuario, @movimiento_saldo, @id_sucursal)";
                command.Parameters.AddWithValue("@id_caja_transaccion", CLASS.cs_usuario.id_caja);
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@usuario", CLASS.cs_usuario.usuario);
                command.Parameters.AddWithValue("@movimiento_saldo", monto);
                command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
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

        public bool recibirCierreCaja(ref string error, string id_asignacion_caja, string monto, string id_caja)
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
                command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
                command.Parameters.AddWithValue("@monto_caja_general", monto);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando el saldo de la caja general.");
                }

                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_usuario SET caja_asignada = 0 WHERE id_usuario = @id_usuario";
                command.Parameters.AddWithValue("@id_usuario", CLASS.cs_usuario.id_usuario);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    throw new SystemException("Error actualizando la tabla de usuario en la recepcion.");
                }

                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja, monto, estado_transaccion, fecha_transaccion, usuario, movimiento_saldo, id_sucursal) " +
                                        "VALUES(5, @id_caja_transaccion, @monto, 1, GETDATE(), @usuario, @movimiento_saldo, @id_sucursal)";
                command.Parameters.AddWithValue("@id_caja_transaccion", CLASS.cs_usuario.id_usuario);
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@usuario", CLASS.cs_usuario.usuario);
                command.Parameters.AddWithValue("@movimiento_saldo", monto);
                command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
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