using KlingerSystem.Business.Domain.Models;
using KlingerSystem.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KlingerSystem.Business.Infrastructure.Mapping
{
    public class CompanyMapping : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Cpf, tf =>
            {
                tf.Property(c => c.Number)
                .HasMaxLength(Cpf.CpfMaxLength)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(x => x.Cnpj, tf =>
            {
                tf.Property(c => c.Number)
                .HasMaxLength(Cnpj.CnpjMaxLength)
                .HasColumnName("Cnpj")
                .HasColumnType($"varchar({Cnpj.CnpjMaxLength})");
            });

            builder.HasOne(x => x.Address).WithOne(x => x.Company);
            builder.HasOne(x => x.Email).WithOne(x => x.Company);

            builder.HasMany(x => x.Phones)
               .WithOne(x => x.Company)
               .HasForeignKey(x => x.CompanyId);            

            builder.ToTable("TB_Company");
        }
    }
}
