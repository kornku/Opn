namespace Opn.Models;

public class ChangePasswordToken
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
}