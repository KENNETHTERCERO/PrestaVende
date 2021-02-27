using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_cambio_precio
    {

        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public DataSet buscarPrestamo(Decimal numero_prestamo, int id_sucursal)
        {
            DataSet ds = new DataSet();

            connection.connection.Open();
            command.Connection = connection.connection;
            command.Parameters.Clear();

            SqlDataAdapter adapter;

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_ConsultarInventarioPorPrestamo";
          
            command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
            command.Parameters.AddWithValue("@id_sucursal", id_sucursal);          
            
         

            adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);


            return ds;
        }

        public bool grabarPrecioInventario(int id_prestamo_detalle, decimal precio_nuevo, int id_usuario , ref string error)
        {

            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "SP_GrabarPrecioInventario";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_prestamo_detalle", id_prestamo_detalle);
                command.Parameters.AddWithValue("@precio_nuevo", precio_nuevo);
                command.Parameters.AddWithValue("@id_usuario", id_usuario);
                


                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    command.Transaction.Rollback();
                    throw new SystemException("No se pudo insertar el producto a inventario, por favor, valide los datos o comuniquese con el administrador del sistema.");
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                command.Transaction.Rollback();
                return false;
            }
            finally
            {
                connection.connection.Close();
            }
        }
        
    }
}