using Comunidades.Models;

namespace Comunidades.Services
{
    public class PostService
    {
        private readonly FirebaseService _firebaseService;

        public PostService(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public void CreatePost(Post post)
        {
            _firebaseService.WriteData("posts", post);
        }
        //comentarios
        //obtener comentarios de un post
        public IEnumerable<Comment> GetCommentsByPostId(string postId)
        {
            return _firebaseService.GetCommentsByPostId(postId);
        }
        //Reactions
        //obtener comentarios de un post
        public IEnumerable<Reaction> GetReactionsByPostId(string postId)
        {
            return _firebaseService.GetReactionsByPostId(postId);
        }

    }

}
