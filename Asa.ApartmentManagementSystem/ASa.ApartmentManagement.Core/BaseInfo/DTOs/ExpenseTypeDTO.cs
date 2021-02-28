using ASa.ApartmentManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core.BaseInfo.DTOs
{
    public class ExpenseTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FormulaType Formula { get; set; }
    }
}
