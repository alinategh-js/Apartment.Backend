using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
using ASa.ApartmentManagement.Core.CalculateCharge.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.Infra.Repositories.CalculateCharge
{
    public class EfExpenseTypeRepository : IExpenseTypeRepository
    {
        ApartmentDbContext _dbContext;
        public EfExpenseTypeRepository(ApartmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<ExpenseType> GetExpenseTypeByIdAsync(int expenseTypeId)
        {
            var expenseType = _dbContext.ExpenseType.FirstOrDefault(expType => expType.ExpenseTypeId == expenseTypeId);
            return Task.FromResult(expenseType);
        }
    }
}
