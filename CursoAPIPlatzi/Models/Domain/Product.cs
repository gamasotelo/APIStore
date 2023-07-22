using System.Text.Json.Serialization;

namespace CursoAPIPlatzi.Models.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //[JsonIgnore]
        //public List<Sale> Sales { get; set; }
    }
}
