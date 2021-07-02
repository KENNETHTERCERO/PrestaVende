using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PrestaVende.CLASS
{
    public class cs_Empresa
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        
        public string getIDMaxEmpresa(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_empresa), 0) +1 FROM tbl_empresa";
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

        public int getShowReportViewer(ref string error, string id_sucursal)
        {
            try
            {
                int returnShow = 0;
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT show_report_viewer FROM tbl_sucursal" +
                                        "WHERE id_sucursal = @id_sucursal";
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                returnShow = Convert.ToInt32(command.ExecuteScalar());
                return returnShow;
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

        public int getEnabledComboBoxTypeLoan(ref string error, string id_empresa)
        {
            try
            {
                int returnShow = 0;
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT enabled_combo_box_type_loan FROM tbl_empresa" +
                                        "WHERE id_empresa = @id_empresa";
                command.Parameters.AddWithValue("@id_empresa", id_empresa);
                returnShow = Convert.ToInt32(command.ExecuteScalar());
                return returnShow;
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

        public DataTable getEmpresa(ref string error)
        {
            try
            {
                DataTable AreaEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT e.id_empresa, ae.descripcion, te.tipo_empresa, e.empresa, e.nit_empresa, e.direccion_empresa, e.patente_numero, "
                                    + "        e.libro, e.folio, (CASE WHEN e.estado = 1 THEN 'ACTIVO' WHEN e.estado = 0 THEN 'INACTIVO' ELSE 'OTRO ESTADO' END) AS estado, "
                                    + "        (CASE WHEN e.puede_vender = 1 THEN 'SI' ELSE 'NO' END) AS puede_vender                               "
                                    + " FROM tbl_empresa e                                  "
                                    + "     INNER JOIN tbl_area_empresa ae                  "
                                    + "      ON e.id_area_empresa = ae.id_area_empresa      "
                                    + "     INNER JOIN tbl_tipo_empresa te                  "
                                    + "      ON te.id_tipo_empresa = e.id_tipo_empresa";    
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

        public DataTable getAreaEmpresa(ref string error)
        {
            try
            {
                DataTable AreaEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_area_empresa, 'SELECCIONAR' AS descripcion UNION SELECT id_area_empresa, descripcion FROM tbl_area_empresa where estado = 1";
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

        public DataTable getTipoEmpresa(ref string error)
        {
            try
            {
                DataTable TipoEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_tipo_empresa, 'SELECCIONAR' AS tipo_empresa UNION SELECT id_tipo_empresa, tipo_empresa FROM tbl_tipo_empresa where estado = 1";
                TipoEmpresa.Load(command.ExecuteReader());
                return TipoEmpresa;
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

        public DataTable getEstadoEmpresa(ref string error)
        {
            try
            {
                DataTable EstadoEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id, 'INACTIVO' AS estado UNION SELECT 1 AS id, 'ACTIVO' AS estado";
                EstadoEmpresa.Load(command.ExecuteReader());
                return EstadoEmpresa;
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

        public DataTable getObtieneDatosModificar(ref string error, string id_empresa)
        {
            try
            {
                DataTable DatosAreaEmpresa = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT id_empresa, id_area_empresa, id_tipo_empresa, empresa, nit_empresa, "
                                    + " direccion_empresa, patente_numero, libro, folio, estado,puede_vender      "
                                    + " FROM tbl_empresa WHERE id_empresa = @id_empresa";
                command.Parameters.AddWithValue("@id_empresa", id_empresa);
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

        public bool insertEmpresa(ref string error, string id_empresa, string id_area_empresa, string id_tipo_empresa,
                                      string empresa, string nit_empresa, string direccion_empresa, 
                                      string patente_numero, string libro, string folio, string estado, 
                                      string fecha_creacion, string fecha_modificacion, bool puede_vender)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = " INSERT INTO tbl_empresa (id_empresa, id_area_empresa, id_tipo_empresa, empresa, "
                                   + "         nit_empresa, direccion_empresa, patente_numero, libro,                   "
			                       + "		folio, estado, fecha_creacion, fecha_modificacion, puede_vender)            "
			                       + " VALUES(@id_empresa, @id_area_empresa, @id_tipo_empresa, @empresa,                "
                                   + "         @nit_empresa, @direccion_empresa, @patente_numero, @libro,               "
                                   + "         @folio, @estado, @fecha_creacion, @fecha_modificacion, @puede_vender) ";
                command.Parameters.AddWithValue("@id_empresa", id_empresa);
                command.Parameters.AddWithValue("@id_area_empresa", id_area_empresa);
                command.Parameters.AddWithValue("@id_tipo_empresa", id_tipo_empresa);
                command.Parameters.AddWithValue("@empresa", empresa);
                command.Parameters.AddWithValue("@nit_empresa", nit_empresa);
                command.Parameters.AddWithValue("@direccion_empresa", direccion_empresa);
                command.Parameters.AddWithValue("@patente_numero", patente_numero);
                command.Parameters.AddWithValue("@libro", libro);
                command.Parameters.AddWithValue("@folio", folio);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@fecha_creacion", fecha_creacion);
                command.Parameters.AddWithValue("@fecha_modificacion", fecha_modificacion);

                if (puede_vender == false)
                {
                    command.Parameters.AddWithValue("@puede_vender", "0");
                }
                else
                {
                    command.Parameters.AddWithValue("@puede_vender", "1");
                }
                
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo insertar la empresa, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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

        public bool updateEmpresa(ref string error, string[] datos)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_empresa SET id_area_empresa = @id_area_empresa, id_tipo_empresa = @id_tipo_empresa, "
                                       + "                  empresa = @empresa, nit_empresa = @nit_empresa, direccion_empresa = @direccion_empresa, "
                                       + "    				patente_numero = @patente_numero, libro = @libro, folio = @folio, estado = @estado, "
                                       + "    				fecha_modificacion = @fecha_modificacion, puede_vender = @puede_vender "
                                       + "  WHERE id_empresa = @id_empresa";
                command.Parameters.AddWithValue("@id_empresa", datos[0]);
                command.Parameters.AddWithValue("@id_area_empresa", datos[1]);
                command.Parameters.AddWithValue("@id_tipo_empresa", datos[2]);
                command.Parameters.AddWithValue("@empresa", datos[3]);
                command.Parameters.AddWithValue("@nit_empresa", datos[4]);
                command.Parameters.AddWithValue("@direccion_empresa", datos[5]);
                command.Parameters.AddWithValue("@patente_numero", datos[6]);
                command.Parameters.AddWithValue("@libro", datos[7]);
                command.Parameters.AddWithValue("@folio", datos[8]);
                command.Parameters.AddWithValue("@estado", datos[9]);
                command.Parameters.AddWithValue("@fecha_modificacion", datos[10]);

                if (datos[11].ToString() == "False")
                {
                    command.Parameters.AddWithValue("@puede_vender", 0);
                }
                else
                {
                    command.Parameters.AddWithValue("@puede_vender", 1);
                }

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