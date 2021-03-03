using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core.CalculateCharge.Domain
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string Title { get; set; }
        public int ExpenseTypeId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal Cost { get; set; }
    }
}
