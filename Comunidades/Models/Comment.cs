namespace Comunidades.Models
{
    public class Comment
    {
        public string IdPost { get; set; }
        public string IdUser { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool Estate { get; set; }
        public int Reports { get; set; }
    }

}
