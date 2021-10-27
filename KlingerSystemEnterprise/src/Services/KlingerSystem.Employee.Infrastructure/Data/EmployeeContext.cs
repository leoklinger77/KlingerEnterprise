using FluentValidation.Results;
using KlingerSystem.Core.Data;
using KlingerSystem.Core.Interfaces;
using KlingerSystem.Core.Mediatr;
using KlingerSystem.Core.Messages;
using KlingerSystem.Employee.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KlingerSystem.Employee.Infrastructure.Data
{
    public class EmployeeContext : DbContext, IUnitOfWork
    {
        private readonly IMediatrHandler _mediatrHandler;

        public EmployeeContext(DbContextOptions<EmployeeContext> options, IMediatrHandler mediatorHandler) : base(options)
        {
            _mediatrHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Domain.Models.Employee> Employee { get; set; }
        public DbSet<Phone> Phone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(255)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;            

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("InsertDate") != null || 
                                entry.Entity.GetType().GetProperty("UpdateDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("InsertDate").CurrentValue = DateTime.Now;
                    entry.Property("UpdateDate").IsModified = false;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdateDate").CurrentValue = DateTime.Now;
                    entry.Property("InsertDate").IsModified = false;
                }
            }
            var success = await base.SaveChangesAsync() > 0;
            if (success) await _mediatrHandler.SendEvent(this);

            return success;
        }
    }
}
