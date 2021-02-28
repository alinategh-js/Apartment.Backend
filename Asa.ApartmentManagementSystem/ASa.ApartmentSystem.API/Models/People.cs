﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class People
    {
        public int PersonId { get; set; }
        public int UnitId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int UnitNumber { get; set; }
        public bool IsOwner { get; set; }
    }
}
