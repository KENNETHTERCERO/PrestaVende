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

        public DataTable getPrestamos(ref string error, string NumeroPrestamo)
        {
            try
            {
                DataTable Liquidacion = new DataTable();
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = "select a.id_prestamo_encabezado, a.numero_prestamo, total_prestamo, saldo_prestamo,c.estado_prestamo from tbl_prestamo_encabezado a "
                                       + " inner join tbl_sucursal b "
                                       + "     on a.id_sucursal = b.id_sucursal "
                                       + " inner join tbl_estado_prestamo c "
                                       + "     on a.estado_prestamo = c.id_estado_prestamo "
                                       + " where a.id_sucursal = @id_sucursal "
                                       + " and (CAST(a.fecha_proximo_pago AS datetime) + b.dias_para_liquidar) < getdate() "
                                       + " and a.estado_prestamo in (1,4) and a.numero_prestamo = @numero_prestamo";
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
                command.Parameters.AddWithValue("@id_caja", (int)HttpContext.Current.Session["id_caja"]);
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
    }

}
