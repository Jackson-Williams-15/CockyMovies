using CM.API.Models;
public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Cart Cart { get; set; }
}