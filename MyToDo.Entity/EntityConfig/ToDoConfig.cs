using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Library.Entity;

namespace MyToDo.Library.EntityConfig
{
    public class ToDoConfig : IEntityTypeConfiguration<ToDo>
    {
        public void Configure(EntityTypeBuilder<ToDo> builder)
        {
            builder.Property(e => e.Status).HasDefaultValue(0);
            builder.Property(e => e.CreateDate).HasDefaultValue(DateTime.Now);
            builder.Property(e => e.ModifyDate).HasDefaultValue(DateTime.Now);
            builder.Property(e => e.Title).HasMaxLength(50).IsRequired(false);
            builder.Property(e => e.Content).HasMaxLength(50).IsRequired(false);
            builder.ToTable("T_ToDos");
        }
    }
}
