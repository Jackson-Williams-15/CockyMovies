using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CM.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Asn1.X509;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Mysqlx.Crud;


namespace T4MYSQLINSTALLER
{
    internal class MYSQLINSTALLER
    {
        public static void createmysqldb()
        { 
            string connectionString = "server=localhost;user=root;password=test12345;port=3307";
            Console.WriteLine("Your Current Connection String is:" + connectionString + "If This is incorrect your installation will give an error.");
            MySqlConnection connection = new MySqlConnection(connectionString); 
            try { 
                connection.Open(); 
                string createDbQuery = "CREATE DATABASE IF NOT EXISTS cm_db;"; 
                MySqlCommand command = new MySqlCommand(createDbQuery, connection); 
                command.ExecuteNonQuery(); 
                Console.WriteLine("Database created successfully OR Already Exists.");

                //COTS DATABASE SHELLS ONLY
                string createDbQuery2 = "CREATE DATABASE IF NOT EXISTS cockysso;";
                MySqlCommand command2 = new MySqlCommand(createDbQuery2, connection);
                command2.ExecuteNonQuery();
                Console.WriteLine("Database created successfully OR Already Exists.");

                string createDbQuery3 = "CREATE DATABASE IF NOT EXISTS cockyshop;";
                MySqlCommand command3 = new MySqlCommand(createDbQuery3, connection);
                command3.ExecuteNonQuery();
                Console.WriteLine("Database created successfully OR Already Exists.");

                string createDbQuery4 = "CREATE DATABASE IF NOT EXISTS velocity;";
                MySqlCommand command4 = new MySqlCommand(createDbQuery4, connection);
                command4.ExecuteNonQuery();
                Console.WriteLine("Database created successfully OR Already Exists.");

                // Switch to the new database
                command.CommandText = "USE cm_db;"; 
                command.ExecuteNonQuery();

                string c01 = @" CREATE TABLE IF NOT EXISTS carts(id int primary key auto_increment, ticketId int , UserID int, CartId int)"; 
                string c02 = @" CREATE TABLE IF NOT EXISTS users(Id int primary key auto_increment, Email varchar(250), Username varchar(250), DateOfBirth varchar(250), Password varchar(250))";
                string c03 = @" CREATE TABLE IF NOT EXISTS ticket(Id int primary key auto_increment, Price varchar(250), ShowtimeId int, Quantity int, IsSold tinyint)";
                string c04 = @" CREATE TABLE IF NOT EXISTS showtime(Id int primary key auto_increment, StartTime varchar(250), MovieID int, Capacity int, TicketsSold int, TicketsAvailable int, CartID int)";                    
                string c05 = @" CREATE TABLE IF NOT EXISTS moviegenres(Id int primary key auto_increment, GenresId int, MoviesID int, GenreName varchar(250))";                
                string c06 = @" CREATE TABLE IF NOT EXISTS genres(Id int primary key auto_increment, Name varchar(250))";
                string c07 = @" CREATE TABLE IF NOT EXISTS ratings(Id int primary key auto_increment, Name varchar(250))";
                string c08 = @" CREATE TABLE IF NOT EXISTS review(Id int primary key auto_increment, Title varchar(250), Rating int, Description varchar(250), MovieID int, Movie varchar(250))";
                string c09 = @" CREATE TABLE IF NOT EXISTS movies(Id int primary key auto_increment, Title varchar(250), Description varchar(250), DateReleased varchar(250), CartId int, GenresId int, ShowtimesId int, ReviewsId int, _imageUrl varchar(250), ImageUrl varchar(250), RatingId int)";
                string c10 = @" CREATE TABLE IF NOT EXISTS OrderResult(Id int primary key auto_increment, CartId int, ProcessedDate varchar(250), Success bool, Details varchar(250), TotalPrice varchar(250), TicketId int, UserId int, Username varchar(250))";
                string c11 = @" CREATE TABLE IF NOT EXISTS checkoutrequest(Id int primary key auto_increment, CartId int, UserId int, RequestDate varchar(250), PaymentDetailsId int)";
                string c12 = @" CREATE TABLE IF NOT EXISTS ordertickets(OrderTicketId int primary key auto_increment, TicketId int, ShowtimeId int, MovieId int, Price varchar(250), ShowtimesId int, OrderResultId int, Quantity int)";
                string c13 = @" CREATE TABLE IF NOT EXISTS PaymentDetails(Id int primary key auto_increment, OrderId int, Amount varchar(250), PaymentMethod varchar(250), PaymentDate varchar(250), CardNumber varchar(250), ExpiryDate varchar(250), CVV varchar(250), CardHolderName varchar(250))";
                string c14 = @" CREATE TABLE IF NOT EXISTS employees(id int primary key auto_increment, employeefullname varchar(200), firstname varchar(50), middle varchar(50), lastname varchar(100), companyid varchar(100), subaccount varchar(100), jid int, ncrid varchar(50), dynamicsid varchar(50), address1 varchar(50), address2 varchar(50), city varchar(50), regionstate varchar(50), postalcode varchar(50))";
                string c15 = @" CREATE TABLE IF NOT EXISTS employee_reviews(id int primary key auto_increment, employeefullname varchar(200), reviewdate varchar(50), reviewgivendate varchar(50), reviewdetails varchar(50), calendaryear varchar(50), notesondelivery varchar(200), reviewurl varchar(200), reviewimage varbinary(1000))";
                string c16 = @" CREATE TABLE IF NOT EXISTS stores(id int primary key auto_increment, storeid varchar(50), address1 varchar(100), address2 varchar(200), city varchar(100), region varchar(100), country varchar(100), regionid int, companyid varchar(100), subaccount varchar(100))";
                string c17 = @" CREATE TABLE IF NOT EXISTS region(id int primary key auto_increment, regionid int, hqaddress1 varchar(100), hqaddress2 varchar(200), hqcity varchar(100), hqregion varchar(100), hqcountry varchar(100), companyid varchar(100), subaccount varchar(100), storeid varchar(50))";
                string c18 = @" CREATE TABLE IF NOT EXISTS countryhq(id int primary key auto_increment, hqid int, hqaddress1 varchar(100), hqaddress2 varchar(200), hqcity varchar(100), hqregion varchar(100), hqcountry varchar(100), companyid varchar(100), subaccount varchar(100), storeid varchar(50))";
                string c19 = @" CREATE TABLE IF NOT EXISTS customerteams(id int primary key auto_increment, teamid varchar(50), teamdescription varchar(100), teammanagerJid int, jgroupid int, storeorigination varchar(50), companyid varchar(100), subaccount varchar(100))";
                string c20 = @" CREATE TABLE IF NOT EXISTS indivtransactions(id int primary key auto_increment, custid varchar(50), currencyid varchar(50), transamount varchar(50), transdate varchar(100), transcomment1 varchar(100), transcomment2 varchar(100), branchmgrcomment varchar(100), erpsystemid varchar(100), erptransid varchar(100), companyid varchar(100), subaccount varchar(100), adultqty int, childrenqty int, seniorqty int, statetx float, fedtax float, cityregiontax float, othertax1 float, othertax2 float, othertax1descr varchar(100), othertax2descr varchar(100))";
                string c21 = @" CREATE TABLE IF NOT EXISTS teamtransactions(id int primary key auto_increment, teamid varchar(50), currencyid varchar(50), transamount varchar(50), transdate varchar(100), transcomment1 varchar(100), transcomment2 varchar(100), branchmgrcomment varchar(100), erpsystemid varchar(100), erptransid varchar(100), companyid varchar(100), subaccount varchar(100), adultqty int, childrenqty int, seniorqty int, statetx float, fedtax float, cityregiontax float, othertax1 float, othertax2 float, othertax1descr varchar(100), othertax2descr varchar(100))";
                string c22 = @" CREATE TABLE IF NOT EXISTS currencytype(id int primary key auto_increment, currencyid varchar(50), currencydescription varchar(200), exchangecorpid varchar(50), exchangecorpsubid varchar(50))";
                string c23 = @" CREATE TABLE IF NOT EXISTS currencymark(id int primary key auto_increment, currencyid varchar(50), currencydescription varchar(200), dollarmark float, yuanmark float, sterlingmark float)";
                string c24 = @" CREATE TABLE IF NOT EXISTS products(id int primary key auto_increment, prodid varchar(50), productdescr varchar(255), productmrc float, productnrc float, discount1 float, discount2 float, discount3 float, discountid varchar(50))";
                string c25 = @" CREATE TABLE IF NOT EXISTS soesummary(id int primary key auto_increment, invoiceid varchar(50), soemrctotal float, soenrctotal float, companyid varchar(100), subaccount varchar(100), purhcaseordernum varchar(50), paytype varchar(10), checkid varchar(50), stripetransid varchar(100), paypaltransid varchar(100), creditdebit varchar(100))";
                string c26 = @" CREATE TABLE IF NOT EXISTS soedetail(id int primary key auto_increment, detailid int, invoiceid varchar(50), productid varchar(50), qty int, subtotalmrc float, subtotalnrc float, subtotaltax float)";
                string c27 = @" CREATE TABLE IF NOT EXISTS company_creditcards(id int primary key auto_increment, userid int, cardtype varchar(50), cardnumber varchar(50), expdate varchar(50), pincode varchar(50), account_number varchar(50), account_subaccount varchar(50))";
                string c28 = @" CREATE TABLE IF NOT EXISTS company_credittrans(id int primary key auto_increment, userid int, cardtype varchar(50), cardnumber varchar(50), transdate varchar(50), transamount float, account_number varchar(50), account_subaccount varchar(50))";
                string c29 = @" CREATE TABLE IF NOT EXISTS velo_users(id int primary key auto_increment, name varchar(400), username varchar(150), email varchar(100), password varchar(100), sendEmailtinyint, registerDate datetime, lastvisitDate datetime, activation varchar(100), params text, lastResetTime datetime, resetCount int, otpKey varchar(1000), otep varchar(1000), requireReset tinyint, authProvider varchar(100))";
                string c30 = @" CREATE TABLE IF NOT EXISTS velo_user_usergroup_map(id int auto_increment primary key, user_id int, group_id int)";
                string c31 = @" CREATE TABLE IF NOT EXISTS velo_usergroups(id int primary key auto_increment, parent_id int, lft int, rgt int, title varchar(100))";
                string c32 = @" CREATE TABLE IF NOT EXISTS velo_users_movies(id INT PRIMARY KEY AUTO_INCREMENT, moviename char(150), movieid int, moviegeneration varchar(100), movietype varchar(100), maleleadid int, femaleleadid int, moviepicture1url varchar(150), moviepicture2url varchar(150), moviepicture3url varchar(150), youtubeurl varchar(150))";
                string c33 = @" CREATE TABLE IF NOT EXISTS velo_users_music(id INT PRIMARY KEY AUTO_INCREMENT, musicname char(150), muscid int, musicgeneration varchar(100), musictype varchar(100), maleleadid int, femaleleadid int, music1url1 varchar(150), music1url2 varchar(150), music3url3 varchar(150), youtubeurl varchar(150))";
                string c34 = @" CREATE TABLE IF NOT EXISTS velo_users_moviestars(id INT PRIMARY KEY AUTO_INCREMENT, starid int, starbirthdayyear varchar(4), starname varchar(100), otherwork1 int, otherwork2 int, otherwork3 int, pictureurl varchar(100))";
                string c35 = @" CREATE TABLE IF NOT EXISTS velo_users_moviereviews(id INT PRIMARY KEY AUTO_INCREMENT, uid int, movieid int, moviename char(150), moviestars int, fullreview text, movietype varchar(100), moviegeneration varchar(100), authphone varchar(100), authemail varchar(100), authfax varchar(100), malestarid int, femalestarid int, reviewtime varchar(100), closed int, totalhits int)";
                string c36 = @" CREATE TABLE IF NOT EXISTS velo_users_musicreviews(id INT PRIMARY KEY AUTO_INCREMENT, uid int, musicid int, musicname char(150), musicstars int, fullreview text, musictype varchar(100), musicgeneration varchar(100), authphone varchar(100), authemail varchar(100), authfax varchar(100), malestarid int, femalestarid int, reviewtime varchar(100), closed  int, totalhits  int, playlist tinyint)";
                string c37 = @" CREATE TABLE IF NOT EXISTS velo_users_creditcards(id int primary key AUTO_INCREMENT, userid int, cardtype varchar(50), cardnumber varchar(50), expdate varchar(50), pincode varchar(50), account_number varchar(50), account_subaccount varchar(50))";
                string c38 = @" CREATE TABLE IF NOT EXISTS velo_users_credittrans(id int primary key AUTO_INCREMENT, userid int, cardtype varchar(50), cardnumber varchar(50), transdate varchar(50), transamount float, account_number varchar(50), account_subaccount varchar(50))";
                string c39 = @" CREATE TABLE IF NOT EXISTS velo_users_movies_times(id INT PRIMARY KEY auto_increment, moviename char(150), movieid int, timesone varchar(250), timestwo varchar(250), timesthree varchar(250), timesfour varchar(250), timesfive varchar(250), timessix varchar(250), timesseven varchar(250), moviepicture1url varchar(150), moviepicture2url varchar(150), moviepicture3url varchar(150), youtubeurl varchar(150))";


                command.CommandText = c01;
                command.ExecuteNonQuery();
                command.CommandText = c02;
                command.ExecuteNonQuery();
                command.CommandText = c03;
                command.ExecuteNonQuery();
                command.CommandText = c04;
                command.ExecuteNonQuery();
                command.CommandText = c05;
                command.ExecuteNonQuery();
                command.CommandText = c06;
                command.ExecuteNonQuery();
                command.CommandText = c07;
                command.ExecuteNonQuery();
                command.CommandText = c08;
                command.ExecuteNonQuery();
                command.CommandText = c09;
                command.ExecuteNonQuery();
                command.CommandText = c10;
                command.ExecuteNonQuery();

                command.CommandText = c11;
                command.ExecuteNonQuery();
                command.CommandText = c12;
                command.ExecuteNonQuery();
                command.CommandText = c13;
                command.ExecuteNonQuery();
                command.CommandText = c14;
                command.ExecuteNonQuery();
                command.CommandText = c15;
                command.ExecuteNonQuery();
                command.CommandText = c16;
                command.ExecuteNonQuery();
                command.CommandText = c17;
                command.ExecuteNonQuery();
                command.CommandText = c18;
                command.ExecuteNonQuery();
                command.CommandText = c19;
                command.ExecuteNonQuery();
                command.CommandText = c20;
                command.ExecuteNonQuery();

                command.CommandText = c21;
                command.ExecuteNonQuery();
                command.CommandText = c22;
                command.ExecuteNonQuery();
                command.CommandText = c23;
                command.ExecuteNonQuery();
                command.CommandText = c24;
                command.ExecuteNonQuery();
                command.CommandText = c25;
                command.ExecuteNonQuery();
                command.CommandText = c26;
                command.ExecuteNonQuery();
                command.CommandText = c27;
                command.ExecuteNonQuery();
                command.CommandText = c28;
                command.ExecuteNonQuery();
                command.CommandText = c29;
                command.ExecuteNonQuery();
                command.CommandText = c30;
                command.ExecuteNonQuery();
                
                command.CommandText = c31;
                command.ExecuteNonQuery();
                command.CommandText = c32;
                command.ExecuteNonQuery();
                command.CommandText = c33;
                command.ExecuteNonQuery();
                command.CommandText = c34;
                command.ExecuteNonQuery();
                command.CommandText = c35;
                command.ExecuteNonQuery();
                command.CommandText = c36;
                command.ExecuteNonQuery();
                command.CommandText = c37;
                command.ExecuteNonQuery();
                command.CommandText = c38;
                command.ExecuteNonQuery();
                command.CommandText = c39;
                command.ExecuteNonQuery();
                Console.WriteLine("Installation PhaseI Complete.");


                /* SOME TEST TABLES WHICH ARE GENERIC
                // Create a new table
                string createTableQuery = @" CREATE TABLE IF NOT EXISTS TestUsers ( UserID INT AUTO_INCREMENT PRIMARY KEY, Username VARCHAR(50) NOT NULL, Email VARCHAR(100), CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP );"; 
                command.CommandText = createTableQuery; 
                command.ExecuteNonQuery(); 
                Console.WriteLine("Table 'TestUsers' created successfully or already exists.");

                // Create Posts table
                string createPostsTableQuery = @" CREATE TABLE IF NOT EXISTS TestPosts ( PostID INT AUTO_INCREMENT PRIMARY KEY, UserID INT, Title VARCHAR(100) NOT NULL, Content TEXT, CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP, FOREIGN KEY (UserID) REFERENCES TestUsers(UserID) );"; 
                command.CommandText = createPostsTableQuery; 
                command.ExecuteNonQuery(); 
                Console.WriteLine("Table 'TestPosts' created successfully or already exists.");
                */

                // Create a new user and grant privileges
                string createUserQuery = @" CREATE USER 'cm_user'@'localhost' IDENTIFIED BY 'CockyMoviePassword'; GRANT ALL PRIVILEGES ON cm_db.* TO 'cm_user'@'localhost'; FLUSH PRIVILEGES;"; 
                command.CommandText = createUserQuery; 
                command.ExecuteNonQuery(); 
                Console.WriteLine("User 'cm_user' created and granted privileges successfully."); 
                }
            catch (MySqlException ex) 
            {
                Console.WriteLine("User Already Exists or Error");
                Console.WriteLine("Error: " + ex.Message); 
            } 
            finally 
            { 
                connection.Close(); 
            } 
        }
        /*
        public void MYSQLConnectionCheck()
        {
            MySqlConnection MyConnection = null;
            MySqlDataReader MyReader = null;

            // Create the SQL connection.
            MySqlConnection Connection = new MySqlConnection("Server=localhost;DataBase=cm_db;User=cm_user;Password=CockyMoviePassword");
            Connection.Open();

            // Create the command.
            MySqlCommand MyCommand = new MySqlCommand("SELECT * from genres", MyConnection);

            // Execute the command
            MyReader = MyCommand.ExecuteReader();

            // ...

            MyReader.Close();
            MyConnection.Close();
        }

        // DataContext
        class public class MyDataContext : DataContext
        {
            public Table<Product> Products;
            public MyDataContext(string connectionString) : base(connectionString) { }
        } 
       using (MyDataContext context = new MyDataContext())
       {
       var results = from s in context.People
                     where s.ID > 5
                     select s;

       return results.ToList();
       }*/


