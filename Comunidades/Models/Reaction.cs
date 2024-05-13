namespace Comunidades.Models
{
    public class Reaction
    {
        public string IdPost { get; set; }
        public int ReactionType { get; set; }
        public string IdUser { get; set; }
        public bool Estate { get; set; }
    }

}
