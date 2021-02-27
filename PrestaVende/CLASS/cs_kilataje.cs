using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_kilataje
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public cs_kilataje()
        {

        }

        public DataTable getKilataje(ref string error)
        {
            try
            {
                DataTable dtKilataje = new DataTable("kilataje");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_kilataje, 'SELECCIONAR' AS kilataje UNION " + 
                                      "SELECT id_kilataje, kilataje FROM tbl_kilataje WHERE estado = 1";
                dtKilataje.Load(command.ExecuteReader());
                return dtKilataje;
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

        public DataTable getKilatajeByID(ref string error, string id_kilataje)
        {
            try
            {
                DataTable dtKilataje = new DataTable("tblkilataje");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT * FROM tbl_kilataje WHERE estado = 1 AND id_kilataje = @id_kilajate";
                command.Parameters.AddWithValue("@id_kilajate", id_kilataje);
                dtKilataje.Load(command.ExecuteReader());
                return dtKilataje;
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

        public DataTable getKilatajeCambioPrecio(ref string error)
        {
            try
            {
                DataTable dtKilataje = new DataTable("tblkilatajePrecio");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT id_kilataje, kilataje, precio_kilataje FROM tbl_kilataje WHERE estado = 1";
                dtKilataje.Load(command.ExecuteReader());
                return dtKilataje;
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

        public int updatePrecioKilataje(ref string error, string id_kilataje, decimal precio_nuevo)
        {
            try
            {
                int numberReturn = 0;
                command = new SqlCommand();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_kilataje SET precio_kilataje = @precio_nuevo, fecha_modificacion = GETDATE() WHERE id_kilataje = @id_kilataje";
                command.Parameters.AddWithValue("@id_kilataje", id_kilataje);
                command.Parameters.AddWithValue("@precio_nuevo", precio_nuevo);
                numberReturn = command.ExecuteNonQuery();

                return numberReturn;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return 999999;
            }
            finally
            {
                connection.connection.Close();
            }
        }
    }
}