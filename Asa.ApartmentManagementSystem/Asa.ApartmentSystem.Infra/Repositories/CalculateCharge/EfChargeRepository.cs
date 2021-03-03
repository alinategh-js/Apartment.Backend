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
    public class EfChargeRepository : IChargeRepository
    {
        ApartmentDbContext _dbContext;
        public EfChargeRepository(ApartmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Charge>> GetAllCharges()
        {
            return await _dbContext.Charge.ToListAsync();
        }

        public Task InsertCharge(Charge charge)
        {
            _dbContext.Charge.Add(charge);
            return Task.CompletedTask;
        }
    }
}
