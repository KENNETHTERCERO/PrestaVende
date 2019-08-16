using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_menu
    {
        private cs_connection connect = new cs_connection();
        private SqlCommand command = new SqlCommand();


        public DataTable getMenuHeader(ref string error)
        {
            DataTable dtMenuHeader = new DataTable("menuHeader");
            try
            {
                connect.connection.Open();
                command.Connection = connect.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT " +
                                            "men.id_menu, " +
	                                        "men.opcion_menu " +
                                        "FROM tbl_menu_principal AS men " +
                                        "INNER JOIN tbl_menu_rol AS mro ON mro.id_menu_principal = men.id_menu " +
                                        "WHERE men.es_nodo = 0 AND mro.id_rol = @id_rol";
                command.Parameters.AddWithValue("@id_rol", CLASS.cs_usuario.id_rol);
                dtMenuHeader.Load(command.ExecuteReader());
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
            finally
            {
                connect.connection.Close();
            }
            return dtMenuHeader;
        }

        public DataTable getMenuFirstLevel(ref string error, string grupo)
        {
            DataTable dtMenuHeader = new DataTable("menuHeader");
            try
            {
                connect.connection.Open();
                command.Connection = connect.connection;
                command.Parameters.Clear();
                command.CommandText = "SELECT " +
                                            "men.id_menu, " +
                                            "men.opcion_menu, " +
                                            "men.link_pagina " +
                                        "FROM tbl_menu_principal AS men " +
                                        "INNER JOIN tbl_menu_rol AS mro ON mro.id_menu_principal = men.id_menu " +
                                        "WHERE men.es_nodo = 0 AND mro.id_rol = @id_rol " + 
                                            "AND men.grupos = @grupo " +
                                            "AND men.es_nodo = 1";
                command.Parameters.AddWithValue("@id_rol", CLASS.cs_usuario.id_rol);
                command.Parameters.AddWithValue("@grupo", grupo);
                dtMenuHeader.Load(command.ExecuteReader());
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
            finally
            {
                connect.connection.Close();
            }
            return dtMenuHeader;
        }
    }
}