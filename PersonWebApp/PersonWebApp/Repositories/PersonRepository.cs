using PersonWebApp.Models;
using System.Data;
using System.IO;
using System.Text.Json;

namespace PersonWebApp.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private const string _filePathConfig = "PersonFilePath";
        private readonly string _filePath;


        public PersonRepository(IConfiguration configuration)
        {
            _filePath = configuration.GetValue(_filePathConfig, "person.json");

            if (!File.Exists(_filePath))
            {
                FileStream fs = File.Create(_filePath);
                fs.Dispose();
            }
        }

        public void AddPerson(Person person)
        {
            var jsonString = JsonSerializer.Serialize(person);

            using (StreamWriter sw = File.AppendText(_filePath))
            {
                sw.WriteLine(jsonString);
            }
        }

        public IEnumerable<Person> GetPeople()
        {
            var people = new List<Person>();

            using StreamReader sr = new StreamReader(_filePath);
            while (!sr.EndOfStream)
            {
                var row = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(row))
                {
                    var person = JsonSerializer.Deserialize<Person>(row);
                    if (person != null)
                    {
                        people.Add(person);
                    }
                }
            }

            return people;
        }
    }
}
