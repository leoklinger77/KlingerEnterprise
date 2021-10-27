using KlingerSystem.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KlingerSystem.Employee.Infrastructure.Mapping
{
    public class EmployeeMapping : IEntityTypeConfiguration<Domain.Models.Employee>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Employee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.OwnsOne(x => x.Cpf, tf =>
            {
                tf.Property(c => c.Number)
                .HasMaxLength(Cpf.CpfMaxLength)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(x => x.Rg, tf =>
            {
                tf.Property(c => c.Number)
                .HasMaxLength(Rg.RgMaxLength)
                .HasColumnName("Rg_Number")
                .HasColumnType($"varchar({Rg.RgMaxLength})");

                tf.Property(c => c.Issuer)
                .HasColumnName("Rg_Issuer");

                tf.Property(c => c.ExpeditionDate)
                .HasColumnName("RG_ExpeditionDate");
                
            });

            builder.HasOne(x => x.Address).WithOne(x => x.Employee);
            builder.HasOne(x => x.Email).WithOne(x => x.Employee);

            builder.HasMany(x => x.Phones)
               .WithOne(x => x.Employee)
               .HasForeignKey(x => x.EmployeeId);

            builder.ToTable("TB_Employee");
        }
    }
}
