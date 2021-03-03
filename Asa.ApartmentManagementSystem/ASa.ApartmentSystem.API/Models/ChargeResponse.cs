using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class ChargeResponse
    {
        public int ChargeId { get; set; }
        public int UnitId { get; set; }
        public int UnitNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal Amount { get; set; }
    }
}
