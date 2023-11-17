using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagerSolution.Core.Interfaces.Services.Products;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Products;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Products;

namespace SalesManagerSolution.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] ProductPagingViewModel request)
        {
            var products = await _productService.GetAllPaging(request);
            return Ok(products);
        }

        [HttpPost("Create")]		
		public async Task<IActionResult> CreateProduct(ProductCreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var productId = await _productService.Create(model);

			if (productId == 0)
				return BadRequest();

			var product = await _productService.GetById(productId);

			return CreatedAtAction(nameof(GetById), new { id = productId }, product);
		}

		[HttpGet("{productId}")]
		public async Task<IActionResult> GetById(int productId)
		{
			var product = await _productService.GetById(productId);
			if (product == null)
				return BadRequest("Cannot find product");
			return Ok(product);
		}
	}
}
