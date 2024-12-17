namespace BellamyGutierrezEscher
{

    //this file is for deserialization
    //AppSettings class is used to deserialize the JSON configuration data in C# objects
    public class AppSettings
    {
        public FilePaths FilePaths { get; set; }
    }

    public class FilePaths
    {
        public string PeopleFile { get; set; }
        public string SpouseDirectory { get; set; }
    }
}