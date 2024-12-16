using System;

namespace BellamyGutierrezEscher
{
    public static class BusinessRules
    {
        public static bool IsRegistrationAllowed(Person person, bool parentalAuthorization)
        {
            int age = CalculateAge(person.DateOfBirth);

            if (age < 16)
                return false;

            if (age < 18 && !parentalAuthorization)
                return false;

            return true;
        }

        public static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}