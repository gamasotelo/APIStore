using CursoAPIPlatzi.Models.Domain;
using CursoAPIPlatzi.Models.DTO;

namespace CursoAPIPlatzi.Services
{
    public class ProductService:IProductService
    {
        private readonly StoreContext _dbContext;
        public ProductService(StoreContext dbContext)
        { 
            _dbContext = dbContext;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            List<ProductDTO> productDTOList = new List<ProductDTO>();
            var products = _dbContext.Products;
            foreach (var product in products)
            {
                var productDTO = _ConvertToDTO(product);
                productDTOList.Add(productDTO);
            }
            return productDTOList;
        }

        public ProductDTO? GetById(int id) 
        { 
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) 
            {
                return null;
            }
            var productDTO = _ConvertToDTO(product);
            return productDTO;
        }

        public ProductDTO? Create(Product product) { 
            var newProduct = new Product() { 
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            try
            {
                _dbContext.Products.Add(newProduct);
                _dbContext.SaveChanges();
                var productDTO = _ConvertToDTO(newProduct);
                return productDTO;
            }
            catch (ArgumentException) 
            {
                return null;
            }
        }

        public ProductDTO? Update(int id, Product newProduct) 
        {
            var oldProduct = _dbContext.Products.FirstOrDefault(p =>p.Id == id);
            if (oldProduct == null) 
            {
                return null;
            }
            oldProduct.Name = newProduct.Name;
            oldProduct.Price = newProduct.Price;
            _dbContext.SaveChanges();
            var productDTO = new ProductDTO()
            {
                Id = oldProduct.Id,
                Name = oldProduct.Name,
                Price = oldProduct.Price
            };
            return productDTO;
        }

        public ProductDTO? Delete(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            var productDTO = _ConvertToDTO(product);
            return productDTO;
        }

        private ProductDTO _ConvertToDTO(Product product) 
        { 
            var dto = new ProductDTO() { 
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return dto;
        }
    }

    public interface IProductService 
    {
        IEnumerable<ProductDTO> GetAll();
        ProductDTO? GetById(int id);
        ProductDTO? Create(Product product);
        ProductDTO? Update(int id, Product product);
        ProductDTO? Delete(int id);
    }
}
