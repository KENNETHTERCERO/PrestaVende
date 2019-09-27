using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_transaccion
    {
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public cs_transaccion()
        {
            
        }

        public DataTable ObtenerTransaccion(ref string error, string id_tipo_transaccion)
        {
            DataTable dtReturnFacturas = new DataTable("dtTransaccion");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select id_tipo_transaccion,tipo_transaccion,operacion_matematica,estado,fecha_creacion,fecha_modificacion " +
                                        "from tbl_tipo_transaccion " +
                                        "where id_tipo_transaccion = @id_transaccion and estado = 1";
                command.Parameters.AddWithValue("@id_transaccion", id_tipo_transaccion);
                dtReturnFacturas.Load(command.ExecuteReader());
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
            return dtReturnFacturas;
        }
    }
}