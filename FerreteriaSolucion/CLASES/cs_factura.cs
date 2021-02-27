using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaSolucion.CLASES
{
    class cs_factura
    {
        private cs_connection_ferremas connection = new cs_connection_ferremas();
        private SqlCommand command = new SqlCommand();

        public bool insertEncabezadoFactura(ref string error, string id_serie, string numero_factura, string id_cliente, string total_factura, DataTable detalle_factura)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Transaction = connection.connection.BeginTransaction();

                decimal total_facturaD = decimal.Parse(total_factura), total_factura_sin_Iva = 0, ivaEnc = 0;
                ivaEnc = getIVAToAmount(total_facturaD);
                total_factura_sin_Iva = total_facturaD - ivaEnc;
                
                command.CommandText = "INSERT INTO tbl_factura_encabezado (id_serie, numero_factura, id_cliente, total_factura, total_factura_sin_iva, fecha_creacion, fecha_modificacion, estado, iva) " +
                                        "VALUES (@id_serieEnc, @numero_facturaEnc, @id_clienteEnc, @total_facturaEnc, @total_factura_sin_ivaEnc, GETDATE(), GETDATE(), 1, @ivaEnc)";
                command.Parameters.AddWithValue("@id_serieEnc", id_serie);
                command.Parameters.AddWithValue("@numero_facturaEnc", numero_factura);
                command.Parameters.AddWithValue("@id_clienteEnc", id_cliente);
                command.Parameters.AddWithValue("@total_facturaEnc", total_facturaD);
                command.Parameters.AddWithValue("@total_factura_sin_ivaEnc", total_factura_sin_Iva);
                command.Parameters.AddWithValue("@ivaEnc", ivaEnc);
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
                {
                    command.CommandText = "SELECT MAX(id_factura_encabezado) FROM tbl_factura_encabezado";
                    string id_factura_enca = command.ExecuteScalar().ToString();
                    if (insertDetalleFactura(ref error, detalle_factura, id_factura_enca))
                    {
                        if (updateSerie(ref error, id_serie))
                        {
                            command.Transaction.Commit();
                            return true;
                        }
                        else
                        {
                            throw new SystemException("No se pudo actualizar la serie " + error);
                        }
                    }
                    else
                    {
                        throw new SystemException("No se pudo agregar detalle de factura " + error);
                    }
                }
                else
                {
                    throw new SystemException("No se pudo agregar factura encabezado." + error);
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

        private bool insertDetalleFactura(ref string error, DataTable detalle, string id_factura_encabezado)
        {
            try
            {
                decimal IVA = getOnlyIVA();
                
                

                int contadorDetalle = detalle.Rows.Count, contadorAumenta = 0;

                foreach (DataRow item in detalle.Rows)
                {
                    decimal totalItemsDet = decimal.Parse(item[7].ToString());
                    decimal IVAmas1 = (IVA / 100) + 1;
                    decimal IVAmenos1 = IVAmas1 - 1;
                    decimal totalIva = totalItemsDet / IVAmas1;
                    totalIva = Math.Round((totalIva * IVAmenos1), 2, MidpointRounding.ToEven);
                    decimal total_sin_iva = totalItemsDet - totalIva;


                    if (IVA == 0)
                    {
                        throw new SystemException("Error en calculo de IVA");
                    }

                    command.CommandText = "INSERT INTO tbl_factura_detalle (id_factura_encabezado, id_producto, cantidad, valor_por_producto, total_de_items, total_sin_iva, iva) " +
                                                                $"VALUES({id_factura_encabezado}, " +
                                                                $"{item[1].ToString()}, " +
                                                                $"{item[5].ToString()}, " + 
                                                                $"{item[6].ToString()}, " +
                                                                $"{totalItemsDet}, " +
                                                                $"{total_sin_iva}, " +
                                                                $"{totalIva})";

                    //SqlParameter id_factura_encabezadoDet = new SqlParameter("@id_factura_encabezadoDet", );
                    //command.Parameters.Add(id_factura_encabezadoDet);
                    //SqlParameter id_productoDet = new SqlParameter("@id_productoDet", );
                    //command.Parameters.Add(id_productoDet);
                    //SqlParameter cantidadDet = new SqlParameter("@cantidadDet", );
                    //command.Parameters.Add(cantidadDet);
                    //SqlParameter valor_por_productoDet = new SqlParameter("@valor_por_productoDet", );
                    //command.Parameters.Add(valor_por_productoDet);
                    //SqlParameter total_de_itemsDet = new SqlParameter("@total_de_itemsDet", );
                    //command.Parameters.Add(total_de_itemsDet);
                    //SqlParameter total_sin_ivaDet = new SqlParameter("@total_sin_ivaDet", );
                    //command.Parameters.Add(total_sin_ivaDet);
                    //SqlParameter ivaDet = new SqlParameter("@ivaDet", totalIva);
                    //command.Parameters.Add(ivaDet);

                    if (int.Parse(command.ExecuteNonQuery().ToString())>0)
                    {
                        if (updateInventario(ref error, item[5].ToString(), item[1].ToString()))
                        {
                            if (logInventario(ref error, item[1].ToString(), item[5].ToString()))
                            {
                                contadorAumenta++;
                            }
                            else
                            {
                                throw new SystemException("Error insertando log de inventario. " + error);
                            }
                        }
                        else
                        {
                            throw new SystemException("Error actualizando inventario " + error);
                        }
                    }
                    else
                    {
                        throw new SystemException("No se pudo insertar detalle de factura");
                    }
                }
                if (contadorDetalle.Equals(contadorAumenta))
                {
                    return true;
                }
                else
                {
                    throw new SystemException("EL DETALLE NO COINCIDE CON EL NUMERO DE DATOS INSERTADOS");
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        private decimal getIVAToAmount(decimal monto)
        {
            try
            {
                command.CommandText = "SELECT IVA from tbl_parametros_generales";
                decimal IVA = Convert.ToDecimal(command.ExecuteScalar()) / 100;
                decimal totalIva = monto / (IVA + 1);
                totalIva = Math.Round(totalIva * IVA, 2, MidpointRounding.ToEven);
                return totalIva;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return 0;
            }
        }

        private decimal getOnlyIVA()
        {
            try
            {
                command.CommandText = "SELECT IVA from tbl_parametros_generales";
                decimal IVA = Convert.ToDecimal(command.ExecuteScalar());
                return IVA;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return 0;
            }
        }

        private bool updateSerie(ref string error, string id_serieSer)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_serie SET contador = contador + 1 WHERE id_serie = @id_serieSer";
                command.Parameters.AddWithValue("@id_serieSer", id_serieSer);
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
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
        }

        private bool updateInventario(ref string error, string cantidadInv, string id_productoInv)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "UPDATE tbl_inventario SET stock = stock - @cantidadInvUp WHERE id_producto = @id_productoInvUp";
                command.Parameters.AddWithValue("@cantidadInvUp", cantidadInv);
                command.Parameters.AddWithValue("@id_productoInvUp", id_productoInv);
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
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
        }

        private bool logInventario(ref string error, string id_productoLog, string cantidadLog)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO tbl_log_inventario (id_producto, id_tipo_transaccion, cantidad, fecha_transaccion)" +
                                                               "VALUES(@id_productoLogIn, 3, @cantidadLogIn, GETDATE())";

                command.Parameters.AddWithValue("@id_productoLogIn", id_productoLog);
                command.Parameters.AddWithValue("@cantidadLogIn", cantidadLog);
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
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
        }

        public DataTable impresionFactura(ref string error, string id_serie, string numero_factura)
        {
            try
            {
                DataTable returnTable = new DataTable("dataFactura");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = $"exec sp_reporte_factura {id_serie}, {numero_factura}";
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

        public int validateBillIfIsActive(ref string error, string id_serie, string numero_factura)
        {
            try
            {
                DataTable table = new DataTable("tabe");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = $"SELECT id_serie, numero_factura, estado FROM tbl_factura_encabezado WHERE id_serie = {id_serie} AND numero_factura = {numero_factura} AND estado = 1";
                table.Load(command.ExecuteReader());
                int intReturn = 0;
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow item in table.Rows)
                    {
                        if (item[2].ToString().Equals("1"))
                        {
                            intReturn = 1;
                        }
                        else if (item[2].ToString().Equals("0"))
                        {
                            intReturn = 0;
                        }
                        else if (int.Parse(item[2].ToString()) > 1)
                        {
                            intReturn = int.Parse(item[2].ToString());
                        }
                    }
                }
                return intReturn;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return 0;
            }
            finally
            {
                connection.connection.Close();
            }
        }

        public bool anulaFactura(ref string error, string id_serie, string numero_factura)
        {
            try
            {
                connection.connection.Open();
                command.Connection = connection.connection;
                command.CommandText = $"UPDATE tbl_factura_encabezado SET estado = 0 WHERE id_serie = {id_serie} AND numero_factura = {numero_factura} AND estado = 1";
                if (int.Parse(command.ExecuteNonQuery().ToString()) > 0)
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

        public DataTable reporteFacturas(ref string error, string fecha_inicial, string fecha_final)
        {
            try
            {
                DataTable returnTable = new DataTable("tabl");
                connection.connection.Open();
                command.Connection = connection.connection;
                command.Parameters.Clear();
                command.CommandText = "exec sp_reporte_facturas @fecha_inicial, @fecha_final";
                command.Parameters.AddWithValue("@fecha_inicial", fecha_inicial);
                command.Parameters.AddWithValue("@fecha_final", fecha_final);
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
    }
}
