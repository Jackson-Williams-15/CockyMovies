using System;
using System.Collections;

//THIS IS THE PRIMARY DATA SET FOR OUR PROJECT. IF IT DOESNT EXIST IT CAN BE LOADED.

namespace T4DATA
{
class T4LISTS
{
    public class Store
    {
// Declare 3 variables - id, age and name 
    public int id;  
    public string storecode; 
    public string manager; 
    public string state;
    }

    public class Product 
    { 
    public int ProductID { get; set; } 
    public string ProductName { get; set; } 
    public decimal Price { get; set; }
    }

        public List<Store> stores = new List<Store>()
        { 
            // Create 5 Student details 
        new Store{ id = 101, manager = "Sravan", storecode = "Forrest33", state = "SC" },
        new Store{ id = 102, manager = "deepu", storecode = "Chicago01" , state = "IL"},
        new Store{ id = 103, manager = "manoja", storecode = "Dallas01" , state= "TX"},
        new Store{ id = 104, manager = "Sathwik", storecode = "Sacramento01" ,  state="CA" },
        new Store{ id = 105, manager = "Saran", storecode = "Seatle01", state = "WA" },
        new Store{ id = 106, manager = "Joe", storecode = "Smithfield01", state = "VA" },
        new Store{ id = 107, manager = "April", storecode = "Stohrs01", state = "VT" }
        };

        public List<Product> products = new List<Product>
        {
        new Product { ProductName = "Sr. Ticket", Price = 5.99m },
        new Product { ProductName = "Child Ticket", Price = 4.99m },
        new Product { ProductName = "Adult Ticket", Price = 8.99m }
        };

        //This is the Database Parameters for Site Load
        public List<string> users = new List<string>() { "john@test.com", "ryan@gmail.com", "brenan@microsoft.com", "scott@wsfs.com", "jackson@fort.com", "liam@taken.com" };
        public List<string> ratings = new List<string>() { "G", "PG", "R", "Adult", "History", "PG-13" };
        public List<string> movies = new List<string>() { "TopGun2", "BlueCrush", "IronMan2"};
        public List<string> genres = new List<string>() { "70s", "80s", "90s"};
        public List<string> regions = new List<string>() {"NE", "SE", "NC", "SC", "SW", "NW"};
        public List<string> showtimes = new List<string>() {"1:30", "3:30", "5:30", "7:30", "9:30"};
        public List<string> employees = new List<string>() {"John", "Hannah", "Lucy", "Bill"};
        public List<string> sstores = new List<string>() {"Forrest33", "Chicago01", "Dallas01", "Seattle01", "Sacremento01"};
     
}
}
