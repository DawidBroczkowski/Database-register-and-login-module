using Euvic.Application.Interfaces;
using System.Net.Mail;

namespace Euvic.Application.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool ValidatePhoneNumber(string phoneNumber)
        {
            // Check if phone number is digits only
            if (phoneNumber.All(char.IsDigit) == false)
            {
                return false;
            }
            return true;
        }

        public bool ValidatePesel(string pesel, out ushort age)
        {
            age = 0;

            // Check if PESEL is digits only
            if (pesel.All(char.IsDigit) == false)
            {
                return false;
            }

            // Check if control sum is correct
            var controlSum = ControlSum(pesel);
            if (controlSum.ToString() != pesel[10].ToString())
            {
                return false;
            }

            // Check if birth date is correct
            var birthDate = TryExtractBirthDate(pesel);
            if (birthDate is null)
            {
                return false;
            }

            // Extract age
            age = (ushort)(DateTime.Now.Year - ((DateTime)birthDate).Year);
            if (DateTime.Now.DayOfYear < ((DateTime)birthDate).DayOfYear)
            {
                age--;
            }

            return true;
        }

        /// <summary>
        /// Calculates control sum of PESEL number.
        /// </summary>
        /// <param name="pesel">The PESEL number</param>
        /// <returns>The control sum</returns>
        private int ControlSum(string pesel)
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(pesel[i].ToString()) * weights[i];
            }
            int controlSum = 10 - sum % 10;
            if (controlSum == 10)
            {
                controlSum = 0;
            }
            return controlSum;
        }

        /// <summary>
        /// Extracts birth date from PESEL number.
        /// </summary>
        /// <param name="pesel">The PESEL number</param>
        /// <returns>The birth date DateTime if it's valid, null if not.</returns>
        private DateTime? TryExtractBirthDate(string pesel)
        {
            int year = 0;

            // Extract century from month format
            int month = int.Parse(pesel.Substring(2, 2));
            if (month > 0 && month < 13)
            {
                year += 1900;
            }
            else if (month > 20 && month < 33)
            {
                month -= 20;
                year += 2000;
            }
            else
            {
                return null;
            }

            // Check if year is valid
            year += int.Parse(pesel.Substring(0, 2));
            if (year > DateTime.Now.Year)
            {
                return null;
            }

            // Check if birth date is valid
            int day = int.Parse(pesel.Substring(4, 2));

            DateTime birthDate;
            try
            {
                birthDate = DateTime.Parse($"{year}-{month}-{day}");
            }
            catch
            {
                return null;
            }

            if (birthDate > DateTime.Now)
            {
                return null;
            }
            return birthDate;
        }
    }
}
