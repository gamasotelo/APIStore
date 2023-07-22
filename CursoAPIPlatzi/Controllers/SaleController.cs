using CursoAPIPlatzi.Models.Domain;
using CursoAPIPlatzi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CursoAPIPlatzi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        public SaleController(ISaleService saleService) 
        { 
            _saleService = saleService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var sales = _saleService.GetAll();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var sale = _saleService.GetById(id);
            return sale == null ? NotFound(): Ok(sale);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Sale sale)
        {
            var createdSale = _saleService.Create(sale);
            return createdSale == null ? BadRequest() : CreatedAtAction(nameof(GetById), new{ createdSale.Id},createdSale);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Sale sale)
        {
            var updatedSale = _saleService.Update(id,sale);
            return updatedSale == null ? BadRequest() : Ok(updatedSale);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            var deletedSale = _saleService.Delete(id);
            return deletedSale == null ? BadRequest() : Ok(deletedSale);
        }
    }
}
