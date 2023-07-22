using CursoAPIPlatzi.Models.Domain;
using CursoAPIPlatzi.Models.DTO;

namespace CursoAPIPlatzi.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly StoreContext _dbContext;
        public CustomerService(StoreContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CustomerDTO> GetAll() 
        {
            List<CustomerDTO> customersDTO = new List<CustomerDTO>();
            var customers = _dbContext.Customers;

            foreach (var customer in customers) 
            { 
                var customerDTO = new CustomerDTO() { 
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address,
                    Debit = customer.Debit,
                };
                customersDTO.Add(customerDTO);
            }

            return customersDTO;
        }

        public CustomerDTO? Get(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(p => p.Id == id);
            if (customer == null)
            {
                return null;
            }
            var customerDTO = new CustomerDTO()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Debit = customer.Debit
            };
            return customerDTO;
        }

        public CustomerDTO? Post(Customer customer) 
        {
            var newCustomer = new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Debit = customer.Debit
            };
            try {
                _dbContext.Customers.Add(newCustomer);
                _dbContext.SaveChanges();

                var customerDTO = _ConvertToDTO(newCustomer);
                return customerDTO;
            }
            catch (ArgumentException) { 
                return null;
            }
        } 
        
        public CustomerDTO? Update(int id, Customer newCustomer) 
        {
            var oldCustomer = _dbContext.Customers.FirstOrDefault(p => p.Id == id);
            if (oldCustomer != null)
            {
                oldCustomer.Name = newCustomer.Name;
                oldCustomer.Address = newCustomer.Address;
                oldCustomer.Debit = newCustomer.Debit;
                _dbContext.SaveChanges();

                var customerDTO = _ConvertToDTO(oldCustomer);
                return customerDTO;
            }
            return null;
        }

        public  CustomerDTO? Delete(int id) {
            var customer = _dbContext.Customers.FirstOrDefault(p => p.Id == id);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChangesAsync();
                var customerDTO = _ConvertToDTO(customer);
                return customerDTO;
            }
            return null;
        }

        private CustomerDTO _ConvertToDTO(Customer customer)
        {
            var dto = new CustomerDTO()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Debit = customer.Debit,
            };
            return dto;
        }
    }

    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAll();
        CustomerDTO? Get(int id);
        CustomerDTO? Post(Customer customer);
        CustomerDTO? Update(int id, Customer newCustomer);
        CustomerDTO? Delete(int id);
    }
}
