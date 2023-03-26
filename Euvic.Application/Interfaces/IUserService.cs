using Euvic.Application.Dtos;

namespace Euvic.Application.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Gets the data of an already logged in user.
        /// </summary>
        /// <param name="email">User's E-mail</param>
        /// <returns>User data of the logged user.</returns>
        Task<UserDto> GetLoggedUserAsync(string email, CancellationToken cancellationToken);
        /// <summary>
        /// Gets the data of a user that is not already logged in.
        /// </summary>
        /// <param name="email">User's E-mail</param>
        /// <param name="password">User's password</param>
        /// <returns>User data of the user with given credentials.</returns>
        Task<UserDto> GetUserAsync(string email, string password, CancellationToken cancellationToken);
        /// <summary>
        /// Gets the data of all users.
        /// </summary>
        /// <returns>A list with every user.</returns>
        Task<IEnumerable<UserDto>> GetUserListAsync(CancellationToken cancellationToken);
    }
}