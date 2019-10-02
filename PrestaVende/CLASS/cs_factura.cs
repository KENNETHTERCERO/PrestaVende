using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_factura
    {
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public cs_factura()
        {
            
        }

        public DataTable ObtenerFacturas(ref string error, string id_prestamo)
        {
            DataTable dtReturnFacturas = new DataTable("dtFacturas");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "select isnull(se.serie,'Creacion') serie,isnull(cast(numero_factura as varchar(50)),'Prestamo') numero_factura, " +
                                        "tr.numero_prestamo,cast(fecha_transaccion as date) fecha_transaccion " +
                                        "from tbl_transaccion tr " +
                                        "inner join tbl_prestamo_encabezado pre on pre.numero_prestamo = tr.numero_prestamo " +
                                        "left join tbl_serie se on se.id_serie = tr.id_serie " +
                                        "where pre.id_prestamo_encabezado = @id_prestamo";
                command.Parameters.AddWithValue("@id_prestamo", id_prestamo);
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