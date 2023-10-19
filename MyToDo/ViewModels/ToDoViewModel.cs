using MyToDo.Common.Dialogs;
using MyToDo.Common.Extensions;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using MyToDo.Services.Interface;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    /// <summary>
    /// ToDoViewModel
    /// </summary>
    public class ToDoViewModel : NavigationViewModel
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        public ObservableCollection<ToDoDto> ToDoDtos { get; set; }
        /// <summary>
        /// 数据模型选中命令
        /// </summary>
        public DelegateCommand<ToDoDto> SelectedCommand {  get; set; }
        /// <summary>
        /// 操作命令
        /// </summary>
        public DelegateCommand<string> ExecuteCommand { get; set; }
        /// <summary>
        /// 删除命令
        /// </summary>
        public DelegateCommand<ToDoDto> DeleteCommand { get; set; }

        private bool isRightDrawerOpen;
        private readonly IToDoService service;
        private readonly IDialogHostService dialogHostService;
        private string todoTitle;
        private string search;
        private int selSearchStatus;
        private ToDoDto currentData;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        /// <param name="aggregator"></param>
        public ToDoViewModel(IToDoService service, IEventAggregator aggregator, IDialogHostService dialogHostService) : base (aggregator)
        {
            this.service = service;
            this.dialogHostService = dialogHostService;
            ToDoDtos = new ObservableCollection<ToDoDto>();
            SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
            ExecuteCommand = new DelegateCommand<string>(Execute);
            DeleteCommand = new DelegateCommand<ToDoDto>(DeleteTodo);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dto"></param>
        private async void DeleteTodo(ToDoDto dto)
        {
            if (await dialogHostService.ShowMessageBox("温馨提示","确定要删除吗？") == ButtonResult.OK)
            {
                if (dto == null || dto.Id == 0)
                    return;
                var delResult = await service.DeleteAsync(dto.Id);
                if (delResult.Status)
                {
                    ToDoDtos.Remove(ToDoDtos.First(e => e.Id == dto.Id));
                }
            }
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="option"></param>
        private async void Execute(string option)
        {
            SetLoading(true);
            switch (option) 
            {
                case "Add":
                    CurrentData = new ToDoDto() 
                    {
                        Status = 0
                    };
                    IsRightDrawerOpen = true;
                    TodoTitle = "添加待办";
                    break;
                case "Save":
                    if (CurrentData.Id > 0)
                    {
                        var updateResult = await service.UpdateAsync(CurrentData);
                        if (updateResult.Status)
                        {
                            var old = ToDoDtos.First(e => e.Id == CurrentData.Id);
                            old.ModifyDate = CurrentData.ModifyDate;
                            old.Content = CurrentData.Content;
                            old.Title = CurrentData.Title;
                            old.Status = CurrentData.Status;
                        }
                    }
                    else
                    {
                        var addResult = await service.AddAsync(CurrentData);
                        if (addResult.Status)
                        {
                            ToDoDtos.Add(addResult.Result);
                        }
                    }
                    IsRightDrawerOpen = false;
                    break;
                case "Search":
                    GetListAsync();
                    break;
            }
            SetLoading(false);
        }
        /// <summary>
        /// 模型选中
        /// </summary>
        /// <param name="dto"></param>
        private async void Selected(ToDoDto dto)
        {
            if (dto == null)
                return;
            var results = await service.GetSingle(dto.Id);
            if (results.Status)
            {
                CurrentData = results.Result;
                TodoTitle = "修改待办";
                IsRightDrawerOpen = true;
            }
        }
        
        /// <summary>
        /// 状态查询条件
        /// </summary>
        public int SelSearchStatus
        {
            get { return selSearchStatus; }
            set { selSearchStatus = value; RaisePropertyChanged(); GetListAsync();}
        }

        /// <summary>
        /// 标题查询条件
        /// </summary>
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 右边抽屉的标题
        /// </summary>
        public string TodoTitle
        {
            get { return todoTitle; }
            set { todoTitle = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 当前选中的数据
        /// </summary>
        public ToDoDto CurrentData
        {
            get { return currentData; }
            set { currentData = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 控制右边抽屉的
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 导航到当前窗口时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("Status"))
            {
                var status = navigationContext.Parameters.GetValue<int>("Status");
                SelSearchStatus = status + 1;
            }
            base.OnNavigatedTo(navigationContext);
            GetListAsync();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private async void GetListAsync()
        {
            SetLoading(true);
            var results = await service.GetListAsync(new QueryParameter()
            {
                PageIndex = 0,
                PageSize = 20,
                Search = Search,
                Status = SelSearchStatus == 0 ? null : SelSearchStatus -1
            }) ;
            if (results.Status)
            {
                ToDoDtos.Clear();
                foreach (var todo in results.Result.Lists)
                {
                    ToDoDtos.Add(todo);
                }
            }
            SetLoading(false);
        }
    }
}
