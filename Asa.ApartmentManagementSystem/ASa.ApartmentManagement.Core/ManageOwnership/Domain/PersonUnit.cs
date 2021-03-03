using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core.ManageOwnership.Domain
{
    public class PersonUnit
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int PersonId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsOwner { get; set; }
        public int ResidentCount { get; set; }
    }
}
