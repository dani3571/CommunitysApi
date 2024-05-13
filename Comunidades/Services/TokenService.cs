using Comunidades.Models;

namespace Comunidades.Services
{
    public class TokenService
    {
        private readonly FirebaseService _firebaseService;

        public TokenService(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }
        
        public IEnumerable<Channel> GetChannelsByUserId(string userId)
        {
            return _firebaseService.GetChannelsByUserId(userId);
        }

    }
}
