using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class UnitDataRequest
    {
        public int Number { get; set; }
        public int BuildingId { get; set; }
        public string Area { get; set; }
    }
}
