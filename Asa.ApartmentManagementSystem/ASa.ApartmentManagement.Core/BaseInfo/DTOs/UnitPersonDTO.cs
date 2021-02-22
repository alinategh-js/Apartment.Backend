using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core.BaseInfo.DTOs
{
    public class UnitPersonDTO
    {
        public int UnitId { get; set; }
        public int UnitNumber { get; set; }
        public string OwnerName { get; set; }
        public string ResidentName { get; set; }
        public int Area { get; set; }
    }
}
