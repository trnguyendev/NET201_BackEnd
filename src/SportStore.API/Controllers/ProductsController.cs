using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;

namespace SportStore.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
        {
            var product = await _productService.CreateProductAsync(request);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateProductRequest request)
        {
            await _productService.UpdateProductAsync(id, request);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("home")]
        public async Task<IActionResult> GetHomeProducts([FromQuery] int page = 1, [FromQuery] int limit = 20)
        {
            var products = await _productService.GetHomeProductsAsync(page, limit);
            return Ok(products);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var product = await _productService.GetProductDetailAsync(id);
            if (product == null) return NotFound("Sản phẩm không tồn tại hoặc đã ngừng kinh doanh.");
            return Ok(product);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId, [FromQuery] int page = 1, [FromQuery] int limit = 20)
        {
            var products = await _productService.GetProductsByCategoryIdAsync(categoryId, page, limit);
            if (products == null) return NotFound("Không có sản phẩm nào thuộc loại này.");
            return Ok(products);
        }
    }
}