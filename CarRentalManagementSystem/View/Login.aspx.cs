using CarRentalManagementSystem.View.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalManagementSystem.View
{
    public partial class Login : System.Web.UI.Page
    {
        Models.Functions connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            connection = new Models.Functions();
        }

        public static string CustomerEmail = "";
        public static int CustomerID;

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string email = EmailField.Value;
            string password = PasswordField.Value;

            if (!IsValidEmail(email))
            {
                Console.WriteLine("Invalid email format. Please enter a valid email address.");
                return;
            }

            if (!IsValidPassword(password))
            {
                Console.WriteLine("Invalid password format.Password must be between 5 and 10 digits.");
                return;
            }
            if (email == "Admin@gmail.com" && password == "1234567")
            {
                CustomerEmail = "Admin";
                Response.Redirect("../View/Admin/Cars.aspx");
            }
            else
            {
                string query = "SELECT EmailAddress, Password, ID FROM customer_info WHERE EmailAddress='{0}' AND Password='{1}'";
                query = string.Format(query, email, password);
                DataTable dataTable = connection.GetData(query);

                if (dataTable.Rows.Count == 0)
                {
                    Console.WriteLine("Error logging in as a customer. Please try again.");
                }
                else
                {
                    CustomerEmail = dataTable.Rows[0]["EmailAddress"].ToString();
                    CustomerID = Convert.ToInt32(dataTable.Rows[0]["ID"]);
                    Response.Redirect("../View/Customer/Cars.aspx");
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            // Регулярное выражение для проверки формата email
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 5 || password.Length > 10)
                return false;

            // Регулярное выражение для проверки, что пароль состоит только из цифр
            return Regex.IsMatch(password, "^[0-9]+$");
        }
    }
}