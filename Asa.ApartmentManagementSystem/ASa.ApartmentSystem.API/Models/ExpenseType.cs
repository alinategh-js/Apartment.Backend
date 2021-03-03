using ASa.ApartmentManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Formula { get; set; }
    }
}