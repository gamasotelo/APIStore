namespace CursoAPIPlatzi.Models.Domain
{
    public class Sale
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public int IdProduct { get; set; }
        public int IdWorker { get; set; }
        
        public virtual Customer? Customer { get; set; }
        public virtual Worker? Worker { get; set; }
        public virtual Product? Product { get; set; }
    }
}
