using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Library.Entity;

namespace MyToDo.Library.EntityConfig
{
    public class BlockItemConfig : IEntityTypeConfiguration<BlockItem>
    {
        public void Configure(EntityTypeBuilder<BlockItem> builder)
        {
            builder.Property(e => e.Title).HasMaxLength(50);
            builder.Property(e => e.IconName).HasMaxLength(50);
            builder.Property(e => e.Value).HasMaxLength(50);
            builder.Property(e => e.BackColor).HasMaxLength(50);
            builder.ToTable("T_BlockItems");
        }
    }
}
