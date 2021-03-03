using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.CalculateCharge.Repository
{
    public interface IUnitRepository
    {
        Task<ICollection<Unit>> GetAllUnitsAsync();
    }
}
