using Asa.ApartmentSystem.Infra.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using ASa.ApartmentManagement.Core.BaseInfo.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.ApplicationService
{
    public class ExpenseInfoApplicationService
    {
        ITableGatwayFactory tableGatewayFactory;

        public ExpenseInfoApplicationService(string connectionString)
        {
            tableGatewayFactory = new SqlTableGatewayFactory(connectionString);
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpensesByPageAsync(int page, int size)
        {
            ExpenseManager expenseManager = new ExpenseManager(tableGatewayFactory);
            return await expenseManager.GetExpensesByPageAsync(page, size);
        }

        public async Task<int> InsertExpenseAsync(ExpenseDTO expenseDTO)
        {
            ExpenseManager expenseManager = new ExpenseManager(tableGatewayFactory);
            await expenseManager.InsertExpenseAsync(expenseDTO);
            return expenseDTO.Id;
        }

        public async Task DeleteExpenseById(int expenseId)
        {
            ExpenseManager expenseManager = new ExpenseManager(tableGatewayFactory);
            await expenseManager.DeleteExpenseByIdAsync(expenseId);
        }

        public async Task<int> GetCountOfExpenses()
        {
            ExpenseManager expenseManager = new ExpenseManager(tableGatewayFactory);
            return await expenseManager.GetCountOfExpensesAsync();
        }
    }
}
