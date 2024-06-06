using CarRentalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalManagementSystem.View.Customer
{
    public partial class Cars : System.Web.UI.Page
    {
        //Customer module
        Models.Functions connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            connection = new Models.Functions();
            ShowCars();
        }

        private void ShowCars()
        {
            string Status = "Available";
            string Query = "select * from car_info where Status ='"+Status+"'";
            Carlist.DataSource = connection.GetData(Query);
            Carlist.DataBind();
            Customer=Login.CustomerID;
        }

        private void UpdateCar()
        {
            string Status = "Booked";
            string Query = "update car_info set Status ='{0}'where PlateNumber ='{1}'";
            Query = string.Format(Query, Status, Carlist.SelectedRow.Cells[1].Text);
            int result = connection.SetData(Query);
            ShowCars();
            if (result > 0)
            {
                // Показываем всплывающее окно с сообщением об успешном изменении
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Car Edited successfully');", true);
            }
            else
            {
                // Показываем всплывающее окно с сообщением об ошибке
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Failed to edit car. Please try again.');", true);
            }
        }

        protected void BookButton_Click(object sender, EventArgs e)
        {
            TimeSpan DDays = Convert.ToDateTime(ReturnDateField.Value) - DateTime.Today.Date;
            int Days = DDays.Days;
            int Price;
            Price = Convert.ToInt32(Carlist.SelectedRow.Cells[4].Text);
            int Fees = Price * Days;

            // Генерация уникального числа
            int uniqueNumber = GenerateUniqueNumber();

            if (Carlist.SelectedRow.Cells[1].Text == "")
            {
                // Показываем всплывающее окно с сообщением об ошибке
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Failed to rent a car. Please try again.');", true);
            }
            else
            {
                string rentalDate = DateTime.Today.Date.ToString("yyyy-MM-dd");
                string returnDate = Convert.ToDateTime(ReturnDateField.Value).ToString("yyyy-MM-dd");

                string query = "INSERT INTO rental_info (ID, Car, Customer, RentalDate, ReturnDate, Fees_info) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')";
                query = string.Format(query, uniqueNumber, Carlist.SelectedRow.Cells[1].Text, Login.CustomerID, rentalDate, returnDate, Fees);

                connection.SetData(query);
                UpdateCar();
                ShowCars();

                // Показываем всплывающее окно с сообщением
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Car rented successfully');", true);
            }
        }

        // Метод для генерации уникального числа для записи в базу данных
        private int GenerateUniqueNumber()
        {
            Random rand = new Random();
            int uniqueNumber;
            do
            {
                uniqueNumber = rand.Next(1, 1001); // Генерация числа от 1 до 1000
            } while (IsUniqueNumberExists(uniqueNumber)); // Проверка на уникальность
            return uniqueNumber;
        }

        // Метод для проверки, существует ли уже в базе данных указанное уникальное число
        private bool IsUniqueNumberExists(int number)
        {
            // Предположим, что у вас есть экземпляр класса Functions для доступа к базе данных
            Functions functions = new Functions();

            // Запрос к базе данных для проверки наличия числа
            string query = $"SELECT COUNT(*) FROM rental_info WHERE ID = {number}";

            // Получение данных из базы данных
            DataTable dataTable = functions.GetData(query);

            // Получение количества строк из результата запроса
            int count = Convert.ToInt32(dataTable.Rows[0][0]);

            // Если количество строк больше 0, значит число уже существует в базе данных
            return count > 0;
        }

        string Number, RentalDate, ReturnDate;
        int Price, Customer;
        protected void Carlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Number = Carlist.SelectedRow.Cells[1].Text;
            RentalDate=System.DateTime.Today.Date.ToString();
            ReturnDate = ReturnDateField.Value;
            Price = Convert.ToInt32(Carlist.SelectedRow.Cells[4].Text);
        }
    }
}