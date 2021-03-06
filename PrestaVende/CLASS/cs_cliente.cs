﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestaVende.CLASS
{
    public class cs_cliente
    {
        private static string id_cliente;
        private static string nombre_cliente;


        cs_connection connection = new cs_connection();
        SqlCommand command = new SqlCommand();

        public static string Id_cliente
        {
            get
            {
                return id_cliente;
            }

            set
            {
                id_cliente = value;
            }
        }

        public static string Nombre_cliente
        {
            get
            {
                return nombre_cliente;
            }

            set
            {
                nombre_cliente = value;
            }
        }

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

                if (validateDPIAndNit(ref error, datos[0], datos[1]) == "0")
                {
                    command.Parameters.Clear();
                    command.Transaction = connection.connection.BeginTransaction();
                    command.CommandText = "INSERT INTO tbl_cliente (DPI, nit, primer_nombre, segundo_nombre, tercer_nombre, primer_apellido, segundo_apellido, apellido_casada, direccion, correo_electronico, " +
                                                                    "numero_telefono, estado, id_pais, id_departamento, id_municipio, id_subcategoria_medio, id_categoria_medio, id_profesion, fecha_creacion, id_pais_nacionalidad, fecha_nacimiento) " +
                                                            "VALUES(@DPI, @nit, @primer_nombre, @segundo_nombre, @tercer_nombre, @primer_apellido, @segundo_apellido, @apellido_casada, @direccion, @correo_electronico, " +
                                                                    "@numero_telefono, @estado, @id_pais, @id_departamento, @id_municipio, @id_subcategoria_medio, @id_categoria_medio, @id_profesion, GETDATE(), @id_pais_nacionalidad, @fecha_nacimiento)";
                    command.Parameters.AddWithValue("@DPI", datos[0]);
                    command.Parameters.AddWithValue("@nit", datos[1]);
                    command.Parameters.AddWithValue("@primer_nombre", datos[2]);
                    command.Parameters.AddWithValue("@segundo_nombre", datos[3]);
                    command.Parameters.AddWithValue("@tercer_nombre", datos[4]);
                    command.Parameters.AddWithValue("@primer_apellido", datos[5]);
                    command.Parameters.AddWithValue("@segundo_apellido", datos[6]);
                    command.Parameters.AddWithValue("@apellido_casada", datos[7]);
                    command.Parameters.AddWithValue("@direccion", datos[8]);
                    command.Parameters.AddWithValue("@correo_electronico", datos[9]);
                    command.Parameters.AddWithValue("@numero_telefono", datos[10]);
                    command.Parameters.AddWithValue("@estado", datos[11]);
                    command.Parameters.AddWithValue("@id_pais", datos[12]);
                    command.Parameters.AddWithValue("@id_departamento", datos[13]);
                    command.Parameters.AddWithValue("@id_municipio", datos[14]);
                    command.Parameters.AddWithValue("@id_subcategoria_medio", datos[15]);
                    command.Parameters.AddWithValue("@id_categoria_medio", datos[16]);
                    command.Parameters.AddWithValue("@id_profesion", datos[17]);
                    command.Parameters.AddWithValue("@id_pais_nacionalidad", datos[18]);
                    command.Parameters.AddWithValue("@fecha_nacimiento", datos[19]);
                    returnInt = command.ExecuteNonQuery();

                    if (returnInt > 0)
                    {

                        command.CommandText = "SELECT MAX(id_cliente) FROM tbl_cliente WHERE DPI = '" + datos[0] + "'";
                        returnInt = Convert.ToInt32(command.ExecuteScalar().ToString());
                        if (returnInt > 0)
                        {
                            command.Transaction.Commit();
                            return returnInt;
                        }
                        else
                        {
                            throw new SystemException("NO SE PUDO GUARDAR CLIENTE.");
                        }
                        
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    error = "El DPI o Nit ya existe en el catalogo de clientes, por favor busquelo.";
                    return 0;
                }
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                error = ex.ToString();
                return 0;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public int updateClient(ref string error, string[] datos)
        {
            try
            {
                int returnInt = 0;
                DataTable returnTable = new DataTable("estados");
                connection.connection.Open();
                command.Connection = connection.connection;

                command.Parameters.Clear();
                command.Transaction = connection.connection.BeginTransaction();
                command.CommandText = "UPDATE tbl_cliente SET " +
                                            "primer_nombre = @primer_nombre, " +
                                            "segundo_nombre = @segundo_nombre, " +
                                            "tercer_nombre = @tercer_nombre, " +
                                            "primer_apellido = @primer_apellido, " +
                                            "segundo_apellido = @segundo_apellido, " +
                                            "apellido_casada = @apellido_casada, " +
                                            "direccion = @direccion, " +
                                            "correo_electronico = @correo_electronico, " +
                                            "numero_telefono = @numero_telefono, " +
                                            "estado = @estado, " +
                                            "id_profesion = @id_profesion, " +
                                            "id_pais = @id_pais, " +
                                            "id_departamento = @id_departamento, " +
                                            "id_municipio = @id_municipio, " +
                                            "id_subcategoria_medio = @id_subcategoria_medio, " +
                                            "id_categoria_medio = @id_categoria_medio, " +
                                            "fecha_modificacion = getdate(), " +
                                            "id_pais_nacionalidad = @id_pais_nacionalidad, " +
                                            "fecha_nacimiento = @fecha_nacimiento " +
                                      "WHERE id_cliente = @id_cliente";

                command.Parameters.AddWithValue("@primer_nombre", datos[0]);
                command.Parameters.AddWithValue("@segundo_nombre", datos[1]);
                command.Parameters.AddWithValue("@primer_apellido", datos[2]);
                command.Parameters.AddWithValue("@segundo_apellido", datos[3]);
                command.Parameters.AddWithValue("@direccion", datos[4]);
                command.Parameters.AddWithValue("@correo_electronico", datos[5]);
                command.Parameters.AddWithValue("@numero_telefono", datos[6]);
                command.Parameters.AddWithValue("@estado", datos[7]);
                command.Parameters.AddWithValue("@id_cliente", datos[8]);
                command.Parameters.AddWithValue("@id_profesion", datos[9]);
                command.Parameters.AddWithValue("@id_pais", datos[10]);
                command.Parameters.AddWithValue("@id_departamento", datos[11]);
                command.Parameters.AddWithValue("@id_municipio", datos[12]);
                command.Parameters.AddWithValue("@id_categoria_medio", datos[13]);
                command.Parameters.AddWithValue("@id_subcategoria_medio", datos[14]);
                command.Parameters.AddWithValue("@tercer_nombre", datos[15]);
                command.Parameters.AddWithValue("@apellido_casada", datos[16]);
                command.Parameters.AddWithValue("@id_pais_nacionalidad", datos[17]);
                command.Parameters.AddWithValue("@fecha_nacimiento", datos[18]);
                returnInt = command.ExecuteNonQuery();

                if (returnInt > 0)
                {
                    command.Transaction.Commit();
                }
                else
                {
                    throw new Exception("No se pudo actualizar el cliente, por favor valide la informacion y vuelva a intentarlo.");
                }
                return returnInt;
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                error = ex.ToString();
                return 0;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public string validateDPIAndNit(ref string error, string DPI, string nit)
        {
            try
            {
                string returnIDClientMax;

                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_cliente), 0) " +
                                        "FROM tbl_cliente " +
                                        "WHERE DPI = @validaDPI ";
                command.Parameters.AddWithValue("@validaDPI", DPI);
                command.Parameters.AddWithValue("@validaNit", nit);
                returnIDClientMax = command.ExecuteScalar().ToString();

                return returnIDClientMax;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
        }

        public string getMaxIDClient(ref string error)
        {
            try
            {
                string returnIDClientMax;

                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT ISNULL(MAX(id_cliente), 0) + 1 FROM tbl_cliente";
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