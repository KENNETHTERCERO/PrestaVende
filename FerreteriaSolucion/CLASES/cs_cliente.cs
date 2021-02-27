using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaSolucion.CLASES
{
    class cs_cliente
    {
        private cs_connection_ferremas connection = new cs_connection_ferremas();
        private SqlCommand command = new SqlCommand();
        public cs_cliente()
        {
        }

        public DataTable getTypeCustomer(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("data");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_tipo_cliente, 'SELECCIONAR' AS descripcion UNION SELECT id_tipo_cliente, descripcion FROM tbl_tipo_cliente WHERE estado = 1";
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

        public DataTable getDataCliente(ref string error, string nit)
        {
            try
            {
                DataTable returnTable = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;

                command.CommandText = $"SELECT nit FROM tbl_cliente WHERE nit = '{nit}'";
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

        public string getMaxIdCliente(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_cliente), 0) + 1 AS id_cliente FROM tbl_cliente";
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

        public DataTable findClient(ref string error, string condition, ref DataTable paraComboBox)
        {

            try
            {
                DataTable OneCustomer = new DataTable();
                paraComboBox = new DataTable("paraEnvioCombo");
                string condicion = "";
                connection.connection.Open();
                command.Connection = connection.connection;
                condicion = string.Format("SELECT " +
                                            "prov.id_cliente, " +
                                            "prov.nit, " +
                                            "prov.primer_nombre + ' ' + prov.segundo_nombre + ' ' + prov.primer_apellido + ' ' + prov.segundo_apellido AS nombre, " +
                                            "prov.direccion "+
                                        "FROM " +
                                        "tbl_cliente as prov  " +
                                        "WHERE ({0}) ", condition);
                command.CommandText = condicion;
                OneCustomer.Load(command.ExecuteReader());
                if (OneCustomer.Rows.Count > 1)
                {
                    condicion = string.Format("SELECT 0 AS id_cliente, 'SELECCIONAR' AS nombre UNION " +
                                                "SELECT " +
                                                    "prov.id_cliente, " +
                                                    "prov.nit + ' ' + prov.primer_nombre + ' ' + prov.segundo_nombre + ' ' + prov.primer_apellido + ' ' + prov.segundo_apellido + ' ' + prov.direccion AS nombre " +
                                                "FROM " +
                                                "tbl_cliente as prov  " +
                                                "WHERE ({0}) ", condition);
                    command.CommandText = condicion;
                    paraComboBox.Load(command.ExecuteReader());
                }
                else if (OneCustomer.Rows.Count == 1)
                {
                    command.CommandText = "SELECT 0 AS id_cliente, 'SELECCIONAR' AS nombre";
                    paraComboBox.Load(command.ExecuteReader());
                }
                return OneCustomer;
            }
            catch (SqlException ex)
            {
                error = ex.ToString();
                return null;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public DataTable findClientForID(ref string error, string id_cliente)
        {
            try
            {
                if (!id_cliente.Equals("System.Data.DataRowView"))
                {
                    DataTable returnTable = new DataTable();
                    connection.connection.Open();
                    command.Connection = connection.connection;
                    command.CommandText = "SELECT TOP 15 " +
                                                "id_cliente, " +
                                                "nit, " +
                                                "primer_nombre + ' ' + segundo_nombre + ' ' + primer_apellido + ' ' + segundo_apellido AS nombre, " +
                                                "direccion " +
                                            "FROM " +
                                            "tbl_cliente " +
                                            $"WHERE id_cliente = {id_cliente}";
                    returnTable.Load(command.ExecuteReader());

                    return returnTable;
                }
                else
                {
                    return null;
                }
                
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

        public DataTable getOneClientForID(ref string error, string id_cliente)
        {

            try
            {
                DataTable OneCustomer = new DataTable();

                string condicion = "";
                connection.connection.Open();
                command.Connection = connection.connection;
                condicion = string.Format("SELECT TOP 1 " +
                                            "id_cliente, " +
                                            "primer_nombre, " +
                                            "segundo_nombre, "+ 
                                            "primer_apellido, " + 
                                            "segundo_apellido, " +
                                            "direccion, " +
                                            "id_tipo_cliente, " +
                                            "nit " +
                                        "FROM " +
                                        "tbl_cliente " +
                                        "WHERE id_cliente = {0}", id_cliente);
                command.CommandText = condicion;
                OneCustomer.Load(command.ExecuteReader());
                return OneCustomer;
            }
            catch (SqlException ex)
            {
                error = ex.ToString();
                return null;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public bool insertCliente(ref string error, string primer_nombre, string segundo_nombre, string primer_apellido, string segundo_apellido, string direccion, string nit, string id_tipo_cliente)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "INSERT INTO tbl_cliente (primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, direccion, nit, estado, id_tipo_cliente) " +
                                        "VALUES(@primer_nombre, @segundo_nombre, @primer_apellido, @segundo_apellido, @direccion, @nit, 1, @id_tipo_cliente)";
                command.Parameters.AddWithValue("@primer_nombre", primer_nombre);
                command.Parameters.AddWithValue("@segundo_nombre", segundo_nombre);
                command.Parameters.AddWithValue("@primer_apellido", primer_apellido);
                command.Parameters.AddWithValue("@segundo_apellido", segundo_apellido);
                command.Parameters.AddWithValue("@direccion", direccion);
                command.Parameters.AddWithValue("@nit", nit);
                command.Parameters.AddWithValue("@id_tipo_cliente", id_tipo_cliente);
                if (command.ExecuteNonQuery() > 0)
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
                return false;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public bool updateCliente(ref string error, string primer_nombre, string segundo_nombre, string primer_apellido, string segundo_apellido, string direccion, string id_cliente, string id_tipo_cliente)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_cliente " +
                                            "SET " + 
                                                "primer_nombre = @primer_nombre, " +
                                                "segundo_nombre = @segundo_nombre, " +
                                                "primer_apellido = @primer_apellido, " +
                                                "segundo_apellido = @segundo_apellido, " +
                                                "direccion = @direccion, " +
                                                "id_tipo_cliente = @id_tipo_cliente " +
                                                "WHERE id_cliente = @id_cliente";
                command.Parameters.AddWithValue("@primer_nombre", primer_nombre);
                command.Parameters.AddWithValue("@segundo_nombre", segundo_nombre);
                command.Parameters.AddWithValue("@primer_apellido", primer_apellido);
                command.Parameters.AddWithValue("@segundo_apellido", segundo_apellido);
                command.Parameters.AddWithValue("@direccion", direccion);
                command.Parameters.AddWithValue("@id_cliente", id_cliente);
                command.Parameters.AddWithValue("@id_tipo_cliente", id_tipo_cliente);
                if (command.ExecuteNonQuery() > 0)
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
                return false;
            }
            finally
            {
                connection.connection.Close();
            }
        }
    }
}
