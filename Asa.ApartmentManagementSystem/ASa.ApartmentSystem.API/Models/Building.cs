using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfUnits { get; set; }
    }
}
