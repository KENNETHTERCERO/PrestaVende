using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaSolucion.CLASES
{
    class cs_connection_ferremas
    {
        #if DEBUG
                //conexion a PC Kenneth
                private string connectionString = @"Data Source=DESKTOP-VEQ9H2G\ITECSA;Initial Catalog=FERREMAS;Persist Security Info=True;User ID=sa;Password=tercero#3";

                //conexion a PC Antonio
                //private string connectionString = @"Data Source=MNG\SQLEXPRESS;Initial Catalog=FERREMAS;Persist Security Info=True;User ID=sa;Password=sa1";
        #else
                    //conexion a PC cliente
                private string connectionString = @"Data Source=192.168.1.150;Initial Catalog=FERREMAS;Persist Security Info=True;User ID=sa;Password=Temporal100";
        #endif



        public SqlConnection connection = new SqlConnection();

        private void connect()
        {
            connection = new SqlConnection(connectionString);
        }

        public cs_connection_ferremas()
        {
            connect();
        }

        public SqlConnection getCon()
        {
            return connection;
        }
    }
}
