using Asa.ApartmentSystem.API.Models;
using Asa.ApartmentSystem.ApplicationService;
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
        private readonly BuildingInfoApplicationService _service;

        public UnitsController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApartmentManagementCNX"].ConnectionString;
            _service = new BuildingInfoApplicationService(connectionString);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitPerson>>> GetUnitsByPage([FromBody] UnitPersonRequest data)
        {
            int page = data.Page;
            int size = data.Size;
            var unitPersonDTOList = await _service.GetUnitsByPage(page, size);
            // we then need to know how many pages exists, we get the count of all the records and calculate total pages:
            var totalPages = await _service.GetCountOfUnitPerson() / size;

            List<UnitPerson> result = new List<UnitPerson>();
            foreach (var u in unitPersonDTOList)
            {
                var unitPerson = new UnitPerson { TotalPages = totalPages, UnitId = u.UnitId, Area = u.Area, OwnerName = u.OwnerName, ResidentName = u.ResidentName, UnitNumber = u.UnitNumber };
                result.Add(unitPerson);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertUnit([FromBody] UnitDataRequest unitData)
        {
            int unitId = await _service.InsertUnit(unitData.BuildingId, unitData.Number, Convert.ToDecimal(unitData.Area));
            return unitId;
        }
    }
}
