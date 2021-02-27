using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PrestaVende.CLASS
{
    public class cs_tipo_caja
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        
        public string getIDTipoCaja(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_tipo_caja), 0) +1 FROM tbl_tipo_caja";
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

        public DataTable getTipoCaja(ref string error)
        {
            try
            {
                DataTable TipoCaja = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT  id_tipo_caja, "
                                    + "         tipo_caja,   "
                                    + " 		(CASE WHEN estado = 1 THEN 'ACTIVO' WHEN estado = 0 THEN 'INACTIVO' ELSE 'OTRO ESTADO' END) AS estado "
                                    + " FROM tbl_tipo_caja ";
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
       
        public DataTable getEstadoTipoCaja(ref string error)
        {
            try
            {
                DataTable EstadoTipoCaja = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id, 'INACTIVO' AS estado UNION SELECT 1 AS id, 'ACTIVO' AS estado";
                EstadoTipoCaja.Load(command.ExecuteReader());
                return EstadoTipoCaja;
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

        public DataTable getObtieneDatosTipoCajaModificar(ref string error, string id_tipo_caja)
        {
            try
            {
                DataTable DatosTipoCaja = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT id_tipo_caja, tipo_caja, estado FROM tbl_tipo_caja WHERE id_tipo_caja = @id_tipo_caja";
                command.Parameters.AddWithValue("@id_tipo_caja", id_tipo_caja);
                DatosTipoCaja.Load(command.ExecuteReader());
                return DatosTipoCaja;
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

        public bool insertTipoCaja(ref string error, string tipo_caja, string estado, string fecha_creacion, string fecha_modificacion)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = " INSERT INTO tbl_tipo_caja (tipo_caja, estado, fecha_creacion, fecha_modificacion) "
                                    + " VALUES( @tipo_caja, @estado, @fecha_creacion, @fecha_modificacion) ";
                command.Parameters.AddWithValue("@tipo_caja", tipo_caja);
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
                    throw new SystemException("No se pudo insertar el tipo de caja, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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

        public bool updateTipoCaja(ref string error, string[] datos)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_tipo_caja SET tipo_caja = @tipo_caja, estado = @estado, fecha_modificacion = @fecha_modificacion "
                                    + " WHERE id_tipo_caja = @id_tipo_caja";
                command.Parameters.AddWithValue("@id_tipo_caja", datos[0]);
                command.Parameters.AddWithValue("@tipo_caja", datos[1]);
                command.Parameters.AddWithValue("@estado", datos[2]);
                command.Parameters.AddWithValue("@fecha_modificacion", datos[3]);              
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