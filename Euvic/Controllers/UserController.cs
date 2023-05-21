using Euvic.Application.Dtos;
using Euvic.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Euvic.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IServiceProvider _serviceProvider;
        private IUserService _userService;

        public UserController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userService = _serviceProvider.GetRequiredService<IUserService>();
        }

        [Authorize]
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserListAsync(CancellationToken cancellationToken)
        {
            var users = await _userService.GetUserListAsync(cancellationToken);
            return Ok(users);
        }

        [Authorize]
        [HttpGet("Self")]
        public async Task<ActionResult<UserDto>> GetLoggedUserAsync(CancellationToken cancellationToken)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            UserDto user = await _userService.GetLoggedUserAsync(email, cancellationToken);
            return Ok(user);
        }
    }
}
