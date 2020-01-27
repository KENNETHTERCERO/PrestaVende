using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_recibo
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public cs_recibo()
        {
            command = new SqlCommand();
        }

        public string getIDRecibo(ref string error, string id_serie, string numero_documento)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT id_recibo FROM tbl_recibo WHERE numero_recibo = @numero_recibo AND id_serie = @id_serie";
                command.Parameters.AddWithValue("@numero_recibo", numero_documento);
                command.Parameters.AddWithValue("@id_serie", id_serie);
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
    }
}