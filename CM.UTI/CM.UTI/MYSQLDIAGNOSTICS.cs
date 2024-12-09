using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query;
using T4DATA;

// MyClass.cs using System; 
namespace T4DBMYSQL
{
    class T4MYSQL
    {

        // Test The Use of a Namespace Class Function;

        public void sayMyHelloPing()
        {
            //STEP0 CHECK IF SERVICE IS RUNNING
            //STEP1 CHECK IF QUERY STRINGS RUN PROPERLY FOR SELECT AND INSERT.
            //STEP2 CHECK UPDATE AND DELETE QUERY STRINGS
            //STEP3 RETURN STATUS OF MSPLATFORM. (GREEN, RED, AND YELLOW).
            Console.WriteLine("Hello John. Can we please finish before Christmas?");
        }

        //If MySQL is Selected it will run this function which will then execute the programs as required.
        public void StartMyUtilities()
        {
            Console.WriteLine("Right Now we are not doing anything special.");
        }

        //Test Executing a SQL Statement Against MySQL
        public static void connectiontest()
        {
            string connectionString = "server=localhost;port=3307;database=cm_db;uid=cm_user;pwd=CockyMoviePassword;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command1 = new MySqlCommand(useQuery, conn);
                    command1.CommandText = "USE cm_db;";
                    command1.ExecuteNonQuery();
                    Console.WriteLine("Connected to MySQL.");
                     string query = "SELECT * FROM users;";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["username"]}, Email: {reader["email"]}");
                        }
                        Console.WriteLine("QED. User Table Outputted Successfully....\n\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\n\n");
                }
            }
        }

        public static void connectiontest1()
        {
            string connectionString = "server=localhost;port=3307;database=cm_db;uid=cm_user;pwd=CockyMoviePassword;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command1 = new MySqlCommand(useQuery, conn);
                    command1.CommandText = "USE cm_db;";
                    command1.ExecuteNonQuery();
                    Console.WriteLine("Connected to MySQL.");
                    string query = "SELECT * FROM users;";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["username"]}, Email: {reader["email"]}");
                        }
                        Console.WriteLine("QED. User Table Outputted Successfully....\n\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\n\n");
                }
            }
        }
        public static void connectiontest2()
        {
            string connectionString = "server=localhost;port=3307;database=cm_db;uid=cm_user;pwd=CockyMoviePassword;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command1 = new MySqlCommand(useQuery, conn);
                    command1.CommandText = "USE cm_db;";
                    command1.ExecuteNonQuery();
                    Console.WriteLine("Connected to MySQL.");
                    string query = "SELECT * FROM users;";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["username"]}, Email: {reader["email"]}");
                        }
                        Console.WriteLine("QED. User Table Outputted Successfully....\n\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\n\n");
                }
            }
        }
        public static void connectiontest3()
        {
            string connectionString = "server=localhost;port=3307;database=cm_db;uid=cm_user;pwd=CockyMoviePassword;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command1 = new MySqlCommand(useQuery, conn);
                    command1.CommandText = "USE cm_db;";
                    command1.ExecuteNonQuery();
                    Console.WriteLine("Connected to MySQL.");
                    string query = "SELECT * FROM users;";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["username"]}, Email: {reader["email"]}");
                        }
                        Console.WriteLine("QED. User Table Outputted Successfully....\n\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\n\n");
                }
            }
        }
        public static void connectiontest4()
        {
            string connectionString = "server=localhost;port=3307;database=cm_db;uid=cm_user;pwd=CockyMoviePassword;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command1 = new MySqlCommand(useQuery, conn);
                    command1.CommandText = "USE cm_db;";
                    command1.ExecuteNonQuery();
                    Console.WriteLine("Connected to MySQL.");
                    string query = "SELECT * FROM users;";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["username"]}, Email: {reader["email"]}");
                        }
                        Console.WriteLine("QED. User Table Outputted Successfully....\n\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\n\n");
                }
            }
        }
        public static void connectiontest5()
        {
            string connectionString = "server=localhost;port=3307;database=cm_db;uid=cm_user;pwd=CockyMoviePassword;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command1 = new MySqlCommand(useQuery, conn);
                    command1.CommandText = "USE cm_db;";
                    command1.ExecuteNonQuery();
                    Console.WriteLine("Connection successful!\n\n");
                    string query = "SELECT * FROM users;";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["username"]}, Email: {reader["email"]}");
                        }
                        Console.WriteLine("QED. User Table Outputted Successfully....\n\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\n\n");
                }
            }
        }
        public static void connectiontest6()
        {
            string connectionString = "server=localhost;port=3307;database=cm_db;uid=cm_user;pwd=CockyMoviePassword;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command1 = new MySqlCommand(useQuery, conn);
                    command1.CommandText = "USE cm_db;";
                    command1.ExecuteNonQuery();
                    Console.WriteLine("Connection successful!\n\n");
                    string query = "SELECT * FROM users;";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["username"]}, Email: {reader["email"]}");
                        }
                        Console.WriteLine("QED. User Table Outputted Successfully....\n\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\n\n");
                }
            }
        }

        public static void showtables()
        {
            string connectionString = "server=localhost;port=3307;database=cm_db;uid=cm_user;pwd=CockyMoviePassword;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    int counter = 1;
                    string useQuery = "USE cm_db;";
                    conn.Open();
                    MySqlCommand command1 = new MySqlCommand(useQuery, conn);
                    command1.CommandText = "USE cm_db;";
                    command1.ExecuteNonQuery();
                    Console.WriteLine("Connection successful!\n\n");
                    string query = "show tables";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID:{counter},{reader["Tables_in_cm_db"]}");
                            counter = counter + 1;
                        }
                        Console.WriteLine("QED. TableList Outputted Successfully....\n\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\n\n");
                }
            }
        }

    }
}
