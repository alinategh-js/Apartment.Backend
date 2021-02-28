using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.BaseInfo.DataGateways
{
    public interface IExpenseTableGateway
    {
        Task<IEnumerable<ExpenseDTO>> GetExpensesByPageAsync(int page, int size);
        Task<int> InsertExpenseAsync(ExpenseDTO expenseDto);
        Task DeleteExpenseByIdAsync(int expenseId);
        Task<int> GetCountOfExpensesAsync();
    }
}