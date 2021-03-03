using Asa.ApartmentSystem.Infra.Repositories.CalculateCharge;
using Asa.ApartmentSystem.Infra.Repositories.ManageOwnership.EfConfigurations;
using ASa.ApartmentManagement.Core;
using ASa.ApartmentManagement.Core.CalculateCharge.Domain;
using ASa.ApartmentManagement.Core.CalculateCharge.Repository;
using ASa.ApartmentManagement.Core.ManageOwnership.Domain;
using ASa.ApartmentManagement.Core.ManageOwnership.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.Infra.Repositories
{
    public class ApartmentDbContext : DbContext
    {
        string _connectionString;
        public ApartmentDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonUnitEntityConfiguration).Assembly);
        }

        public Task Commit() => this.SaveChangesAsync();
        

        public DbSet<PersonUnit> PersonUnit { get; set; }
        public DbSet<Charge> Charge { get; set; }
        public DbSet<ChargeItem> ChargeItem { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<ExpenseType> ExpenseType { get; set; }
        public DbSet<Unit> Unit { get; set; }


        public IUnitPersonRepository UnitPersonRepository => new EfUnitPersonRepository(this);
        public IUnitRepository UnitRepository => new EfUnitRepository(this);
        public IExpenseRepository ExpenseRepository => new EfExpenseRepository(this);
        public IExpenseTypeRepository ExpenseTypeRepository => new EfExpenseTypeRepository(this);
        public IChargeRepository ChargeRepository => new EfChargeRepository(this);
        public IChargeItemRepository chargeItemRepository => new EfChargeItemRepository(this);
    }
}
