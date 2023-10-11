using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyToDo.Library.Entity;

namespace MyToDo.Library.Contexts
{
    public class MyDbContext : DbContext
    {
        private readonly IOptionsSnapshot<Config> optionsSnapshot;

        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Memo> Memos { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<BlockItem> BlockItems { get; set; }
        public DbSet<User> Users { get; set; }

        public MyDbContext(IOptionsSnapshot<Config> optionsSnapshot)
        {
            this.optionsSnapshot = optionsSnapshot;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(optionsSnapshot.Value.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
