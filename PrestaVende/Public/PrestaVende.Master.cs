﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class PrestaVende : System.Web.UI.MasterPage
    {
        private CLASS.cs_menu cs_menu = new CLASS.cs_menu();
        private string error = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    getMenu();
                    string caja = "";

                    if (Convert.ToInt32(Session["id_caja"]) == 0)
                        lblCajaAsignada.Text = "ID: CAJA NO ASIGNADA.";
                    else
                        lblCajaAsignada.Text = "ID: " + Convert.ToInt32(Session["id_caja"]);

                    lblUsuario.Text = "U: " + Session["primer_nombre"].ToString() + " " + Session["primer_apellido"].ToString();
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
        }

        public void getMenu()
        {

            try
            {
                HtmlGenericControl divMenu = new HtmlGenericControl("div");
                HtmlGenericControl ulPrincipal = new HtmlGenericControl("ul");

                divMenu.ID = "header";
                ulPrincipal.Attributes.Add("class", "nav");

                foreach (DataRow itemHeader in cs_menu.getMenuHeader(ref error).Rows)
                {
                    HtmlGenericControl tmpLiEncabezado = new HtmlGenericControl("li");
                    HtmlGenericControl tmpAEncabezado = new HtmlGenericControl("a");

                    tmpAEncabezado.InnerText = itemHeader[1].ToString();
                    tmpLiEncabezado.Controls.Add(tmpAEncabezado);

                    DataTable dtFirstLevel = new DataTable("FirsLevel");
                    dtFirstLevel = cs_menu.getMenuFirstLevel(ref error, itemHeader[0].ToString());

                    if (dtFirstLevel.Rows.Count > 0)
                    {
                        HtmlGenericControl tmpUlFirstLevel = new HtmlGenericControl("ul");
                        foreach (DataRow itemPrimerNivel in dtFirstLevel.Rows)
                        {
                            HtmlGenericControl tmpLiFirstLevel = new HtmlGenericControl("li");
                            HtmlGenericControl tmpAFirstLevel = new HtmlGenericControl("a");

                            tmpAFirstLevel.InnerText = itemPrimerNivel[1].ToString();
                            tmpAFirstLevel.Attributes.Add("href", itemPrimerNivel[2].ToString());

                            tmpUlFirstLevel.Controls.Add(tmpAFirstLevel);
                        }
                        tmpLiEncabezado.Controls.Add(tmpUlFirstLevel);
                    }

                    ulPrincipal.Controls.Add(tmpLiEncabezado);
                }

                divMenu.Controls.Add(ulPrincipal);
                form1.Controls.Add(divMenu);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                string id_usuario = this.Session["id_usuario"].ToString();
                cancelarRecepcion();
                cs_menu.exitSystem(id_usuario);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cancelarRecepcion()
        {
            try
            {
                Session["id_usuario"] = 0;
                Session["id_empresa"] = 0;
                Session["id_sucursal"] = 0;
                Session["id_rol"] = 0;
                Session["id_caja"] = 0;
                Session["usuario"] = "";
                Session["id_tipo_caja"] = 0;
                Session["primer_nombre"] = "";
                Session["primer_apellido"] = "";
                Session["saldo_caja"] = 0;
                Session.RemoveAll();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnCambio_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFMantenimientoContraseña.aspx", false);
        }
    }
}