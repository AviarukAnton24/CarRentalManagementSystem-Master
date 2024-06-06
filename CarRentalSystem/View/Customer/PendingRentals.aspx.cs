using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalManagementSystem.View.Customer
{
    public partial class PendingRentals : System.Web.UI.Page
    {
        Models.Functions connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            connection = new Models.Functions();
            ShowCars();
        }

        private void ShowCars()
        {
            string Query = "select * from rental_info where Customer ='" + Login.CustomerID + "'";
            Carlist.DataSource = connection.GetData(Query);
            Carlist.DataBind();
        }
    }
}