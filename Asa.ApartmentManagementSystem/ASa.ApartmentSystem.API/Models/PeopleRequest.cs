using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class PeopleRequest
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int IsOwner { get; set; }
    }
}
