﻿using MyToDo.Library.BaseModes;
using MyToDo.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Library.Modes
{
    public class MemoDto : EntityBase
    {
        private string title;
        private string content;
        private int status;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
    }
}
