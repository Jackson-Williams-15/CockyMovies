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

namespace MENUSYSTEM35
{
    public class Menu35
    {

        // Declare 3 variables - id, age and name 
        int id;
        string storecode;
        string manager;
        string state;
       
        // Driver code 
        static public void DiagMenu()
        {

            int exit = 0;
            int number = 0;
            do
            {
                Console.WriteLine("\nSystem Utilities: Cocky Movies & Entertainment[V2.1] Installation and Maintenance.");
                Console.WriteLine("CE Uses a React & ASPX FrontEnd, RESTBackEnd, ASP.NET->LanManager, and COTS Demonstrating The Breadth of Our Team Skills.");
                Console.WriteLine("Please Enter Your Choice:");
                Console.WriteLine("9.Diagnostics Processes, and SQL.Utilities");
                Console.WriteLine("99.Exit:");
                Console.WriteLine("Please Enter Your Choice(9,99(Exit):\n");
                string somestring = "1";
                char myChar = '1';
                somestring = Console.ReadLine();
                //myChar = somestring[0];
                number = Convert.ToInt32(somestring);
                //string upperstring = somestring.ToUpper();
                if (number == 99)
                {
                    Console.WriteLine("\nYou Choose Option 99: (Exit). You Have Selected to Leave. Thank You.");
                    Console.WriteLine("\n\n");
                    exit = 99;
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


                            T4MYSQL.showtables();

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
