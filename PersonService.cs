using System.IO;
using System.Text;

namespace BellamyGutierrezEscher
{
    public class PersonService
    {
        private readonly string _peopleFilePath;
        private readonly string _spouseDirectoryPath;

        public PersonService(string peopleFilePath, string spouseDirectoryPath)
        {
            _peopleFilePath = peopleFilePath;
            _spouseDirectoryPath = spouseDirectoryPath;

            Directory.CreateDirectory(Path.GetDirectoryName(_peopleFilePath) ?? string.Empty);
            Directory.CreateDirectory(_spouseDirectoryPath);
        }

        public void SavePerson(Person person, Person spouse = null, bool parentalAuthorization = false)
        {
            if (spouse != null)
            {
                string spouseFilePath = Path.Combine(_spouseDirectoryPath, $"{spouse.FirstName}_{spouse.Surname}.txt");
                person.SpouseFilePath = spouseFilePath;

                File.WriteAllText(spouseFilePath, FormatPerson(spouse), Encoding.UTF8);
            }

            string personRecord = FormatPerson(person, parentalAuthorization);
            File.AppendAllText(_peopleFilePath, personRecord + Environment.NewLine, Encoding.UTF8);
        }

        private string FormatPerson(Person person, bool parentalAuthorization = false)
        {
            // Adds "null" if no spouse file path exists
            string spouseFilePath = string.IsNullOrWhiteSpace(person.SpouseFilePath) ? "null" : person.SpouseFilePath;

            //Adds "yes" if parental authorization is required and granted
            string parentalAuthField = parentalAuthorization ? "yes" : string.Empty;

            return $"{person.FirstName}|{person.Surname}|{person.DateOfBirth:MM-dd-yyyy}|{person.MaritalStatus}|{parentalAuthField}|{spouseFilePath}";
        }
    }
}