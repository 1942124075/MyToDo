using MyToDo.Library.Modes;
using MyToDo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services
{
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        public ToDoService(HttpRestClient client) : base(client, "ToDo")
        {

        }
    }
}
