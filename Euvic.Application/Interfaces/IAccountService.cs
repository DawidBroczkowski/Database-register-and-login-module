using Euvic.Application.Dtos;

namespace Euvic.Application.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <param name="loginDto">Dto with user's email and password.</param>
        /// <returns>JSON Web Token with E-mail claim.</returns>
        Task<string> LoginUserAsync(LoginDto loginDto, CancellationToken cancellationToken);
        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="registerDto">Dto with user's register data data.</param>
        /// <returns></returns>
        Task RegisterUserAsync(RegisterDto registerDto, CancellationToken cancellationToken);
    }
}