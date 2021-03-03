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

        public async Task<IEnumerable<ChargeItemView>> GetAllChargeItemsByChargeId(int chargeId)
        {
            return await _dbContext.ChargeItemView.Where(chargeItem => chargeItem.ChargeId == chargeId).ToListAsync();
        }
    }
}
