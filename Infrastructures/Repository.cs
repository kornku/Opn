using System.Collections;

namespace Opn.Infrastructures;


public class Repository
{
    public static Repository Of { get; } =
        new Repository() { User = new UserRepository() };

    public IRepository User { get; private set; }
}

public class UserRepository : IRepository
{
    private List<UserEntity> Users { get; set; } = new List<UserEntity>();

    public IEnumerator<UserEntity> GetEnumerator()
    {
        return Users.GetEnumerator();
    }

    public UserEntity? GetById(Guid userId)
    {
        return Users.FirstOrDefault(x => x.UserId == userId);
    }

    public void Add(UserEntity user)
    {
        Users.Add(user);
    }

    public void Update(Guid userId, UserEntity user)
    {
        var entity = Users.FirstOrDefault(x => x.UserId == userId);

        if (entity == null)
        {
            throw new KeyNotFoundException("user id not exists");
        }

        entity.Address = user.Address;
        entity.Gender = user.Gender;
        entity.DateOfBrith = user.DateOfBrith;
        entity.IsSubscribe = user.IsSubscribe;
        entity.Token = user.Token;
        entity.TokenDatetime = user.TokenDatetime;
    }

    public void Remove(Guid userId)
    {
        var index = Users.FindIndex(x => x.UserId == userId);
        if (index == -1)
        {
            throw new KeyNotFoundException("user id not exists");
        }
        Users.RemoveAt(index);
    }

    public void ChangePassword(UserEntity entity)
    {
        var user = Users.FirstOrDefault(x => x.UserId == entity.UserId);

        if (user == null)
        {
            throw new KeyNotFoundException("user id not exists");
        }

        user.Password = entity.Password;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}