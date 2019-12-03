using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_recepcion_liquidacion
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();


        public DataSet obtenerLiquidaciones(int numero_prestamo)
        {
            DataSet ds = new DataSet();

            connection.connection.Open();
            command.Connection = connection.connection;
            command.Parameters.Clear();

            SqlDataAdapter adapter;
            SqlParameter param;

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_ConsultarLiquidacionPorPrestamo";

            param = new SqlParameter("@numero_prestamo", numero_prestamo);
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);


            return ds;
        }

        public bool grabarDatosInventario(int id_prestamo_detalle, int estado_inventario, int cantidad, int precio_producto, ref string error)
        {
            
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "SP_GrabarDatosInventario";
                command.CommandType = CommandType.StoredProcedure;                
                command.Parameters.AddWithValue("@id_prestamo_detalle", id_prestamo_detalle);
                command.Parameters.AddWithValue("@estado_inventario", estado_inventario);
                command.Parameters.AddWithValue("@cantidad", cantidad);
                command.Parameters.AddWithValue("@precio_producto", precio_producto);
                                    

                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
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