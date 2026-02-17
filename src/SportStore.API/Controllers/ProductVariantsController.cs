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
        private readonly IProductVariantService _productVariantService;
        public ProductVariantsController(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }

        [HttpGet]
        public async Task<IActionResult> GelAll()
        {
            var productVariants = await _productVariantService.GetAllProductVariantsAsync();
            return Ok(productVariants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var productVariant = await _productVariantService.GellProductVariantByIdAsnyc(id);
            return Ok(productVariant);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVariantRequest request)
        {
            var brand = await _productVariantService.CreateProductVarianAsync(request);
            return Ok(brand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductVariantRequest request)
        {
            await _productVariantService.UpdateProductVarianAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productVariantService.DeleteProductVariantAsync(id);
            return NoContent();
        }
    }
}
