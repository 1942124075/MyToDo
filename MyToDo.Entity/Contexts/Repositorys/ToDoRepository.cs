using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MyToDo.Library.Entity;

namespace MyToDo.Library.Contexts.Repositorys
{
    public class ToDoRepository : Repository<ToDo>
    {
        public ToDoRepository(MyDbContext dbContext) : base(dbContext)
        {

        }
    }
}
