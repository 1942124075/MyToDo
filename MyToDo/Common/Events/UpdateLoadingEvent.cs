using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Events
{
    public class UpdateModel
    {
        public UpdateModel(bool ipOpen)
        {
            IsOpen = ipOpen;
        }
        public bool IsOpen { get; set; }
    }
    public class UpdateLoadingEvent : PubSubEvent<UpdateModel>
    {
    }
}
