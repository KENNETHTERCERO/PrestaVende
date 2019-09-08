using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PrestaVende.CLASS
{
    public class cs_liquidacion
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        private int intTransaccionLiquidacion = 12;
        
        public string getIDMaxLiquidacion(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_liquidacion), 0) +1 FROM tbl_liquidacion";
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

        public DataTable getLiquidacion(ref string error)
        {
            try
            {
                DataTable Liquidacion = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = " SELECT l.id_liquidacion, "
                                        + " 	   l.id_sucursal, "
                                        + " 	   s.sucursal, "
                                        + " 	   l.numero_prestamo, "
                                        + " 	   l.monto_liquidacion, "
                                        + " 	   (CASE WHEN l.estado_liquidacion = 1 THEN 'ACTIVO' WHEN l.estado_liquidacion = 0 THEN 'ANULADO' ELSE 'OTRO ESTADO' END) AS estadoLiquidacion "
                                        + " FROM tbl_liquidacion l "
                                        + "     INNER JOIN tbl_sucursal s "
                                        + "         ON l.id_sucursal = s.id_sucursal "
                                        + "     INNER JOIN tbl_prestamo_encabezado pe "
                                        + "         ON l.numero_prestamo = pe.numero_prestamo "
                                        + " WHERE l.estado_liquidacion = 1 ";
                Liquidacion.Load(command.ExecuteReader());
                return Liquidacion;
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

        public DataTable getSucursal(ref string error)
        {
            try
            {
                DataTable Sucursal = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_sucursal, 'SELECCIONAR' AS sucursal UNION SELECT id_sucursal, UPPER(sucursal) from tbl_sucursal WHERE estado = 1";
                Sucursal.Load(command.ExecuteReader());
                return Sucursal;
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

        public bool getValidaPrestamo(ref string error, string id_prestamo_encabezado)
        {
            try
            {
                DataTable Prestamo = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT * FROM tbl_prestamo_encabezado WHERE id_prestamo_encabezado = @id_prestamo_encabezado and estado_prestamo = 2";
                command.Parameters.AddWithValue("@id_prestamo_encabezado", id_prestamo_encabezado);
                Prestamo.Load(command.ExecuteReader());

                if (Prestamo.Rows.Count > 0)
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
            finally
            {
                connection.connection.Close();
            }
        }

        public DataTable getEstadoLiquidacion(ref string error)
        {
            try
            {
                DataTable EstadoLiquidacion = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id, 'ANULADO' AS estado UNION SELECT 1 AS id, 'ACTIVO' AS estado";
                EstadoLiquidacion.Load(command.ExecuteReader());
                return EstadoLiquidacion;
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

        public DataTable getObtieneDatosModificar(ref string error, string id_liquidacion)
        {
            try
            {
                DataTable DatosLiquidacion = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT id_liquidacion, id_sucursal, numero_prestamo, monto_liquidacion, estado_liquidacion FROM tbl_liquidacion WHERE id_liquidacion = @id_liquidacion";
                command.Parameters.AddWithValue("@id_liquidacion", id_liquidacion);
                DatosLiquidacion.Load(command.ExecuteReader());
                return DatosLiquidacion;
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

        public bool insertLiquidacion(ref string error, string id_sucursal, string numero_prestamo, string monto_liquidacion, string estado_liquidacion, string fecha_liquidacion, string fecha_anulacion_liquidacion)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_liquidacion (id_sucursal, numero_prestamo, monto_liquidacion, estado_liquidacion, fecha_liquidacion, fecha_anulacion_liquidacion) "
                                        + " VALUES(@id_sucursal, @numero_prestamo, @monto_liquidacion, @estado_liquidacion, @fecha_liquidacion, @fecha_anulacion_liquidacion) ";
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
                command.Parameters.AddWithValue("@monto_liquidacion", monto_liquidacion);
                command.Parameters.AddWithValue("@estado_liquidacion", estado_liquidacion);
                command.Parameters.AddWithValue("@fecha_liquidacion", fecha_liquidacion);
                command.Parameters.AddWithValue("@fecha_anulacion_liquidacion", fecha_anulacion_liquidacion);


                //MODIFICA EL ESTADO DEL PRESTAMO A LIQUIDADO
                command.Parameters.Clear();
                command.CommandText = " UPDATE tbl_prestamo_encabezado SET estado_prestamo = 3 WHERE numero_prestamo = @numero_prestamo ";
                command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
                

                //INSERTANDO TRANSACCION
                //command.Parameters.Clear();
                //command.CommandText = " INSERT INTO tbl_transaccion (id_tipo_transaccion, id_caja ,monto, "
                //                      + " estado_transaccion, fecha_transaccion, usuario) "
                //                      + "  VALUES(@id_tipo_transaccion, @id_caja, @monto, "
                //                      + " @estado_transaccion, @fecha_transaccion, @usuario) ";
                //command.Parameters.AddWithValue("@id_tipo_transaccion", intTransaccionLiquidacion);
                //command.Parameters.AddWithValue("@id_caja", id_caja);
                //command.Parameters.AddWithValue("@monto", monto);
                //command.Parameters.AddWithValue("@estado_transaccion", estado_transaccion);
                //command.Parameters.AddWithValue("@fecha_transaccion", fecha_transaccion);
                //command.Parameters.AddWithValue("@usuario", usuario);


                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo insertar la liquidación, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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

        public bool updateLiquidacion(ref string error, string[] datos)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_liquidacion SET estado_liquidacion = @estado_liquidacion, fecha_anulacion_liquidacion = @fecha_anulacion_liquidacion WHERE id_liquidacion = @id_liquidacion";
                command.Parameters.AddWithValue("@id_liquidacion", datos[0]);
                command.Parameters.AddWithValue("@fecha_anulacion_liquidacion", datos[1]);
                command.Parameters.AddWithValue("@estado_liquidacion", datos[2]);

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

   
    }


}