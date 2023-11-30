﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagerSolution.Core.Interfaces.Users;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Authentications;

namespace SalesManagerSolution.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userService.Register(request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		//PUT: http://localhost/api/users/id
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] UserUpdateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userService.Update(id, request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPut("{id}/roles")]
		public async Task<IActionResult> RoleAssign(int id, [FromBody] RoleAssignRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userService.RoleAssign(id, request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		//http://localhost/api/users/paging?pageIndex=1&pageSize=10&keyword=
		[HttpGet("paging")]
		public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
		{
			var products = await _userService.GetUsersPaging(request);
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var user = await _userService.GetById(id);
			return Ok(user);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _userService.Delete(id);
			return Ok(result);
		}
	}
}
