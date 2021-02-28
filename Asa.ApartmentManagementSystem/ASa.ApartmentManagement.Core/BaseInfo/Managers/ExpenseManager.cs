using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.BaseInfo.Managers
{
    public class ExpenseManager
    {
        ITableGatwayFactory _tablegatwayFactory;

        public ExpenseManager(ITableGatwayFactory tableGatwayFactory)
        {
            _tablegatwayFactory = tableGatwayFactory;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpensesByPageAsync(int page, int size)
        {
            IExpenseTableGateway tableGateway = _tablegatwayFactory.CreateIExpenseTableGateway();
            IEnumerable<ExpenseDTO> expenseDTOList = await tableGateway.GetExpensesByPageAsync(page, size).ConfigureAwait(false);
            return expenseDTOList;
        }

        public async Task InsertExpenseAsync(ExpenseDTO expenseDTO)
        {
            IExpenseTableGateway tableGateway = _tablegatwayFactory.CreateIExpenseTableGateway();
            int expenseId = await tableGateway.InsertExpenseAsync(expenseDTO).ConfigureAwait(false);
            expenseDTO.Id = expenseId;
        }

        public async Task DeleteExpenseByIdAsync(int expenseId)
        {
            IExpenseTableGateway tableGateway = _tablegatwayFactory.CreateIExpenseTableGateway();
            await tableGateway.DeleteExpenseByIdAsync(expenseId);
        }

        public async Task<int> GetCountOfExpensesAsync()
        {
            IExpenseTableGateway tableGateway = _tablegatwayFactory.CreateIExpenseTableGateway();
            return await tableGateway.GetCountOfExpensesAsync();
        }
    }
}
