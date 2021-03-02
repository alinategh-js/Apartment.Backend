﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asa.ApartmentSystem.ApplicationService;
using Asa.ApartmentSystem.API.Models;

namespace Asa.ApartmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypeController : ControllerBase
    {
        private readonly ExpenseInfoApplicationService _service;

        public ExpenseTypeController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApartmentManagementCNX"].ConnectionString;
            _service = new ExpenseInfoApplicationService(connectionString);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseType>>> GetAllExpenseTypesByPage([FromBody] ExpenseTypeRequest req)
        {
            var expenseTypeList = await _service.GetAllExpenseTypesByPageAsync(req.Page, req.Size);
            var totalCount = await _service.GetTotalCountOfExpenseTypesAsync();
            var totalPages = totalCount / req.Size;

            if (expenseTypeList.Count() == 0)
            {
                return NotFound("No people to show.");
            }

            var expenseTypes = new List<ExpenseType>();

            foreach (var receivedExpenseType in expenseTypeList)
            {
                var expenseType = new ExpenseType();

                expenseType.Id = receivedExpenseType.Id;
                expenseType.Name = receivedExpenseType.Name;
                expenseType.Formula = receivedExpenseType.Formula;

                expenseTypes.Add(expenseType);
            }

            return expenseTypes;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ExpenseType expenseType)
        {
            return await _service.CreateExpenseTypeAsync(expenseType.Name, expenseType.Formula);
        }

        [HttpDelete("{expenseTypeId}")]
        public async Task Delete([FromRoute] int expenseTypeId)
        {
            await _service.DeleteExpenseTypeByIdAsync(expenseTypeId);
        }
    }
}