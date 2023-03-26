using Euvic.Application.Dtos;
using Euvic.Application.Interfaces;
using Euvic.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Euvic.Infrastructure.DataAccess
{
    public class DbUserRepository : IUserRepository
    {
        private UserContext _db;

        public DbUserRepository(UserContext db)
        {
            _db = db;
        }

        public async Task AddNewUserAsync(User user, CancellationToken cancellationToken)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<UserDto>> GetUserListAsync(CancellationToken cancellationToken)
        {
            List<UserDto> users  = await _db.Users
               .IgnoreAutoIncludes()
               .Select(u => new UserDto
               {
                   Pesel = u.Pesel,
                   Name = u.Name,
                   Surname = u.Surname,
                   Email = u.Email,
                   PhoneNumber = u.PhoneNumber,
                   Age = u.Age,
                   ElectricityConsumption = u.ElectricityConsumption
               })
               .ToListAsync(cancellationToken);
           
            return users;
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            UserDto? user = await _db.Users
                .IgnoreAutoIncludes()
                .Select(u => new UserDto
                {
                    Pesel = u.Pesel,
                    Name = u.Name,
                    Surname = u.Surname,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Age = u.Age,
                    ElectricityConsumption = u.ElectricityConsumption
                })
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            return user;
        }

        public async Task<bool> CheckIfUserExistsAsync(string pesel, string email, CancellationToken cancellationToken)
        {
            bool exists = await _db.Users
                .IgnoreAutoIncludes()
                .AnyAsync(u => u.Pesel == pesel && u.Email == email, cancellationToken);
            return exists;
        }

        public async Task<bool> CheckIfPeselIsTakenAsync(string pesel, CancellationToken cancellationToken)
        {
            bool taken = await _db.Users
                .IgnoreAutoIncludes()
                .AnyAsync(u => u.Pesel == pesel, cancellationToken);
            return taken;
        }

        public async Task<bool> CheckIfMailIsTakenAsync(string mail, CancellationToken cancellationToken)
        {
            bool taken = await _db.Users
                .IgnoreAutoIncludes()
                .AnyAsync(u => u.Email == mail, cancellationToken);
            return taken;
        }

        public async Task<CredentialsDto?> GetUserCredentialsAsync(string email, CancellationToken cancellationToken)
        {
            CredentialsDto? credentials = await _db.Users
                .IgnoreAutoIncludes()
                .Where(u => u.Email == email)
                .Select(u =>
                new CredentialsDto
                {
                    Email = email,
                    PasswordHash = u.PasswordHash,
                    PasswordSalt = u.PasswordSalt
                })
                .FirstOrDefaultAsync(cancellationToken);

            return credentials;
        }
    }
}