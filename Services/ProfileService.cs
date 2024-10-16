using Opn.Controllers;
using Opn.Infrastructures;
using Opn.Interfaces;
using Opn.Models;

namespace Opn.Services;

public class ProfileService : IUserService
{
    private readonly IRepository _repository;

    public ProfileService(IRepository repository)
    {
        _repository = repository;
    }

    public UserDto GetProfile(Guid id)
    {
        var entity = _repository.GetById(id);

        if (entity == null)
        {
            throw new KeyNotFoundException("user not exists");
        }

        var user = new UserDto
        {
            UserId = entity.UserId,
            Email = entity.Email,
            Name = entity.Name,
            Dob = entity.DateOfBrith.ToString("dd-MM-yyyy"),
            Address = entity.Address,
            IsSubscribe = entity.IsSubscribe
        };

        return user;
    }

    public bool UpdateProfile(Guid id, ProfileController.UpdateUserRequest request)
    {
        var entity = _repository.GetById(id);

        if (entity == null)
        {
            throw new KeyNotFoundException("user not exists");
        }

        UserEntity.Update(entity, request);

        _repository.Update(id, entity);

        return true;
    }
}