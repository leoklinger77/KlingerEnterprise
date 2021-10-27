using KlingerSystem.Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KlingerSystem.Business.Infrastructure.Mapping
{
    public class CnaeMapping : IEntityTypeConfiguration<Cnae>
    {
        public void Configure(EntityTypeBuilder<Cnae> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Devision)
                .IsRequired()
            .HasColumnType("varchar(7)");

            builder.Property(x => x.Description)
                .IsRequired()
            .HasColumnType("varchar(500)");

            builder.HasMany(x => x.Companies)
               .WithOne(x => x.Cnae)
               .HasForeignKey(x => x.CnaeId);

            builder.ToTable("TB_Cnae");
        }
    }
}
