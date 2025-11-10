using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookingSystem.Application.Contract.Categories.req;
using BookingSystem.Application.Contract.Categories.res;
using BookingSystem.Application.Interfaces;
using BookingSystem.Core.common;
using ChatApi.Application.Contract.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CategoryApi.Api.Controllers
{
    [ApiController]
    [Route("api/Category")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost("GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<PagedResult<CategoryReq>>>> GetAllCategories(GetCategoryReq Category)
        => Ok(await _service.GetAllAsync(Category));
        
        [HttpPost("GetAllCategoriesForStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<PagedResult<CategoryReq>>>> GetAllCategoriesForStore( GetCategoryReq Category)
        => Ok(await _service.GetAllForStoreAsync(Category));


        [HttpGet("{CategoryId}", Name = "GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<CategoryRes>>> GetCategoryById(int CategoryId)
        => Ok(await _service.GetByIdAsync(CategoryId));

        [HttpDelete("DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public async Task<ActionResult<GeneralResponse<bool?>>> DeleteCategory([FromRoute] int CategoryId)
        => Ok(await _service.DeleteAsync(CategoryId));

        [HttpPut("UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<bool?>>> UpdateCategory([FromBody] CategoryReq Category, int CategoryId)
        =>
            Ok(await _service.Update(Category, CategoryId));

        [HttpPost("AddCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<int>>> AddCategory([FromBody] CategoryReq Category)
        {
            
           return Ok(await _service.AddAsync(Category));
        }
    }
}