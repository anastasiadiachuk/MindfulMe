using System.Security.Claims;
using Entities;
using Entities.ViewModels;


namespace ServiceContracts
{
    public interface IUserService
    {
        // Create
        void CreateUser(User user);

        // Read
        User GetUserById(int userId);
        List<User> GetAllUsers();

        // Update
        void UpdateUser(User updatedUser);

        // Delete
        void DeleteUser(int userId);

        ClaimsIdentity Authenticate(User user);

        ClaimsIdentity Register(RegisterViewModel model);

        ClaimsIdentity Login(LoginViewModel model);
    }
}
