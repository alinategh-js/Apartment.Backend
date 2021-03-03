using ASa.ApartmentManagement.Core.ManageOwnership.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asa.ApartmentSystem.Infra.Repositories.ManageOwnership.EfConfigurations
{
    public class PersonUnitEntityConfiguration : IEntityTypeConfiguration<PersonUnit>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PersonUnit> builder)
        {
            builder
                 .Property(personUnit => personUnit.To)
                 .IsRequired()
                 .HasColumnType("Date")
                 .HasDefaultValueSql("GetDate()");
        }
    }
}
