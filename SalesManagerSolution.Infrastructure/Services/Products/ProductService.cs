using eShopSolution.ViewModels.Common;
using SalesManagerSolution.Core.Interfaces.Services.Products;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Products;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Products;
using SalesManagerSolution.Infrastructure.EntityFramework;
using SalesManagerSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace SalesManagerSolution.Infrastructure.Services.Products
{
	public class ProductService : IProductService
	{
		private readonly ApplicationDbContext _context;

		public ProductService(ApplicationDbContext context)
		{
			_context = context;
		}

		public Task AddViewcount(int productId)
		{
			throw new NotImplementedException();
		}

		public async Task<int> Create(ProductCreateViewModel request)
		{
			var product = new Product()
			{
				Name = request.Name,
				Description = request.Description,
				Price = request.Price,
				OriginalPrice = request.OriginalPrice,
				IsFeatured = request.IsFeatured,
				IsDeleted = false,
				Stock = request.Stock,
				ViewCount = 0
			};

			_context.Products.Add(product);

			await _context.SaveChangesAsync();

			return product.Id;	
		}

		public Task<int> Delete(int productId)
		{
			throw new NotImplementedException();
		}

		public Task<PagedResult<ProductViewModel>> GetAllPaging(ProductPagingViewModel request)
		{
			throw new NotImplementedException();
		}

		public async Task<ProductViewModel> GetById(int productId)
		{
			var product = await _context.Products
				                        .Where(x=>x.Id == productId)
										.Select(product => new ProductViewModel()
										{
											Id = product.Id,
											DateCreated = product.DateCreated,
											Description = product.Description ,
											Name = product.Name,
											OriginalPrice = product.OriginalPrice,
											Price = product.Price,
											Stock = product.Stock,
											ViewCount = product.ViewCount,
										})
										.FirstOrDefaultAsync();

			return product == null ? new ProductViewModel() : product;
		}

		public async Task<int> Update(ProductCreateViewModel request)
		{
			var product = await GetById(request.Id);

			product.Name = request.Name;
			product.OriginalPrice = request.OriginalPrice;
			product.Price = request.Price;
			product.Stock = request.Stock;
			product.Description = request.Description;

			await _context.SaveChangesAsync();

			return product.Id;

		}

		public Task<bool> UpdatePrice(int productId, decimal newPrice)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateStock(int productId, int addedQuantity)
		{
			throw new NotImplementedException();
		}
	}
}
