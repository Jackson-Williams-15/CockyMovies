using System;
using System.Collections.Generic;
using System.Linq;

public partial class Movies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
	List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20, 30 };
	int result = 0;
	//Use LINQ to filter even numbers
        var evenNumbers = numbers.Where(n => n % 2 == 0);

        //Print the even numbers
        foreach (var num in evenNumbers)
        {
   		if(result > num)
		{
		//do nothing
		}         
        	else
		{
		result = num;
		}
	}
	

        Label1.Text = "MovieTimes:  Movie Times for the Xmen are the same for each show at CockyTheatres 1:30, 3:30, 5:30, 8:00pm and 11:00pm!\n\n";
	Label2.Text = "Description: XMen-> Its Jennifer Lawrence Painted in Blue... Whats Not to Love.\n\n";
	Label3.Text = "Linq Response: OpenSeats for the 8:00PM Show is:" + result + " Its a Premium Ticket with a Price of $12.50\n";
	jenimage.ImageUrl = "./code/lawrenceblue.jpg";
	
    }

}