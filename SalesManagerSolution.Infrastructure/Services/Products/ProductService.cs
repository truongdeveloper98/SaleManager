using SalesManagerSolution.Core.ViewModels.Common;
using SalesManagerSolution.Core.Interfaces.Services.Products;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Products;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Products;
using SalesManagerSolution.Infrastructure.EntityFramework;
using SalesManagerSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using SalesManagerSolution.Core.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace SalesManagerSolution.Infrastructure.Services.Products
{
	public class ProductService : IProductService
	{
		private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ProductService(ApplicationDbContext context, IStorageService storageService)
		{
			_context = context;
			_storageService = storageService;
		}

		public async Task AddViewcount(int productId)
		{
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
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

            //Save image
            if (request.ThumbnailImage != null)
            {
                try
                {
                    product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
                }
                catch(Exception  ex)
                {

                }
            }

            _context.Products.Add(product);

			await _context.SaveChangesAsync();

			return product.Id;	
		}

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public Task<int> Delete(int productId)
		{
			throw new NotImplementedException();
		}

		public async Task<PagedResult<ProductViewModel>> GetAllPaging(ProductPagingViewModel request)
		{
            var query = from p in _context.Products
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        where pi.IsDefault == true
                        select new { p, pic, pi };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword));

            if (request.CategoryId != null && request.CategoryId != 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.p.Description,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
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
