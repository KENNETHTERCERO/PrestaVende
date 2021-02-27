using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_casilla
    {
        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public DataTable getCasillas(ref string error, string id_categoria)
        {
            try
            {
                DataTable dtCasilla = new DataTable("dtCasilla");

                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT 0 AS id_casilla, 'SELECCIONAR' AS casilla UNION " +
                                                "SELECT id_casilla, casilla FROM tbl_casilla WHERE estado = 0 AND id_sucursal = @id_sucursal AND id_categoria = @id_categoria";
                command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
                command.Parameters.AddWithValue("@id_categoria", id_categoria);
                dtCasilla.Load(command.ExecuteReader());
                return dtCasilla;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
        }

        public int insertCasillas(ref string error, string id_categoria, string letra, int numero_inicial, int numero_final)
        {
            try
            {
                DataTable contador = new DataTable("casillasCreadas");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec SP_CrearCasillas @id_sucursal, @id_categoria, @letra, @numero_inicial, @numero_final";
                command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
                command.Parameters.AddWithValue("@id_categoria", id_categoria);
                command.Parameters.AddWithValue("@letra", letra);
                command.Parameters.AddWithValue("@numero_inicial", numero_inicial);
                command.Parameters.AddWithValue("@numero_final", numero_final);
                contador.Load(command.ExecuteReader());
                return contador.Rows.Count;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return 0;
            }
        }
    }
}