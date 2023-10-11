using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Library.Entity;

namespace MyToDo.Library.EntityConfig
{
    public class MenuItemConfig : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.Property(e => e.Title).HasMaxLength(50);
            builder.Property(e => e.IconName).HasMaxLength(50);
            builder.Property(e => e.ItemNameSpace).HasMaxLength(50);
            builder.Property(e => e.ItemType).HasMaxLength(50);
            builder.ToTable("T_MenuItems");
        }
    }
}
