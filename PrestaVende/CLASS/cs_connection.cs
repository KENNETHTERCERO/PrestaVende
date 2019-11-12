using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_connection
    {
        public static int id_usuario = 0;

        //private string connectionString = @"Data Source=DESKTOP-GUBN2LO\SQLPALKI;Initial Catalog=DEVCACSUSDB;Persist Security Info=True;User ID=sa;Password=tercero#3";//Connection Kenneth Tercero
#if DEBUG
        private static string connectionString = @"Data Source=DESKTOP-VEQ9H2G\ITECSA;Initial Catalog=PRESTAVENDEDB;Persist Security Info=True;User ID=sa;Password=tercero#3"; //Conecction Server SQL
        private static string ConstConnectionString = @"Data Source=DESKTOP-VEQ9H2G\ITECSA;Initial Catalog=PRESTAVENDEDB;Persist Security Info=True;User ID=sa;Password=tercero#3"; //Conecction Server SQL


        //private static string connectionString = @"Data Source=LAPTOP-R6UMVN3B;Initial Catalog=PRESTAVENDEDB;Persist Security Info=True;User ID=sa;Password='FunkoPop,06'";  //Conecction Server SQL
        //private static string ConstConnectionString = @"Data Source=LAPTOP-R6UMVN3B;Initial Catalog=PRESTAVENDEDB;Persist Security Info=True;User ID=sa;Password='FunkoPop,06'"; //Conecction Server SQL

        //private static string connectionString = @"Data Source=sql5047.site4now.net;Initial Catalog=DB_A4F0BE_prestavendedb;User Id=DB_A4F0BE_prestavendedb_admin;Password=tercero#3;"; //Conecction Server SQL
        //private static string ConstConnectionString = @"Data Source=sql5047.site4now.net;Initial Catalog=DB_A4F0BE_prestavendedb;User Id=DB_A4F0BE_prestavendedb_admin;Password=tercero#3;"; //Conecction Server SQL
#else

        private static string connectionString = @"Data Source=sql5047.site4now.net;Initial Catalog=DB_A4F0BE_prestavendedb;User Id=DB_A4F0BE_prestavendedb_admin;Password=tercero#3;"; //Conecction Server SQL
        private static string ConstConnectionString = @"Data Source=sql5047.site4now.net;Initial Catalog=DB_A4F0BE_prestavendedb;User Id=DB_A4F0BE_prestavendedb_admin;Password=tercero#3;"; //Conecction Server SQL
#endif


        //private static string connectionString = @"Data Source=CINDYGAITAN;Initial Catalog=PRESTAVENDEDB;Persist Security Info=True;User ID=sa;Password='Agosto.2019'"; //Conecction Server SQL
        public SqlConnection connection = new SqlConnection();

        private void connect()
        {
            connection = new SqlConnection(connectionString);
        }

        public cs_connection(string user, string password)
        {
            string conexionString;
            connectionString = ConstConnectionString;
            //conexionString = connectionString.Replace("usuario", user).ToString().Replace("contra", password).ToString();
            conexionString = connectionString;
            connectionString = conexionString;
            connect();
        }

        public cs_connection()
        {
            connect();
        }

        public SqlConnection getCon()
        {
            return connection;
        }
    }
}