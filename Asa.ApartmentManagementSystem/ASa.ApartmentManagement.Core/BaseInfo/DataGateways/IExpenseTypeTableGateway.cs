using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.BaseInfo.DataGateways
{
    public interface IExpenseTypeTableGateway
    {
        Task<IEnumerable<ExpenseTypeDTO>> GetAllExpenseTypesByPage(int page, int size);
        Task<int> InsertExpenseType(ExpenseTypeDTO expenseType);
        Task DeleteExpenseTypeById(int typeId);
    }
}
