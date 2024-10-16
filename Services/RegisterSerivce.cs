using Opn.Controllers;
using Opn.Infrastructures;
using Opn.Interfaces;
using Opn.Models;

namespace Opn.Services;

public class RegisterService : IRegisterService
{
    private readonly IRepository _repository;

    public RegisterService(IRepository repository)
    {
        _repository = repository;
    }

    public RegisterDto Register(RegisterController.RegisterRequest request)
    {
        var entity = UserEntity.Create(request);

        _repository.Add(entity);

        return new RegisterDto
        {
            Status = true,
            UserId = entity.UserId
        };
    }

    public ChangePasswordToken GetToken(Guid userId)
    {
        var entity = _repository.GetById(userId);

        if (entity == null)
        {
            throw new KeyNotFoundException("user not exists");
        }

        var token = entity.GenerateToken();

        var result = new ChangePasswordToken
        {
            UserId = userId,
            Token = token
        };


        return result;
    }

    public bool ChangePassword(Guid userId, string password, string token)
    {
        var entity = _repository.GetById(userId);

        if (entity == null)
        {
            throw new KeyNotFoundException("user not exists");
        }

        var isSuccess = UserEntity.ChangePassword(entity, password, token);

        if (isSuccess)
        {
            _repository.ChangePassword(entity);
            return true;
        }

        return false;
    }
}