using CarRentalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalManagementSystem.View.Admin
{
    public partial class Rentals : System.Web.UI.Page
    {
        Models.Functions connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            connection = new Models.Functions();
            ShowRents();
        }

        private void ShowRents()
        {
            string Query = "select * from rental_info";
            RentalList.DataSource = connection.GetData(Query);
            RentalList.DataBind();
        }

        string LicensePlate;
        protected void RentalList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LicensePlate = RentalList.SelectedRow.Cells[1].Text;
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

        private void UpdateCar()
        {
            string Status = "Available";
            string Query = "update car_info set Status ='{0}'where PlateNumber ='{1}'";
            Query = string.Format(Query, Status, RentalList.SelectedRow.Cells[2].Text);
            int result = connection.SetData(Query);
            ShowRents();
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

        private void ReturnCar()
        {
            string Query = "delete from rental_info where ID ='{0}'";
            Query = string.Format(Query, RentalList.SelectedRow.Cells[1].Text);
            int result = connection.SetData(Query);
            if (result > 0)
            {
                // Показываем всплывающее окно с сообщением об успешном удалении
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Car deleted successfully');", true);
            }
            else
            {
                // Показываем всплывающее окно с сообщением об ошибке
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Failed to delete car. Please try again.');", true);
            }
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            // Генерация уникального числа
            int uniqueNumber = GenerateUniqueNumber();

            if (RentalList.SelectedRow.Cells[1].Text == "")
            {
                // Показываем всплывающее окно с сообщением об ошибке
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Select a rent. Please try again.');", true);
            }
            else
            {
                string returnDate = DateTime.Today.ToString("yyyy-MM-dd");
                string query = "INSERT INTO return_info VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')";
                query = string.Format(query, uniqueNumber, RentalList.SelectedRow.Cells[2].Text, RentalList.SelectedRow.Cells[3].Text, returnDate, DelayField.Value, FineField.Value);

                connection.SetData(query);

                UpdateCar();
                ReturnCar();
                ShowRents();

                // Показываем всплывающее окно с сообщением
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Car returned successfully');", true);
            }
        }
    }
}