using Eventsourcing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventsourcing.DataAccess.Sql.Entities.TypesConfiguration;

internal class CityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities", "dbo");
        builder.HasKey(t => t.Code);

        builder.Property(t => t.Code).HasColumnName("Code").IsRequired();
        builder.Property(t => t.CountryCode).HasColumnName("CountryCode").IsRequired();
        builder.Property(t => t.Name).HasColumnName("Name").HasMaxLength(250).IsRequired();

        builder.HasMany(t => t.Airports)
            .WithOne(t => t.City)
            .HasForeignKey(t => t.CityCode)
            .IsRequired();
    }
}
