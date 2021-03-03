using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class OwnerResidentRequest
    {
        public int OwnerId { get; set; }
        public int ResidentId { get; set; }
        public DateTime Date { get; set; }
        public int ResidentCount { get; set; }
    }
}
