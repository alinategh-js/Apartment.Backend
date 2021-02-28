using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asa.ApartmentSystem.ApplicationService;
using Asa.ApartmentSystem.API.Models;
using System.Configuration;

namespace Asa.ApartmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonInfoApplicationService _service;

        public PersonController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApartmentManagementCNX"].ConnectionString;
            _service = new PersonInfoApplicationService(connectionString);
        }

        // GET: api/Person/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get([FromRoute]int id)
        {
            var personDTO = await _service.GetPersonByIdAsync(id);

            if (personDTO == null)
            {
                return NotFound($"Person with id {id} not found.");
            }

            var person = new Person { Id = personDTO.Id, FullName = personDTO.FullName, PhoneNumber = personDTO.PhoneNumber };
            return person;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<People>>> GetPeople([FromBody] PeopleRequest req)
        {
            var peopleList = await _service.GetAllPeopleByPageAndType(req.Page, req.Size, req.IsOwner);
            var totalCount = await _service.GetTotalCountOfPeopleAsync();
            var totalPages = totalCount / req.Size;

            if (peopleList == null)
            {
                return NotFound("No people to show.");
            }

            var people = new List<People>();

            foreach (var person in peopleList)
            {
                var personInPeople = new People();

                personInPeople.PersonId = person.PersonId;
                personInPeople.UnitId = person.UnitId;
                personInPeople.FullName = person.FullName;
                personInPeople.PhoneNumber = person.PhoneNumber;
                personInPeople.UnitNumber = person.UnitNumber;
                personInPeople.IsOwner = person.IsOwner;

                people.Add(personInPeople);
            }

            return people;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Person person)
        {
            return await _service.CreatePersonAsync(person.FullName, person.PhoneNumber);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Put([FromBody] Person person)
        {
            return await _service.UpdatePersonAsync(person.Id, person.FullName, person.PhoneNumber);
        }

        [HttpDelete]
        public async Task Delete([FromRoute] int personId)
        {
            await _service.DeletePersonByIdAsync(personId);
        }
    }
}
