using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class OwnerResidentRequest
    {
        public int OwnerId { get; set; }
        public int MyProperty { get; set; }
    }
}
