using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
using ASa.ApartmentManagement.Core.CalculateCharge.Repository;
using System;
using System.Collections.Generic;
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
        public Task InsertCharge(Charge charge)
        {
            _dbContext.Charge.Add(charge);
            return Task.CompletedTask;
        }
    }
}
