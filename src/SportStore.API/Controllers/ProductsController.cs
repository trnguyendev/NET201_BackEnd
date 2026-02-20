using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;

namespace SportStore.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
        {
            var product = await _productService.CreateProductAsync(request);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateProductRequest request)
        {
            await _productService.UpdateProductAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        // GET: api/Products/home
        [HttpGet("home")]
        public async Task<IActionResult> GetHomeProducts()
        {
            var products = await _productService.GetHomeProductsAsync();
            return Ok(products);
        }

        // GET: api/Products/detail/5
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var product = await _productService.GetProductDetailAsync(id);
            if (product == null) return NotFound("Sản phẩm không tồn tại hoặc đã ngừng kinh doanh.");
            return Ok(product);
        }
    }
}