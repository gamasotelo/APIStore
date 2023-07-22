using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CursoAPIPlatzi.Models.Domain
{
    public class StoreContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Customer> customerInit = new List<Customer>();
            customerInit.Add( new Customer() 
            {
                Id=1,
                Name= "Bentley Kingsley",
                Address= "Baker Street #7, Islington"
            });

            modelBuilder.Entity<Customer>(customer => 
            { 
                customer.ToTable("Cliente");
                customer.HasKey(p => p.Id);
                customer.Property(p => p.Id)
                        .ValueGeneratedNever();
                customer.Property(p => p.Name)
                        .HasColumnName("nombre")
                        .HasMaxLength(150);
                customer.Property(p => p.Address)
                        .HasColumnName("direccion");
                customer.Property(p => p.Debit)
                        .HasColumnName("adeudo")
                        .IsRequired(false);
                customer.HasData(customerInit);
            });

            List<Worker> workerInit = new List<Worker>();
            workerInit.Add(new Worker()
            {
                Id = 1,
                Name = "Jasper Worthington",
                WorkPosition = CursoAPIPlatzi.Models.Domain.Position.Gerente
            });


            modelBuilder.Entity<Worker>(worker =>
            {
                worker.ToTable("Trabajador");
                worker.HasKey(p => p.Id);
                worker.Property(p => p.Id)
                      .ValueGeneratedNever();
                worker.Property(p => p.Name)
                      .HasColumnName("nombre")
                      .HasMaxLength(150);
                worker.Property(p => p.WorkPosition)
                      .HasColumnName("cargo");
                worker.HasData(workerInit);
            });

            List<Product> productInit = new List<Product>();
            productInit.Add(new Product()
            {
                Id = 1,
                Name = "Fridge",
                Price = 12000
            });

            modelBuilder.Entity<Product>(product => 
            {
                product.ToTable("Producto");
                product.HasKey(p => p.Id);
                product.Property(p => p.Id)
                       .ValueGeneratedNever();
                product.Property(p => p.Name)
                       .HasColumnName("nombre")
                       .HasMaxLength(150);
                product.Property(p => p.Price)
                       .HasColumnName("Precio");
                product.HasData(productInit);
            });

            List<Sale> saleInit = new List<Sale>();
            saleInit.Add(new Sale()
            {
                Id = 1,
                IdCustomer = 1,
                IdProduct = 1,
                IdWorker = 1
            });

            modelBuilder.Entity<Sale>(sale => 
            {
                sale.ToTable("Venta");
                sale.HasKey(p => p.Id);
                sale.Property(p => p.Id)
                    .ValueGeneratedNever();
                sale.HasOne(P => P.Customer)
                    .WithMany()
                    .HasForeignKey(P => P.IdCustomer);
                sale.HasOne(P => P.Product)
                    .WithMany()
                    .HasForeignKey(P => P.IdProduct);
                sale.HasOne(P => P.Worker)
                    .WithMany()
                    .HasForeignKey(P => P.IdWorker);
                sale.HasData(saleInit);
            });
        }
    }
}
