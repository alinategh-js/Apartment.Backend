using Asa.ApartmentSystem.API.Models;
using Asa.ApartmentSystem.ApplicationService.CalculateCharge;
using Asa.ApartmentSystem.ApplicationService.ManageOwnership;
using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
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
    public class ChargesController : ControllerBase
    {
        private readonly CalculateChargeApplicationService _chargeService;
        private readonly ManageOwnershipApplicationService _manageService;
        public ChargesController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApartmentManagementCNX"].ConnectionString;
            _chargeService = new CalculateChargeApplicationService(connectionString);
        }

        [HttpPost]
        public async Task<ActionResult> CalculateCharge([FromBody] CalculateChargeRequest request)
        {
            if (request.To < request.From)
            {
                return BadRequest("[To] must not be lower than [From]");
            }
            if (request.IssueDate < request.To)
            {
                return BadRequest("[IssueDate] must not be lower than [To]");
            }
            await _chargeService.CalculateCharges(request.From, request.To, request.IssueDate);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Charge>>> GetAllCharges()
        {
            var charges = await _chargeService.GetAllCharges();
            foreach (var charge in charges)
            {
                var unit = await _manageService.GetUnitByIdAsync(charge.UnitId);
                var unitNumber = unit.Number;
                charge.UnitNumber = unitNumber;
            }
            return Ok(charges);
        }

        [HttpGet("{chargeId}")]
        public async Task<ActionResult<IEnumerable<ChargeItemResponse>>> GetAllChargeItems([FromRoute] int chargeId)
        {
            var res =  await _chargeService.GetAllChargeItemsByChargeId(chargeId);
            return Ok(res);
        }
    }
}
