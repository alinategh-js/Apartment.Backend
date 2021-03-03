using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core.CalculateCharge.Domain
{
    public class ExpenseType
    {
        public int ExpenseTypeId { get; set; }
        public string Name { get; set; }
        public string FormulaName { get; set; }
        public bool ForOwner { get; set; }
    }
}
