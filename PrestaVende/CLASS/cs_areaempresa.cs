using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PrestaVende.CLASS
{
    public class cs_areaempresa
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        
        public string getIDMaxAreaEmpresa(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_area_empresa), 0) +1 FROM tbl_area_empresa";
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


        public DataTable getAreaEmpresa(ref string error)
        {
            try
            {
                DataTable AreaEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = " SELECT "
                                    + "     ae.id_area_empresa,  "
                                    + "     ae.id_pais, "
                                    + "		p.pais, "
                                    + "        ae.descripcion, "
                                    + "        (CASE WHEN ae.estado = 1 THEN 'ACTIVO' WHEN ae.estado = 0 THEN 'INACTIVO' ELSE 'OTRO ESTADO' END) AS estadoAreaEmpresa, "
                                    + "        ae.fecha_creacion, "
                                    + "       ae.fecha_modificacion, "
                                    + "        ae.estado "
                                    + "  FROM tbl_area_empresa AS ae "
                                    + "    INNER JOIN tbl_pais p "
                                    + "        ON p.id_pais = ae.id_pais "
                                    + "  WHERE p.estado = 1 ";
                AreaEmpresa.Load(command.ExecuteReader());
                return AreaEmpresa;
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

        public DataTable getPais(ref string error)
        {
            try
            {
                DataTable pais = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_pais, 'SELECCIONAR' AS descripcion UNION SELECT id_pais, UPPER(pais) From tbl_pais WHERE estado = 1";
                pais.Load(command.ExecuteReader());
                return pais;
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

        public DataTable getEstadoAreaEmpresa(ref string error)
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

        public DataTable getObtieneDatosModificar(ref string error, string id_area_empresa)
        {
            try
            {
                DataTable DatosAreaEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT id_area_empresa, id_pais, descripcion, estado FROM tbl_area_empresa WHERE id_area_empresa = @id_area_empresa";
                command.Parameters.AddWithValue("@id_area_empresa", id_area_empresa);
                DatosAreaEmpresa.Load(command.ExecuteReader());
                return DatosAreaEmpresa;
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

        public bool insertAreaEmpresa(ref string error, string id_area_empresa, string id_pais, string descripcion, string estado, string fecha_creacion, string fecha_modificacion)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = " INSERT INTO tbl_area_empresa(id_area_empresa, id_pais, descripcion, estado, fecha_creacion, fecha_modificacion) " +
                                      " VALUES(@id_area_empresa, @id_pais, @descripcion, @estado, @fecha_creacion, @fecha_modificacion)";
                command.Parameters.AddWithValue("@id_area_empresa", id_area_empresa);
                command.Parameters.AddWithValue("@id_pais", id_pais);
                command.Parameters.AddWithValue("@descripcion", descripcion);
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
                    throw new SystemException("No se pudo insertar area empresa, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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

        public bool updateAreaEmpresa(ref string error, string[] datos)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_area_empresa SET id_pais = @id_pais, descripcion = @descripcion, estado = @estado, fecha_modificacion = @fecha_modificacion WHERE id_area_empresa = @id_area_empresa";
                command.Parameters.AddWithValue("@id_area_empresa", datos[0]);
                command.Parameters.AddWithValue("@id_pais", datos[1]);
                command.Parameters.AddWithValue("@descripcion", datos[2]);
                command.Parameters.AddWithValue("@estado", datos[3]);
                command.Parameters.AddWithValue("@fecha_modificacion", datos[4]);                
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