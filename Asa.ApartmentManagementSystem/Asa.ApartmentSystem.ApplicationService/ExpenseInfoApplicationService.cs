using Asa.ApartmentSystem.Infra.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using ASa.ApartmentManagement.Core.BaseInfo.Managers;
using ASa.ApartmentManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.ApplicationService
{
    public class ExpenseInfoApplicationService
    {
        ITableGatwayFactory _tableGatewayFactory;
        ExpenseManager _expenseManager;

        public ExpenseInfoApplicationService(string connectionString)
        {
            _tableGatewayFactory = new SqlTableGatewayFactory(connectionString);
            _expenseManager = new ExpenseManager(_tableGatewayFactory);
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpensesByPageAsync(int page, int size)
        {
            return await _expenseManager.GetExpensesByPageAsync(page, size);
        }

        public async Task<int> InsertExpenseAsync(ExpenseDTO expenseDTO)
        {
            await _expenseManager.InsertExpenseAsync(expenseDTO);
            return expenseDTO.Id;
        }

        public async Task DeleteExpenseById(int expenseId)
        {
            await _expenseManager.DeleteExpenseByIdAsync(expenseId);
        }

        public async Task<int> GetCountOfExpenses()
        {
            return await _expenseManager.GetCountOfExpensesAsync();
        }

        public async Task<IEnumerable<ExpenseTypeDTO>> GetAllExpenseTypesByPageAsync(int page, int size)
        {
            return await _expenseManager.GetAllExpenseTypesByPageAsync(page, size);
        }

        public async Task<int> CreateExpenseTypeAsync(string name, FormulaType formulaType)
        {
            var expenseType = new ExpenseTypeDTO { Name = name, Formula = formulaType };
            return await _expenseManager.CreateExpenseTypeAsync(expenseType);
        }

        public async Task DeleteExpenseTypeByIdAsync(int expenseTypeId)
        {
            await _expenseManager.DeleteExpenseTypeByIdAsync(expenseTypeId);
        }
    }
}
