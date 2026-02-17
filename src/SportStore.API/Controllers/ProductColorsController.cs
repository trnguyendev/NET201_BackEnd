using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;

namespace SportStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductColorsController : ControllerBase
    {
        private readonly IProductColorService _productColorService;
        public ProductColorsController(IProductColorService productColorService)
        {
            _productColorService = productColorService;
        }

        [HttpGet]
        public async Task<IActionResult> GelAll()
        {
            var productColors = await _productColorService.GetAllProductColorsAsync();
            return Ok(productColors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var productSize = await _productColorService.GetProductColorByIdAsync(id);
            return Ok(productSize);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductColorRequest request)
        {
            var brand = await _productColorService.CreateProductColorAsync(request);
            return Ok(brand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductColorRequest request)
        {
            await _productColorService.UpdateProductColorAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productColorService.DeleteProductColorAsync(id);
            return NoContent();
        }
    }
}
