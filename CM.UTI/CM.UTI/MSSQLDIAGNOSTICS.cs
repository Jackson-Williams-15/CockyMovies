using System;
using System;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query;

// MyClass.cs using System; 
namespace T4DBMSSQL
{
    class T4MSSQL
        {
            // Test The Use of a Namespace Class Function;

            public void sayMSHelloPing()
            {
            //STEP0 CHECK IF SERVICE IS RUNNING
            //STEP1 CHECK IF QUERY STRINGS RUN PROPERLY FOR SELECT AND INSERT.
            //STEP2 CHECK UPDATE AND DELETE QUERY STRINGS
            //STEP3 RETURN STATUS OF MSPLATFORM. (GREEN, RED, AND YELLOW).

                Console.WriteLine("Hello John. SQL Server Diagnostics. Can we please finish before Christmas?");
            }
        
            //If MySQL is Selected it will run this function which will then execute the programs as required.
            public void StartMSUtilities()
            {
                Console.WriteLine("Right Now we are not doing anything special.");
            }

            //Test Executing a SQL Statement Against MySQL
            public static void connectiontest()
            {
            string connectionString = "Server=localhost;Database=MyNewDatabase;Integrated Security=True;"; 
            SqlConnection connection = new SqlConnection(connectionString); 
            try
            {
                connection.Open(); 
                // Query to select all users
                string selectUsersQuery = "SELECT * FROM TestUsers;"; 
                SqlCommand command = new SqlCommand(selectUsersQuery, connection); 
                SqlDataReader reader = command.ExecuteReader(); 
                while (reader.Read()) 
                { 
                    Console.WriteLine("UserID: {0}, Username: {1}, Email: {2}, CreatedAt: {3}", reader["UserID"], reader["Username"], reader["Email"], reader["CreatedAt"]); 
                } 
                reader.Close(); 
            } 
            catch (SqlException ex) 
            { 
                Console.WriteLine("Error: " + ex.Message); 
            } 
            finally 
            { 
                connection.Close(); 
            }
            }
        /*
        //Test Loading Some Basic Values into MySQL
            public static void connectionload()
            {
            string connectionString = "server=localhost;port=3307;database=cm_db;uid=cm_user;pwd=CockyMoviePassword;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection successful!\n\n");
                    //string insertquery = "INSERT INTO cm_db.ratings(Name) VALUES(?ratings)";
                    string query = "SELECT * FROM ratings;";
                    //MySqlCommand command2 = new MySqlCommand(insertquery, conn);
                    MySqlCommand command = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["Name"]}");
                        }
                    Console.WriteLine("QED. Thats Roughly Equivalent to PHP Connection String....\n\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\n\n");
                }
            }
        }  */
    }

}
