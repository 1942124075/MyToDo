using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MyToDo.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Library.Contexts.Repositorys
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(MyDbContext dbContext) : base(dbContext)
        {

        }
    }
}
