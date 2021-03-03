using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
using ASa.ApartmentManagement.Core.CalculateCharge.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.Infra.Repositories.CalculateCharge
{
    public class EfChargeItemRepository : IChargeItemRepository
    {
        ApartmentDbContext _dbContext;
        public EfChargeItemRepository(ApartmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task InsertChargeItemAsync(ChargeItem chargeItem)
        {
            _dbContext.ChargeItem.Add(chargeItem);
            return Task.CompletedTask;
        }
    }
}
