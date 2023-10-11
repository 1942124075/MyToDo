using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MyToDo.Library.Entity;

namespace MyToDo.Library.Contexts.Repositorys
{
    public class BlockItemRepository : Repository<BlockItem>
    {
        public BlockItemRepository(MyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
