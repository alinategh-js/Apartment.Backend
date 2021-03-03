using Asa.ApartmentSystem.Infra.Repositories;
using ASa.ApartmentManagement.Core;
using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
using ASa.ApartmentManagement.Core.ManageOwnership.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.ApplicationService.ManageOwnership
{
    public class ManageOwnershipApplicationService
    {
        string _connectionString;

        public ManageOwnershipApplicationService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task ManageOwnerResidentForUnit(PersonUnit unitPerson)
        {
            using (var apartmentDb = new ApartmentDbContext(_connectionString))
            {
                var unitP = await apartmentDb.UnitPersonRepository.GetUnitPeopleByUnitIdWhereToIsNullAsync(unitPerson.UnitId, unitPerson.IsOwner); 
                if(unitPerson.PersonId == unitP.PersonId) // specified person is already owner/resident of the unit.
                {
                     return;
                }
                else
                {
                     unitP.To = unitPerson.From;
                     await apartmentDb.UnitPersonRepository.UpdateUnitPersonAsync(unitP);
                }
                await apartmentDb.UnitPersonRepository.InsertUnitPersonAsync(unitPerson);
                await apartmentDb.Commit();
            }
        }

        public async Task<Unit> GetUnitByIdAsync(int unitId)
        {
            using (var apartmentDb = new ApartmentDbContext(_connectionString))
            {
                var unit = await apartmentDb.UnitRepository.GetUnitByIdAsync(unitId);
                return unit;
            }
        }
    }
}
