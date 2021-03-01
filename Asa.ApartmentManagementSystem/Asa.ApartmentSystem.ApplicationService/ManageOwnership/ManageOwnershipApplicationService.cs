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
                var unitPeople = await uow.UnitPersonRepository.GetUnitPeopleByUnitIdWhereToIsNullAsync(unitPerson.UnitId, unitPerson.IsOwner); // returns 2 results max : 1 which IsOwner = 1 , another which IsOwner = 0
                if(unitPeople == null) // no records exists
                {
                    //insert new record
                    await uow.UnitPersonRepository.InsertUnitPersonAsync(unitPerson);
                }
                else // atleast one record exists
                {

                }
                await uow.Commit();
            }
        }
    }
}
