﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
