﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Events
{
    public class MessageModel 
    {
        public string Filter { get; set; } = "Main";
        public string Message { get; set; }
    }


    public class MessageEvent : PubSubEvent<MessageModel>
    {

    }
}
