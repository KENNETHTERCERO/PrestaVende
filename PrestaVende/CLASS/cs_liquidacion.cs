﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace PrestaVende.CLASS
{
    public class cs_liquidacion
    {
        private cs_connection connection = new cs_connection();
        private SqlCommand command = new SqlCommand();

        public DataTable getPrestamosLiquidacion(ref string error, string NumeroPrestamo)
        {
            try
            {
                DataTable Liquidacion = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "EXEC SP_devuelve_prestamo_para_liquidar @id_sucursal, @numero_prestamo";
                command.Parameters.AddWithValue("@id_sucursal", Convert.ToInt32(HttpContext.Current.Session["id_sucursal"]));
                command.Parameters.AddWithValue("@numero_prestamo", NumeroPrestamo);
                Liquidacion.Load(command.ExecuteReader()); 
                return Liquidacion;
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

        public bool insertLiquidacion(ref string error, string id_prestamo_encabezado)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();
                command.Parameters.Clear();
                command.CommandText = "EXEC EjecutaLiquidacion @id_prestamo_encabezado, @id_caja, @id_usuario";
                command.Parameters.AddWithValue("@id_prestamo_encabezado", id_prestamo_encabezado);
                command.Parameters.AddWithValue("@id_caja", Convert.ToInt32(HttpContext.Current.Session["id_caja"]));
                command.Parameters.AddWithValue("@id_usuario", Convert.ToInt32(HttpContext.Current.Session["id_usuario"]));

                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.Transaction.Commit();
                    return true;
                }
                else
                {
                    throw new SystemException("No se pudo insertar la liquidación, por favor, valide los datos, o comuniquese con el administrador del sistema. ");
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

        public DataTable getSucursal(ref string error)
        {
            try
            {
                DataTable Sucursal = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "SELECT 0 AS id_sucursal, 'SELECCIONAR' AS sucursal UNION SELECT id_sucursal, UPPER(sucursal) from tbl_sucursal WHERE estado = 1";
                Sucursal.Load(command.ExecuteReader());
                return Sucursal;
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

        public DataTable getReporteLiquidacion(ref string error, string id_sucursal, string fecha_inicio, string fecha_fin)
        {
            try
            {
                DataTable Liquidacion = new DataTable("reporteLiquidaciones");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "EXEC SP_reporte_liquidaciones @id_sucursal, @fecha_inicio, @fecha_fin";
                command.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                command.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
                command.Parameters.AddWithValue("@fecha_fin", fecha_fin);
                Liquidacion.Load(command.ExecuteReader());
                return Liquidacion;
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
