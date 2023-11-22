using Microsoft.EntityFrameworkCore;
using SalesManagerSolution.Core.Interfaces.Services.Carts;
using SalesManagerSolution.Core.ViewModels.Common;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Carts;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Categories;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Carts;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Categories;
using SalesManagerSolution.Domain.Entities;
using SalesManagerSolution.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerSolution.Infrastructure.Services.Carts
{
	public class CartService : ICartService
	{
		private readonly ApplicationDbContext _context;

		public CartService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<int> Create(CartResquestViewModel request)
		{
			var cart = new Cart()
			{
				Quantity = request.Quantity,
				ProductId = request.ProductId,
				Price = request.Price,
				UserId = request.UserId
			};

			_context.Carts.Add(cart);

			return await _context.SaveChangesAsync();
		}

		public async Task<int> Delete(DeleteCartRequest request)
		{
			var cart = await _context.Carts
									 .Where(x => x.Id == request.Id)
									 .FirstOrDefaultAsync();

			if (cart is null)
			{
				throw new Exception($"Cant find cart with Id : {request.Id}");
			}

			_context.Carts.Remove(cart);

			return await _context.SaveChangesAsync();
		}

		public async Task<PagedResult<CartViewModel>> GetAll()
		{
			var query = from c in _context.Carts
						select new { c };

			int totalRow = await query.CountAsync();

			var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(x => new CategoryViewModel()
				{
					Id = x.c.Id,
					Name = x.c.Name,
					Description = x.c.Description
				}).ToListAsync();

			//4. Select and projection
			var pagedResult = new PagedResult<CategoryViewModel>()
			{
				TotalRecords = totalRow,
				PageSize = request.PageSize,
				PageIndex = request.PageIndex,
				Items = data
			};

			return pagedResult;
		}


		public async Task<CartViewModel> GetById(int id)
		{
			var query = from c in _context.Categories
						where c.Id == id
						select new { c };
			//var result = await query.Select(x => new CartViewModel()
			//{
			//	Id = x.c.Id,
			//	Name = x.c.Name,
			//	ParentId = x.c.ParentId
			//}).FirstOrDefaultAsync();

			return result ?? new CartViewModel();
		}

		public async Task<int> Update(CartResquestViewModel request)
		{
			var cart = await _context.Carts
										  .Where(x => x.Id == request.Id)
										  .FirstOrDefaultAsync();

			if (cart is null)
			{
				throw new Exception($"Cant find cart with Id : {request.Id}");
			}

			//category.Name = request.Name;
			//category.Description = request.Description ?? string.Empty;
			//category.UpdatedAt = DateTime.Now;

			_context.Carts.Update(cart);

			return await _context.SaveChangesAsync();
		}
	}
}
