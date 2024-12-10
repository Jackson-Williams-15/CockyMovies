using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Register : Page
{
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;

        if (RegisterUser(username, password))
        {
            lblMessage.Text = "Registration successful!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblMessage.Text = "Registration failed. Please try again.";
        }
    }

    private bool RegisterUser(string username, string password)
    {
        // Add your database insertion logic here
        // Example: Insert into a SQL database
        using (SqlConnection conn = new SqlConnection("your_connection_string"))
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Users (uname, Pwd) VALUES (@uname, @pwd)", conn);
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@pwd", password);
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
