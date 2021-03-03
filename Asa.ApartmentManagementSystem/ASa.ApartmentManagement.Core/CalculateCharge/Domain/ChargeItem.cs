using ASa.ApartmentManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core.CalculateCharge.Domain
{
    public class ChargeItem
    {
        public int ChargeItemId { get; set; }
        public int ChargeId { get; set; }
        public int PayerId { get; set; }
        public int ExpenseId { get; set; }
        public decimal Amount { get; set; }

        public void Calculate(FormulaType formula, decimal expenseAmount, decimal totalArea, int totalPeopleCount, decimal thisUnitArea, int thisUnitPeopleCount, int totalUnits)
        {
            switch (formula)
            {
                case FormulaType.Constant:
                    Amount = expenseAmount / totalUnits;
                    break;
                case FormulaType.ByArea:
                    Amount = expenseAmount * thisUnitArea / totalArea;
                    break;
                case FormulaType.ByPeople:
                    Amount = expenseAmount * thisUnitPeopleCount / totalPeopleCount;
                    break;
                case FormulaType.ByPeopleAndArea:
                    Amount = (expenseAmount / 2 * thisUnitPeopleCount / totalPeopleCount) + (expenseAmount / 2 * thisUnitArea / totalArea);
                    break;
                default:
                    break;
            }
        }
    }
}
