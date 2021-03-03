using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class GetExpenseTypesResponse
    {
        public int TotalPages { get; set; }
        public IEnumerable<ExpenseType> ExpenseTypes { get; set; }
    }
}
