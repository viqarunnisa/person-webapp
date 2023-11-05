using Microsoft.AspNetCore.Mvc;
using PersonWebApp.Models;
using PersonWebApp.Services;
using System.Diagnostics;

namespace PersonWebApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        /// <summary>
        /// View all person available
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            IEnumerable<Person> people = GetPeople();
            return View(people);
        }

        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// POST: Person/Add
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind("FirstName, LastName")] Person person)
        {
            if (ModelState.IsValid)
            {
                _personService.AddPerson(person);
                TempData["Message"] = $"New person {person.FirstName} {person.LastName} has been successfully added!";
                return RedirectToAction(nameof(Index));
            }

            return View(person);

        }

        private IEnumerable<Person> GetPeople()
        {
            List<Person> people= new List<Person>();
            people.AddRange(_personService.GetPeople());
            return people;
        }
    }
}