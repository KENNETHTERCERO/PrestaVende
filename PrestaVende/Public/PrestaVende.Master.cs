using System;
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
            getMenu();
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
    }
}