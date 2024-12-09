using System;
using System.Collections;
using System.Security.Policy;
using System.Diagnostics;

//THIS IS THE PRIMARY DATA SET FOR OUR PROJECT. IF IT DOESNT EXIST IT CAN BE LOADED.

namespace T4SWAGGER
{
public class T4SWAGGERDUMP
{
        
        static public void callswaggerinfo()
        {
            string swaggerurl = "http://class.usc547team4.info:8080/swagger/swagger.json";
            Console.WriteLine($"Paste the Following URL in your Browser(When API is Running): {swaggerurl}");
            Console.WriteLine("Would You like to Launch This URL? yes or no");
            string youranswer = Console.ReadLine();

            if (youranswer == "yes")
            {
                // Alternatively, open the URL in the default web browser
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = swaggerurl,
                    UseShellExecute = true
                }
                );
            }
            else
            {
                Console.WriteLine("You Answered No or Some Other Value. Have a Nice Day!");
            }
            
        }

    }
}
