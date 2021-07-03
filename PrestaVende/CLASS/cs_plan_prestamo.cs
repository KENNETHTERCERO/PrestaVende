using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_plan_prestamo
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public cs_plan_prestamo()
        {
        }

        public DataTable getPlanPrestamo(ref string error, string id_empresa)
        {
            try
            {
                DataTable dtPlanPrestamo = new DataTable("planPrestamo");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_plan_prestamo, 'SELECCIONAR' AS plan_prestamo UNION " +
                                      "SELECT id_plan_prestamo, plan_prestamo FROM tbl_plan_prestamo WHERE estado_plan = 1 AND id_empresa = @id_empresa";
                command.Parameters.AddWithValue("@id_empresa", id_empresa);
                dtPlanPrestamo.Load(command.ExecuteReader());
                return dtPlanPrestamo;
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