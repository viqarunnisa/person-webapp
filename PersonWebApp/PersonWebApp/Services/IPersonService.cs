using PersonWebApp.Models;

namespace PersonWebApp.Services
{
    public interface IPersonService
    {
        void AddPerson(Person person);
        IEnumerable<Person> GetPeople();
    }
}
