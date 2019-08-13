using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class WFPrincipal : System.Web.UI.Page
    {
        private string error = "";
        private string valor_cookie;

        protected void Page_Load(object sender, EventArgs e)
        {
            //codigo para obtener datos de la cookie
            //<-- de aqui

            HttpCookie cookie = Request.Cookies["userLogin"];

            if (cookie == null && CLASS.cs_usuario.id_usuario == 0)
            {
                Response.Redirect("~/WFWebLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    valor_cookie = cookie["id_user"].ToString();
                }
            }
            // hasta aqui-->
        }
    }
}