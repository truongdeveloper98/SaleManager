using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesManagerSolution.Core.Constants;
using SalesManagerSolution.Core.Interfaces.Services.Products;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Products;
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

        [HttpGet("Index")]
        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            keyword = string.Empty;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateViewModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = await _productApiClient.GetById(productId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }

    }
}
