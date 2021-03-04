using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ASa.ApartmentManagement.Core.ManageOwnership.Repository;
using ASa.ApartmentManagement.Core.ManageOwnership.Domain;
using Microsoft.EntityFrameworkCore;

namespace Asa.ApartmentSystem.Infra.Repositories
{
    public class EfUnitPersonRepository : IUnitPersonRepository
    {
        ApartmentDbContext _dbContext;
        public EfUnitPersonRepository(ApartmentDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<PersonUnit> GetCurrentUnitPeopleByUnitIdAsync(int unitId, bool isOwner)
        {
            //var res = await _dbContext.PersonUnit.FirstOrDefaultAsync(unitPerson => unitPerson.UnitId == unitId && unitPerson.To == null && unitPerson.IsOwner == isOwner);
            var res = await _dbContext.PersonUnit.Where(unitPerson => unitPerson.IsOwner == true).FirstOrDefaultAsync();
            return res;
        }

        public Task InsertUnitPersonAsync(PersonUnit unitPerson)
        {
            _dbContext.PersonUnit.Add(unitPerson);
            return Task.CompletedTask;
        }

        public Task UpdateUnitPersonAsync(PersonUnit unitPerson)
        {
            _dbContext.PersonUnit.Update(unitPerson);
            return Task.CompletedTask;
        }
    }
}
