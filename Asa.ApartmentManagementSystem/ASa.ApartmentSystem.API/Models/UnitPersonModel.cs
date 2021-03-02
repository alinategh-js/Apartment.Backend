using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class UnitPersonModel
    {
        public int UnitId { get; set; }
        public int UnitNumber { get; set; }
        public string OwnerName { get; set; }
        public string ResidentName { get; set; }
        public decimal Area { get; set; }
        public int TotalPages { get; set; }
    }
}
