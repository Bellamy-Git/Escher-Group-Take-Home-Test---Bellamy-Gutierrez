using System;
namespace BellamyGutierrezEscher
{
    public class Person
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseFilePath { get; set; } // For married people
    }
}