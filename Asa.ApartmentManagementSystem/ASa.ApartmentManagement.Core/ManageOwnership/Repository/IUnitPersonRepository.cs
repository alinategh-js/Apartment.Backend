using ASa.ApartmentManagement.Core.ManageOwnership.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.ManageOwnership.Repository
{
    public interface IUnitPersonRepository
    {
        Task<PersonUnit> GetUnitPeopleByUnitIdWhereToIsNullAsync(int unitId, bool isOwner);
        Task InsertUnitPersonAsync(PersonUnit unitPerson);
        Task UpdateUnitPersonAsync(PersonUnit unitPerson);
    }
}
