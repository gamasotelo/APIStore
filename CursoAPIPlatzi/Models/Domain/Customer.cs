using System.Text.Json.Serialization;

namespace CursoAPIPlatzi.Models.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal? Debit { get; set; }
        //[JsonIgnore]
        //public Sale SaleId { get; set; }
    }
}
