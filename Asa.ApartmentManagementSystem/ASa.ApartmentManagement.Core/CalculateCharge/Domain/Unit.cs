using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core.CalculateCharge.Domain
{
    public class Unit
    {
        public int UnitId { get; set; }
        public int BuildingId { get; set; }
        public int Number { get; set; }
        public decimal Area { get; set; }
    }
}
