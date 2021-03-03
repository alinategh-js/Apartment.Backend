using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class ChargeItemResponse
    {
        public string ExpenseName { get; set; }
        public string PayerName { get; set; }
        public decimal Amount { get; set; }
        public string FormulaName { get; set; }
    }
}
