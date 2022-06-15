using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TODOLIST.Domain.Models;

namespace Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(p => p.UserId);
            builder.HasIndex(p => p.Email)
                .IsUnique();
            builder.Property(u => u.Email)
                .IsRequired();
            builder.Property(u => u.FullName)
                .IsRequired();

        }
    }
}
