namespace MotionGlow.Models;

public class UserViewModel
{
    public int UsersID { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }
}