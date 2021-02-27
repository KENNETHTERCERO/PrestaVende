using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaSolucion.CLASES
{
    class cs_serie
    {
        private CLASES.cs_connection_ferremas connection = new cs_connection_ferremas();
        private SqlCommand command = new SqlCommand();

        public cs_serie()
        {
        }

        public DataTable getSeriesToBill(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("data");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_serie, 'SELECCIONAR' AS serie UNION SELECT id_serie, serie FROM tbl_serie WHERE estado = 1 AND contador < numero_final";
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

        public string numberMaxBill(ref string error, ref string facturas_restantes, string id_serie)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = $"SELECT numero_final - contador AS facturas_restantes FROM tbl_serie WHERE id_serie = {id_serie}";
                facturas_restantes = command.ExecuteScalar().ToString();
                command.CommandText = $"SELECT (ISNULL(MAX(numero_factura), (SELECT numero_inicial - 1 FROM tbl_serie WHERE id_serie = {id_serie})) + 1) AS numero FROM tbl_factura_encabezado WHERE id_serie = {id_serie}";
                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return "0";
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public DataTable getAllSeries(ref string error)
        {
            try
            {
                DataTable returnTable = new DataTable("table");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT id_serie AS ID, " + //0
                                            "serie AS [Serie], " + //1
                                            "numero_inicial AS [Numero inicial], " + //2
                                            "numero_final AS [Numero final], " + //3
                                            "numero_facturas AS numero_fac, " + //4
                                            "resolucion, " +  //5
                                            "fecha_resolucion AS [Fecha resolucion], " + //6
                                            "(CASE WHEN estado = 1 THEN 'ACTIVA' WHEN estado = 0 THEN 'INACTIVA' END) AS [Estado], " + //7
                                            "estado AS id_estado " + //8
                                            "FROM tbl_serie";
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

        public string getMaxIdSerie(ref string error)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_serie), 1) AS id_serie FROM tbl_serie";
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

        public DataTable estadosSerie()
        {
            try
            {
                DataTable returnTable = new DataTable("dat");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id, 'INACTIVA' AS descripcion UNION SELECT 1 AS id, 'ACTIVA' AS descripcion";
                returnTable.Load(command.ExecuteReader());
                return returnTable;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public bool insertSerie(ref string error, string serie, string numero_facturas, string numero_inicial, string numero_final, string contador, string resolucion, string fecha_resolucion)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_serie (serie, numero_facturas, numero_inicial, numero_final, contador, resolucion, fecha_resolucion, estado) " +
                                                    "VALUES(@serie, @numero_facturas, @numero_inicial, @numero_final, @contador, @resolucion, @fecha_resolucion, 1)";
                command.Parameters.AddWithValue("@serie", serie);
                command.Parameters.AddWithValue("@numero_facturas", numero_facturas);
                command.Parameters.AddWithValue("@numero_inicial", numero_inicial);
                command.Parameters.AddWithValue("@numero_final", numero_final);
                command.Parameters.AddWithValue("@contador", contador);
                command.Parameters.AddWithValue("@resolucion", resolucion);
                command.Parameters.AddWithValue("@fecha_resolucion", fecha_resolucion);
                int insert = int.Parse(command.ExecuteNonQuery().ToString());

                if (insert > 0)
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
            finally
            {
                connection.connection.Close();
            }
        }

        public bool updateSerie(ref string error, string serie, string numero_facturas, string numero_inicial, string numero_final, string resolucion, string fecha_resolucion, string estado, string id_serie)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_serie SET " +
                                                "serie = @serie, " +
                                                "numero_facturas = @numero_facturas, " +
                                                "numero_inicial = @numero_inicial, " +
                                                "numero_final = @numero_final, " +
                                                "resolucion = @resolucion, " +
                                                "fecha_resolucion = @fecha_resolucion, " +
                                                "estado = @estado " +
                                                "WHERE id_serie = @id_serie";
                command.Parameters.AddWithValue("@serie", serie);
                command.Parameters.AddWithValue("@numero_facturas", numero_facturas);
                command.Parameters.AddWithValue("@numero_inicial", numero_inicial);
                command.Parameters.AddWithValue("@numero_final", numero_final);
                command.Parameters.AddWithValue("@resolucion", resolucion);
                command.Parameters.AddWithValue("@fecha_resolucion", fecha_resolucion);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@id_serie", id_serie);
                int insert = int.Parse(command.ExecuteNonQuery().ToString());

                if (insert > 0)
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
            finally
            {
                connection.connection.Close();
            }
        }
    }
}
