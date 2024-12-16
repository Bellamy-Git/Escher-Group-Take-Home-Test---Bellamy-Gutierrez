using System;
using System.IO;
using System.Text.Json;

namespace BellamyGutierrezEscher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load configuration
            var config = LoadConfiguration();

            var peopleFile = config.FilePaths.PeopleFile;
            var spouseDirectory = config.FilePaths.SpouseDirectory;

            var personService = new PersonService(peopleFile, spouseDirectory);

            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Surname:");
            string surname = Console.ReadLine();

            Console.WriteLine("Enter Date of Birth (MM-DD-YYYY):");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

            int age = BusinessRules.CalculateAge(dateOfBirth);
            bool parentalAuthorization = false;

            if (age < 16)
            {
                Console.WriteLine("Registration denied. Must be at least 16 years old.");
                return;
            }
            else if (age < 18)
            {
                Console.WriteLine("Parent's Authorization? (yes/no):");
                parentalAuthorization = Console.ReadLine().ToLower() == "yes";

                if (!BusinessRules.IsRegistrationAllowed(new Person { DateOfBirth = dateOfBirth }, parentalAuthorization))
                {
                    Console.WriteLine("Registration denied. Parental authorization required.");
                    return;
                }
            }

            Console.WriteLine("Enter Marital Status (Single/Married):");
            string maritalStatus = Console.ReadLine();

            Person spouse = null;
            if (maritalStatus.ToLower() == "married")
            {
                Console.WriteLine("Enter Spouse First Name:");
                string spouseFirstName = Console.ReadLine();

                Console.WriteLine("Enter Spouse Surname:");
                string spouseSurname = Console.ReadLine();

                Console.WriteLine("Enter Spouse Date of Birth (MM-DD-YYYY):");
                DateTime spouseDateOfBirth = DateTime.Parse(Console.ReadLine());

                spouse = new Person
                {
                    FirstName = spouseFirstName,
                    Surname = spouseSurname,
                    DateOfBirth = spouseDateOfBirth,
                    MaritalStatus = "Married"
                };
            }

            var person = new Person
            {
                FirstName = firstName,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                MaritalStatus = maritalStatus
            };

            personService.SavePerson(person, spouse, parentalAuthorization);

            Console.WriteLine("Person details saved successfully!");
        }

        private static AppSettings LoadConfiguration()
        {
            string json = File.ReadAllText("appsettings.json");
            return JsonSerializer.Deserialize<AppSettings>(json);
        }
    }
}