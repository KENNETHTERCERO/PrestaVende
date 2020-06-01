using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_traslado
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();


        public bool grabarTrasladoEncabezado(int id_sucursal_origen, int id_sucursal_destino, int id_serie, int numero_boleta, ref string error, ref int id_traslado_encabezado)
        {
            DataTable dt = new DataTable();
            bool respuesta = false;
            id_traslado_encabezado = -1;

            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "SP_GrabarDatosTrasladoEncabezado";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_sucursal_origen", id_sucursal_origen);
                command.Parameters.AddWithValue("@id_sucursal_destino", id_sucursal_destino);
                command.Parameters.AddWithValue("@id_serie", id_serie);
                command.Parameters.AddWithValue("@numero_boleta", numero_boleta);

                dt.Load(command.ExecuteReader());

                if (dt.Rows.Count > 0)
                {
                    id_traslado_encabezado = int.Parse(dt.Rows[0]["id_traslado_encabezado"].ToString());

                    if (id_traslado_encabezado > 0)
                    {
                        respuesta = true;
                    }
                    else
                    {
                        respuesta = false;
                    }
                    
                }
                else
                {
                    throw new SystemException("No se pudo insertar el traslado, por favor, valide los datos o comuniquese con el administrador del sistema.");
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                respuesta = false;               
            }
            

            return respuesta;
        }

        public bool grabarTrasladoDetalle(int id_traslado_encabezado, int id_inventario, string observaciones, ref string error)
        {

            try
            {
                
                command.Parameters.Clear();
                command.CommandText = "SP_GrabarDatosTrasladoDetalle";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_traslado_encabezado", id_traslado_encabezado);
                command.Parameters.AddWithValue("@id_inventario", id_inventario);
                command.Parameters.AddWithValue("@observaciones", observaciones);
                


                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo insertar el producto al traslado, por favor, valide los datos o comuniquese con el administrador del sistema.");
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();               
                return false;
            }
            
        }

        public DataTable ObtenerDatosTrasladoPorBoleta(int id_sucursal, int id_serie, int numero_boleta)
        {
            DataTable dtArticulos = new DataTable("dtArticulos");

            connection.connection.Open();
            command.Connection = connection.connection;
            command.Parameters.Clear();

           
            SqlParameter param;

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_ObtenerDatosTrasladoPorBoleta";

            param = new SqlParameter("@id_sucursal", id_sucursal);
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.Int16;
            command.Parameters.Add(param);

            param = new SqlParameter("@id_serie", id_serie);
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.Int16;
            command.Parameters.Add(param);

            param = new SqlParameter("@numero_boleta", numero_boleta);
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.Int16;
            command.Parameters.Add(param);

            dtArticulos.Load(command.ExecuteReader());
            return dtArticulos;
        }

        public DataSet ObtenerTrasladosPendientes(ref string error, int id_sucursal, int id_serie)
        {
            DataSet ds = new DataSet();

            connection.connection.Open();
            command.Connection = connection.connection;
            command.Parameters.Clear();

            SqlDataAdapter adapter;
            SqlParameter param;

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_ObtenerTrasladosPendientes";

            param = new SqlParameter("@id_sucursal", id_sucursal);
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.Int16;
            command.Parameters.Add(param);

            param = new SqlParameter("@id_serie", id_serie);
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.Int16;
            command.Parameters.Add(param);

       
            adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);


            return ds;
        }

        public bool RecibirDatosTrasladoPorBoleta(ref string error, int id_serie, int numero_boleta, int id_inventario, decimal precio_producto)
        {

            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "SP_RecibirDatosTrasladoPorBoleta";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_serie", id_serie);
                command.Parameters.AddWithValue("@numero_boleta", numero_boleta);
                command.Parameters.AddWithValue("@id_inventario", id_inventario);
                command.Parameters.AddWithValue("@precio_producto", precio_producto);



                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo insertar el producto al traslado, por favor, valide los datos o comuniquese con el administrador del sistema.");
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

        public bool update_correlativo_serie(ref string error, string id_serie, string numero_factura, string id_sucursal)
        {
            try
            {
                int update = 0;                
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE tbl_serie SET correlativo = @numero_factura_update, " +
                                      "estado = CASE WHEN @numero_factura_update >= numero_de_facturas THEN  0 ELSE estado END " +
                                      "WHERE id_sucursal = @id_sucursal_update AND id_serie = @id_serie_update";
                command.Parameters.AddWithValue("@id_serie_update", id_serie);
                command.Parameters.AddWithValue("@numero_factura_update", numero_factura);
                command.Parameters.AddWithValue("@id_sucursal_update", id_sucursal);

                update = command.ExecuteNonQuery();

                if (update > 0)
                {                    
                    return true;
                }
                else
                {                 
                    return false;
                }
            }
            catch (Exception ex)
            {                
                error = ex.ToString();
                return false;
            }
        }

        internal bool grabarTraslado(int id_sucursal_origen, int id_sucursal_destino, int id_serie, int numero_boleta, DataTable dtArticulos, ref string str_id_traslado_encabezado, ref string error)
        {
            int id_traslado_encabezado = 0;
            bool respuesta = false;

            try
            {

                respuesta = grabarTrasladoEncabezado(id_sucursal_origen, id_sucursal_destino, id_serie, numero_boleta, ref error, ref id_traslado_encabezado);

                if ((respuesta) && (id_traslado_encabezado > 0))
                {

                    foreach (DataRow item in dtArticulos.Rows)
                    {

                        respuesta = grabarTrasladoDetalle(id_traslado_encabezado, int.Parse(item["id_inventario"].ToString()), item["observaciones"].ToString(), ref error);

                        if (respuesta)
                        {
                            if (!update_correlativo_serie(ref error, id_serie.ToString(), numero_boleta.ToString(), id_sucursal_origen.ToString()))
                            {
                                throw new Exception("No se pudo actualizar el correlativo de la boleta. " + error);
                            }
                            else
                            {
                                str_id_traslado_encabezado = id_traslado_encabezado.ToString();
                                respuesta = true;                                
                            }
                        }
                        else
                        {
                            respuesta = false;
                            error = ("Error al grabar el detalle del traslado: " + error);                            
                        }
                    }
                }
                else
                {
                    respuesta = false;                    
                    error = ("Error al grabar el traslado: " + error);
                }
            }catch(Exception ex)
            {
                respuesta = false;
                error = ("Error al grabar el traslado: " + ex.ToString());
                
            }
            finally
            {
                if (respuesta)
                {
                    command.Transaction.Commit();
                }
                else
                {
                    command.Transaction.Rollback();
                }

                command.Connection.Close();
            }

            return respuesta;
        }

        internal DataTable ObtenerDatosBoletaTraslado(ref string error, string id_traslado_encabezado)
        {
            DataTable dtInventario = new DataTable("dtBoleta");

            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec SP_ObtenerDatosBoletaTraslado @id_traslado_encabezado";
                command.Parameters.AddWithValue("@id_traslado_encabezado", id_traslado_encabezado);
                dtInventario.Load(command.ExecuteReader());
                return dtInventario;
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