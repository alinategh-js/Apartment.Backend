using Asa.ApartmentSystem.API.Models;
using Asa.ApartmentSystem.ApplicationService;
using Asa.ApartmentSystem.ApplicationService.ManageOwnership;
using Asa.ApartmentSystem.Infra.Repositories;
using ASa.ApartmentManagement.Core.ManageOwnership.Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("React")]
    public class UnitsController : ControllerBase
    {
        private readonly BuildingInfoApplicationService _buildingService;
        private readonly ManageOwnershipApplicationService _manageService;

        public UnitsController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApartmentManagementCNX"].ConnectionString;
            _buildingService = new BuildingInfoApplicationService(connectionString);
            _manageService = new ManageOwnershipApplicationService(connectionString);
        }

        [HttpGet]
        public async Task<ActionResult<GetUnitsResponse>> GetUnitsByPage([FromQuery] int page, [FromQuery] int size)
        {
            var unitPersonDTOList = await _buildingService.GetUnitsByPage(page, size);
            // we then need to know how many pages exists, we get the count of all the records and calculate total pages:
            var totalCount = await _buildingService.GetCountOfUnitPerson();
            var totalPagesDecimal = (int) Math.Ceiling( (double) totalCount /size);
            var totalPages = Convert.ToInt32(totalPagesDecimal);
             
            List<UnitPersonModel> result = new List<UnitPersonModel>();
            foreach (var u in unitPersonDTOList)
            {
                var unitPerson = new UnitPersonModel { UnitId = u.UnitId, Area = u.Area, OwnerName = u.OwnerName, ResidentName = u.ResidentName, UnitNumber = u.UnitNumber };
                result.Add(unitPerson);
            }
            var getUnitsResponse = new GetUnitsResponse { TotalPages = totalPages, UnitPersonModels = result };
            return Ok(getUnitsResponse);
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertUnit([FromBody] UnitDataRequest unitData)
        {
            int unitId = await _buildingService.InsertUnit(unitData.BuildingId, unitData.Number, Convert.ToDecimal(unitData.Area));
            return unitId;
        }

        [HttpPut("{unitId}")]
        public async Task<ActionResult> SetUnitOwnerResident([FromBody] OwnerResidentRequest ownerResidentRequest, [FromRoute] int unitId)
        {
            var owner = new PersonUnit { From = ownerResidentRequest.Date, PersonId = ownerResidentRequest.OwnerId, UnitId = unitId, IsOwner = true, ResidentCount = ownerResidentRequest.ResidentCount };
            await _manageService.ManageOwnerResidentForUnit(owner);
            var resident = new PersonUnit { From = ownerResidentRequest.Date, PersonId = ownerResidentRequest.ResidentId, UnitId = unitId, IsOwner = false, ResidentCount = ownerResidentRequest.ResidentCount };
            await _manageService.ManageOwnerResidentForUnit(resident);
            return Ok();
        }
    }
}
