using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;

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
            builder.HasOne<User>(e => e.User).WithMany().HasForeignKey(e => e.UserId);
            builder.ToTable("T_ToDos");
        }
    }
}
