using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PrestaVende.Public
{
    public partial class PrestaVende : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getMenu();
        }

        public void getMenu()
        {
            Panel myFieldSet = new Panel();
            myFieldSet.GroupingText = "Contact Details";
            HtmlGenericControl divMenu = new HtmlGenericControl("div");
            HtmlGenericControl ulPrincipal = new HtmlGenericControl("ul");

            HtmlGenericControl myOrderedList = new HtmlGenericControl("ol");

            HtmlGenericControl listItem1 = new HtmlGenericControl("li");
            HtmlGenericControl listItem2 = new HtmlGenericControl("li");
            HtmlGenericControl listItem3 = new HtmlGenericControl("li");
            HtmlGenericControl aControl = new HtmlGenericControl("a");

            // code here which would add labels and textboxes to the ListItems
            listItem1.ID = "ITEM1";
            aControl.ID = "test";
            aControl.Attributes.Add("href", "#");
            aControl.InnerText = "PRUEBA";
            listItem1.Controls.Add(aControl);
            ulPrincipal.Controls.Add(listItem1);
            divMenu.Controls.Add(ulPrincipal);
            //myOrderedList.Controls.Add(listItem1);
            //myOrderedList.Controls.Add(listItem2);
            //myOrderedList.Controls.Add(listItem3);

            //myFieldSet.Controls.Add(myOrderedList);

            form1.Controls.Add(divMenu);

        }
    }
}