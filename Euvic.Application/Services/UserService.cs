using Euvic.Application.Dtos;
using Euvic.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Euvic.Application.Services
{
    public class UserService : IUserService
    {
        private IServiceProvider _serviceProvider;
        private IUserRepository _repository;
        private IAuthService _authService;

        public UserService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _repository = _serviceProvider.GetRequiredService<IUserRepository>();
            _authService = _serviceProvider.GetRequiredService<IAuthService>();
        }

        public async Task<IEnumerable<UserDto>> GetUserListAsync(CancellationToken cancellationToken)
        {
            var users = await _repository.GetUserListAsync(cancellationToken);
            return users;
        }

        public async Task<UserDto> GetLoggedUserAsync(string email, CancellationToken cancellationToken)
        {
            UserDto user = (await _repository.GetUserByEmailAsync(email, cancellationToken))!;
            return user;
        }

        public async Task<UserDto> GetUserAsync(string email, string password, CancellationToken cancellationToken)
        {
            // Get credentials and check if user exists
            CredentialsDto? credentials = await _repository.GetUserCredentialsAsync(email, cancellationToken);
            if (credentials is null)
            {
                var ex = new ValidationException("Can't validate E-mail.");
                ex.Data.Add("Email", "User not found.");
                throw ex;
            }

            // Verify if password is correct
            if (_authService.VerifyPasswordHash(password, credentials.PasswordHash, credentials.PasswordSalt) is false)
            {
                var ex = new ValidationException("Can't validate password.");
                ex.Data.Add("Password", "Wrong password.");
                throw ex;
            }

            // Get and return the user data
            UserDto user = (await _repository.GetUserByEmailAsync(email, cancellationToken))!;
            return user;
        }
    }
}
