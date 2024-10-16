using Opn.Controllers;
using Opn.Models;

namespace Opn.Interfaces;

public interface IUserService
{
    public UserDto GetProfile(Guid Id);
    public bool UpdateProfile(Guid Id,ProfileController.UpdateUserRequest request);
}

