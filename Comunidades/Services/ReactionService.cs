using Comunidades.Models;

namespace Comunidades.Services
{
    public class ReactionService
    {
        private readonly FirebaseService _firebaseService;

        public ReactionService(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public void CreateReaction(Reaction reaction)
        {
            _firebaseService.WriteData("reactions", reaction);
        }
    }

}
