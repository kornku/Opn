using Opn.Controllers;
using Opn.Models;

namespace Opn.Interfaces;

public interface IRegisterService
{
    public RegisterDto Register(RegisterController.RegisterRequest request);
    public ChangePasswordToken GetToken(Guid UserId);
    public bool ChangePassword(Guid UserId, string Password, string Token);
}



