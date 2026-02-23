using EBP.API.Models;
using EBP.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBP.API.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController(ISender _sender) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Create(RegisterRequest registerRequest)
        {
            var command = new RegisterUserCommand(registerRequest.Email, registerRequest.Password, registerRequest.IsAdmin);
            await _sender.Send(command);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var command = new LoginUserCommand(loginRequest.Email, loginRequest.Password);
            var token = await _sender.Send(command);
            return Ok(new { token });
        }
    }
}
