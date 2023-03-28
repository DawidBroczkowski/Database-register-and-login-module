using Euvic.Application.Dtos;
using Euvic.Domain.Models;

namespace Euvic.Application.Extensions
{
    public static class DtoExtensions
    {
        public static User AsUser(this RegisterDto registerDto, byte[] passwordHash, byte[] passwordSalt)
        {
            return new User()
            {
                Age = registerDto.Age!.Value,
                Email = registerDto.Email,
                ElectricityConsumption = registerDto.ElectricityConsumption,
                Name = registerDto.Name,
                Pesel = registerDto.Pesel,
                PhoneNumber = registerDto.PhoneNumber,
                Surname = registerDto.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        }
    }
}
