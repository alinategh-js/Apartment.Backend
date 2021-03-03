using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
using ASa.ApartmentManagement.Core.CalculateCharge.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.Infra.Repositories.CalculateCharge
{
    public class EfExpenseRepository : IExpenseRepository
    {
        ApartmentDbContext _dbContext;
        public EfExpenseRepository(ApartmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Expense>> GetExpensesByDate(DateTime from, DateTime to)
        {
            var expenses = await _dbContext.Expense.Where(expense => expense.From >= from && expense.To <= to).ToListAsync();
            return expenses;
        }
    }
}
