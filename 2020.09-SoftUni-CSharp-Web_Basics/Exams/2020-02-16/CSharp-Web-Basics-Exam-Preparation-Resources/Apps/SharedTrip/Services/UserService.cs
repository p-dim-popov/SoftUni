using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.ViewModels;

namespace SharedTrip.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void RegisterUser(UserRegisterInputModel model)
        {
            var user = new User
            {
                Email = model.Email,
                Password = ComputeHash(model.Password),
                Username = model.Username
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
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

        public bool IsUsernameAvailable(string modelUsername)
        {
            return !_dbContext.Users.Any(u => u.Username == modelUsername);
        }

        public bool IsEmailAvailable(string modelEmail)
        {
            return !_dbContext.Users.Any(u => u.Email == modelEmail);
        }

        public string GetUserId(string username, string password)
        {
            var hash = ComputeHash(password);
            return _dbContext.Users
                .Where(x => x.Username == username && x.Password == hash)
                .Select(x => x.Id)
                .FirstOrDefault();
        }
    }
}
