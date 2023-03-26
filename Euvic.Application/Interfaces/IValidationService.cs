namespace Euvic.Application.Interfaces
{
    public interface IValidationService
    {
        /// <summary>
        /// Validates the E-mail address.
        /// </summary>
        /// <param name="email">E-mail address</param>
        /// <returns>True if the E-mail is vaild, false if not.</returns>
        bool ValidateEmail(string email);
        /// <summary>
        /// Validates the PESEL number.
        /// </summary>
        /// <param name="pesel">PESEL number</param>
        /// <param name="age">Age extracted from the PESEL</param>
        /// <returns>True if the PESEL is valid, false if not.</returns>
        bool ValidatePesel(string pesel, out ushort age);
        /// <summary>
        /// Checks if the phone number contains digits only.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns>True if the phone number is valid, false if not.</returns>
        bool ValidatePhoneNumber(string phoneNumber);
    }
}