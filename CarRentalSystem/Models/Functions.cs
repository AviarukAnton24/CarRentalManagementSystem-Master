using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Data;
using Org.BouncyCastle.Asn1.Mozilla;

namespace CarRentalManagementSystem.Models
{
    public class Functions
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;
        private DataTable dataTable;
        private string Connection_string;
        private MySqlDataAdapter adapter;

        public Functions()
        {
            Connection_string = ("server=localhost;port=3306;username=root;password=root;database=car_rental_system");
            connection = new MySqlConnection(Connection_string);
            command = new MySqlCommand();
            command.Connection = connection;
        }

        public DataTable GetData(string Query)
        {
            dataTable = new DataTable();
            adapter=new MySqlDataAdapter(Query,Connection_string);
            adapter.Fill(dataTable);
            return dataTable;
        }

        public int SetData(string Query)
        {
            int rental = 0;
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            command.CommandText = Query;
            rental=command.ExecuteNonQuery();
            connection.Close();
            return rental;
        }
    }
}