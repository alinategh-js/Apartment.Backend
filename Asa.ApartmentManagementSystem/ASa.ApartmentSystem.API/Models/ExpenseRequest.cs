﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class ExpenseRequest
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
