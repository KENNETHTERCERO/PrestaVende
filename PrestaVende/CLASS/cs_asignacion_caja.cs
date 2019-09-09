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

        private string[] TipoCajaGeneral = {"1","4"};

        //ESTADOS CAJA
        private string Disponible = "1";
        private string AsignacionCaja = "2";
        private string CajaRecibida = "3";
        private string CierreCaja = "4";
        private string AsignacionCajaGeneral = "5";
        private string RecepcionCajaGeneral = "6";
        private string IncrementoCapitalCaja = "7";
        private string DecrementoCapitalCaja = "8";    
        
        private int TransaccionAperturaCajaGeneral = 1;
        private int TransaccionCierreCajaGeneral = 2;
        private int TransaccionIncrementoCapitalCajaGeneral = 3;
        private int TransaccionDecrementoCapitalCajaGeneral = 4;
        private int TransaccionIncrementoCapitalCajaTransaccional = 15;
        private int TransaccionDecrementoCapitalCajaTransaccional = 16;
        private int TransaccionAperturaCajaTransaccional = 17;
        private int TransaccionCierreCajaTransaccional = 18;
        private int TransaccionRecepcionCajaGeneral = 14;
        private int TransaccionRecepcionCajaTransaccional = 19;

        #endregion

        #region combos

        public DataTable getCaja(ref string error)
        {
            try
            {
                DataTable Caja = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_caja, 'SELECCIONAR' AS nombre_caja UNION select id_caja, nombre_caja from tbl_caja";
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

        public DataTable getEstadoCajaPorRol(ref string error)
        {
            try
            {
                DataTable EstadoCaja = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = " SELECT 0 AS id_estado_caja, 'SELECCIONAR' AS estado_caja UNION "
                                    + "  select id_estado_caja, estado_caja "
                                    + "     from tbl_estado_caja "
                                    + " where id_tipo_caja in (select id_tipo_caja from tbl_rol_Tipo_Caja where id_rol = @rol) and id_estado_caja not in (@Disponible) ";
                command.Parameters.AddWithValue("@rol", CLASS.cs_usuario.id_rol);
                command.Parameters.AddWithValue("@Disponible", Disponible);
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

        public DataTable getEstadoAsignacionCaja(ref string error)
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


        public DataTable getUsuarioAsignado(ref string error)
        {
            try
            {
                DataTable EstadoAreaEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_usuario, 'SELECCIONAR' AS usuario UNION select id_usuario, usuario from tbl_usuario";
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
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = " select top (20) ac.id_asignacion_caja, " 
				                                    + " ac.id_caja,                                                "
				                                    + " c.nombre_caja,                                             "
				                                    + " ec.estado_caja,                                            "
				                                    + " ac.monto,                                                  "
				                                    + " case                                                       "
				                                    + "	when ac.estado_asignacion = 1 then 'ACTIVO'                "
				                                    + "	else 'INACTIVO'                                            "
				                                    + " end estado_asignacion,                                     "
				                                    + " ac.usuario_asigna as usuario_asigna,                        "
                                                    + " (us.primer_nombre + ' ' + us.segundo_nombre + ' ' +us.primer_apellido + ' ' + us.segundo_apellido) as usuario_asignado "
                                        + "	from tbl_asignacion_caja ac                                             "
                                        + "		inner join tbl_caja c                                               "
                                        + "			on ac.id_caja = c.id_caja                                       "
                                        + "		inner join tbl_estado_caja ec                                       "
                                        + "			on ac.id_estado_caja = ec.id_estado_caja                        "
                                        + "		inner join tbl_usuario us                                           "
                                        + "			on ac.id_usuario_asignado = us.id_usuario                       "
                                        + "		inner join tbl_rol_Tipo_Caja rtc                                    "
                                        + "			on rtc.id_tipo_caja = c.id_tipo_caja                            "
                                        + "		inner join tbl_rol r                                                "
                                        + "			on r.id_rol = rtc.id_rol                                        "
                                        + "	where r.id_rol = @id_rol                                                "
                                        + "	group by ac.id_asignacion_caja,                                         "
                                        + "				 ac.id_caja,                                                "
                                        + "				 c.nombre_caja,                                             "
                                        + "				 ec.estado_caja,                                            "
                                        + "				 ac.monto,                                                  "
                                        + "				 ac.estado_asignacion,                                      "
                                        + "				 ac.usuario_asigna,                                         "
                                        + "				 us.primer_nombre,                                          "
                                        + "				 us.segundo_nombre,                                         "
                                        + "				 us.primer_apellido,                                        "
                                        + "				 us.segundo_apellido, ac.fecha_creacion order by ac.fecha_creacion ";
                command.Parameters.AddWithValue("@id_rol", CLASS.cs_usuario.id_rol);
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
                                            string fecha_creacion, string fecha_modificacion, string usuario_asigna, string id_usuario_asignado, Boolean  BlnRecibir)
        {
            try
            {
                DataTable DtDatosAsignacionCaja = new DataTable();
                string TipoCajaAsignacion = "";
                string EstadoCajaAsignacion = "";
                bool General_Transaccional = false;
                int TipoTransaccion = 0;

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = " INSERT INTO tbl_asignacion_caja (id_caja, id_estado_caja, monto,                    "
                                     + "       estado_asignacion, fecha_creacion, fecha_modificacion, usuario_asigna, id_usuario_asignado)      "
                                     + "       VALUES(@id_caja, @id_estado_caja, @monto,                                   "
                                     + "       @estado_asignacion, @fecha_creacion, @fecha_modificacion, @usuario_asigna, @id_usuario_asignado)";
                if (BlnRecibir == true) //CUANDO LA CAJA ES RECIBIDA
                {
                    command.Parameters.AddWithValue("@id_estado_caja", CajaRecibida);
                    command.Parameters.AddWithValue("@id_usuario_asignado", CLASS.cs_usuario.id_usuario);
                }
                else
                {
                    command.Parameters.AddWithValue("@id_estado_caja", id_estado_caja);
                    command.Parameters.AddWithValue("@id_usuario_asignado", id_usuario_asignado);
                }

                command.Parameters.AddWithValue("@id_caja", id_caja);               
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@estado_asignacion", estado_asignacion);
                command.Parameters.AddWithValue("@fecha_creacion", fecha_creacion);
                command.Parameters.AddWithValue("@fecha_modificacion", fecha_modificacion);
                command.Parameters.AddWithValue("@usuario_asigna", usuario_asigna);
                
                command.ExecuteNonQuery();

                //OBTIENE TIPO DE CAJA y ESTADO CAJA
                command.Parameters.Clear();
                command.CommandText = " select id_tipo_caja from tbl_caja where id_caja = @id_caja ";
                command.Parameters.AddWithValue("@id_caja", id_caja);
                DtDatosAsignacionCaja.Load(command.ExecuteReader());

                foreach (DataRow item in DtDatosAsignacionCaja.Rows)
                {
                    TipoCajaAsignacion = item[0].ToString();
                }

                EstadoCajaAsignacion = id_estado_caja;
                //INSERTANDO TRANSACCION
                command.Parameters.Clear();
                command.CommandText = " INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja ,monto, "
                                      + " estado_transaccion, fecha_transaccion, usuario) "
                                      + "  VALUES(@id_tipo_transaccion, @id_caja, @monto, "
                                      + " @estado_transaccion, @fecha_transaccion, @usuario) ";

                foreach (string item in TipoCajaGeneral)
                {
                    if (TipoCajaAsignacion.ToString() == item)
                    {
                        General_Transaccional = true; //GENERAL
                    }
                }

                if (General_Transaccional == false) //TRANSACCIONAL
                {
                    if (BlnRecibir == true)
                    {
                        TipoTransaccion = TransaccionRecepcionCajaTransaccional;
                    }
                    else
                    {
                        if (EstadoCajaAsignacion == AsignacionCaja)
                        {
                            TipoTransaccion = TransaccionAperturaCajaTransaccional;
                        }
                        else if (EstadoCajaAsignacion == CierreCaja)
                        {
                            TipoTransaccion = TransaccionCierreCajaTransaccional;
                        }
                        else if (EstadoCajaAsignacion == IncrementoCapitalCaja)
                        {
                            TipoTransaccion = TransaccionIncrementoCapitalCajaTransaccional;
                        }
                        else if (EstadoCajaAsignacion == DecrementoCapitalCaja)
                        {
                            TipoTransaccion = TransaccionDecrementoCapitalCajaTransaccional;
                        }
                    }
                    
                }
                else  //GENERAL
                {
                    if (BlnRecibir == true)
                    {
                        TipoTransaccion = TransaccionRecepcionCajaGeneral;
                    }
                    else
                    {
                        if (EstadoCajaAsignacion == AsignacionCaja)
                        {
                            TipoTransaccion = TransaccionAperturaCajaGeneral;
                        }
                        else if (EstadoCajaAsignacion == CierreCaja)
                        {
                            TipoTransaccion = TransaccionCierreCajaGeneral;
                        }
                        else if (EstadoCajaAsignacion == IncrementoCapitalCaja)
                        {
                            TipoTransaccion = TransaccionIncrementoCapitalCajaGeneral;
                        }
                        else if (EstadoCajaAsignacion == DecrementoCapitalCaja)
                        {
                            TipoTransaccion = TransaccionDecrementoCapitalCajaGeneral;
                        }
                    }                   
                }


                command.Parameters.AddWithValue("@id_tipo_transaccion", TipoTransaccion);
                command.Parameters.AddWithValue("@id_caja", id_caja);
                command.Parameters.AddWithValue("@monto", monto);                
                command.Parameters.AddWithValue("@estado_transaccion", 2);
                command.Parameters.AddWithValue("@fecha_transaccion", fecha_creacion);
                command.Parameters.AddWithValue("@usuario", usuario_asigna);
                command.ExecuteNonQuery();

                command.Parameters.Clear();
                command.CommandText = " UPDATE tbl_caja SET id_estado_caja = @id_estado_caja, fecha_modificacion = @fecha_modificacion"
                                    + " WHERE id_caja = @id_caja";
                command.Parameters.AddWithValue("@id_estado_caja", id_estado_caja);
                command.Parameters.AddWithValue("@id_caja", id_caja);
                command.Parameters.AddWithValue("@fecha_modificacion", fecha_creacion);
                command.ExecuteNonQuery();

                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo insertar la asiganación de caja, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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

        public DataTable getDatosAsignacionCaja(ref string error, string id_asignacion_caja)
        {
            try
            {
                DataTable EstadoAreaEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = " select * from tbl_asignacion_caja where id_asignacion_caja = @id_asignacion_caja";
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

        public bool recibirCaja(ref string error, string id_asignacion_caja)
        {
            try
            {
                int rowsUpdated = 0;
                
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_asignacion_caja SET id_estado_caja = 3, estado_asignacion = 1 WHERE id_asignacion_caja = @id_asignacion_caja";
                command.Parameters.AddWithValue("@id_asignacion_caja", id_asignacion_caja);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    command.Transaction.Rollback();
                    return false;
                }

                command.CommandText = "UPDATE tbl_caja SET id_estado_caja = 3 WHERE id_caja = @id_caja";
                command.Parameters.AddWithValue("@id_caja", CLASS.cs_usuario.id_caja);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    command.Transaction.Rollback();
                    return false;
                }
                command.CommandText = "UPDATE tbl_caja SET saldo = saldo - @saldo WHERE id_tipo_caja = 1 AND id_sucursal = @id_sucursal";
                command.Parameters.AddWithValue("@id_sucursal", CLASS.cs_usuario.id_sucursal);
                command.Parameters.AddWithValue("@saldo", CLASS.cs_usuario.Saldo_caja);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    command.Transaction.Rollback();
                    return false;
                }

                command.CommandText = "UPDATE tbl_usuario SET caja_asignada = 1 WHERE id_usuario = @id_usuario";
                command.Parameters.AddWithValue("@id_usuario", CLASS.cs_usuario.id_usuario);
                rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated <= 0)
                {
                    command.Transaction.Rollback();
                    return false;
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
        #endregion

    }

}