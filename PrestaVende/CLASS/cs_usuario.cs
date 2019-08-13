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
        public static int       id_usuario = 0;
        public static int       id_empresa = 0;
        public static int       id_sucursal = 0;
        public static int       id_rol = 0;
        public static string    usuario = "";
        public static string    primer_nombre = "";
        public static string    primer_apellido = "";

        private cs_connection connect;
        private SqlCommand command = new SqlCommand();

        public cs_usuario()
        {

        }

        public string[] Login(string user, string password)
        {
            string errorExec = "";
            string[] error;
            error = new string[9];

            try
            {
                connect = new cs_connection(user, password);
                DataTable dtUser = new DataTable("user");

                connect.connection.Open();

                command.Connection = connect.connection;
                command.CommandText = "SELECT " +
                                            "id_usuario, " +        //0, 1
	                                        "id_empresa, " +        //1, 2
	                                        "id_sucursal, " +       //2, 3
	                                        "usuario, " +           //3, 4
	                                        "primer_nombre," +      //4, 5
	                                        "primer_apellido, " +   //5, 6
	                                        "id_rol, " +            //6, 7
	                                        "estado " +             //7, 8
                                        "FROM " +
                                        "tbl_usuario " + 
                                        "WHERE usuario = @usuario and password_user = @password "+
                                              "AND estado = 1";
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
    }
}