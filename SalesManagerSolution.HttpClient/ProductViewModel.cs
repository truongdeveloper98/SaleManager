using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SalesManagerSolution.Core.ViewModels.Common;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Products;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Products;
using SalesManagerSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerSolution.HttpClient
{
    public class ProductApiClient : BaseApiClient,IProductApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProductApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public Task<bool> CreateProduct(ProductCreateViewModel request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetById(int id, string languageId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetLatestProducts(string languageId, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ProductViewModel>> GetPagings(ProductPagingViewModel request)
        {
            var data = await GetAsync<PagedResult<ProductViewModel>>(
                $"/api/products/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}&categoryId={request.CategoryId}");

            return data;
        }

        public Task<bool> UpdateProduct(ProductCreateViewModel request)
        {
            throw new NotImplementedException();
        }
    }
}
