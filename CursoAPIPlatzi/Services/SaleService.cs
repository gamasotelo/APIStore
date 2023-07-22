using CursoAPIPlatzi.Models.Domain;
using CursoAPIPlatzi.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CursoAPIPlatzi.Services
{
    public class SaleService:ISaleService
    {
        private readonly StoreContext _dbContext;
        public SaleService(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<SaleDTO> GetAll(){
            List<SaleDTO> salesList = new List<SaleDTO>();
            var sales = _dbContext.Sales
                        .Include(p => p.Customer)
                        .Include(p => p.Worker)
                        .Include(p => p.Product);
            foreach (var sale in sales) 
            {
                var saleDTO = _ConvertToDTO(sale);
                salesList.Add(saleDTO);           
            }
            return salesList;
        }

        public SaleDTO? GetById(int id) 
        { 
            var sale = _dbContext.Sales
                       .Include(p => p.Customer) 
                       .Include(p => p.Worker) 
                       .Include(p => p.Product)
                       .FirstOrDefault(p => p.Id == id);
            if (sale == null) 
            {
                return null;
            }
            var saleDTO = _ConvertToDTO(sale);
            return saleDTO;
        }

        public SaleDTO? Create(Sale sale) 
        {
            var customer = _dbContext.Customers.FirstOrDefault(p => p.Id == sale.IdCustomer);
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == sale.IdProduct);
            var worker = _dbContext.Workers.FirstOrDefault(p => p.Id == sale.IdWorker);

            sale.Worker = worker;
            sale.Product = product;
            sale.Customer = customer;

            _dbContext.Sales.Add(sale);
            _dbContext.SaveChanges();
            var saleDTO = _ConvertToDTO(sale); 
            return saleDTO;
        }

        public SaleDTO? Update(int id, Sale sale) 
        {
            var oldSale = _dbContext.Sales.FirstOrDefault(p => p.Id == id);
            if (oldSale == null) 
            {
                return null;
            }
            oldSale.IdWorker = sale.IdWorker;
            oldSale.IdCustomer = sale.IdCustomer;
            oldSale.IdProduct = sale.IdProduct;

            var customer = _dbContext.Customers.FirstOrDefault(p => p.Id == sale.IdCustomer);
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == sale.IdProduct);
            var worker = _dbContext.Workers.FirstOrDefault(p => p.Id == sale.IdWorker);

            oldSale.Worker = worker;
            oldSale.Product = product;
            oldSale.Customer = customer;

            _dbContext.SaveChanges();
            var saleDTO = _ConvertToDTO(oldSale);
            return saleDTO;
        }

        public SaleDTO? Delete(int id) 
        { 
            var sale = _dbContext.Sales.FirstOrDefault(p => p.Id == id);
            if (sale == null) 
            {
                return null;
            }

            var customer = _dbContext.Customers.FirstOrDefault(p => p.Id == sale.IdCustomer);
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == sale.IdProduct);
            var worker = _dbContext.Workers.FirstOrDefault(p => p.Id == sale.IdWorker);

            sale.Worker = worker;
            sale.Product = product;
            sale.Customer = customer;

            _dbContext.Sales.Remove(sale);
            _dbContext.SaveChanges();

            var saleDTO = _ConvertToDTO(sale);
            return saleDTO;
        }

        private SaleDTO _ConvertToDTO(Sale sale)
        {
            var dto = new SaleDTO()
            {
                Id = sale.Id,
                CustomerName = sale.Customer.Name,
                WorkerName = sale.Worker.Name,
                ProductName = sale.Product.Name,
                ProductPrice = sale.Product.Price
            };
            return dto;
        }
    }

    public interface ISaleService
    {
        IEnumerable<SaleDTO> GetAll();
        SaleDTO? GetById(int id);
        SaleDTO? Create(Sale sale);
        SaleDTO? Update(int id, Sale sale);
        SaleDTO? Delete(int id);
    };
}
