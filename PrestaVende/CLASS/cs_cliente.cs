using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_cliente
    {
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public cs_cliente()
        {

        }

        public DataTable findClient(ref string error, string condicion)
        {
            DataTable dtReturnClients = new DataTable("dtReturnClients");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT TOP (20) id_cliente " +
                                                  ",DPI " +
                                                  ",nit " +
                                                  ",primer_nombre + ' ' + segundo_nombre + ' ' + primer_apellido + ' ' + segundo_apellido AS nombre_completo " +
                                                  ", direccion " +
                                                  ",(CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END) AS estadoLetras " +
                                                  ", estado " +
                                            "FROM tbl_cliente " +
                                            "WHERE " + condicion;
                dtReturnClients.Load(command.ExecuteReader());
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
            return dtReturnClients;
        }

        public DataTable getSpecificClient(ref string error, string id_cliente)
        {
            DataTable dtReturnClient = new DataTable("dtReturnClients");
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT * " +
                                            "FROM tbl_cliente " +
                                            "WHERE id_cliente = @id_cliente";
                command.Parameters.AddWithValue("@id_cliente", id_cliente);
                dtReturnClient.Load(command.ExecuteReader());
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
            return dtReturnClient;
        }

        public DataTable getStateClient(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("estados");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 as id, 'INACTIVO' AS descripcion UNION SELECT 1 as id, 'ACTIVO' AS descripcion";
                returnTable.Load(command.ExecuteReader());

                return returnTable;
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

        public int insertClient(ref string error, string[] datos)
        {
            try
            {
                int returnInt = 0;
                DataTable returnTable = new DataTable("estados");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.Transaction =  connection.connection.BeginTransaction();
                command.CommandText = "INSERT INTO tbl_cliente (DPI, nit, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, direccion, correo_electronico, numero_telefono, estado) " +
                                                        "VALUES(@DPI, @nit, @primer_nombre, @segundo_nombre, @primer_apellido, @segundo_apellido, @direccion, @correo_electronico, @numero_telefono, @estado)";
                command.Parameters.AddWithValue("@DPI", datos[0]);
                command.Parameters.AddWithValue("@nit", datos[1]);
                command.Parameters.AddWithValue("@primer_nombre", datos[2]);
                command.Parameters.AddWithValue("@segundo_nombre", datos[3]);
                command.Parameters.AddWithValue("@primer_apellido", datos[4]);
                command.Parameters.AddWithValue("@segundo_apellido", datos[5]);
                command.Parameters.AddWithValue("@direccion", datos[6]);
                command.Parameters.AddWithValue("@correo_electronico", datos[7]);
                command.Parameters.AddWithValue("@numero_telefono", datos[8]);
                command.Parameters.AddWithValue("@estado", datos[9]);
                returnInt = command.ExecuteNonQuery();
                
                if (returnInt > 0)
                {
                    command.Transaction.Commit();
                }
                else
                {
                    command.Transaction.Rollback();
                    returnInt = 0;
                }
                return returnInt;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return 0;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public string getMaxIDClient(ref string error)
        {
            try
            {
                string returnIDClientMax;

                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT MAX(id_cliente) + 1 FROM tbl_cliente";
                returnIDClientMax = command.ExecuteScalar().ToString();

                return returnIDClientMax;
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