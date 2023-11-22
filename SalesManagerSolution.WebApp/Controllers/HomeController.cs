using Microsoft.AspNetCore.Mvc;
using SalesManagerSolution.Core.Constants;
using SalesManagerSolution.Core.ViewModels.Common;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels;
using SalesManagerSolution.Database.Pages;
using SalesManagerSolution.HttpClient;
using System.Drawing.Printing;
using System.Text.RegularExpressions;

namespace SalesManagerSolution.WebApp.Controllers
{
	public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly IProductApiClient _productApiClient;
		private readonly ICategoryApiClient _categoryApiClient;

		public HomeController(ILogger<HomeController> logger,
		  IProductApiClient productApiClient,
		  ICategoryApiClient categoryApiClient)
		{
			_logger = logger;
			_productApiClient = productApiClient;
			_categoryApiClient = categoryApiClient;
		}

		public async Task<IActionResult> Index()
        {

			var resultProducts = await _productApiClient.GetFeaturedProducts(SystemConstants.ProductSettings.NumberOfFeaturedProducts);


            var request = new PagingRequestBase()
            {
                PageIndex = 1,
                PageSize = 10
            };

            var resultCategories = await _categoryApiClient.GetAll(request);

			var viewModel = new HomeViewModel
			{
				ProductViewModels = resultProducts,
				CategoryViewModels = resultCategories.Items
			};
			return View(viewModel);
        }

		public async Task<IActionResult> GetById(int productId)
		{
			var result = await _productApiClient.GetById(productId);

			return Json(result);
		}


	}
}
