using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.Application.Contract.Auth.Req;
using ChatApi.Application.Contract.Auth.Res;
using ChatApi.Application.Contract.Token.req;
using ChatApi.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApi.Api.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
         private readonly IAuth _authService;

        public AuthController(IAuth authService) =>
                    _authService = authService;

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] SingUp model) =>
            Ok(await _authService.Register(model));

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginModel model) =>
            Ok(await _authService.Login(model));

        [HttpPost("Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] TokenReq model) =>
            Ok(await _authService.Refresh(model));
    }
}