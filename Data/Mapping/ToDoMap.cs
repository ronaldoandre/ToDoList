using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TODOLIST.Domain.Models;

namespace Data.Mapping
{
    public class ToDoMap : IEntityTypeConfiguration<ToDo>
    {
        public void Configure(EntityTypeBuilder<ToDo> builder)
        {
            builder.ToTable("ToDos");

            builder.HasKey(u => u.ToDoId);
            builder.HasIndex(u => u.Titulo);
            builder.Property(u => u.Titulo)
                .IsRequired();
            builder.Property(u => u.Descricao)
                .IsRequired();
            builder.HasOne(x => x.User)
                .WithMany(x => x.ToDos)
                .HasForeignKey(x => x.UserId);

        }
    }
}
