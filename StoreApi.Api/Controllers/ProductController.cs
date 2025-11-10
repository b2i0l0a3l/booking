using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Application.Contract.ProductContract.Req;
using BookingSystem.Application.Contract.ProductContract.Res;
using BookingSystem.Application.Interfaces;
using BookingSystem.Core.common;
using ChatApi.Application.Contract.Common;
using Microsoft.AspNetCore.Mvc;

namespace StoreApi.Api.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service) => _service= service;
       
        [HttpPost("GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<PagedResult<ProductRes>>>> GetAllProducts( GetProductReq Product)
        => Ok(await _service.GetAllAsync(Product));

        [HttpGet("{ProductId}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<ProductRes>>> GetProductById(int ProductId)
        => Ok(await _service.GetByIdAsync(ProductId));

        [HttpDelete("{ProductId}",Name ="DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<bool?>>> DeleteProduct(int ProductId)
        => Ok(await _service.DeleteAsync(ProductId));

        [HttpPut("UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<bool?>>> UpdateProduct([FromBody] ProductReq Product, int ProductId)
        =>
            Ok(await _service.Update(Product, ProductId));
        

        [HttpPost("AddProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<int>>> AddProduct([FromBody] ProductReq Product)
        {
            return Ok(await _service.AddAsync(Product));
        }
    }
}