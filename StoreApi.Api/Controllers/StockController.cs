using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Application.Interfaces;

namespace StoreApi.Api.Controllers
{
    [ApiController]
    [Route("api/Stock")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _service;
        public StockController(IStockService service) => _service = service;
        
    }
}