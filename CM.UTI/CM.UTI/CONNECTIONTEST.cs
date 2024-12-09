using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CONNECTIONTEST
{
    internal class PINGME
    {
        static public void runpings()
        {
            int exit = 0;
            do
            {
            Console.WriteLine("\nSystemCockyEntertainment[V2.1] Installation and Maintenance Utilities");
            Console.WriteLine("Would You Like to Run Tests To Your SQL Servers?");
            Console.WriteLine("CE Uses a React & ASPX FrontEnd, RESTBackEnd, ASP.NET->LanManager, and Integrating COTS Demonstrating The Breadth of Our Team Skills.");
            Console.WriteLine("Please Enter Your Choice:");
            Console.WriteLine("1. Press One to Test Your MySQL Server:");
            Console.WriteLine("2. Press Two to Test Your MSSQL Server:");
            Console.WriteLine("3. Test Both Server Types:");
            Console.WriteLine("99.Exit:");
            Console.WriteLine("Please Enter Your Choice(1,2,3,99):\n");
            string youranswer = Console.ReadLine();
            if (youranswer == "1")
            {
                pingmssql();
            }
            else if (youranswer == "2")
            {
                pingmysql();
            }
            else if (youranswer == "3")
            {
                pingmysql();
                pingmssql();
            }
            else if (youranswer == "99")
            {
                    Console.WriteLine("You Selected 99(Exit). Have a Nice Day");
                    exit = 99;
            }
                else 
            { 
                Console.WriteLine("Invalid Option. Leaving");
                exit = 99;
            }
            } while (exit != 99);
            
            Console.WriteLine("Press Any Key and Hit Return to Close Window");
            Console.ReadLine(); // Keep the console window open
        }
        static public void pingmysql()
        {
            Console.WriteLine("MYSQL: This program runs an MS-PS Connection Test to the Current DBMS Port And Returns the Results To The Screen");
            // Define the server name and port
            string server = "127.0.0.1";
            int port = 3307;

            // Create the PowerShell command
            string command = $"Test-NetConnection -ComputerName {server} -Port {port}";

            // Set up the process start info
            ProcessStartInfo psi = new ProcessStartInfo("powershell", $"-Command \"{command}\"")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Start the process and get the output
            using (Process process = Process.Start(psi))
            {
                using (System.IO.StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
           
        }

        static public void pingmssql()
        {
                Console.WriteLine("MSSQL:This program runs an MS-PS Connection Test And Returns the Results To The Screen");
                // Define the server name and port
                string server = "127.0.0.1";
                int port = 1433;

                // Create the PowerShell command
                string command = $"Test-NetConnection -ComputerName {server} -Port {port}";

                // Set up the process start info
                ProcessStartInfo psi = new ProcessStartInfo("powershell", $"-Command \"{command}\"")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Start the process and get the output
                using (Process process = Process.Start(psi))
                {
                    using (System.IO.StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine(result);
                    }
                }
            }
        }
}
