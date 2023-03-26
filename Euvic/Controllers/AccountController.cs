using Euvic.Application.Dtos;
using Euvic.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Euvic.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private IServiceProvider _serviceProvider;
        private IAccountService _accountService;

        public AccountController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _accountService = _serviceProvider.GetRequiredService<IAccountService>();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<ValidationResult>> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            await _accountService.RegisterUserAsync(registerDto, cancellationToken);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ValidationResult>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken)
        {
            string token;

            // Try to log in the user
            token = await _accountService.LoginUserAsync(loginDto, cancellationToken);

            // Return JWT
            return Ok(token);
        }
    }
}
