using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_serie
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public string getIDMaxSerie(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_serie), 0) +1 FROM tbl_serie";
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

        public DataTable getSerieDDL(ref string error, int id_sucursal)
        {
            try
            {
                DataTable Serie = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_serie, 'SELECCIONAR' AS serie UNION "
                                    + "SELECT  id_serie,serie"
                                    + " FROM tbl_serie       "
                                    + " WHERE estado = 1 AND id_sucursal = @id_sucursal";
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                Serie.Load(command.ExecuteReader());
                return Serie;
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

        public Int64 getCorrelativoSerie(ref string error, int id_serie)
        {
            try
            {
                DataTable Serie = new DataTable();
                Int64 correlativo = 0;
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT Correlativo"
                                    + " FROM tbl_serie       "
                                    + " WHERE estado = 1 AND id_serie = @id_serie";
                command.Parameters.AddWithValue("@id_serie", id_serie);
                Serie.Load(command.ExecuteReader());

                if(Serie.Rows.Count > 0)
                {
                    correlativo = Int64.Parse(Serie.Rows[0]["Correlativo"].ToString());
                }

                return correlativo;
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

        public DataTable getSerie(ref string error)
        {
            try
            {
                DataTable Serie = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT s.id_serie, s.serie, s.resolucion, CONVERT(VARCHAR(10), s.fecha_resolucion, 103) AS fecha_resolucion, "
                                    + "        (CASE WHEN s.estado = 1 THEN 'ACTIVO' WHEN s.estado = 0 THEN 'INACTIVO' ELSE 'OTRO ESTADO' END) AS estado, "
                                    + "        s.fecha_creacion, suc.sucursal, s.correlativo, s.numero_de_facturas "
                                    + " FROM tbl_serie s                                    "
                                    + "     INNER JOIN tbl_sucursal suc                     "
                                    + "      ON suc.id_sucursal = s.id_sucursal                ";
                Serie.Load(command.ExecuteReader());
                return Serie;
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
                command.CommandText = "SELECT 0 AS id_sucursal, 'SELECCIONAR' AS sucursal UNION SELECT id_sucursal, sucursal FROM tbl_sucursal where estado = 1";
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

        public DataTable getEstadoSerie(ref string error)
        {
            try
            {
                DataTable EstadoSerie = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id, 'INACTIVO' AS estado UNION SELECT 1 AS id, 'ACTIVO' AS estado";
                EstadoSerie.Load(command.ExecuteReader());
                return EstadoSerie;
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

        public DataTable getObtieneDatosModificar(ref string error, string id_serie)
        {
            try
            {
                DataTable DatosSerie = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT id_serie, serie, resolucion, fecha_resolucion, estado      "
                                    + " , id_sucursal, correlativo, numero_de_facturas                   "
                                    + " FROM tbl_serie WHERE id_serie = @id_serie";
                command.Parameters.AddWithValue("@id_serie", id_serie);
                DatosSerie.Load(command.ExecuteReader());
                return DatosSerie;
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

        public bool insertSerie(ref string error, string id_serie, string serie, string resolucion,
                                      string fecha_resolucion, string estado,
                                      string id_sucursal, string numero_de_facturas)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = " INSERT INTO tbl_serie (serie, resolucion, fecha_resolucion,    "
                                   + "      estado, fecha_creacion, id_sucursal, correlativo,  numero_de_facturas) "
                                   + " VALUES(@serie, @resolucion, @fecha_resolucion,                   "
                                   + "         @estado, GETDATE(), @id_sucursal, @correlativo,               "
                                   + "         @numero_de_facturas) ";
                command.Parameters.AddWithValue("@serie", serie);
                command.Parameters.AddWithValue("@resolucion", resolucion);
                command.Parameters.AddWithValue("@fecha_resolucion", fecha_resolucion);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                command.Parameters.AddWithValue("@correlativo", 0);
                command.Parameters.AddWithValue("@numero_de_facturas", numero_de_facturas);

                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo insertar la serie, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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

        public bool updateSerie(ref string error, string[] datos)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_serie SET serie = @serie, "
                                       + "                  resolucion = @resolucion, fecha_resolucion = @fecha_resolucion, estado = @estado, "
                                       + "    				id_sucursal = @id_sucursal, numero_de_facturas = @numero_de_facturas "
                                       + "  WHERE id_serie = @id_serie";
                command.Parameters.AddWithValue("@id_serie", datos[0]);
                command.Parameters.AddWithValue("@serie", datos[1]);
                command.Parameters.AddWithValue("@resolucion", datos[2]);
                command.Parameters.AddWithValue("@fecha_resolucion", datos[3]);
                command.Parameters.AddWithValue("@estado", datos[4]);
                command.Parameters.AddWithValue("@id_sucursal", datos[6]);                
                command.Parameters.AddWithValue("@numero_de_facturas", datos[8]);

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