using FluentValidation.Results;
using KlingerSystem.Business.Domain.Models;
using KlingerSystem.Core.Data;
using KlingerSystem.Core.Interfaces;
using KlingerSystem.Core.Mediatr;
using KlingerSystem.Core.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KlingerSystem.Business.Infrastructure.Data
{
    public class BusinessContext : DbContext, IUnitOfWork
    {
        private readonly IMediatrHandler _mediatrHandler;

        public BusinessContext(DbContextOptions<BusinessContext> options, IMediatrHandler mediatorHandler) : base(options)
        {
            _mediatrHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Cnae> Cnae { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(255)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BusinessContext).Assembly);
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
