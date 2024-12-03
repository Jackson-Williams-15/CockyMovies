﻿using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;
using T4DBMYSQL;
using T4DBMSSQL;
using MENUSYSTEM33;
using MENUSYSTEM34;
using MENUSYSTEM35;
using T4DATA;
using static T4DATA.T4LISTS;
using T4MYSQLINSTALLER;
using T4MSSQLINSTALLER;


public class Program
{

    // Driver code 
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome To CockyMovies & Entertainment.");
        Console.WriteLine("Please Review the Following Menu Options Below.");
        //Menu33.DiagMenu();


        //FIRST DEMONSTRATE - USE OF LISTS - PHASE I REQUIREMENT - AND INEUMERABLE DATA STRUCTURE 

        // Declare list variables Before Building a Menu System.  
        // List Variables Have Been Moved to a Seperate Namespace in lists.cs which just has the type declarations, and constructors;
        //T4MYSQL MyDbclass = new T4MYSQL();
        //T4MSSQL MSDbclass = new T4MSSQL();
        //T4LISTS OurLists = new T4LISTS();
        // USING SQL WAS A A PHASE II REQUIRMENET TO TEST PLATFORM FROM SQL/ENTITY FRAMEWORK.
        // Phase II Requirements were met in Option 9 Which creates Databases if they dont exist.
        // We are Running Diagnostic Queries Against Both DB Engines which should be up or they should return not in action.
        //MyDbclass.sayMyHelloPing();
        //MSDbclass.sayMSHelloPing();

        // LOOP CONTROL VARIABLES AND SHELL VARIABLE REQUIREMENTS FOR SYSTEM SHELLS
        int exit = 0;
        int number = 0;
        string pString = "c:/windows/system32/WindowsPowerShell/v1.0/";
        do
        {
            Console.WriteLine("\nSystemCockyEntertainment[V2.1] Installation and Maintenance Utilities");
            Console.WriteLine("CE Uses a React & ASPX FrontEnd, RESTBackEnd, ASP.NET->LanManager, and COTS Demonstrating The Breadth of Our Team Skills.");
            Console.WriteLine("Please Enter Your Choice:");
            Console.WriteLine("0.Review Seed Data:");
            Console.WriteLine("1.Review Current Table Information:");
            Console.WriteLine("2.Create DBMS and Tables:");
            Console.WriteLine("3.Load Data Into System:");
            Console.WriteLine("99.Exit:");
            Console.WriteLine("Please Enter Your Choice(0,1,2,3,99):\n");
            string somestring = null;
            somestring = Console.ReadLine();
            number = Convert.ToInt32(somestring);
            if (number == 99)
            {
                Console.WriteLine("\nYou Have Selected to Leave. Thank You.");
                Console.WriteLine("\n\n");
                exit = 99;
            }
            else if (number == 0)
            {
                Console.WriteLine("You Choose Option: 0-Review Seed Data\n");
                Menu33.DiagMenu();
                exit = 99;
            }
            else if (number == 1)
            {
                Console.WriteLine("You Choose Option: 1-Review Current Table Information\n");
                Console.WriteLine("\n\n");
                Menu35.DiagMenu();
                exit = 99;
            }
            else if (number == 2)
            {
                Console.WriteLine("You Choose Option: 2-Create DBMS and Tables\n");
                Menu34.DiagMenu();
                exit = 99;
            }
            else if (number == 3)
            {
                Console.WriteLine("You Choose Option: 3-Load Data Into the System\n");
                Menu34.DiagMenu();
                exit = 99;
            }
            else
            {
                Console.WriteLine("\nYou Have Made an Unknown Choice. Please Try Again Thank You.");
                Console.WriteLine("\n\n");
                exit = 1;
            }

        } while (exit != 99);
    }
    }
