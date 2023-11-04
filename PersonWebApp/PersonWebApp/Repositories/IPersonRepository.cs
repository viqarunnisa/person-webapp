using PersonWebApp.Models;

namespace PersonWebApp.Repositories
{
    public interface IPersonRepository
    {
        void AddPerson(Person person);
        IEnumerable<Person> GetPeople();
    }
}
