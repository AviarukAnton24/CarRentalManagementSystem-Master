using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalManagementSystem.View.Admin
{
    public partial class Returns : System.Web.UI.Page
    {
        Models.Functions connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            connection = new Models.Functions();
            ShowReturn();
        }

        private void ShowReturn()
        {
            string Query = "select * from return_info";
            Returnlist.DataSource = connection.GetData(Query);
            Returnlist.DataBind();
        }
    }
}