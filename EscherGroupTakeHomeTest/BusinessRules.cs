using System;

namespace BellamyGutierrezEscher
{
    public static class BusinessRules
    {
        //checks if registration is allowed - based on age and parental authorization
        public static bool IsRegistrationAllowed(Person person, bool parentalAuthorization)
        {
            int age = CalculateAge(person.DateOfBirth);

            if (age < 16)
                return false;

            if (age < 18 && !parentalAuthorization)
                return false;

            return true;
        }

        //calculates age base on DOB
        public static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}