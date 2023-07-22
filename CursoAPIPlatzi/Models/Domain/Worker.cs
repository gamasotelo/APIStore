using System.Text.Json.Serialization;

namespace CursoAPIPlatzi.Models.Domain
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Position WorkPosition { get; set; }
        //[JsonIgnore]
        //public List<Sale> Sales { get; set; }
    }

    public enum Position { 
        Vendedor,
        Gerente,
        Cobrador
    }
}
