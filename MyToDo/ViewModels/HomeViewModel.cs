using MaterialDesignThemes.Wpf;
using MyToDo.Common.Dialogs;
using MyToDo.Common.Extensions;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using MyToDo.Services.Interface;
using MyToDo.StaticInfo;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class HomeViewModel : NavigationViewModel
    {
        private readonly IDialogHostService dialogService;
        private readonly IMemoService memoService;
        private readonly IToDoService toDoService;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;


        public HomeViewModel(IDialogHostService dialogService, 
            IMemoService memoService,
            IToDoService toDoService,
            IRegionManager regionManager,
            IEventAggregator eventAggregator) : base(eventAggregator)
        {
            Blocks = new ObservableCollection<BlockItemDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            ModifyToDoCommand = new DelegateCommand<ToDoDto>(AddToDo);
            ModifyMemoCommand = new DelegateCommand<MemoDto>(AddMemo);
            ToDoCompltedCommand = new DelegateCommand<ToDoDto>(ToDoComplted);
            JumpMenuCommand = new DelegateCommand<BlockItemDto>(JumpMenu);
            CreateBlocks();


            this.dialogService = dialogService;
            this.memoService = memoService;
            this.toDoService = toDoService;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
        }

        private void JumpMenu(BlockItemDto dto)
        {
            if (dto != null && !string.IsNullOrWhiteSpace(dto.MenuNamespace))
            {
                NavigationParameters param = new NavigationParameters();
                if (dto.Title == "已完成")
                {
                    param.Add("Status",1);
                }
                regionManager.Regions[StaticBase.MenuNavigateName].RequestNavigate(dto.MenuNamespace, param);
            }
        }

        private async void ToDoComplted(ToDoDto dto)
        {
            SetLoading(true);
            var result =  await toDoService.UpdateAsync(dto);
            if (result.Status)
            {
                MySummaryDto.TodoList.Remove(dto);
                MySummaryDto.TodoFinshCount++;
                MySummaryDto.TodoFinshRatioRatio = MySummaryDto.CalcRatio(MySummaryDto.TodoFinshCount, MySummaryDto.TodoCount);
                Blocks[1].Value = MySummaryDto.TodoFinshCount.ToString();
                Blocks[2].Value = MySummaryDto.TodoFinshRatioRatio.ToString();
            }
            SetLoading(false);
        }

        async void AddToDo(ToDoDto dto)
        {
            DialogParameters param = new DialogParameters();
            if (dto != null)
            {
                param.Add("Value", dto);
            }
            var todoResult = await dialogService.ShowDialog("AddToDoView", param);
            SetLoading(true);
            if (todoResult.Result == ButtonResult.OK)
            {
                var addTodo = todoResult.Parameters.GetValue<ToDoDto>("CurrentData");
                if (addTodo.Id > 0)
                {
                    var todoAddresult = await toDoService.UpdateAsync(addTodo);
                    if (todoAddresult.Status)
                    {
                        if (MySummaryDto.TodoList.Contains(dto))
                        {
                            var modifyTodo = MySummaryDto.TodoList.First(e => e.Id == todoAddresult.Result.Id);
                            if (todoAddresult.Result.Status == 1)
                            {
                                MySummaryDto.TodoList.Remove(dto);
                                MySummaryDto.TodoFinshCount++;
                                MySummaryDto.TodoFinshRatioRatio = MySummaryDto.CalcRatio(MySummaryDto.TodoFinshCount, MySummaryDto.TodoCount);
                                Blocks[1].Value = MySummaryDto.TodoFinshCount.ToString();
                                Blocks[2].Value = MySummaryDto.TodoFinshRatioRatio.ToString();
                            }
                            else
                            {
                                modifyTodo.Status = todoAddresult.Result.Status;
                                modifyTodo.Content = todoAddresult.Result.Content;
                                modifyTodo.Title = todoAddresult.Result.Title;
                                modifyTodo.ModifyDate = todoAddresult.Result.ModifyDate;
                            }
                            eventAggregator.SendMessage("已修改");
                        }
                    }
                }
                else
                {
                    var todoAddresult = await toDoService.AddAsync(addTodo);
                    if (todoAddresult.Status)
                    {
                        MySummaryDto.TodoList.Add(todoAddresult.Result);
                        MySummaryDto.TodoCount++;
                        Blocks[0].Value = MySummaryDto.TodoCount.ToString();
                        MySummaryDto.TodoFinshRatioRatio = MySummaryDto.CalcRatio(MySummaryDto.TodoFinshCount, MySummaryDto.TodoCount);
                        Blocks[2].Value = MySummaryDto.TodoFinshRatioRatio.ToString();
                        eventAggregator.SendMessage("已添加");
                    }
                }
            }
            SetLoading(false);
        }

        async void AddMemo(MemoDto dto)
        {
            DialogParameters param = new DialogParameters();
            if (dto != null)
            {
                param.Add("Value", dto);
            }
            var memoResult = await dialogService.ShowDialog("AddMemoView", param);
            if (memoResult.Result == ButtonResult.OK)
            {
                SetLoading(true);

                var addMemo = memoResult.Parameters.GetValue<MemoDto>("CurrentData");
                if (addMemo.Id > 0)
                {
                    var memoAddresult = await memoService.UpdateAsync(addMemo);
                    if (memoAddresult.Status)
                    {
                        if (MySummaryDto.MemoList.Contains(dto))
                        {
                            var modifyMemo = MySummaryDto.MemoList.First(e => e.Id == memoAddresult.Result.Id);
                            modifyMemo.Content = memoAddresult.Result.Content;
                            modifyMemo.Title = memoAddresult.Result.Title;
                            modifyMemo.ModifyDate = memoAddresult.Result.ModifyDate;
                            eventAggregator.SendMessage("已修改");
                        }
                        MySummaryDto.MemoList.Add(memoAddresult.Result);
                    }
                }
                else
                {
                    var memoAddresult = await memoService.AddAsync(addMemo);
                    if (memoAddresult.Status)
                    {
                        MySummaryDto.MemoList.Add(memoAddresult.Result);
                        MySummaryDto.MemoCount = MySummaryDto.MemoCount + 1;
                        Blocks[3].Value = MySummaryDto.MemoCount.ToString();
                        eventAggregator.SendMessage("已添加");
                    }
                }
                SetLoading(false);
            }
        }

        private async void Execute(string option)
        {
            switch (option)
            {
                case "AddToDo":
                    AddToDo(null);
                    break;
                case "AddMemo":
                    AddMemo(null);
                    break;
            }
        }

        public ObservableCollection<BlockItemDto> Blocks { get; set; }
        private IRegionNavigationJournal journal;
        private SummaryDto summaryDto;
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }


        public SummaryDto MySummaryDto
        {
            get { return  summaryDto; }
            set {  summaryDto = value; RaisePropertyChanged(); }
        }


        public DelegateCommand<ToDoDto> ModifyToDoCommand { get; set; }
        public DelegateCommand<string> ExecuteCommand { get; set; }
        public DelegateCommand<MemoDto> ModifyMemoCommand { get; set; }
        public DelegateCommand<ToDoDto> ToDoCompltedCommand { get; set; }

        public DelegateCommand<BlockItemDto> JumpMenuCommand { get; set; }

        void CreateBlocks()
        {
            Blocks.Add(new BlockItemDto()
            {
                Id = 1,
                IconName = "ClipboardTextClock",
                Title = "汇总",
                BackColor = "#0097ff",
                MenuNamespace = "ToDo"
            });
            Blocks.Add(new BlockItemDto()
            {
                Id = 2,
                IconName = "TimerCheckOutline",
                Title = "已完成",
                BackColor = "#10b135",
                MenuNamespace = "ToDo"
            });
            Blocks.Add(new BlockItemDto()
            {
                Id = 3,
                IconName = "ChartTimelineVariant",
                Title = "完成比例",
                BackColor = "#00b4dd",
                MenuNamespace = "ToDo"
            });
            Blocks.Add(new BlockItemDto()
            {
                Id = 4,
                IconName = "Notebook",
                Title = "备忘录",
                BackColor = "#ff9f00",
                MenuNamespace = "Memo"
            });
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            SetLoading(true);
            UserName = StaticBase.CurrentUser.UserName;
            base.OnNavigatedTo(navigationContext);
            await CreateContent();
            SetLoading(false);
        }

        async Task CreateContent()
        {
            var result = await toDoService.GetSummarySaync();
            if (result.Status)
            {
                MySummaryDto = result.Result;
                Blocks[0].Value = MySummaryDto.TodoCount.ToString();
                Blocks[1].Value = MySummaryDto.TodoFinshCount.ToString();
                Blocks[2].Value = MySummaryDto.TodoFinshRatioRatio.ToString();
                Blocks[3].Value = MySummaryDto.MemoCount.ToString();
            }
            else
            {
                eventAggregator.SendMessage(result.Message);
            }
        }
    }
}
