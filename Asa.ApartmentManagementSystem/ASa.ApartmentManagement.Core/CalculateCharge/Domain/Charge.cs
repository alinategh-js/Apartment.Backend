using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core.CalculateCharge.Domain
{
    public class Charge
    {
        public int ChargeId { get; set; }
        public int UnitId { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal Amount { get; set; }
    }
}
