using Comunidades.Models;

namespace Comunidades.Services
{
    public class CommentService
    {
        private readonly FirebaseService _firebaseService;

        public CommentService(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public void CreateComment(Comment comment)
        {
            _firebaseService.WriteData("comments", comment);
        }
    }

}
