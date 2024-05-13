namespace Comunidades.Models
{
    public class Channel
    {
        public string Key { get; set; } //esta debe quedar null siempre, luego sera reemplazado por la key generada por firebase al momento de enviarse como json
        public string Name { get; set; }
        public string Description { get; set; }
        public string IdUser { get; set; }
        public string Visibility { get; set; }
        public bool Estate { get; set; }
        public string Category { get; set; }
    }

}
