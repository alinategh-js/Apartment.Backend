using Asa.ApartmentSystem.API.Models;
using Asa.ApartmentSystem.ApplicationService;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("React")]
    public class BuildingController : ControllerBase
    {
        private readonly BuildingInfoApplicationService _service;

        public BuildingController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApartmentManagementCNX"].ConnectionString;
            _service = new BuildingInfoApplicationService(connectionString);
        }


        // GET: api/building
        [HttpGet]
        public async Task<ActionResult<Building>> GetBuilding()
        {
            var buildingDTO = await _service.GetFirstBuilding();

            if(buildingDTO == null)
            {
                return NotFound("No buildings found.");
            }

            var building = new Building { Id = buildingDTO.Id, Name = buildingDTO.Name, NumberOfUnits = buildingDTO.NumberOfUnits };
            return building;
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertBuilding([FromBody] BuildingDataRequest buildingData)
        {
            int buildingId = await _service.CreateBuilding(buildingData.Name, buildingData.NumberOfUnits);
            return buildingId;
        }
    }
}
