using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using ASa.ApartmentManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.BaseInfo.Managers
{
    public class ExpenseManager
    {
        ITableGatwayFactory _tableGatewayFactory;
        public ExpenseManager(ITableGatwayFactory tableGatwayFactory)
        {
            _tableGatewayFactory = tableGatwayFactory;
        }

        public async Task<IEnumerable<ExpenseTypeDTO>> GetAllExpenseTypesByPageAsync(int page, int size)
        {
            var tableGateway = _tableGatewayFactory.CreateIExpenseTypeTableGateway();
            return await tableGateway.GetAllExpenseTypesByPageAsync(page, size);
        }

        public async Task<int> CreateExpenseTypeAsync(ExpenseTypeDTO expenseType)
        {
            const int MAX_EXPENSE_TYPE_NAME_LENGTH = 50;
            const int MIN_EXPENSE_TYPE_NAME_LENGTH = 5;

            var personNameIsNotValid = string.IsNullOrWhiteSpace(expenseType.Name) || expenseType.Name.Length > MAX_EXPENSE_TYPE_NAME_LENGTH || expenseType.Name.Length < MIN_EXPENSE_TYPE_NAME_LENGTH;

            if (personNameIsNotValid)
            {
                throw new ValidationException(ErrorCodes.Invalid_Person_Name, $"Person name should be between {MIN_EXPENSE_TYPE_NAME_LENGTH} and {MAX_EXPENSE_TYPE_NAME_LENGTH}.");
            }

            var tableGateway = _tableGatewayFactory.CreateIExpenseTypeTableGateway();
            return await tableGateway.InsertExpenseTypeAsync(expenseType).ConfigureAwait(false);
        }

        public async Task DeleteExpenseTypeByIdAsync(int expenseTypeId)
        {
            var tableGateway = _tableGatewayFactory.CreateIExpenseTypeTableGateway();
            await tableGateway.DeleteExpenseTypeByIdAsync(expenseTypeId).ConfigureAwait(false);
        }
    }
}
