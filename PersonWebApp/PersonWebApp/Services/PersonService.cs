using PersonWebApp.Models;
using PersonWebApp.Repositories;

namespace PersonWebApp.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public void AddPerson(Person person)
        {
            _personRepository.AddPerson(person);
        }

        public IEnumerable<Person> GetPeople()
        {
            return _personRepository.GetPeople();
        }
    }
}
