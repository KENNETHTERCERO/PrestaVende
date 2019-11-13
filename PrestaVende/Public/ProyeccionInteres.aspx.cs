using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class ProyeccionInteres : System.Web.UI.Page
    {
        private static string error;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (lblWarning.Text == "") { divWarning.Visible = false; }
                if (lblError.Text == "") { divError.Visible = false; }
                if (lblSuccess.Text == "") { divSucceful.Visible = false; }
            }
            else
            {
                getDataProyeccion();
            }
        }

        private void getDataProyeccion()
        {
            try
            {
                CLASS.cs_prestamo cs_prestamo = new CLASS.cs_prestamo();
                gvProyeccion.DataSource = cs_prestamo.getDTProyeccion(ref error);
                gvProyeccion.DataBind();
            }
            catch (Exception ex)
            {
                showError(error + " / " + ex.ToString());
            }
        }

        #region messages
        private bool showWarning(string warning)
        {
            divWarning.Visible = true;
            lblWarning.Controls.Add(new LiteralControl(string.Format("<span style='color:Orange'>{0}</span>", warning)));
            return true;
        }

        private bool showError(string error)
        {
            divError.Visible = true;
            lblError.Controls.Add(new LiteralControl(string.Format("<span style='color:Red'>{0}</span>", error)));
            return true;
        }

        private bool showSuccess(string error)
        {
            divSucceful.Visible = true;
            lblSuccess.Controls.Add(new LiteralControl(string.Format("<span style='color:Green'>{0}</span>", error)));
            return true;
        }
        #endregion
    }
}