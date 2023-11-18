using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SalesManagerSolution.Core.Constants;
using SalesManagerSolution.Core.ViewModels.Common;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Products;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Products;
using SalesManagerSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        public async Task<bool> CreateProduct(ProductCreateViewModel request)
        {
            var sessions = _httpContextAccessor
                .HttpContext.Request.Cookies[SystemConstants.AppSettings.Token];

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.OriginalPrice.ToString()), "originalPrice");
            requestContent.Add(new StringContent(request.Stock.ToString()), "stock");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");

            var response = await client.PostAsync($"/api/products/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public Task<bool> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductViewModel> GetById(int id)
        {
            var data = await GetAsync<ProductViewModel>($"/api/products/{id}");

            return data;
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
            string url = $"/api/products/paging?PageIndex={request.PageIndex}" +
                $"&PageSize={request.PageSize}";

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                url += $"&Keyword={request.Keyword}";
            }

            if(request.CategoryId != null)
            {
                url += $"&CategoryId ={request.CategoryId}";
            }

            var data = await GetAsync<PagedResult<ProductViewModel>>(url);

            return data;
        }

        public Task<bool> UpdateProduct(ProductCreateViewModel request)
        {
            throw new NotImplementedException();
        }
    }
}
