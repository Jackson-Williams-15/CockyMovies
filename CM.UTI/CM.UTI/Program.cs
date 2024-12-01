using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;
using T4DBMYSQL;
using T4DBMSSQL;
using MENUSYSTEM;
using T4DATA;
using static T4DATA.T4LISTS;
using T4MYSQLINSTALLER;
using T4MSSQLINSTALLER;


public class Program
{
    
    // Driver code 
    static void Main(string[] args) 
{
        
        Menu33.DiagMenu();
     /*
      
    //FIRST DEMONSTRATE - USE OF LISTS - PHASE I REQUIREMENT - AND INEUMERABLE DATA STRUCTURE 

    // Declare list variables Before Building a Menu System.  
    // List Variables Have Been Moved to a Seperate Namespace in lists.cs which just has the type declarations, and constructors;
    //T4MYSQL MyDbclass = new T4MYSQL();
    //T4MSSQL MSDbclass = new T4MSSQL();
    T4LISTS OurLists = new T4LISTS();
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
        Console.WriteLine("0.Products:");
        Console.WriteLine("1.Movies:");
        Console.WriteLine("2.Genres:");
        Console.WriteLine("3.Ratings:");
        Console.WriteLine("4.Showtimes:");
        Console.WriteLine("5.Regions:");
        Console.WriteLine("6.Stores:");
        Console.WriteLine("7.Employees:");
        Console.WriteLine("8.Users:");
        Console.WriteLine("9.Diagnostics Processes, and SQL.Utilities");
        Console.WriteLine("10.Installation(MySQL or MSSQL), and Data Manipulation:");
        Console.WriteLine("E.Exit:");
        Console.WriteLine("Please Enter Your Choice(0,1,2,3,4,5,6,7,8,9,10,E):\n");
        string somestring = null;
        somestring = Console.ReadLine();
        number = Convert.ToInt32(somestring);
        if (somestring[0] == 'E')
        {
          exit = 99;
        }
        else if (number == 0) 
        {
            Console.WriteLine("You Choose Option: 0-Products\n");
            Console.WriteLine(String.Join("\n", OurLists.products[0]));
            Console.WriteLine("\n\n");
            //Directory.SetCurrentDirectory(pString);
            //Process.Start("powershell.exe", "cls");
            exit = 0;
        }
        else if (number == 1) 
        {
            Console.WriteLine("You Choose Option: 1-Movies\n");
            Console.WriteLine(String.Join("\n", OurLists.movies));
            Console.WriteLine("\n\n");
            //Directory.SetCurrentDirectory(pString);
            //Process.Start("powershell.exe", "cls");
            exit = 1;
        }
        else if(number == 2) 
        {
            Console.WriteLine("You Choose Option: 2-Genres\n");
            Console.WriteLine(String.Join("\n", OurLists.genres));
            Console.WriteLine("\n\n");
            exit = 2;
        }
        else if(number == 3) 
        {
            Console.WriteLine("You Choose Option: 3-Ratings\n");
            Console.WriteLine(String.Join("\n", OurLists.ratings));
            Console.WriteLine("\n\n");
            exit = 3;
        }
        else if(number == 4) 
        {
            Console.WriteLine("You Choose Option: 4-Showtimes\n");
            Console.WriteLine(String.Join("\n", OurLists.showtimes));
            Console.WriteLine("\n\n");
            exit = 4;
        }
        else if (number == 5) 
        {
            Console.WriteLine("You Choose Option: 5-Regions\n");
            Console.WriteLine(String.Join("\n", OurLists.regions));
            Console.WriteLine("\n\n");
            exit = 5;
        }
        else if(number == 6) 
        {
            Console.WriteLine("You Choose Option: 6-Stores\n");
            Console.WriteLine(String.Join("\n", OurLists.sstores));
           // Iterate the Stores by selecting stores // name starts with S  // Using Where() function 

                // Print the contents of the list
                Console.WriteLine("The contents of the data structures list are:");
                        
            IEnumerable<T4LISTS.Store> Query = OurLists.stores.Where(s => s.storecode[0] == 'S'); 
      
            // Display employee details 
            Console.WriteLine("LinqSelected:ID  Manager              Storecode State"); 
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++"); 
            foreach (T4LISTS.Store e in Query) 
            { 
          
            // Call the to string method 
            Console.WriteLine(e.ToString()); 
            }     
            Console.WriteLine("\n\n");
            exit = 6;
                          
            }
        else if(number == 7) 
        {
            Console.WriteLine("You Choose Option: 7-Employees\n");
            Console.WriteLine(String.Join("\n", OurLists.employees));
            Console.WriteLine("\n\n");
            exit = 7;
        }
        else if(number == 8) 
        {
            Console.WriteLine("You Choose Option: 8-Users\n");
            Console.WriteLine(String.Join("\n", OurLists.users));
            Console.WriteLine("\n");
            exit = 8;
        }
        else if(number == 9) 
        {
            Console.WriteLine("You Choose Option: 9-SQL Diagnostics\n");
            //T4MYSQL.connectiontest();     
            Console.WriteLine(String.Join("\n", OurLists.users));
            //Console.WriteLine("\n");
            exit = 9;
        }
        else if(number == 10) 
        {
            Console.WriteLine("You Choose Option: 10-Installation/LoadData\n");
            //T4MYSQL.connectionload();     
            Console.WriteLine(String.Join("\n", OurLists.users));
            //Console.WriteLine("\n");
            exit = 10;
        }
        else 
        {
            Console.WriteLine("\nYou Have Selected to Leave. Thank You.");
            Console.WriteLine("\n\n");
            exit = 99;
        }
        
    }while(exit != 99);
*/

}

    public override string ToString() 
    { 
    return "LinqSelected:";// + id + " " + manager + "        " + storecode + "," + state; 
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

  // CODEPILOT OUTPUT OF PRINTING INENUMERABLE LIST
Create a list to hold different data structures
            List<object> dataStructures = new List<object>();

            // Add various data structures to the list
            dataStructures.Add(new int[] { 1, 2, 3, 4, 5 }); // Array
            dataStructures.Add(new Dictionary<string, int> { { "One", 1 }, { "Two", 2 } }); // Dictionary
            dataStructures.Add(new List<string> { "Apple", "Banana", "Cherry" }); // List
        foreach (var item in dataStructures)
        {
            if (item is Array array)
            {
                Console.WriteLine("Array: " + string.Join(", ", array));
            }
            else if (item is Dictionary<string, int> dictionary)
            {
                Console.WriteLine("Dictionary: " + string.Join(", ", dictionary.Select(kvp => $"{kvp.Key}: {kvp.Value}")));
            }
            else if (item is List<string> list)
            {
                Console.WriteLine("List: " + string.Join(", ", list));
            }
        }
 */