using ASa.ApartmentManagement.Core;
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

        public Task Commit() => this.SaveChangesAsync();
        

        public DbSet<PersonUnit> PersonUnit { get; set; }

        public IUnitPersonRepository UnitPersonRepository => new EfUnitPersonRepository(this);
    }
}
