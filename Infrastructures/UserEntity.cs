using System.Globalization;

namespace Opn.Infrastructures;
public class UserEntity
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBrith { get; set; }
    public string Address { get; set; }
    public bool IsSubscribe { get; set; }
    public string Token { get; set; }
    public DateTime TokenDatetime { get; set; }

    public static UserEntity Create(Opn.Controllers.RegisterController.RegisterRequest request)
    {
        var user = new UserEntity();
        user.UserId = Guid.NewGuid();
        user.Gender = request.Gender;
        user.Email = request.Email;
        user.Name = request.Name;
        user.Address = request.Address;
        user.DateOfBrith = DateTime.ParseExact(request.Dob,"dd-MM-yyyy",  CultureInfo.CurrentCulture);
        user.IsSubscribe = request.IsSubscribe;

        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        return user;
    }

    public Gender Gender { get; set; }

    public static void Update(UserEntity entity, Opn.Controllers.ProfileController.UpdateUserRequest request)
    {
        entity.Gender = request.Gender;
        entity.Address = request.Address;
        entity.DateOfBrith = DateTime.ParseExact( request.Dob,"dd-MM-yyyy", CultureInfo.CurrentCulture);
        entity.IsSubscribe = request.IsSubscribe;
    }

    public static class GenerateRandomLetter
    {
        private static Random random = new Random();

        public static string Generate()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 16)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public string GenerateToken()
    {
        Token = GenerateRandomLetter.Generate();
        TokenDatetime = DateTime.UtcNow;

        return Token;
    }

    public static bool ChangePassword(UserEntity user, string password, string token)
    {
        if (user.Token == token &&
            BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return true;
        }

        return false;
    }
}