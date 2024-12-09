using System;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace T4MSSQLINSTALLER
{

    public class MSSQLINSTALLER
    {
        public static void createmssqldb()
        {
            string connectionString = "Data Source=TEAM4LAB2\\COCKYMSSQL;Initial Catalog=COCKY;Integrated Security=True;";
            Console.WriteLine("Your Current Connection String is:" + connectionString + "If This is incorrect your installation will give an error.");
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                // Create database
                string createDbQuery = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'COCKY') CREATE DATABASE COCKY;";
                SqlCommand command = new SqlCommand(createDbQuery, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("Database created successfully OR Already Exists.");

                // Switch to the new database
                connection.ChangeDatabase("COCKY");

                // Create Users table
                string createUsersTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TestUsers' and xtype='U')
                CREATE TABLE TestUsers (
                    UserID INT IDENTITY(1,1) PRIMARY KEY,
                    Username NVARCHAR(50) NOT NULL,
                    Email NVARCHAR(100),
                    CreatedAt DATETIME DEFAULT GETDATE()
                );";
                command.CommandText = createUsersTableQuery;
                command.ExecuteNonQuery();
                Console.WriteLine("Table 'TestUsers' created successfully OR Already Exists.");

                // Create Posts table
                string createPostsTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TestPosts' and xtype='U')
                CREATE TABLE TestPosts (
                    PostID INT IDENTITY(1,1) PRIMARY KEY,
                    UserID INT,
                    Title NVARCHAR(100) NOT NULL,
                    Content NVARCHAR(MAX),
                    CreatedAt DATETIME DEFAULT GETDATE(),
                    FOREIGN KEY (UserID) REFERENCES TestUsers(UserID)
                );";
                command.CommandText = createPostsTableQuery;
                command.ExecuteNonQuery();
                Console.WriteLine("Table 'TestPosts' created successfully OR Already Exists.");

                // Create Comments table
                string createCommentsTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TestComments' and xtype='U')
                CREATE TABLE TestComments (
                    CommentID INT IDENTITY(1,1) PRIMARY KEY,
                    PostID INT,
                    UserID INT,
                    Content NVARCHAR(MAX),
                    CreatedAt DATETIME DEFAULT GETDATE(),
                    FOREIGN KEY (PostID) REFERENCES TestPosts(PostID),
                    FOREIGN KEY (UserID) REFERENCES TestUsers(UserID)
                );";
                command.CommandText = createCommentsTableQuery;
                command.ExecuteNonQuery();
                Console.WriteLine("Table 'TestComments' created successfully OR Already Exists.");

                // Create a new user and grant privileges
                string createUserQuery = @"
                IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'cm_user')
                CREATE LOGIN cm_user WITH PASSWORD = 'cm_password';
                IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'cm_user')
                CREATE USER cm_user FOR LOGIN cm_user;
                EXEC sp_addrolemember N'db_owner', N'cm_user';";
                command.CommandText = createUserQuery;
                command.ExecuteNonQuery();
                Console.WriteLine("User 'cm_user' created and granted privileges successfully.");
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
    }
}
        /*
        public void MSSQLinstall()
        {
            string MyConnectionString = "ConnectionString=127.0.0.1;Catalog=COCKY;Username=sa;Password=*Columbia1";
            //STEP0 -> ASK FOR SIMPLEX(SINGLE DB, OR FISMA HIGH INSTALLATION(DECOMPOSED)

            //STEP1 -> CHECK IF DATABASE COCKY, COCKYSSO, COCKYSHOP AND VELOCITY EXISTS

            //STEP2A -> IF SIMPLEX MODE, AND DATABASE DOESNT EXIST CREATE COCKY

            //STEP2B -> IF COMPLEX MODE, AND DATABASES DONT EXIST CREATE THEM ALL AND RUN SCHEMA BUILDER IN MULTIPLE STEPS.

            //STEP3 -> LOAD LIST DATA INTO THE DATABASE

        }

    } }
/*static void BuildProducts() 
    { 
    string connectionString = "server=localhost;port=3307;database=cm_db;uid=cm_user;pwd=CockyMoviePassword;";
   
    using (MySqlConnection conn = new MySqlConnection(connectionString)) 
    { 
    conn.Open(); 
    foreach (var product in T4LISTS.products) 
    { 
    string query = "INSERT INTO Products (ProductName, Price) VALUES (@ProductName, @Price)"; 
    using (MySqlCommand cmd = new MySqlCommand(query, conn)) 
    { 
    cmd.Parameters.AddWithValue("@ProductName", product.ProductName); 
    cmd.Parameters.AddWithValue("@Price", product.Price); 
    cmd.ExecuteNonQuery(); 
    } 
    } 
    } 
    Console.WriteLine("Products have been inserted into the database."); 
    }
}
}*/