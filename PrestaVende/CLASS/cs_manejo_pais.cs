using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_manejo_pais
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public DataTable get_pais()
        {
            try
            {
                DataTable returnTable = new DataTable("paises");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_pais, 'SELECCIONAR' AS pais UNION " +
                                        "SELECT id_pais, pais From tbl_pais";
                returnTable.Load(command.ExecuteReader());
                return returnTable;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public DataTable get_Departamento(string id_pais)
        {
            try
            {
                DataTable returnTable = new DataTable("departamentos");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_departamento, 'SELECCIONAR' departamento UNION " +
                                        "SELECT id_departamento, departamento fROM tbl_departamento WHERE id_pais = @id_pais";
                command.Parameters.AddWithValue("@id_pais", id_pais);
                returnTable.Load(command.ExecuteReader());
                return returnTable;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public DataTable get_municipio(string id_departamento)
        {
            try
            {
                DataTable returnTable = new DataTable("departamentos");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_municipio, 'SELECCIONAR' AS municipio UNION " +
                                        "SELECT id_municipio, municipio fROM tbl_municipio WHERE id_departamento = @id_departamento";
                command.Parameters.AddWithValue("@id_departamento", id_departamento);
                returnTable.Load(command.ExecuteReader());
                return returnTable;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                connection.connection.Close();
            }
        }
    }
}