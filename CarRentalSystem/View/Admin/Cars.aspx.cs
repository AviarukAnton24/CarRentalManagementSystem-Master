using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalManagementSystem.View.Admin
{
    public partial class Cars : System.Web.UI.Page
    {
        Models.Functions connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            connection = new Models.Functions();
            ShowCars();
        }

        private void ShowCars()
        {
            string Query = "select * from car_info";
            Carlist.DataSource=connection.GetData(Query);
            Carlist.DataBind();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            // Получение значений из полей формы
            string number = NumberField.Value.Trim();
            string brand = BrandField.Value.Trim();
            string model = ModelField.Value.Trim();
            int price = Convert.ToInt32(PriceField.Value.Trim()); // Преобразуем строку в целое число
            string color = ColorField.Value.Trim();
            string status = AvailableField.SelectedValue.Trim();

            // Проверка на пустоту и корректность каждого поля
            if (string.IsNullOrWhiteSpace(number))
            {
                NumberErrorMessage.Text = "This field should not be empty";
                NumberErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsValidNumberFormat(number))
            {
                NumberErrorMessage.Text = "Incorrect syntax. The number must be in the format 'DDLLDD' (e.g., 12AB34).";
                NumberErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                NumberErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(brand))
            {
                BrandErrorMessage.Text = "This field should not be empty";
                BrandErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsEnglishAlphabet(brand) || brand.Length < 3 || brand.Length > 10)
            {
                BrandErrorMessage.Text = "Incorrect syntax";
                BrandErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                BrandErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                ModelErrorMessage.Text = "This field should not be empty";
                ModelErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsValidModel(model))
            {
                ModelErrorMessage.Text = "Incorrect syntax. Only letters and digits (5 to 10 characters) are allowed.";
                ModelErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                ModelErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }
            if (string.IsNullOrWhiteSpace(PriceField.Value))
            {
                PriceErrorMessage.Text = "This field should not be empty";
                PriceErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            string priceString = PriceField.Value.Trim().Replace(" ", ""); // Удалить пробелы из строки и обрезать лишние пробелы
            if (!int.TryParse(priceString, out price) || price < 1000 || price > 1000000)
            {
                PriceErrorMessage.Text = "Price must be a valid number between 1000 and 1000000.";
                PriceErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                PriceErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }
            if (string.IsNullOrWhiteSpace(color))
            {
                ColorErrorMessage.Text = "This field should not be empty";
                ColorErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsEnglishAlphabet(color) || color.Length < 2 || color.Length > 10)
            {
                ColorErrorMessage.Text = "Incorrect syntax";
                ColorErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            // Если все проверки прошли успешно, выполнить следующий код
            else
            {
                PriceErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(color))
            {
                ColorErrorMessage.Text = "This field should not be empty";
                ColorErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsEnglishAlphabet(color) || color.Length < 1 || color.Length > 10)
            {
                ColorErrorMessage.Text = "Incorrect syntax";
                ColorErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                ColorErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            string Query = "insert into car_info values('{0}','{1}','{2}','{3}','{4}','{5}')";
            Query = string.Format(Query, number, brand, model, price, color, status);
            int result = connection.SetData(Query);
            if (result > 0)
            {
                // Показываем всплывающее окно с сообщением об успешном добавлении
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Car added successfully');", true);
            }
            else
            {
                // Показываем всплывающее окно с сообщением об ошибке
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showMessage('Failed to add car. Please try again.');", true);
            }
        }

        // Метод для проверки, содержит ли строка только символы английского алфавита
        private bool IsEnglishAlphabet(string value)
        {
            return value.All(char.IsLetter) && value.All(char.IsLetterOrDigit);
        }

        // Метод для проверки, содержит ли строка только английские заглавные буквы и цифры
        private bool ContainsUpperCaseAndDigits(string value)
        {
            bool hasUpperCase = false;
            bool hasDigit = false;

            foreach (char c in value)
            {
                if (char.IsUpper(c))
                {
                    hasUpperCase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }

                // Если обе проверки уже пройдены, выходим из цикла
                if (hasUpperCase && hasDigit)
                {
                    break;
                }
            }

            // Возвращаем true, если строка содержит и английские заглавные буквы, и цифры
            return hasUpperCase && hasDigit;
        }

        private bool IsValidNumberFormat(string value)
        {
            // Регулярное выражение для проверки формата 'DDLLDD'
            return Regex.IsMatch(value, @"^\d{2}[A-Z]{2}\d{2}$");
        }

        private bool IsValidModel(string value)
        {
            // Регулярное выражение для проверки, что строка содержит только цифры и английские буквы длиной от 5 до 10 символов
            return Regex.IsMatch(value, @"^[a-zA-Z0-9]{5,10}$");
        }

        protected void Carlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            NumberField.Value = Carlist.SelectedRow.Cells[1].Text;
            BrandField.Value = Carlist.SelectedRow.Cells[2].Text;
            ModelField.Value = Carlist.SelectedRow.Cells[3].Text;
            PriceField.Value = Carlist.SelectedRow.Cells[4].Text;
            ColorField.Value = Carlist.SelectedRow.Cells[5].Text;
            AvailableField.SelectedValue = Carlist.SelectedRow.Cells[6].Text;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            // Получение значений из полей формы
            string number = NumberField.Value.Trim().ToString();

            // Проверка на пустоту и корректность каждого поля
            if (string.IsNullOrWhiteSpace(number))
            {
                NumberErrorMessage.Text = "This field should not be empty";
                NumberErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (number.Length < 1 || number.Length > 10 || !ContainsUpperCaseAndDigits(number))
            {
                NumberErrorMessage.Text = "Incorrect syntax";
                NumberErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                NumberErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            string Query = "delete from car_info where PlateNumber ='{0}'";
            Query = string.Format(Query, number);
            ShowCars();
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

        protected void EditButton_Click(object sender, EventArgs e)
        {
            // Получение значений из полей формы
            string number = NumberField.Value.Trim();
            string brand = BrandField.Value.Trim();
            string model = ModelField.Value.Trim();
            int price = Convert.ToInt32(PriceField.Value.Trim()); // Преобразуем строку в целое число
            string color = ColorField.Value.Trim();
            string status = AvailableField.SelectedValue.Trim();

            // Проверка на пустоту и корректность каждого поля
            if (string.IsNullOrWhiteSpace(number))
            {
                NumberErrorMessage.Text = "This field should not be empty";
                NumberErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsValidNumberFormat(number))
            {
                NumberErrorMessage.Text = "Incorrect syntax. The number must be in the format 'DDLLDD' (e.g., 12AB34).";
                NumberErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                NumberErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(brand))
            {
                BrandErrorMessage.Text = "This field should not be empty";
                BrandErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsEnglishAlphabet(brand) || brand.Length < 3 || brand.Length > 10)
            {
                BrandErrorMessage.Text = "Incorrect syntax";
                BrandErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                BrandErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                ModelErrorMessage.Text = "This field should not be empty";
                ModelErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsValidModel(model))
            {
                ModelErrorMessage.Text = "Incorrect syntax. Only letters and digits (5 to 10 characters) are allowed.";
                ModelErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                ModelErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }
            if (string.IsNullOrWhiteSpace(PriceField.Value))
            {
                PriceErrorMessage.Text = "This field should not be empty";
                PriceErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            string priceString = PriceField.Value.Trim().Replace(" ", ""); // Удалить пробелы из строки и обрезать лишние пробелы
            if (!int.TryParse(priceString, out price) || price < 1000 || price > 1000000)
            {
                PriceErrorMessage.Text = "Price must be a valid number between 1000 and 1000000.";
                PriceErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                PriceErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }
            if (string.IsNullOrWhiteSpace(color))
            {
                ColorErrorMessage.Text = "This field should not be empty";
                ColorErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsEnglishAlphabet(color) || color.Length < 2 || color.Length > 10)
            {
                ColorErrorMessage.Text = "Incorrect syntax";
                ColorErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            // Если все проверки прошли успешно, выполнить следующий код
            else
            {
                PriceErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            if (string.IsNullOrWhiteSpace(color))
            {
                ColorErrorMessage.Text = "This field should not be empty";
                ColorErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (!IsEnglishAlphabet(color) || color.Length < 1 || color.Length > 10)
            {
                ColorErrorMessage.Text = "Incorrect syntax";
                ColorErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                ColorErrorMessage.Text = ""; // Очистка сообщения об ошибке
            }

            string Query = "update car_info set Brand ='{0}',Model ='{1}',Price ='{2}',Color ='{3}',Status ='{4}' where PlateNumber ='{5}'";
            Query = string.Format(Query,brand, model, price, color, status,number);
            int result = connection.SetData(Query);
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
    }
}