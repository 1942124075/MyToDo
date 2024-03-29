﻿using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Dialogs
{
    public interface IDialogHostAware
    {
        /// <summary>
        /// 主机名称
        /// </summary>
        string DialogHostName { get; set; } 
        /// <summary>
        /// 取消
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }
        /// <summary>
        /// 保存
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }

        /// <summary>
        /// 弹窗
        /// </summary>
        /// <param name="parameters"></param>
        void OnDialogOpened(IDialogParameters parameters);
    }
}
