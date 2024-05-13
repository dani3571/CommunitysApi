using Comunidades.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comunidades.Services
{
    public class UserService
    {
        private readonly FirebaseService _firebaseService;


        public UserService(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public void CreateUser(User user)
        {
            _firebaseService.WriteData("users", user);
        }
        public string AuthenticateUser(string username, string password)
        {
            return _firebaseService.AuthenticateUser(username, password);
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _firebaseService.GetAllUsers();
        }
        
    }
}
