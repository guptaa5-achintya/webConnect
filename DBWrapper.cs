using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webConnect.Controllers;

namespace webConnect
{
    class DBWrapper {

        public static  string _connectionString = "Server=GUPTAA5-STG4\\SQLEXPRESS;Integrated security=SSPI;database=swDB";//"Data Source=GUPTAA5-STG4\\SQLEXPRESS\\SQLEXPRESS,1433;Initial Catalog=swDB;Integrated Security=False;Connect Timeout=60;Application Name=webConnect;User ID=sa;Password=arock19")

        public static SwAPIController.Character getCharacterData(string characterName)
        {
            string queryString = "SELECT * FROM characters WHERE name ='" + characterName + "';";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new SwAPIController.Character {
                            Name = (string) reader["name"],
                            Gender = (string) reader["gender"],
                            Height =  reader["height"] + ""
                        };
                    }
                }
            }
            return null;
        }

        public static void sendCommand(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static string sendData(List<SwAPIController.Character> characters)
        {   
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("name", typeof(String)));
            data.Columns.Add(new DataColumn("gender", typeof(String)));
            data.Columns.Add(new DataColumn("height", typeof(Decimal)));
            data.PrimaryKey = new DataColumn[] { data.Columns["name"] };
            foreach(SwAPIController.Character character in characters){
                DataRow row = data.NewRow();
                row["name"] = character.Name;
                row["gender"] = character.Gender;
                row["height"] = character.Height;
                data.Rows.Add(row);
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {   
                connection.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "characters";

                        // Write from the source to the destination.
                    bulkCopy.WriteToServer(data);
                }
            }
            return "";
        }

        
    }
}