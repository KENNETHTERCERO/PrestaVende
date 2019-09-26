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
        public static int       id_usuario      = 0;
        public static int       id_empresa      = 0;
        public static int       id_sucursal     = 0;
        public static int       id_rol          = 0;
        public static int       id_caja         = 0;
        public static int       id_tipo_caja    = 0;
        public static int       puede_vender    = 0;
        private static decimal  saldo_caja      = 0;
        public static string    usuario         = "";
        public static string    primer_nombre   = "";
        public static string    primer_apellido = "";
        
        private cs_connection connect;
        private SqlCommand command = new SqlCommand();

        public static decimal Saldo_caja
        {
            get
            {
                return saldo_caja;
            }

            set
            {
                saldo_caja = value;
            }
        }

        public cs_usuario()
        {

        }

        public string[] Login(string user, string password)
        {
            string errorExec = "";
            string[] error;
            error = new string[14];

            try
            {
                connect = new cs_connection(user, password);
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
                                            "ISNULL(asi.id_caja, 0), " +           //8, 9
                                            "ISNULL(asi.id_asignacion_caja, 0), " +  //9, 10
                                            "ISNULL(asi.estado_asignacion, 0), " + //10, 11
                                            "usu.caja_asignada, " +     //11, 12
                                            "ISNULL(caj.id_tipo_caja, 0) " +       //12, 13
                                        "FROM " +
                                        "tbl_usuario AS usu " +
                                        "LEFT JOIN tbl_asignacion_caja AS asi ON asi.id_usuario_asignado = usu.id_usuario AND asi.id_estado_caja in (2,3) AND asi.estado_asignacion IN (0,1) " +
                                        "LEFT JOIN tbl_caja AS caj ON caj.id_caja = asi.id_caja " +
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