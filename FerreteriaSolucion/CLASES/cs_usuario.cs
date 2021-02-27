using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaSolucion.CLASES
{
    class cs_usuario
    {
        private cs_connection_ferremas connection = new cs_connection_ferremas();
        private SqlCommand command = new SqlCommand();

        public static int id_usuario = 0;
        

        public string getMaxIDUsuario(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_usuario), 0) + 1 FROM tbl_usuario";
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

        public DataTable getUsuarios(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("dat");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT us.id_usuario, us.usuario, us.contrasenia, us.primer_nombre, us.primer_apellido, (CASE WHEN us.estado = 1 THEN 'ACTIVO' WHEN us.estado = 0 THEN 'INACTIVO' END) AS estado_letras, (SELECT decripcion FROM tbl_tipo_acceso WHERE id_tipo_acceso = us.id_tipo_acceso) AS tipo_acceso_letras, us.id_tipo_acceso, us.estado From tbl_usuario AS us";
                returnTable.Load(command.ExecuteReader());
                return returnTable;
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

        public DataTable getTipoAcceso()
        {
            try
            {
                DataTable returnTable = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT 0 AS id, 'SELECCIONAR' AS descripcion UNION SELECT id_tipo_acceso AS id, decripcion AS descripcion FROM tbl_tipo_acceso";
                returnTable.Load(command.ExecuteReader());
                return returnTable;
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

        public DataTable logIn(ref string error, string usuario, string password)
        {
            try
            {
                DataTable returnTable = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select id_usuario, usuario, contrasenia, id_tipo_acceso From tbl_usuario WHERE usuario = @usuario AND contrasenia = @password";
                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@password", password);
                returnTable.Load(command.ExecuteReader());
                return returnTable;
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

        public bool insertUsuario(ref string error, string usuario, string contrasenia, string primer_nombre, string primer_apellido, string estado, string id_tipo_acceso)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_usuario (usuario, contrasenia, primer_nombre, primer_apellido, estado, id_tipo_acceso) " +
                                                        "VALUES(@usuario, @contrasenia, @primer_nombre, @primer_apellido, @estado, @id_tipo_acceso)";
                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@contrasenia", contrasenia);
                command.Parameters.AddWithValue("@primer_nombre", primer_nombre);
                command.Parameters.AddWithValue("@primer_apellido", primer_apellido);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@id_tipo_acceso", id_tipo_acceso);

                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("NO SE PUDO AGREGAR USUARIO");
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


        public bool updateUsuario(ref string error, string id_usuario, string usuario, string contrasenia, string primer_nombre, string primer_apellido, string estado, string id_tipo_acceso)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_usuario usuario = @usuario, " +
                                                         "contrasenia = @contrasenia, " +
                                                         "primer_nombre = @primer_nombre, " +
                                                         "primer_apellido = @primer_apellido, " +
                                                         "estado = @estado, " +
                                                         "id_tipo_acceso = @id_tipo_acceso " +
                                                         "WHERE id_usuario = @id_usuario";
                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@contrasenia", contrasenia);
                command.Parameters.AddWithValue("@primer_nombre", primer_nombre);
                command.Parameters.AddWithValue("@primer_apellido", primer_apellido);
                command.Parameters.AddWithValue("@usuario", estado);
                command.Parameters.AddWithValue("@id_tipo_acceso", id_tipo_acceso);
                command.Parameters.AddWithValue("@id_usuario", id_usuario);

                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("NO SE PUDO AGREGAR USUARIO");
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
