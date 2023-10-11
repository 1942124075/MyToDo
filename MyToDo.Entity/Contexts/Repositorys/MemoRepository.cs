using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MyToDo.Library.Entity;

namespace MyToDo.Library.Contexts.Repositorys
{
    public class MemoRepository : Repository<Memo>
    {
        public MemoRepository(MyDbContext dbContext) : base(dbContext)
        {

        }
    }
}
