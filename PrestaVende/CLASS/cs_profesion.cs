using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_profesion
    {
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public DataTable getProfesiones()
        {
            try
            {
                DataTable returnTable = new DataTable("tablaProfesiones");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 id_profesion, 'SELECCIONAR' profesion UNION " +
                                        "select id_profesion, profesion From tbl_profesion WHERE estado = 1";
                returnTable.Load(command.ExecuteReader());
                return returnTable;
            }
            catch (Exception )
            {

                throw;
            }
        }
    }
}