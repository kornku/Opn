namespace Opn.Models;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Dob { get; set; }
    public string Address { get; set; }
    public bool IsSubscribe { get; set; }
}