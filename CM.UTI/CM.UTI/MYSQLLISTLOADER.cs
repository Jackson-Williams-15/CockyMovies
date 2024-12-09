using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query;
using MySql.Data.MySqlClient;
using T4DATA;
using T4DBMYSQL;
using T4DBMSSQL;
using T4DATA;
using T4MYSQLINSTALLER;
using T4MSSQLINSTALLER;
using static T4DATA.T4LISTS;
using Google.Protobuf.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MENUSYSTEM36
{
    public class Menu36
    {

        // Declare 3 variables - id, age and name 
        int id;
        string storecode;
        string manager;
        string state;
        string connectionString = "server=localhost;user=root;password=test12345;port=3307";
        public class Product
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
        }

        public override string ToString()
        {
            return "LinqSelected:" + id + " " + manager + "        " + storecode + "," + state;
        }

        // Driver code 
        static public void DiagMenu()
        {
            string connectionString = "server=localhost;user=root;password=test12345;port=3307";
            T4LISTS OurLists2 = new T4LISTS();
            List<Menu36> stores = new List<Menu36>()
    {           
        // Create 5 Student details 
        new Menu36{ id = 101, manager = "Sravan", storecode = "Forrest33", state = "SC" },
        new Menu36{ id = 102, manager = "deepu", storecode = "Chicago01" , state = "IL"},
        new Menu36{ id = 103, manager = "manoja", storecode = "Dallas01" , state= "TX"},
        new Menu36{ id = 104, manager = "Sathwik", storecode = "Sacramento01" ,  state="CA" },
        new Menu36{ id = 105, manager = "Saran", storecode = "Seatle01", state = "WA" },
        new Menu36{ id = 106, manager = "Joe", storecode = "Smithfield01", state = "VA" },
        new Menu36{ id = 107, manager = "April", storecode = "Stohrs01", state = "VT" }
    };

            List<Product> products = new List<Product>
        {
        new Product { ProductName = "Sr. Ticket", Price = 5.99m },
        new Product { ProductName = "Child Ticket", Price = 4.99m },
        new Product { ProductName = "Adult Ticket", Price = 8.99m }
        };
            int exit = 0;
            int number = 0;
            do
            {
                Console.WriteLine("\nSystem Utilities: Cocky Movies & Entertainment[V2.1] Installation and Maintenance.");
                Console.WriteLine("Items #1-5 Load Data Into the System Tables.");
                Console.WriteLine("Please Enter Your Choice:");
                Console.WriteLine("1.Movies:");
                Console.WriteLine("2.Genres:");
                Console.WriteLine("3.Ratings:");
                Console.WriteLine("4.Showtimes:");
                Console.WriteLine("5.Regions:");
                Console.WriteLine("99.Exit:");
                Console.WriteLine("Please Enter Your Choice(1,2,3,4,5,99(Exit):\n");
                string somestring = "1";
                char myChar = '1';
                somestring = Console.ReadLine();
                //myChar = somestring[0];
                number = Convert.ToInt32(somestring);
                //string upperstring = somestring.ToUpper();
                if (number == 99)
                {
                    Console.WriteLine("\nYou Choose Option E: (Exit). You Have Selected to Leave. Thank You.");
                    Console.WriteLine("\n\n");
                    exit = 99;
                }
                else if (number == 1)
                {
                    Console.WriteLine("You Choose Option: 1-Movies\n");
                    Console.WriteLine(String.Join("\n", OurLists2.movies));
                    Console.WriteLine("\n\n");
                    //Directory.SetCurrentDirectory(pString);
                    //Process.Start("powershell.exe", "cls");
                    InsertDataIntoMySQL1(connectionString, OurLists2.movies);
                    exit = 1;
                }
                else if (number == 2)
                {
                    Console.WriteLine("You Choose Option: 2-Genres\n");
                    Console.WriteLine(String.Join("\n", OurLists2.genres));
                    Console.WriteLine("\n\n");
                    InsertDataIntoMySQL2(connectionString, OurLists2.genres);
                    exit = 2;
                }
                else if (number == 3)
                {
                    Console.WriteLine("You Choose Option: 3-Ratings\n");
                    Console.WriteLine(String.Join("\n", OurLists2.ratings));
                    Console.WriteLine("\n\n");
                    InsertDataIntoMySQL3(connectionString, OurLists2.ratings);
                    exit = 3;
                }
                else if (number == 4)
                {
                    Console.WriteLine("You Choose Option: 4-Showtimes\n");
                    Console.WriteLine(String.Join("\n", OurLists2.showtimes));
                    Console.WriteLine("\n\n");
                    // Insert data into MySQL database
                    InsertDataIntoMySQL4(connectionString, OurLists2.showtimes);

                    exit = 4;
                }
                else if (number == 5)
                {
                    Console.WriteLine("You Choose Option: 5-Regions\n");
                    Console.WriteLine(String.Join("\n", OurLists2.regions));
                    Console.WriteLine("\n\n");
                    InsertDataIntoMySQL5(connectionString, OurLists2.regions);
                    exit = 5;
                }
                else if (number == 6)
                {
                    Console.WriteLine("You Choose Option: 6-Stores\n");
                    // Iterate the Stores by selecting stores // name starts with S  // Using Where() function 
                    IEnumerable<Menu36> Query = stores.Where(s => s.storecode[0] == 'S');

                    // Display employee details 
                    Console.WriteLine("LinqSelected:ID  Manager              Storecode State");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
                    foreach (Menu36 e in Query)
                    {

                        // Call the to string method 
                        Console.WriteLine(e.ToString());
                    }
                    Console.WriteLine("\n\n");
                    InsertDataIntoMySQL6(connectionString, OurLists2.stores);
                    exit = 6;
                }
                else
                {
                    Console.WriteLine("\nYou Have Selected to Leave. Thank You.");
                    Console.WriteLine("\n\n");
                    exit = 99;
                }
                somestring = null;
            } while (exit != 99);


        }

               
        static void InsertDataIntoMySQL1(string connectionString, List<string> dataList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command = new MySqlCommand(useQuery, conn);
                    command.CommandText = "USE cm_db;";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Connected to MySQL.");

                    foreach (string data in dataList)
                    {
                        string query = "INSERT INTO movies (title) VALUES (@MOVIENAME)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MOVIENAME", data);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Inserted: {data}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        static void InsertDataIntoMySQL2(string connectionString, List<string> dataList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command = new MySqlCommand(useQuery, conn);
                    command.CommandText = "USE cm_db;";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Connected to MySQL.");

                    foreach (string data in dataList)
                    {
                        string query = "INSERT INTO genres (Name) VALUES (@GENRES)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@GENRES", data);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Inserted: {data}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        static void InsertDataIntoMySQL3(string connectionString, List<string> dataList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command = new MySqlCommand(useQuery, conn);
                    command.CommandText = "USE cm_db;";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Connected to MySQL.");

                    foreach (string data in dataList)
                    {
                        string query = "INSERT INTO ratings (NAME) VALUES (@RATINGNAME)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@RATINGNAME", data);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Inserted: {data}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        static void InsertDataIntoMySQL4(string connectionString, List<string> dataList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command = new MySqlCommand(useQuery, conn);
                    command.CommandText = "USE cm_db;";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Connected to MySQL.");

                    foreach (string data in dataList)
                    {
                        string query = "INSERT INTO showtime (StartTime, MovieID) VALUES (@STARTTIME, @MOVIEID)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@STARTTIME", data);
                        cmd.Parameters.AddWithValue("@MOVIEID", 1);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Inserted: {data}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        static void InsertDataIntoMySQL5(string connectionString, List<string> dataList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command = new MySqlCommand(useQuery, conn);
                    command.CommandText = "USE cm_db;";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Connected to MySQL.");

                    foreach (string data in dataList)
                    {
                        string query = "INSERT INTO region (hqregion) VALUES (@REGIONAME)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@REGIONAME", data);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Inserted: {data}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
            
           
        static void InsertDataIntoMySQL6(string connectionString, IEnumerable<Store> stores)
        {
            
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string useQuery = "USE cm_db;";
                    MySqlCommand command = new MySqlCommand(useQuery, conn);
                    command.CommandText = "USE cm_db;";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Connected to MySQL.");

                    foreach (Store store in stores)
                    {
                        string query = "INSERT INTO stores (id,managername,storeid,regionstate ) VALUES (@SITEID, @MANAGERNAME, @STORECODE, @STATE)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@SITEID", store.id);
                        cmd.Parameters.AddWithValue("@MANAGERNAME", store.manager);
                        cmd.Parameters.AddWithValue("@STORECODE", store.storecode);
                        cmd.Parameters.AddWithValue("@STATE", store.state);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Inserted: {store.id} with StoreCode: {store.storecode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }


}


