﻿using System;
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
using MENUSYSTEM;
using T4DATA;
using T4MYSQLINSTALLER;
using T4MSSQLINSTALLER;
using static T4DATA.T4LISTS;
using Google.Protobuf.Collections;

namespace MENUSYSTEM
{
    public class Menu33
    {

        // Declare 3 variables - id, age and name 
        int id;
        string storecode;
        string manager;
        string state;

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
            T4LISTS OurLists2 = new T4LISTS();

            List<Menu33> stores = new List<Menu33>()
    { 
          
        // Create 5 Student details 
        new Menu33{ id = 101, manager = "Sravan", storecode = "Forrest33", state = "SC" },
        new Menu33{ id = 102, manager = "deepu", storecode = "Chicago01" , state = "IL"},
        new Menu33{ id = 103, manager = "manoja", storecode = "Dallas01" , state= "TX"},
        new Menu33{ id = 104, manager = "Sathwik", storecode = "Sacramento01" ,  state="CA" },
        new Menu33{ id = 105, manager = "Saran", storecode = "Seatle01", state = "WA" },
        new Menu33{ id = 106, manager = "Joe", storecode = "Smithfield01", state = "VA" },
        new Menu33{ id = 107, manager = "April", storecode = "Stohrs01", state = "VT" }
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
                Console.WriteLine("\nSystemCockyEntertainment[V2.1] Installation and Maintenance Utilities");
                Console.WriteLine("CE Uses a React & ASPX FrontEnd, RESTBackEnd, ASP.NET->LanManager, and COTS Demonstrating The Breadth of Our Team Skills.");
                Console.WriteLine("Items #1-8 Show Our Default Data To Be Loaded Into the System At Instllation Time. 9 & 10 Are Production Data.");
                Console.WriteLine("Please Enter Your Choice:");
                Console.WriteLine("1.Movies:");
                Console.WriteLine("2.Genres:");
                Console.WriteLine("3.Ratings:");
                Console.WriteLine("4.Showtimes:");
                Console.WriteLine("5.Regions:");
                Console.WriteLine("6.Stores:");
                Console.WriteLine("7.Employees:");
                Console.WriteLine("8.Users:");
                Console.WriteLine("9.Diagnostics Processes, and SQL.Utilities");
                Console.WriteLine("10.Installation(MySQL or MSSQL)");
                Console.WriteLine("11.Load Sample Data");
                Console.WriteLine("99.Exit:");
                Console.WriteLine("Please Enter Your Choice(1,2,3,4,5,6,7,8,9,10,11,99(Exit):\n");
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
                    exit = 1;
                }
                else if (number == 2)
                {
                    Console.WriteLine("You Choose Option: 2-Genres\n");
                    Console.WriteLine(String.Join("\n", OurLists2.genres));
                    Console.WriteLine("\n\n");
                    exit = 2;
                }
                else if (number == 3)
                {
                    Console.WriteLine("You Choose Option: 3-Ratings\n");
                    Console.WriteLine(String.Join("\n", OurLists2.ratings));
                    Console.WriteLine("\n\n");
                    exit = 3;
                }
                else if (number == 4)
                {
                    Console.WriteLine("You Choose Option: 4-Showtimes\n");
                    Console.WriteLine(String.Join("\n", OurLists2.showtimes));
                    Console.WriteLine("\n\n");
                    exit = 4;
                }
                else if (number == 5)
                {
                    Console.WriteLine("You Choose Option: 5-Regions\n");
                    Console.WriteLine(String.Join("\n", OurLists2.regions));
                    Console.WriteLine("\n\n");
                    exit = 5;
                }
                else if (number == 6)
                {
                    Console.WriteLine("You Choose Option: 6-Stores\n");
                    // Iterate the Stores by selecting stores // name starts with S  // Using Where() function 
                    IEnumerable<Menu33> Query = stores.Where(s => s.storecode[0] == 'S');

                    // Display employee details 
                    Console.WriteLine("LinqSelected:ID  Manager              Storecode State");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
                    foreach (Menu33 e in Query)
                    {

                        // Call the to string method 
                        Console.WriteLine(e.ToString());
                    }
                    Console.WriteLine("\n\n");
                    exit = 6;
                }
                else if (number == 7)
                {
                    Console.WriteLine("You Choose Option: 7-Employees\n");
                    Console.WriteLine(String.Join("\n", OurLists2.employees));
                    Console.WriteLine("\n\n");
                    exit = 7;
                }
                else if (number == 8)
                {
                    Console.WriteLine("You Choose Option: 8-Users\n");
                    Console.WriteLine(String.Join("\n", OurLists2.users));
                    Console.WriteLine("\n");
                    exit = 8;
                }
                else if (number == 9)
                {
                    Console.WriteLine("You Choose Option: 9-UsersBySQL - QUERY DATA\n");
                    Console.WriteLine("Enter O for OracleMysql, or M for Microsoft Sql Server, E to Exit to Previous Menu");
                    do
                    {
                        somestring = Console.ReadLine();
                        if (somestring[0] == 'O')
                        {
                            T4MYSQL.connectiontest();
                            exit = 101;
                        }
                        else if (somestring[0] == 'M')
                        {
                            T4MSSQL.connectiontest();
                            exit = 101;
                        }
                        else if (somestring[0] == 'E')
                        {
                            exit = 101;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Selection. Please Try Again or E to Exit to Previous Menu");
                        }
                    } while (exit != 101);
                    //Console.WriteLine(String.Join("\n", users));
                    //Console.WriteLine("\n");
                    exit = 9;
                }
                else if (number == 10)
                {
                    Console.WriteLine("You Choose Option: 10-Installation\n");
                    Console.WriteLine("Enter O for OracleMysql, or M for Microsoft Sql Server, E to Exit to Previous Menu");
                    do
                    {
                        somestring = Console.ReadLine();
                        if (somestring[0] == 'O')
                        {
                            MYSQLINSTALLER.createmysqldb();
                            exit = 101;
                        }
                        else if (somestring[0] == 'M')
                        {
                            MSSQLINSTALLER.createmssqldb(); 
                            exit = 101;
                        }
                        else if (somestring[0] == 'E')
                        {
                            exit = 101;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Selection. Please Try Again or E to Exit to Previous Menu");
                        }
                    } while (exit != 101);
                    exit = 10;
                }
                else if (number == 11)
                {
                    Console.WriteLine("You Choose Option: 11-Load Sample Data\n");
                    //T4MYSQL.connectionload();
                    //Console.WriteLine(String.Join("\n", users));
                    //Console.WriteLine("\n");
                    exit = 11;
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
    }
}
/*
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = "cls",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
*/