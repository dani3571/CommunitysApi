namespace Comunidades.Models
{
    public class Post
    {
        public string PostKey { get; set; }
        public string IdChannel { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string File { get; set; }
        public DateTime Publication_Date { get; set; }
        public bool Estate { get; set; }
        public List<Reaction>? Reactions { get; set; } // Lista de reacciones
        public List<Comment>? Comments { get; set; } // Lista de comentarios
    }

}
