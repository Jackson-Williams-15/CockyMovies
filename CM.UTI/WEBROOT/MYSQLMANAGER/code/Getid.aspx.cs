using System;
using System.Collections.Generic;
using System.Linq;

public partial class Page : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the id parameter exists in the URL
        if (Request.QueryString["id"] != null)
        {
            // Retrieve the id value from the URL
            string id = Request.QueryString["id"];
            //Response.Write("The ID Passed to the page is: " + id  + "\n\n");
	    Label1.Text = "\n\nMovieID: " + id + "\n";
        }
        else
        {
            Response.Write("ID parameter is missing in the URL");
        }
    }
}