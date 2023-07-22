using CursoAPIPlatzi.Models.Domain;
using CursoAPIPlatzi.Models.DTO;
using CursoAPIPlatzi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursoAPIPlatzi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productSevice) { 
            _productService = productSevice;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var productsDTO = _productService.GetAll();
            return Ok(productsDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id) 
        { 
            var productDTO = _productService.GetById(id);
            return productDTO == null ? NotFound() : Ok(productDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Product product) 
        {
            var productDTO = _productService.Create(product);
            return productDTO == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { productDTO.Id }, productDTO);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id,[FromBody]Product product) 
        {
            var productDTO = _productService.Update(id,product);
            return productDTO == null ? NotFound() : Ok(productDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) 
        {
            var productDTO = _productService.Delete(id);
            return productDTO == null ? NotFound() : Ok(productDTO);
        }
    }
}
