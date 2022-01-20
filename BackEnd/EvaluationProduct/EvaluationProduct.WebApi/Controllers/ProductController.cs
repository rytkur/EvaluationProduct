using System.Collections.Generic;
using System.Threading.Tasks;
using EvaluationProduct.Domain.Model;
using EvaluationProduct.Lib;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationProduct.WebApi.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductProvider _productProvider;

        public ProductController(IProductProvider productProvider)
        {
            _productProvider = productProvider;
        }

        // GET: api/GetProducts
        [Route("Products")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productProvider.GetAllProducts();

            return new OkObjectResult(products);
        }

        // GET: api/GetProducts
        [Route("GetProducts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int count)
        {
            var products = await _productProvider.GetProducts(count);

            return new OkObjectResult(products);
        }

        // POST: api/AddProduct
        [Route("Product")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product request)
        {
            var result = await _productProvider.AddProduct(request);

            return new OkObjectResult(result);
        }

        // PUT: api/UpdateProduct
        [Route("Product")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product request)
        {
            var result = await _productProvider.UpdateProduct(request);

            if (result < 1)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/DeleteProduct
        [Route("Product")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var result = await _productProvider.RemoveProductById(id);

            if (result < 1)
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/AddDemoProduct
        [Route("AddDemoProduct")]
        [HttpPost]
        public async Task<IActionResult> AddDemoProduct()
        {
            var product = new Product()
            {
                Name = "Riyant Kurniawan",
                Price = 100,
                Quantity = 999
            };

            var result = await _productProvider.AddProduct(product);

            return new OkObjectResult(result);
        }
    }
}
