using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asa.ApartmentSystem.Infra.Repositories.EfConfigurations
{
    public class ChargeItemViewEntityConfiguration : IEntityTypeConfiguration<ChargeItemView>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ChargeItemView> builder)
        {
            builder.ToView("vwChargeItem").HasNoKey();
        }
    }
}
