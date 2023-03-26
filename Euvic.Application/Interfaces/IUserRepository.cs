using Euvic.Application.Dtos;
using Euvic.Domain.Models;

namespace Euvic.Application.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns></returns>
        Task AddNewUserAsync(User user, CancellationToken cancellationToken);
        /// <summary>
        /// Checks if user with given E-mail address and PESEL exists in database.
        /// </summary>
        /// <param name="pesel">User's PESEL number</param>
        /// <param name="email">User's E-mail address</param>
        /// <returns>True if user exists, false if not.</returns>
        Task<bool> CheckIfUserExistsAsync(string pesel, string email, CancellationToken cancellationToken);
        /// <summary>
        /// Checks if a user with given E-mail address already exists.
        /// </summary>
        /// <param name="mail">E-mail address</param>
        /// <returns>True if the E-mail address is taken, false if not.</returns>
        Task<bool> CheckIfMailIsTakenAsync(string mail, CancellationToken cancellationToken);
        /// <summary>
        /// Checks if a user with given PESEL number already exists.
        /// </summary>
        /// <param name="pesel">E-mail address</param>
        /// <returns>True if the PESEL number is taken, false if not.</returns>
        Task<bool> CheckIfPeselIsTakenAsync(string pesel, CancellationToken cancellationToken);
        /// <summary>
        /// Gets a single user's data from the database.
        /// </summary>
        /// <param name="email">User's E-mail address</param>
        /// <returns>UserDto if user was found, null if not.</returns>
        Task<UserDto?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        /// <summary>
        /// Gets user's E-mail and hashes of pasword and salt from the database.
        /// </summary>
        /// <param name="email">User's E-mail address</param>
        /// <returns>CredentialsDto if user was found, null if not.</returns>
        Task<CredentialsDto?> GetUserCredentialsAsync(string email, CancellationToken cancellationToken);
        /// <summary>
        /// Gets a list of every user's data from the database.
        /// </summary>
        /// <returns>List of UserDto.</returns>
        Task<List<UserDto>> GetUserListAsync(CancellationToken cancellationToken);
    }
}