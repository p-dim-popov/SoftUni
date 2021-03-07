using System.Linq;
using SUS.MvcFramework;

namespace BattleCards.Services
{
    using Data;
    using Models;

    public class UserService : IUserService

    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string AddUser(string username, string email, string password)
        {
            var user = new User
            {
                Email = email,
                Password = IUserService.ComputeHash(password),
                Role = IdentityRole.User,
                Username = username,
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }

        public bool IsUsernameAvailable(string username)
            => !_dbContext.Users.Any(u => u.Username == username);

        public bool IsEmailAvailable(string email)
            => !_dbContext.Users.Any(u => u.Email == email);


        public string GetUserId(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Username == username);
            return user?.Password != IUserService.ComputeHash(password) ? null : user?.Id;
        }
    }
}
