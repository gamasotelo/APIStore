using CursoAPIPlatzi.Models.Domain;
using CursoAPIPlatzi.Models.DTO;
using CursoAPIPlatzi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CursoAPIPlatzi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        
        public CustomerController(ICustomerService customerService) {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customersDTO = _customerService.GetAll();
            return Ok(customersDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var customerDTO = _customerService.Get(id);
            return customerDTO == null ? NotFound() : Ok(customerDTO);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            var customerDTO= _customerService.Post(customer);
            return customerDTO == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = customerDTO.Id }, customerDTO);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id, [FromBody] Customer newCustomer)
        {
            var customerDTO = _customerService.Update(id,newCustomer);
            return customerDTO == null ? NotFound() : Ok(customerDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deletedCustomer = _customerService.Delete(id);
            return deletedCustomer == null ? NotFound() : Ok(deletedCustomer);
        }
    }
}
