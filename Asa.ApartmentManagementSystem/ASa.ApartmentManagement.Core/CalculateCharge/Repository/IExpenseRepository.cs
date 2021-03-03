using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.CalculateCharge.Repository
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetExpensesByDate(DateTime from, DateTime to);
    }
}
