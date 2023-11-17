using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesManagerSolution.Core.Constants;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Products;
using SalesManagerSolution.HttpClient;

namespace SalesManagerSolution.AdminApp.Controllers
{
	public class ProductController : Controller
	{
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        //private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient,
            IConfiguration configuration
           /* ICategoryApiClient categoryApiClient*/)
        {
            _configuration = configuration;
            _productApiClient = productApiClient;
            //_categoryApiClient = categoryApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {

            var request = new ProductPagingViewModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryId = categoryId
            };
            var data = await _productApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;

            //var categories = await _categoryApiClient.GetAll(languageId);
            ViewBag.Categories = new List<SelectListItem>
                                { 
                                    new SelectListItem()
                                    {
                                        Text = "Thể thao",
                                        Value = "1",
                                        Selected = true
                                    },
                                    new SelectListItem()
                                    {
                                        Text = "Công sở",
                                        Value = "2",
                                        Selected = false
                                    },
                                };

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
    }
}
