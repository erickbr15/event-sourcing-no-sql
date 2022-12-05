using Eventsourcing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventsourcing.DataAccess.Sql.Entities.TypesConfiguration;

internal class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "dbo");
        builder.HasKey(t => t.UserId);

        builder.Property(t => t.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(t => t.Email).HasColumnName("Email").HasMaxLength(500).IsRequired();
        builder.Property(t => t.Name).HasColumnName("Name").HasMaxLength(150).IsRequired();
        builder.Property(t => t.LastName).HasColumnName("LastName").HasMaxLength(500).IsRequired();
        builder.Property(t => t.Genre).HasColumnName("Genre").HasMaxLength(1).IsRequired();
        builder.Property(t => t.Age).HasColumnName("Age").IsRequired();

        builder.HasMany(t => t.Bookings)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .IsRequired();
    }
}
