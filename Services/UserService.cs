using System.Security.Claims;
using Entities;
using Entities.Enums;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services.Helpers;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly ProgramDbContext _dbContext;

        public UserService(ProgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException();
            ValidationHelper.ModelValidation(user);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User GetUserById(int userId)
        {
            return _dbContext.Users.FirstOrDefault(p => p.UserId == userId);
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public void UpdateUser(User updatedUser)
        {
            var existingUser = _dbContext.Users.Find(updatedUser.UserId);

            if (existingUser == null) return;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;
            existingUser.UserName = updatedUser.UserName;
            existingUser.UserType = updatedUser.UserType;

            _dbContext.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var userToDelete = _dbContext.Users.Find(userId);

            if (userToDelete == null) return;
            _dbContext.Users.Remove(userToDelete);
            _dbContext.SaveChanges();
        }

        public ClaimsIdentity Register(RegisterViewModel model)
        {
            if (_dbContext.Users.Any(u => u.Email == model.Email))
            {
                throw new ArgumentException("User with this email already exists");
            }
            if (_dbContext.Users.Any(u => u.UserName == model.UserName))
            {
                throw new ArgumentException("User with this username already exists");
            }

            var user = new User()
            {
                Email = model.Email,
                Password = model.Password,
                UserName = model.UserName,
                UserType = UserType.User
            };
            CreateUser(user);
            return Authenticate(user);
        }

        public ClaimsIdentity Login(LoginViewModel model)
        {
            if (!_dbContext.Users.Any(u => u.Email == model.Email))
            {
                throw new ArgumentException("User with this email not found");
            }
            var user = _dbContext.Users.FirstOrDefault(p => p.Email == model.Email);
            if (model.Password != user.Password)
            {
                throw new ArgumentException("Incorrect password");
            }

            return Authenticate(user);
        }

        public ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserType.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
