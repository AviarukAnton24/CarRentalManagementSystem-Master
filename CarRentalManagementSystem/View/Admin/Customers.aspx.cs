using CarRentalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalManagementSystem.View.Admin
{
    public partial class Customers : System.Web.UI.Page
    {
        Models.Functions connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            connection = new Models.Functions();
            ShowCustomers();
        }

        private void ShowCustomers()
        {
            string Query = "select * from customer_info";
            Customerlist.DataSource = connection.GetData(Query);
            Customerlist.DataBind();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            // Получение значений из полей формы
            string name = NameField.Value.Trim();
            string address = EmailAddressField.Value.Trim();
            string phone = PhoneField.Value.Trim();
            string password = PasswordField.Value.Trim();

            // Проверка на пустоту и корректность каждого поля
            if (string.IsNullOrWhiteSpace(name))
            {
                NameErrorMessage.Text = "This field should not be empty";
                NameErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsEnglishAlphabet(name) || name.Length < 4 || name.Length > 10)
            {
                NameErrorMessage.Text = "Incorrect syntax. Only English letters (4 to 10 characters) are allowed.";
                NameErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                NameErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                EmailAddressErrorMessage.Text = "This field should not be empty";
                EmailAddressErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsValidEmailAddress(address) || address.Length < 8 || address.Length > 20)
            {
                EmailAddressErrorMessage.Text = "Incorrect syntax. Only English letters, digits, '@' symbol, and spaces (8 to 20 characters) are allowed.";
                EmailAddressErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                EmailAddressErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                PhoneErrorMessage.Text = "This field should not be empty";
                PhoneErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (phone.Length != 13 || !IsPhoneValid(phone))
            {
                PhoneErrorMessage.Text = "Incorrect syntax. Phone number must be 13 characters long and only contain '+', digits, and spaces.";
                PhoneErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                PhoneErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                PasswordErrorMessage.Text = "This field should not be empty";
                PasswordErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsValidPassword(password))
            {
                PasswordErrorMessage.Text = "Incorrect syntax.Password must be between 5 and 10 digits.";
                PasswordErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                PasswordErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            // Генерация уникального числа
            int uniqueNumber = GenerateUniqueNumber();

            // Запись данных в базу данных
            string Query = "insert into customer_info values('{0}','{1}','{2}','{3}','{4}')";
            Query = string.Format(Query, uniqueNumber, name, address, phone, password);
            int result = connection.SetData(Query);
            if (result > 0)
            {
                // Показываем всплывающее окно с сообщением об успешном добавлении
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Customer added successfully');", true);
            }
            else
            {
                // Показываем всплывающее окно с сообщением об ошибке
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Failed to add customer. Please try again.');", true);
            }
        }

        // Метод для проверки, содержит ли строка только символы английского алфавита
        private bool IsEnglishAlphabet(string value)
        {
            return value.All(char.IsLetter);
        }

        // Метод для проверки, содержит ли строка только символы '+' и цифры
        private bool IsPhoneValid(string value)
        {
            return value.All(c => char.IsDigit(c) || c == '+');
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

        private bool IsValidEmailAddress(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 5 || password.Length > 10)
                return false;

            // Регулярное выражение для проверки, что пароль состоит только из цифр
            return Regex.IsMatch(password, "^[0-9]+$");
        }

        // Метод для проверки, существует ли уже в базе данных указанное уникальное число
        private bool IsUniqueNumberExists(int number)
        {
            // Предположим, что у вас есть экземпляр класса Functions для доступа к базе данных
            Functions functions = new Functions();

            // Запрос к базе данных для проверки наличия числа
            string query = $"SELECT COUNT(*) FROM customer_info WHERE ID = {number}";

            // Получение данных из базы данных
            DataTable dataTable = functions.GetData(query);

            // Получение количества строк из результата запроса
            int count = Convert.ToInt32(dataTable.Rows[0][0]);

            // Если количество строк больше 0, значит число уже существует в базе данных
            return count > 0;
        }

        protected void Customerlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            NameField.Value = Customerlist.SelectedRow.Cells[2].Text;
            EmailAddressField.Value = Customerlist.SelectedRow.Cells[3].Text;
            PhoneField.Value = Customerlist.SelectedRow.Cells[4].Text;
            PasswordField.Value = Customerlist.SelectedRow.Cells[5].Text;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            // Получение значений из полей формы
            string name = NameField.Value.Trim().ToString();

            // Проверка на пустоту и корректность каждого поля
            if (string.IsNullOrWhiteSpace(name))
            {
                NameErrorMessage.Text = "This field should not be empty";
                NameErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsEnglishAlphabet(name) || name.Length < 4 || name.Length > 10)
            {
                NameErrorMessage.Text = "Incorrect syntax. Only English letters (4 to 10 characters) are allowed.";
                NameErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                NameErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            string Query = "delete from customer_info where Name ='{0}'";
            Query = string.Format(Query, name);
            ShowCustomers();

            int result = connection.SetData(Query);
            if (result > 0)
            {
                // Показываем всплывающее окно с сообщением об успешном удалении
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Customer deleted successfully');", true);
            }
            else
            {
                // Показываем всплывающее окно с сообщением об ошибке
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Failed to delete customer. Please try again.');", true);
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            // Получение значений из полей формы
            string name = NameField.Value.Trim();
            string address = EmailAddressField.Value.Trim();
            string phone = PhoneField.Value.Trim();
            string password = PasswordField.Value.Trim();

            // Проверка на пустоту и корректность каждого поля
            if (string.IsNullOrWhiteSpace(name))
            {
                NameErrorMessage.Text = "This field should not be empty";
                NameErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsEnglishAlphabet(name) || name.Length < 4 || name.Length > 10)
            {
                NameErrorMessage.Text = "Incorrect syntax. Only English letters (4 to 10 characters) are allowed.";
                NameErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                NameErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                EmailAddressErrorMessage.Text = "This field should not be empty";
                EmailAddressErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsValidEmailAddress(address) || address.Length < 8 || address.Length > 20)
            {
                EmailAddressErrorMessage.Text = "Incorrect syntax. Only English letters, digits, '@' symbol, and spaces (8 to 20 characters) are allowed.";
                EmailAddressErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                EmailAddressErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                PhoneErrorMessage.Text = "This field should not be empty";
                PhoneErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsPhoneValid(phone))
            {
                PhoneErrorMessage.Text = "Incorrect syntax. Only '+' symbol, digits, and spaces are allowed.";
                PhoneErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                PhoneErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                PasswordErrorMessage.Text = "This field should not be empty";
                PasswordErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsValidPassword(password))
            {
                PasswordErrorMessage.Text = "Incorrect syntax.Password must be between 5 and 10 digits.";
                PasswordErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                PasswordErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            string Query = "update customer_info set EmailAddress ='{0}', Phone ='{1}', Password ='{2}' where Name ='{3}'";
            Query = string.Format(Query, address, phone, password, name);
            int result = connection.SetData(Query);

            if (result > 0)
            {
                // Показываем всплывающее окно с сообщением об успешном изменении
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Customer Edited successfully');", true);
            }
            else
            {
                // Показываем всплывающее окно с сообщением об ошибке
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Failed to edit customer. Please try again.');", true);
            }
        }
    }
}