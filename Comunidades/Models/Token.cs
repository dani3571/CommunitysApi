namespace Comunidades.Models
{
    public class Token
    {
        public string TokenContent { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
