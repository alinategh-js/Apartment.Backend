using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ASa.ApartmentManagement.Core.ManageOwnership.Repository;
using ASa.ApartmentManagement.Core.ManageOwnership.Domain;

namespace Asa.ApartmentSystem.Infra.Repositories
{
    public class EfUnitPersonRepository : IUnitPersonRepository
    {
        ApartmentDbContext _dbContext;
        public EfUnitPersonRepository(ApartmentDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task<UnitPerson> GetUnitPeopleByUnitIdWhereToIsNullAsync(int unitId, bool isOwner)
        {
            var res = _dbContext.UnitPeople.SingleOrDefault(unitPerson => unitPerson.UnitId == unitId && unitPerson.To == null && unitPerson.IsOwner == isOwner);
            return Task.FromResult(res);
        }

        public Task InsertUnitPersonAsync(UnitPerson unitPerson)
        {
            _dbContext.UnitPeople.Add(unitPerson);
            return Task.CompletedTask;
        }

        public Task UpdateUnitPersonAsync(UnitPerson unitPerson)
        {
            _dbContext.UnitPeople.Update(unitPerson);
            return Task.CompletedTask;
        }
    }
}
