using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_interes
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public cs_interes()
        {

        }

        public DataTable getInteres(ref string error)
        {
            try
            {
                DataTable dtInteres = new DataTable("intereses");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_interes, 'SELECCIONAR' AS interes UNION " + 
                                      "SELECT id_interes, interes FROM tbl_interes WHERE estado = 1";
                dtInteres.Load(command.ExecuteReader());
                return dtInteres;
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

        public string getIdInteres(ref string error, string monto, string id_empresa)
        {
            try
            {
                string id_interes = "";
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "select top 1 ISNULL(id_plan_prestamo, 0) From tbl_plan_prestamo " +
                                      "WHERE @monto between minimo AND maximo " +
                                      "AND id_empresa = @id_empresa";
                command.Parameters.AddWithValue("@monto", monto);
                command.Parameters.AddWithValue("@id_empresa", id_empresa);
                id_interes = command.ExecuteScalar().ToString();
                return id_interes;
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

        public DataSet buscarPrestamo(Decimal numero_prestamo, int id_sucursal)
        {
            DataSet ds = new DataSet();

            connection.connection.Open();
            command.Connection = connection.connection;
            command.Parameters.Clear();

            SqlDataAdapter adapter;

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_ConsultarDatosPrestamo";
            command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);
            if (id_sucursal == -1)
            {
                command.Parameters.AddWithValue("@id_sucursal", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
            }

            adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);


            return ds;
        }

        public bool actualizarInteresPrestamo(ref string error, Int64 numero_prestamo, int id_sucursal, int id_interes)
        {

            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "SP_ActualizarInteresPrestamo";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@numero_prestamo", numero_prestamo);

                if (id_sucursal == -1)
                {
                    command.Parameters.AddWithValue("@id_sucursal", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                }

                command.Parameters.AddWithValue("@id_interes", id_interes);



                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    command.Transaction.Rollback();
                    throw new SystemException("No se pudo actualizar el interés del préstamo, por favor valide los datos o comuniquese con el administrador del sistema.");
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