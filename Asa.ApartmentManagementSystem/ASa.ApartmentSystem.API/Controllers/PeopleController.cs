using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asa.ApartmentSystem.ApplicationService;
using Asa.ApartmentSystem.API.Models;
using System.Configuration;
using Microsoft.AspNetCore.Cors;

namespace Asa.ApartmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("React")]
    public class PeopleController : ControllerBase
    {
        private readonly PersonInfoApplicationService _service;

        public PeopleController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApartmentManagementCNX"].ConnectionString;
            _service = new PersonInfoApplicationService(connectionString);
        }

        // GET: api/Person/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get([FromRoute] int id)
        {
            var personDTO = await _service.GetPersonByIdAsync(id);

            if (personDTO == null)
            {
                return NotFound($"Person with id {id} not found.");
            }

            var person = new Person { Id = personDTO.Id, FullName = personDTO.FullName, PhoneNumber = personDTO.PhoneNumber };
            return person;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PeopleAll>>> GetAllPeople()
        {
            var peopleDTOList = await _service.GetAllPeople();
            var allPeople = new List<PeopleAll>();
            foreach (var person in peopleDTOList)
            {
                var personInPeople = new PeopleAll();
                personInPeople.Id = person.Id;
                personInPeople.FullName = person.FullName;
                personInPeople.PhoneNumber = person.PhoneNumber;

                allPeople.Add(personInPeople);
            }

            return allPeople;
        }

        [HttpGet]
        public async Task<ActionResult<GetPeopleResponse>> GetPeopleByPage([FromQuery] PeopleRequest req)
        {
            var peopleList = await _service.GetAllPeopleByPageAndType(req.Page, req.Size, req.IsOwner);
            var totalCount = await _service.GetTotalCountOfPeopleAsync();
            var totalPagesDecimal = Math.Ceiling(Convert.ToDecimal(totalCount) / req.Size);
            var totalPages = Convert.ToInt32(totalPagesDecimal);

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
            var getPeopleResponse = new GetPeopleResponse { People = people, TotalPages = totalPages };
            return getPeopleResponse;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Person person)
        {
            return await _service.CreatePersonAsync(person.FullName, person.PhoneNumber);
        }

        [HttpPut("{personId}")]
        public async Task<ActionResult<int>> Update([FromRoute] int personId, [FromBody] Person person)
        {
            return await _service.UpdatePersonAsync(personId, person.FullName, person.PhoneNumber);
        }

        [HttpDelete("{personId}")]
        public async Task Delete([FromRoute] int personId)
        {
            await _service.DeletePersonByIdAsync(personId);
        }
    }
}