using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SULS.Data;
using SULS.Data.Models;
using SULS.Models.Users;
using SUS.MvcFramework;

namespace SULS.Services
{
    public class UserService
    {
        private readonly SULSContext _dbContext;

        public UserService(SULSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string RegisterUser(UserRegisterInputModel model)
        {
            var user = new User()
            {
                Email = model.Email,
                Username = model.Username,
                Password = ComputeHash(model.Password),
                Role = IdentityRole.User,
                Id = Guid.NewGuid().ToString()
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public bool IsUsernameAvailable(string username)
        {
            return !_dbContext.Users.Any(u => u.Username == username);
        }

        public bool IsEmailAvailable(string email)
        {
            return !_dbContext.Users.Any(u => u.Email == email);
        }

        public string GetUserId(string username, string password)
        {
            var hash = ComputeHash(password);

            return _dbContext.Users
                .Where(u => u.Username == username && u.Password == hash)
                .Select(u => u.Id)
                .FirstOrDefault();
        }

        public User GetUserById(string userId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
