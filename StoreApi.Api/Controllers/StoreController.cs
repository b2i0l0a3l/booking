using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookingSystem.Core.common;
using ChatApi.Application.Contract.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Application.Contract.StoreContract.req;
using StoreSystem.Application.Contract.StoreContract.res;
using StoreSystem.Application.Interfaces;
using StoreSystem.Application.Services.StoreService;

namespace StoreApi.Api.Controllers
{
    [ApiController]
    [Route("api/Store")]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _store;
        public StoreController(IStoreService store) => _store = store;
       
        [HttpPost("GetAllStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse<PagedResult<StoreRes>>>> GetAllStore( GetStoreReq store)
        => Ok(await _store.GetAllAsync(store));

        [HttpGet("{StoreId}", Name = "GetStoreById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<StoreRes>>> GetStoreById(int StoreId)
        => Ok(await _store.GetByIdAsync(StoreId));

        [HttpDelete("{StoreId}",Name ="DeleteStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public async Task<ActionResult<GeneralResponse<bool?>>> DeleteStore( int StoreId)
        => Ok(await _store.DeleteAsync(StoreId));

        [HttpPut("UpdateStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<bool?>>> UpdateStore([FromBody] StoreReq store, int StoreId)
        =>
            Ok(await _store.Update(store, StoreId));
        

        [HttpPost("AddStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResponse<int>>> AddStore([FromBody] StoreReq store)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated");
            return Ok(await _store.AddAsync(store,userId));
        }
        
    }
}