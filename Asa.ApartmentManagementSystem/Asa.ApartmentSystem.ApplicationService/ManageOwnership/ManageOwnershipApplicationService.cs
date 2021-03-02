using ASa.ApartmentManagement.Core;
using ASa.ApartmentManagement.Core.ManageOwnership.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.ApplicationService.ManageOwnership
{
    public class ManageOwnershipApplicationService
    {
        IUnitOfWorkFactory _unitOfWorkFactory;

        public ManageOwnershipApplicationService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        public async Task ManageOwnerResidentForUnit(UnitPerson unitPerson)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var unitP = await uow.UnitPersonRepository.GetUnitPeopleByUnitIdWhereToIsNullAsync(unitPerson.UnitId, unitPerson.IsOwner); 
                if(unitPerson.PersonId == unitP.PersonId) // specified person is already owner/resident of the unit.
                {
                     return;
                }
                else
                {
                     unitP.To = unitPerson.From;
                     await uow.UnitPersonRepository.UpdateUnitPersonAsync(unitP);
                }
                await uow.UnitPersonRepository.InsertUnitPersonAsync(unitPerson);
                await uow.Commit();
            }
        }
    }
}
