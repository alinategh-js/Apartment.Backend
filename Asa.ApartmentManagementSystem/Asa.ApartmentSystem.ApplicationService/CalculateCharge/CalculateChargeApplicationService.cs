using Asa.ApartmentSystem.Infra.Repositories;
using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
using ASa.ApartmentManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.ApplicationService.CalculateCharge
{
    public class CalculateChargeApplicationService
    {
        string _connectionString;
        public CalculateChargeApplicationService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Charge>> GetAllCharges()
        {
            using (var apartmentDb = new ApartmentDbContext(_connectionString))
            {
                var charges = await apartmentDb.ChargeRepository.GetAllCharges();
                return charges;
            }
        }

        public async Task<IEnumerable<ChargeItemView>> GetAllChargeItemsByChargeId(int chargeId)
        {
            using(var apartmentDb = new ApartmentDbContext(_connectionString))
            {
                return await apartmentDb.ChargeItemRepository.GetAllChargeItemsByChargeId(chargeId);
            }
        }

        public async Task CalculateCharges(DateTime from, DateTime to, DateTime issueDate)
        {
            using (var apartmentDb = new ApartmentDbContext(_connectionString))
            {
                // get all units
                var units = await apartmentDb.UnitRepository.GetAllUnitsAsync();
                // get all expenses between from and to
                var expenses = await apartmentDb.ExpenseRepository.GetExpensesByDate(from, to);
                int totalPeople = 0;
                decimal totalArea = 0;
                var totalUnits = units.Count;
                foreach(var unit in units)
                {
                    var unitPerson = await apartmentDb.UnitPersonRepository.GetUnitPeopleByUnitIdWhereToIsNullAsync(unit.UnitId, true);
                    var peopleCount = unitPerson.ResidentCount;
                    var area = unit.Area;
                    totalPeople += peopleCount;
                    totalArea += area;
                }
                foreach (var unit in units)
                {
                    var charge = new Charge { UnitId = unit.UnitId, IssueDate = issueDate, Amount=0 };
                    foreach (var expense in expenses)
                    {
                        // get expenseType using expense id
                        var expenseType = await apartmentDb.ExpenseTypeRepository.GetExpenseTypeByIdAsync(expense.ExpenseTypeId);
                        var forOwner = expenseType.ForOwner;
                        var personUnit = await apartmentDb.UnitPersonRepository.GetUnitPeopleByUnitIdWhereToIsNullAsync(unit.UnitId, forOwner);
                        var payerId = personUnit.PersonId;
                        var thisUnitPeopleCount = personUnit.ResidentCount;
                        Enum.TryParse(expenseType.FormulaName, out FormulaType formulaType);
                        var chargeItem = new ChargeItem { ChargeId = charge.ChargeId, ExpenseId = expense.ExpenseId, PayerId=payerId};
                        // calculate charge item in each iteration (call a function in the appropriate formula class for calculation)
                        chargeItem.Calculate(formulaType, expense.Cost, totalArea, totalPeople, unit.Area, thisUnitPeopleCount, totalUnits);
                        charge.Amount += chargeItem.Amount;
                        // add charge item in "charge item DbSet"
                        await apartmentDb.ChargeItemRepository.InsertChargeItemAsync(chargeItem);
                    }
                    // add charge in "charge DbSet"
                    await apartmentDb.ChargeRepository.InsertCharge(charge);
                }

                // save changes of DbSets to Database (commit)
                await apartmentDb.Commit();
            }

        }
    }
}
