using System;
using System.IO;
using System.Text.Json;

namespace BellamyGutierrezEscher
{
    class Program
    {


        static void Main(string[] args)
        {
            //Load configuration settings
            var config = LoadConfiguration();

            //defines the default file paths from configuration ^
            var peopleFile = config.FilePaths.PeopleFile;
            var spouseDirectory = config.FilePaths.SpouseDirectory;

            //Overrides file paths if running inside Docker
            if (IsRunningInDocker())
            {
                peopleFile = "/app/people/mainfile.txt";
                spouseDirectory = "/app/people/spouses";
            }

            else
            {
                //Overrides for local execution (non-docker) to save in C:\people
                peopleFile = @"C:\people\mainfile.txt";
                spouseDirectory = @"C:\people\spouses";
            }

            //initializes PersonService with filepaths
            var personService = new PersonService(peopleFile, spouseDirectory);

            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Surname:");
            string surname = Console.ReadLine();

            Console.WriteLine("Enter Date of Birth (MM-DD-YYYY):");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
            
            //calculates age and checks business rules
            int age = BusinessRules.CalculateAge(dateOfBirth);
            bool parentalAuthorization = false;

            if (age < 16)
            {
                Console.WriteLine("Registration denied. Must be at least 16 years old.");
                return;
            }
            else if (age < 18)
            {
                //asks parental authorization for minors
                Console.WriteLine("Parent's Authorization? (yes/no):");
                parentalAuthorization = Console.ReadLine().ToLower() == "yes";

                if (!BusinessRules.IsRegistrationAllowed(new Person { DateOfBirth = dateOfBirth }, parentalAuthorization))
                {
                    Console.WriteLine("Registration denied. Parental authorization required.");
                    return;
                }
            }

            //spouse info gathering
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

            //creates person object with the collected details ^
            var person = new Person
            {
                FirstName = firstName,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                MaritalStatus = maritalStatus
            };

            //saving of the person and spouse to file
            personService.SavePerson(person, spouse, parentalAuthorization);

            Console.WriteLine("Person details saved successfully!");
        }

        //loads file paths and configuration from AppSettings.json - had to edit the json permissions (the "copy if newer")
        private static AppSettings LoadConfiguration()
        {
            string json = File.ReadAllText("AppSettings.json");
            return JsonSerializer.Deserialize<AppSettings>(json);
        }

        //checks if application is running in a Docker container
        private static bool IsRunningInDocker()
        {
            //Docker sets this environment variable when running
            return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
                   || File.Exists("/.dockerenv"); // Docker environment marker file
        }
    }
}