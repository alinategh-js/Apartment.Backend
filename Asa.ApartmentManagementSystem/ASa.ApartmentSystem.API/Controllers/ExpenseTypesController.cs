using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asa.ApartmentSystem.ApplicationService;
using Asa.ApartmentSystem.API.Models;
using Microsoft.AspNetCore.Cors;
using ASa.ApartmentManagement.Core.Common;

namespace Asa.ApartmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("React")]
    public class ExpenseTypesController : ControllerBase
    {
        private readonly ExpenseInfoApplicationService _service;

        public ExpenseTypesController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApartmentManagementCNX"].ConnectionString;
            _service = new ExpenseInfoApplicationService(connectionString);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ExpenseType>>> GetAllExpenseTypes()
        {
            var expenseDTOList = await _service.GetAllExpenseTypes();
            List<ExpenseType> result = new List<ExpenseType>();
            foreach (var e in expenseDTOList)
            {
                var expenseType = new ExpenseType { ExpenseTypeId = e.ExpenseTypeId, FormulaName = e.FormulaName, ForOwner = e.ForOwner, Name = e.Name };
                result.Add(expenseType);
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetExpenseTypesResponse>> GetAllExpenseTypesByPage([FromQuery] ExpenseTypeRequestGet req)
        {
            var expenseTypeList = await _service.GetAllExpenseTypesByPageAsync(req.Page, req.Size);
            var totalCount = await _service.GetTotalCountOfExpenseTypesAsync();
            var totalPagesDecimal = Math.Ceiling(Convert.ToDecimal(totalCount) / req.Size);
            var totalPages = Convert.ToInt32(totalPagesDecimal);
            if (expenseTypeList.Count() == 0)
            {
                return NotFound("No people to show.");
            }

            var expenseTypes = new List<ExpenseType>();

            foreach (var receivedExpenseType in expenseTypeList)
            {
                var expenseType = new ExpenseType();

                expenseType.ExpenseTypeId = receivedExpenseType.ExpenseTypeId;
                expenseType.Name = receivedExpenseType.Name;
                expenseType.FormulaName = receivedExpenseType.FormulaName;

                expenseTypes.Add(expenseType);
            }
            var response = new GetExpenseTypesResponse { ExpenseTypes = expenseTypes, TotalPages = totalPages };

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ExpenseTypeRequestPost expenseType)
        {
            Enum.TryParse(expenseType.FormulaName, out FormulaType formula);
            if (!Enum.IsDefined(typeof(FormulaType), formula))
            {
                return NotFound($"Formula not supported.");
            }
            return await _service.CreateExpenseTypeAsync(expenseType.Name, formula, expenseType.ForOwner);
        }

        [HttpDelete("{expenseTypeId}")]
        public async Task Delete([FromRoute] int expenseTypeId)
        {
            await _service.DeleteExpenseTypeByIdAsync(expenseTypeId);
        }

        [HttpGet("formulas")]
        public ActionResult<IEnumerable<string>> GetFormulas()
        {
            var formulas = Enum.GetNames(typeof(FormulaType));
            return Ok(formulas.AsEnumerable());
        }
    }
}