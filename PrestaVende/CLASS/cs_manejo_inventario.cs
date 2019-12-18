using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_manejo_inventario
    {
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public DataTable getArticulos(ref string error, string numero_prestamo)
        {
            try
            {
                DataTable dtArticulos = new DataTable("dtArticulos");

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec sp_obtiene_producto_venta @id_sucursal, @numero_prestamo";
                command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
                dtArticulos.Load(command.ExecuteReader());
                return dtArticulos;
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
        }

        public DataTable getArticuloEspecifico(ref string error, string numero_prestamo, string id_inventario)
        {
            try
            {
                DataTable dtArticulos = new DataTable("dtArticulos");

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec sp_obtiene_producto_venta_detalle @id_sucursal, @numero_prestamo, @id_inventario";
                command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
                command.Parameters.AddWithValue("@id_sucursal", cs_usuario.id_sucursal);
                command.Parameters.AddWithValue("@id_inventario", id_inventario);
                dtArticulos.Load(command.ExecuteReader());
                return dtArticulos;
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
        }
    }
}