using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Categories;
using SalesManagerSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerSolution.HttpClient
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {

            return await GetListAsync<CategoryViewModel>("/api/categories");
        }

        public async Task<CategoryViewModel> GetById(int id)
        {
            return await GetAsync<CategoryViewModel>($"/api/categories/{id}");
        }
    }
}