        public void MYSQLinstall()
        {
            //static string MyConnectionString = "Server=127.0.0.1;Username=root;Password=test12345";
            //INSTALLER CREATES 1 -> 2B ABOVE
            //STEP0 -> ASK FOR SIMPLEX(SINGLE DB, OR FISMA HIGH INSTALLATION(DECOMPOSED)
            //STEP1 -> CHECK IF DATABASE COCKY, COCKYSSO, COCKYSHOP AND VELOCITY EXISTS
            //STEP2A -> IF SIMPLEX MODE, AND DATABASE DOESNT EXIST CREATE COCKY
            //STEP2B -> IF COMPLEX MODE, AND DATABASES DONT EXIST CREATE THEM ALL AND RUN SCHEMA BUILDER IN MULTIPLE STEPS.

            //STEP3 -> LOAD LIST DATA INTO THE DATABASE

            string connectionString = "server=localhost;user=root;password=test12345;port=3307"; 
            MySqlConnection connection = new MySqlConnection(connectionString); 
            try { 
                connection.Open();
                string createDbQuery = "CREATE DATABASE IF NOT EXISTS velocity;";
                MySqlCommand command = new MySqlCommand(createDbQuery, connection);

                // Switch to the new database
                command.CommandText = "USE velocity;"; 
                command.ExecuteNonQuery();
                string ic01 = "insert into velo_users_movies(moviename, movieid) VALUES('James Bond - Quantum of Solace', 1)";
                string ic02 = "insert into velo_users_movies(moviename, movieid) VALUES('James Bond - Die Another Day', 2)";
                string ic03 = "insert into velo_users_movies(moviename, movieid) VALUES('The Hunger Games - Mockingjay', 3)";
                string ic04 = "insert into velo_users_movies(moviename, movieid) VALUES('Shes All That', 4)";
                string ic05 = "insert into velo_users_movies(moviename, movieid) VALUES('TopGun 2', 5)";
                string ic06 = "insert into velo_users_movies(moviename, movieid) VALUES('Jerry McGuire', 6)";
                string ic07 = "insert into velo_users_moviestars(starid, starname) VALUES(1, 'Timothy Dalton')";
                string ic08 = "insert into velo_users_moviestars(starid, starname) VALUES(2, 'Roger Moore')";
                string ic09 = "insert into velo_users_moviestars(starid, starname) VALUES(3, 'Sean Connery')";
                string ic10 = "insert into velo_users_moviestars(starid, starname) VALUES(4, 'Tom Cruise')";
                string ic11 = "insert into velo_users_moviestars(starid, starname) VALUES(5, 'Jennifer Lawrence')";
                string ic12 = "insert into velo_users_moviestars(starid, starname) VALUES(6, 'Jennifer Love Hewitt')";
                string ic13 = "insert into velo_users_moviestars(starid, starname) VALUES(7, 'Freddie Prince Jr')";
                string ic14 = "insert into velo_users_movies_times(moviename, movieid) VALUES('James Bond - Quantum of Solace', 1)";
                string ic15 = "insert into velo_users_movies(moviename) VALUES('TopGun Maverick')";
                string ic16 = "insert into velo_users_movies(moviename) VALUES('Blue Crush')";
                string ic17 = "insert into velo_users_movies(moviename) VALUES('JerryMcGuire')";
                string ic18 = "insert into velo_users_movies(moviename) VALUES('NottingHill')";
                string ic19 = "insert into velo_users_movies(moviename) VALUES('Gladiator')";
                string ic20 = "insert into velo_users_moviereviews(id) VALUES(1)";
                string ic21 = "insert into velo_users_musicreviews(id) VALUES(1)";

                
                command.CommandText = ic01;
                command.ExecuteNonQuery();
                command.CommandText = ic02;
                command.ExecuteNonQuery();
                command.CommandText = ic03;
                command.ExecuteNonQuery();
                command.CommandText = ic04;
                command.ExecuteNonQuery();
                command.CommandText = ic05;
                command.ExecuteNonQuery();
                command.CommandText = ic06;
                command.ExecuteNonQuery();
                command.CommandText = ic07;
                command.ExecuteNonQuery();
                command.CommandText = ic08;
                command.ExecuteNonQuery();
                command.CommandText = ic09;
                command.ExecuteNonQuery();
                command.CommandText = ic10;
                command.ExecuteNonQuery();
                command.CommandText = ic11;
                command.ExecuteNonQuery();
                command.CommandText = ic12;
                command.ExecuteNonQuery();
                command.CommandText = ic13;
                command.ExecuteNonQuery();
                command.CommandText = ic14;
                command.ExecuteNonQuery();
                command.CommandText = ic15;
                command.ExecuteNonQuery();
                command.CommandText = ic16;
                command.ExecuteNonQuery();
                command.CommandText = ic17;
                command.ExecuteNonQuery();
                command.CommandText = ic18;
                command.ExecuteNonQuery();
                command.CommandText = ic19;
                command.ExecuteNonQuery();
                command.CommandText = ic20;
                command.ExecuteNonQuery();
                command.CommandText = ic21;
                command.ExecuteNonQuery();

                Console.WriteLine("Installation PhaseII Complete.");
                         
                }
            catch (MySqlException ex) 
            {
                Console.WriteLine("User Already Exists or Error");
                Console.WriteLine("Error: " + ex.Message); 
            } 
            finally 
            {
            connection.Close();
            } 
        }

        
    


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
    }*/
    }
}