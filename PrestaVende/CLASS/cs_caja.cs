using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PrestaVende.CLASS
{
    public class cs_caja
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        
        public string getIDMaxCaja(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_caja), 0) +1 FROM tbl_caja";
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

        public DataTable getCaja(ref string error)
        {
            try
            {
                DataTable Caja = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = " SELECT c.id_caja, "
                                + "        s.sucursal, "
                                + " 	   tc.tipo_caja, "
                                + " 	   ec.estado_caja, "
                                + " 	   c.nombre_caja, "
                                + " 	   c.saldo, "
                                + " 	   (CASE WHEN c.estado = 1 THEN 'ACTIVO' WHEN c.estado = 0 THEN 'INACTIVO' ELSE 'OTRO ESTADO' END) AS estado "
                                + " FROM tbl_caja c "
                                + "     INNER JOIN tbl_sucursal s "
                                + "         ON c.id_sucursal = s.id_sucursal "
                                + "     INNER JOIN tbl_tipo_caja tc "
                                + "         ON tc.id_tipo_caja = c.id_tipo_caja "
                                + "     INNER JOIN tbl_estado_caja ec "
                                + "         ON ec.id_estado_caja = c.id_estado_caja "
                                + " INNER JOIN tbl_rol_Tipo_Caja rtc "
                                + "         ON rtc.id_tipo_caja = tc.id_tipo_caja "
                                + "     WHERE rtc.id_rol = @id_rol ";
                command.Parameters.AddWithValue("@id_rol", Convert.ToInt32(HttpContext.Current.Session["id_rol"]));
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

        public DataTable getTipoCaja(ref string error)
        {
            try
            {
                DataTable TipoCaja = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT 0 AS id_tipo_caja, 'SELECCIONAR' AS tipo_caja UNION  "
                                    + " SELECT tc.id_tipo_caja, UPPER(tc.tipo_caja) "
                                    + " From tbl_tipo_caja tc "
                                    + "     INNER JOIN tbl_rol_Tipo_Caja rtc "
                                    + "         ON rtc.id_tipo_caja = tc.id_tipo_caja "
                                    + "     INNER JOIN tbl_rol r " 
                                    + "         ON r.id_rol = rtc.id_rol "
                                    + " WHERE tc.estado = 1 and rtc.id_rol = @id_rol";
                command.Parameters.AddWithValue("@id_rol", Convert.ToInt32(HttpContext.Current.Session["id_rol"]));
                TipoCaja.Load(command.ExecuteReader());
                return TipoCaja;
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

        public DataTable getEstadoCaja(ref string error)
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

        public DataTable getObtieneDatosModificar(ref string error, string id_caja)
        {
            try
            {
                DataTable DatosCaja = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT id_caja, id_sucursal, id_tipo_caja, id_estado_caja, nombre_caja, saldo, estado FROM tbl_caja where id_caja = @id_caja";
                command.Parameters.AddWithValue("@id_caja", id_caja);
                DatosCaja.Load(command.ExecuteReader());
                return DatosCaja;
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

        public bool insertCaja(ref string error, string id_sucursal, string id_tipo_caja, string id_estado_caja, string nombre_caja, string saldo, string estado, string fecha_creacion, string fecha_modificacion)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = " INSERT INTO tbl_caja (id_sucursal, id_tipo_caja, id_estado_caja, nombre_caja, saldo, "
                                        + " estado, fecha_creacion, fecha_modificacion)  "
                                        + " VALUES(@id_sucursal, @id_tipo_caja, @id_estado_caja, @nombre_caja, @saldo, "
                                        + " @estado, @fecha_creacion, @fecha_modificacion) ";
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                command.Parameters.AddWithValue("@id_tipo_caja", id_tipo_caja);
                command.Parameters.AddWithValue("@id_estado_caja", id_estado_caja);
                command.Parameters.AddWithValue("@nombre_caja", nombre_caja);
                command.Parameters.AddWithValue("@saldo", saldo);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@fecha_creacion", fecha_creacion);
                command.Parameters.AddWithValue("@fecha_modificacion", fecha_modificacion);
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo insertar la caja, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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

        public bool updateCaja(ref string error, string[] datos)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_caja SET id_sucursal = @id_sucursal, id_tipo_caja = @id_tipo_caja, nombre_caja = @nombre_caja, saldo = @saldo, estado= @estado, fecha_modificacion = @fecha_modificacion WHERE id_caja = @id_caja ";
                command.Parameters.AddWithValue("@id_caja", datos[0]);
                command.Parameters.AddWithValue("@id_sucursal", datos[1]);
                command.Parameters.AddWithValue("@id_tipo_caja", datos[2]);
                command.Parameters.AddWithValue("@nombre_caja", datos[3]); 
                command.Parameters.AddWithValue("@saldo", datos[4]);
                command.Parameters.AddWithValue("@estado", datos[5]);
                command.Parameters.AddWithValue("@fecha_modificacion", datos[6]);                
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

        public DataTable ObtenerEstadoCuenta(ref string error, string id_sucursal, string id_caja, string fechaInicio, string fechaFinal)
        {
            DataTable dtReturnFacturas = new DataTable("dtEstadoCuenta");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();

                command.CommandText = "exec SP_ConsultaEstadoCuentaCaja @id_sucursal, @id_caja, @fechaInicio, @fecha_final";
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                command.Parameters.AddWithValue("@id_caja", @id_caja);
                command.Parameters.AddWithValue("@fechaInicio", @fechaInicio);
                command.Parameters.AddWithValue("@fecha_final", @fechaFinal);
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

        public DataTable getCajasCombo(ref string error, string id_sucursal)
        {
            try
            {
                DataTable Caja = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_caja, 'TODAS' AS nombre_caja UNION " +
                                      "SELECT id_caja, nombre_caja FROM tbl_caja WHERE id_sucursal = @id_sucursal";
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
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
    }


}