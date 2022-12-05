using Eventsourcing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventsourcing.DataAccess.Sql.Entities.TypesConfiguration;

internal class CountryTypeConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries", "dbo");
        builder.HasKey(t => t.Code);

        builder.Property(t => t.Code).HasColumnName("Code").IsRequired();
        builder.Property(t => t.Name).HasColumnName("Name").HasMaxLength(250).IsRequired();

        builder.HasMany(t=> t.Cities)
            .WithOne(t => t.Country)
            .HasForeignKey(t=> t.CountryCode)
            .IsRequired();
    }
}
