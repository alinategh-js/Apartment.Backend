using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
using ASa.ApartmentManagement.Core.CalculateCharge.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.Infra.Repositories.CalculateCharge
{
    public class EfUnitRepository : IUnitRepository
    {
        ApartmentDbContext _dbContext;
        public EfUnitRepository(ApartmentDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<ICollection<Unit>> GetAllUnitsAsync()
        {
            var units = await _dbContext.Unit.ToListAsync();
            return units;
        }
    }
}
