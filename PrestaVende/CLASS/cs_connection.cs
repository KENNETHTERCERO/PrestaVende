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
        //private static string connectionString = @"Data Source=DESKTOP-VEQ9H2G\ITECSA;Initial Catalog=PRESTAVENDEDB;Persist Security Info=True;User ID=usuario;Password=contra"; //Conecction Server SQL
        //private SqlConnection connectionDB = new SqlConnection();

        //public SqlConnection connection(string user, string password)
        //{
        //    connect(user, password);
        //    return connectionDB;
        //}

        //private void connect(string user, string password)
        //{
            
        //    string conexionString;
        //    conexionString = connectionString.Replace("usuario", user).ToString().Replace("contra", password).ToString();

        //    connectionDB = new SqlConnection(conexionString);
        //}

        //private string connectionString = @"Data Source=DESKTOP-GUBN2LO\SQLPALKI;Initial Catalog=DEVCACSUSDB;Persist Security Info=True;User ID=sa;Password=tercero#3";//Connection Kenneth Tercero
        private static string connectionString = @"Data Source=CINDYGAITAN;Initial Catalog=PRESTAVENDEDB;Persist Security Info=True;User ID=usuario;Password=contra"; //Conecction Server SQL
        public SqlConnection connection = new SqlConnection();

        private void connect()
        {
            connection = new SqlConnection(connectionString);
        }

        public cs_connection(string user, string password)
        {
            string conexionString;
            conexionString = connectionString.Replace("usuario", user).ToString().Replace("contra", password).ToString();
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