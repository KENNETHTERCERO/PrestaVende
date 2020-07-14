using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_usuario
    {
        public static int id_usuario = 0;
        public static int id_empresa = 0;
        public static int id_sucursal = 0;
        public static int id_rol = 0;
        public static int id_caja = 0;
        public static int id_tipo_caja = 0;
        public static int puede_vender = 0;
        private static decimal saldo_caja = new decimal();
        public static string usuario = "";
        public static string primer_nombre = "";
        public static string primer_apellido = "";
        //aqui para arriba

        public static bool      autorizado      = false;

        private cs_connection connect = new cs_connection();
        private SqlCommand command = new SqlCommand();

        //public static decimal Saldo_caja
        //{
        //    get
        //    {
        //        return saldo_caja;
        //    }

        //    set
        //    {
        //        saldo_caja = value;
        //    }
        //}

        public cs_usuario()
        {

        }

        public string[] Login(string user, string password)
        {
            string errorExec = "";
            string[] error;
            error = new string[15];

            try
            {
                DataTable dtUser = new DataTable("user");

                connect.connection.Open();
                command.Connection = connect.connection;
                command.CommandText = "SELECT TOP 1 " +
                                            "usu.id_usuario, " +        //0, 1
	                                        "usu.id_empresa, " +        //1, 2
	                                        "usu.id_sucursal, " +       //2, 3
	                                        "usu.usuario, " +           //3, 4
	                                        "usu.primer_nombre," +      //4, 5
	                                        "usu.primer_apellido, " +   //5, 6
	                                        "usu.id_rol, " +            //6, 7
                                            "usu.estado, " +            //7, 8  
                                            "(CASE WHEN asi.id_estado_caja = 4 THEN 0 ELSE ISNULL(asi.id_caja, 0) END), " +           //8, 9
                                            "ISNULL(asi.id_asignacion_caja, 0), " +  //9, 10
                                            "ISNULL(asi.estado_asignacion, 0), " + //10, 11
                                            "usu.caja_asignada, " +     //11, 12
                                            "ISNULL(caj.id_tipo_caja, 0), " +       //12, 13
                                            "asi.id_estado_caja " + //13, 14
                                        "FROM " +
                                        "tbl_usuario AS usu " +
                                        "LEFT JOIN tbl_asignacion_caja AS asi ON asi.id_usuario_asignado = usu.id_usuario AND asi.id_estado_caja in (1,2,3,4,7,8) AND asi.estado_asignacion IN (0,1)  " +
                                        "LEFT JOIN tbl_caja AS caj ON caj.id_caja = asi.id_caja AND caj.id_sucursal = usu.id_sucursal " +
                                        "WHERE usu.usuario = @usuario and usu.password_user = @password " +
                                              "AND usu.estado = 1 ORDER BY asi.fecha_creacion DESC";
                command.Parameters.AddWithValue("@usuario", user);
                command.Parameters.AddWithValue("@password", password);
                dtUser.Load(command.ExecuteReader());

                if (dtUser.Rows.Count > 0)
                {
                    foreach (DataRow item in dtUser.Rows)
                    {
                        error[0] = "true";
                        error[1] = item[0].ToString();
                        error[2] = item[1].ToString();
                        error[3] = item[2].ToString();
                        error[4] = item[3].ToString();
                        error[5] = item[4].ToString();
                        error[6] = item[5].ToString();
                        error[7] = item[6].ToString();
                        error[8] = item[7].ToString();
                        error[9] = item[8].ToString();
                        error[10] = item[9].ToString();
                        error[11] = item[10].ToString();
                        error[12] = item[11].ToString();
                        error[13] = item[12].ToString();
                        error[14] = item[13].ToString();
                    }
                }
                else
                {
                    error[0] = "Usuario inactivo.";
                }
                
            }
            catch (Exception ex)
            {
                
                errorExec = ex.ToString();
                if (errorExec.Contains("Login failed for user"))
                {
                    error[0] = "Usuario no existe o contraseña es incorrecta.";
                }
            }
            finally
            {
                if (!errorExec.Contains("Login failed for user"))
                {
                    connect.connection.Close();
                }
            }
            return error;
        }

        public DataTable ObtenerEmpresa(ref string error)
        {
            try
            {
                DataTable datosEmpresa = new DataTable();
                connect.connection.Open();
                command.Connection = connect.connection;
                command.CommandText = "select id_empresa, empresa from tbl_empresa where estado = 1";
                datosEmpresa.Load(command.ExecuteReader());
                return datosEmpresa;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
            finally
            {
                connect.connection.Close();
            }
        }

        public DataTable ObtenerRoles(ref string error)
        {
            try
            {
                DataTable datosRol = new DataTable();
                connect.connection.Open();
                command.Connection = connect.connection;
                command.CommandText = "select id_rol, descripcion as rol from tbl_rol where estado = 1";
                datosRol.Load(command.ExecuteReader());
                return datosRol;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
            finally
            {
                connect.connection.Close();
            }
        }

        public DataSet ObtenerUsuarios(ref string error, int id_sucursal, int id_usuario = -1)
        {

            DataSet ds = new DataSet();

            try
            {

                connect.connection.Open();
                command.Connection = connect.connection;

                SqlDataAdapter adapter;
                SqlParameter param;

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_consultar_usuario";

                param = new SqlParameter("@id_sucursal", (id_sucursal == 0 ? DBNull.Value : (object) id_sucursal));
                param.Direction = ParameterDirection.Input;                
                command.Parameters.Add(param);

                param = new SqlParameter("@id_usuario", (id_usuario == -1 ? DBNull.Value : (object)id_usuario));
                param.Direction = ParameterDirection.Input;                
                command.Parameters.Add(param);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                               
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                
            }
            finally
            {
                connect.connection.Close();
            }

            return ds;
        }

        public bool crearActualizarUsuario(ref string error, int id_usuario, int id_empresa,  int id_sucursal, string usuario, string password_user, string primer_nombre, string segundo_nombre, string primer_apellido, string segundo_apellido, int id_rol, int estado, int caja_asignada, bool cambio_estado)
        {

            try
            {
                connect.connection.Open();
                command.Connection = connect.connection;
                command.Transaction = connect.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "SP_crear_actualizar_usuario";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_usuario", (id_usuario == -1 ? DBNull.Value : (object)id_usuario));
                command.Parameters.AddWithValue("@id_empresa", id_empresa);
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@password_user", password_user);
                command.Parameters.AddWithValue("@primer_nombre", primer_nombre);
                command.Parameters.AddWithValue("@segundo_nombre", segundo_nombre);
                command.Parameters.AddWithValue("@primer_apellido", primer_apellido);
                command.Parameters.AddWithValue("@segundo_apellido", segundo_apellido);
                command.Parameters.AddWithValue("@id_rol", id_rol);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@caja_asignada", caja_asignada);
                command.Parameters.AddWithValue("@cambio_estado", cambio_estado);


                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo "+ (id_usuario==-1? "crear" : "actualizar") + " el Usuario, por favor, valide los datos o comuniquese con el administrador del sistema.");
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
                connect.connection.Close();
            }          
                                   
        }

        public bool actualizarContraseña(ref string error, int id_usuario, int id_empresa, int id_sucursal, string password_user)
        {

            try
            {
                connect.connection.Open();
                command.Connection = connect.connection;
                command.Transaction = connect.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "SP_actualizar_password";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_usuario", id_usuario);
                command.Parameters.AddWithValue("@id_empresa", id_empresa);
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);                
                command.Parameters.AddWithValue("@password_user", password_user);
               


                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo actualizar la contraseña del Usuario, por favor, valide los datos o comuniquese con el administrador del sistema.");
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
                connect.connection.Close();
            }



        }


    }
}