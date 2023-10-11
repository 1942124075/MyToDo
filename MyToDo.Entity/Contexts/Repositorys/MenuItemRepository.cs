using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MyToDo.Library.Entity;

namespace MyToDo.Library.Contexts.Repositorys
{
    public class MenuItemRepository : Repository<MenuItem>
    {
        public MenuItemRepository(MyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
