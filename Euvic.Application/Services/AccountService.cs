using Euvic.Application.Dtos;
using Euvic.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Euvic.Domain.Models;
using Euvic.Application.Extensions;

namespace Euvic.Application.Services
{
    public class AccountService : IAccountService
    {
        private IUserRepository _repository;
        private IAuthService _authService;
        private IValidationService _validationService;

        public AccountService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<IUserRepository>();
            _authService = serviceProvider.GetRequiredService<IAuthService>();
            _validationService = serviceProvider.GetRequiredService<IValidationService>();
        }

        public async Task RegisterUserAsync(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            ushort age;

            // Check if PESEL is valid and assign age value
            if (_validationService.ValidatePesel(registerDto.Pesel, out age) is false)
            {
                var ex = new ValidationException("Can't validate PESEL.");
                ex.Data.Add("Pesel", "Invalid PESEL.");
                throw ex;
            }

            // Check if user with this PESEL already exists
            if (await _repository.CheckIfPeselIsTakenAsync(registerDto.Pesel, cancellationToken))
            {
                var ex = new ValidationException("Can't validate PESEL.");
                ex.Data.Add("Pesel", "User with that PESEL already exists. ");
                throw ex;
            }

            // Check if user with this E-mail already exists
            if (await _repository.CheckIfMailIsTakenAsync(registerDto.Email, cancellationToken))
            {
                var ex = new ValidationException("Can't validate E-mail.");
                ex.Data.Add("Email", "User with that E-mail already exists. ");
                throw ex;
            }

            // If age was supplied in the Dto, check if it matches age extracted from PESEL
            if (registerDto.Age is not null && age != registerDto.Age)
            {
                var ex = new ValidationException("Can't validate age.");
                ex.Data.Add("Age", "Supplied age value doesn't match PESEL.");
                throw ex;
            }

            // Check if E-mail is valid
            if (_validationService.ValidateEmail(registerDto.Email) is false)
            {
                var ex = new ValidationException("Can't validate E-mail.");
                ex.Data.Add("Email", "Invalid email.");
                throw ex;
            }

            // Check if phone number is valid
            if (_validationService.ValidatePhoneNumber(registerDto.PhoneNumber) is false)
            {
                var ex = new ValidationException("Can't validate phone number.");
                ex.Data.Add("PhoneNumber", "Invalid phone number. Use digits only.");
                throw ex;
            }

            // Generate password hash and salt
            _authService.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Add new user to the database
            registerDto.Age = age;
            User user = registerDto.AsUser(passwordHash, passwordSalt);

            await _repository.AddNewUserAsync(user, cancellationToken);
        }

        public async Task<string> LoginUserAsync(LoginDto loginDto, CancellationToken cancellationToken)
        {
            // Get user credentials from the database
            var user = await _repository.GetUserCredentialsAsync(loginDto.Email, cancellationToken);

            // Check if user exists
            if (user is null)
            {
                var ex = new ValidationException("Can't validate E-mail.");
                ex.Data.Add("Email", "User not found.");
                throw ex;
            }

            // Check if password is correct
            if (!_authService.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                var ex = new ValidationException("Can't validate password.");
                ex.Data.Add("Password", "Wrong password.");
                throw ex;
            }

            // Create and return a JWT token
            string token = _authService.CreateToken(user.Email);
            return token;
        }
    }
}
