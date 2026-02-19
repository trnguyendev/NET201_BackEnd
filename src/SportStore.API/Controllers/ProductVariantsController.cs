using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;

namespace SportStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantsController : ControllerBase
    {
        private readonly IProductVariantService _variantService;
        public ProductVariantsController(IProductVariantService variantService)
        {
            _variantService = variantService;
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var variants = await _variantService.GetVariantsByProductIdAsync(productId);
            return Ok(variants);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductVariantRequest request) // LƯU Ý: Ở ĐÂY DÙNG [FromBody] VÌ LÀ JSON
        {
            var variant = await _variantService.CreateVariantAsync(request);
            return Ok(variant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductVariantRequest request)
        {
            await _variantService.UpdateVariantAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _variantService.DeleteVariantAsync(id);
            return NoContent();
        }
    }
}
