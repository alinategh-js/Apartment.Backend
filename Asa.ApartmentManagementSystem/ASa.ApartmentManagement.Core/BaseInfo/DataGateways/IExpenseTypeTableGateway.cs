using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.BaseInfo.DataGateways
{
    public interface IExpenseTypeTableGateway
    {
        Task<IEnumerable<ExpenseTypeDTO>> GetAllExpenseTypesByPageAsync(int page, int size);
        Task<int> GetTotalCountOfExpenseTypes();
        Task<int> InsertExpenseTypeAsync(ExpenseTypeDTO expenseType);
        Task DeleteExpenseTypeByIdAsync(int typeId);
        Task<ExpenseTypeDTO> GetExpenseTypeByIdAsync(int expenseTypeId);
        Task<IEnumerable<ExpenseTypeDTO>> GetAllExpenseTypes();
    }
}
