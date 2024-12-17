using System.IO;
using System.Text;

namespace BellamyGutierrezEscher
{
    public class PersonService
    {
        //path and directory for main.txt and spouse txt files
        private readonly string _peopleFilePath;
        private readonly string _spouseDirectoryPath;

        public PersonService(string peopleFilePath, string spouseDirectoryPath)
        {
            //initialize file paths
            _peopleFilePath = peopleFilePath;
            _spouseDirectoryPath = spouseDirectoryPath;

            //ensures the directories exist - null coalescing operator
            Directory.CreateDirectory(Path.GetDirectoryName(_peopleFilePath) ?? string.Empty);
            Directory.CreateDirectory(_spouseDirectoryPath);
        }

        public void SavePerson(Person person, Person spouse = null, bool parentalAuthorization = false)
        {
            //saves spouse details if provided
            if (spouse != null)
            {
                string spouseFilePath = Path.Combine(_spouseDirectoryPath, $"{spouse.FirstName}_{spouse.Surname}.txt");
                person.SpouseFilePath = spouseFilePath;
                
                //writes spouse details to spouse file
                File.WriteAllText(spouseFilePath, FormatPerson(spouse), Encoding.UTF8);
            }

            //appends person details to main.txt file
            string personRecord = FormatPerson(person, parentalAuthorization);
            File.AppendAllText(_peopleFilePath, personRecord + Environment.NewLine, Encoding.UTF8);
        }

        private string FormatPerson(Person person, bool parentalAuthorization = false)
        {
            //includes parental authorization and the spouse file path
            string spouseFilePath = string.IsNullOrWhiteSpace(person.SpouseFilePath) ? "null" : person.SpouseFilePath;
            string parentalAuthField = parentalAuthorization ? "yes" : string.Empty;

            return $"{person.FirstName}|{person.Surname}|{person.DateOfBirth:MM-dd-yyyy}|{person.MaritalStatus}|{parentalAuthField}|{spouseFilePath}";
        }
    }
}