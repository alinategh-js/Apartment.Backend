using Asa.ApartmentSystem.API.Models;
using Asa.ApartmentSystem.ApplicationService.CalculateCharge;
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
        public ChargesController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApartmentManagementCNX"].ConnectionString;
            _chargeService = new CalculateChargeApplicationService(connectionString);
        }

        [HttpPost]
        public async Task<ActionResult> CalculateCharge([FromBody] CalculateChargeRequest request)
        {
            if(request.To < request.From)
            {
                return BadRequest("[To] must not be lower than [From]");
            }
            if(request.IssueDate < request.To)
            {
                return BadRequest("[IssueDate] must not be lower than [To]");
            }
            await _chargeService.CalculateCharges(request.From, request.To, request.IssueDate);
            return Ok();
        }
    }
}
