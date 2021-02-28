﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.API.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal Cost { get; set; }
        public int TotalPages { get; set; }
    }
}