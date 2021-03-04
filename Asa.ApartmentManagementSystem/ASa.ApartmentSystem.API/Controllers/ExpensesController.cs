using Asa.ApartmentSystem.API.Models;
using Asa.ApartmentSystem.ApplicationService;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
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
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseInfoApplicationService _service;

        public ExpensesController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApartmentManagementCNX"].ConnectionString;
            _service = new ExpenseInfoApplicationService(connectionString);
        }

        [HttpGet]
        public async Task<ActionResult<GetExpensesResponse>> GetExpensesByPage([FromQuery] ExpenseRequest data)
        {
            int page = data.Page;
            int size = data.Size;

            var expenseDTOList = await _service.GetExpensesByPageAsync(page, size);
            // we then need to know how many pages exists, we get the count of all the records and calculate total pages:
            var totalCount = await _service.GetCountOfExpenses();
            var totalPagesDecimal = Math.Ceiling(Convert.ToDecimal(totalCount) / size);
            var totalPages = Convert.ToInt32(totalPagesDecimal);
            
            List<Expense> result = new List<Expense>();
            foreach (var e in expenseDTOList)
            {
                var expenseType = await _service.GetExpenseTypeByIdAsync(e.CategoryId);
                var expenseTypeName = expenseType.Name;
                var expense = new Expense { Id = e.Id, CategoryId = e.CategoryId, Cost = e.Cost, From= e.From, Title = e.Title, To = e.To , ExpenseTypeName = expenseTypeName};
                result.Add(expense);
            }

            var response = new GetExpensesResponse { TotalPages = totalPages, Expenses = result };

            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ExpenseType>>> GetAllExpenseTypes()
        {
            var expenseDTOList = await _service.GetAllExpenseTypes();
            List<ExpenseType> result = new List<ExpenseType>();
            foreach(var e in expenseDTOList)
            {
                var expenseType = new ExpenseType { ExpenseTypeId = e.ExpenseTypeId, FormulaName = e.FormulaName, ForOwner = e.ForOwner, Name = e.Name };
                result.Add(expenseType);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertExpense([FromBody] ExpenseDataRequest expenseData)
        {
            var expenseDTO = new ExpenseDTO { CategoryId = expenseData.CategoryId , Cost = expenseData.Cost, From = expenseData.From, Title = expenseData.Title, To = expenseData.To};
            int expenseId = await _service.InsertExpenseAsync(expenseDTO);
            return expenseId;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense([FromRoute] int id)
        {
            await _service.DeleteExpenseById(id);
            return Ok();
        }
    }
}
