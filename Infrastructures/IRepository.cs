using Opn.Interfaces;

namespace Opn.Infrastructures;

public interface IRepository: IEnumerable<UserEntity>
{
    public UserEntity? GetById(Guid userId);
    public void Add(UserEntity user);
    public void Update(Guid userId, UserEntity user);
    public void Remove(Guid userId);
    void ChangePassword(UserEntity entity);
}