namespace Euvic.Application.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Generates a password hash and salt.
        /// </summary>
        /// <param name="password">Plain text password given by the user.</param>
        /// <param name="passwordHash">Generated password hash.</param>
        /// <param name="passwordSalt">Generated password salt.</param>
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        /// <summary>
        /// Creates a JSON Web Token for the logged in user.
        /// </summary>
        /// <param name="user">Logged in user.</param>
        /// <returns>JSON Web Token with user's E-mail claim.</returns>
        string CreateToken(string email);
        /// <summary>
        /// Checks if the password's hash and salt match with the repository.
        /// </summary>
        /// <param name="password">Plain text password given by the user.</param>
        /// <param name="passwordHash">Password hash from the repository.</param>
        /// <param name="passwordSalt">Password salt from the repository.</param>
        /// <returns>True if the password matches, false if not.</returns>
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}