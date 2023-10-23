using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Library.Entity;

namespace MyToDo.Library.EntityConfig
{
    public class MemoConfig : IEntityTypeConfiguration<Memo>
    {
        public void Configure(EntityTypeBuilder<Memo> builder)
        {
            builder.Property(e => e.CreateDate).HasDefaultValue(DateTime.Now);
            builder.Property(e => e.ModifyDate).HasDefaultValue(DateTime.Now);
            builder.Property(e => e.Title).HasMaxLength(50).IsRequired(false);
            builder.Property(e => e.Content).HasMaxLength(50).IsRequired(false);
            builder.HasOne<User>(e => e.User).WithMany().HasForeignKey(e => e.UserId);
            builder.ToTable("T_Memos");
        }
    }
}
