using ASa.ApartmentManagement.Core.ChargeCalculation.Repositories;
using ASa.ApartmentManagement.Core.ManageOwnership.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core
{
    public interface IUnitOfWork:IDisposable
    {
        Task Commit();
        IBuildingRepository BuildingRepository { get; }
        IExpensRepository ExpensRepository { get; }
        IChargeRepository ChargeRepository { get; }
        IUnitPersonRepository UnitPersonRepository { get; }
    }
}
