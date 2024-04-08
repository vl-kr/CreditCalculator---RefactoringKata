using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCalculator.After
{
    internal static class CustomerValidator
    {
        public static bool Validate(string firstName,
        string lastName,
        string email,
        DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            if (!email.Contains('@') && !email.Contains('.'))
            {
                return false;
            }

            var age = CalculateAge(dateOfBirth);

            if (age < 21)
            {
                return false;
            }
            return true;
        }
    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        var age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month ||
            now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)
        {
            age--;
        }
        return age;
    }
}
