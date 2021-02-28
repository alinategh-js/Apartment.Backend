using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core.BaseInfo.DTOs
{
    public class OwnerResidentDTO
    {
        public int PersonId { get; set; }
        public int UnitId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int UnitNumber { get; set; }
        public bool IsOwner { get; set; }
    }
}
