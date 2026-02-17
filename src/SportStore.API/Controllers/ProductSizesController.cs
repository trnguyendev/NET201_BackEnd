using Microsoft.AspNetCore.Mvc;
using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;

namespace SportStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizesController : ControllerBase
    {
        private readonly IProductSizeService _productSizeService;
        public ProductSizesController(IProductSizeService productSizeService)
        {
            _productSizeService = productSizeService;
        }

        [HttpGet]
        public async Task<IActionResult> GelAll()
        {
            var productSizes = await _productSizeService.GetAllProductSizesAsync();
            return Ok(productSizes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var productSize = await _productSizeService.GetProductByIdAsync(id);
            return Ok(productSize);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductSizeRequest request)
        {
            var productSize = await _productSizeService.CreateProductSizeAsync(request);
            return Ok(productSize);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductSizeRequest request)
        {
            await _productSizeService.UpdateProductSizeAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productSizeService.DeleteProductSizeAsync(id);
            return NoContent();
        }
    }
}
