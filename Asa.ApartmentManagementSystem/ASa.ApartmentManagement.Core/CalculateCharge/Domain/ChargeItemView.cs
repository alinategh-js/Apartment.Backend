using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core.CalculateCharge.Domain
{
    public class ChargeItemView
    {
        public int ChargeId { get; set; }
        public string ExpenseName { get; set; }
        public string PayerName { get; set; }
        public decimal Amount { get; set; }
        public string FormulaName { get; set; }
    }
}
