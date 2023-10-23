using MyToDo.Common.Dialogs;
using MyToDo.Common.Extensions;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using MyToDo.Services.Interface;
using MyToDo.StaticInfo;
using MyToDo.Views.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace MyToDo.ViewModels
{
    public class MainWindowViewModel : BindableBase, IRunConfigureService
    {
        private bool isNeedLogin;
        private ObservableCollection<MenuItemDto> menuBars;
        private readonly IRegionManager regionManager;
        private readonly IDialogHostService dialogHostService;
        private IRegionNavigationJournal journal;
        private MenuItemDto currentMenu;
        private UserDto currentUser;
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }



        /// <summary>
        /// 当前用户
        /// </summary>
        public UserDto CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(); }
        }


        /// <summary>
        /// 当前选中的menu
        /// </summary>
        public MenuItemDto CurrentMenu
        {
            get { return currentMenu; }
            set { currentMenu = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 导航命令
        /// </summary>
        public DelegateCommand<MenuItemDto> NavigateCommand { get; set; }
        /// <summary>
        /// 执行操作
        /// </summary>
        public DelegateCommand<string> ExecuteCommand {  get; set; }
        /// <summary>
        /// 菜单列表
        /// </summary>
        public ObservableCollection<MenuItemDto> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 是否已经登录
        /// </summary>
        public bool IsNeedLogin
        {
            get { return isNeedLogin; }
            set { isNeedLogin = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionManager"></param>
        public MainWindowViewModel(IRegionManager regionManager,IDialogHostService dialogHostService,IEventAggregator eventAggregator,
            ITokenService tokenService)
        {
            MenuBars = new ObservableCollection<MenuItemDto>();
            this.regionManager = regionManager;
            this.dialogHostService = dialogHostService;
            NavigateCommand = new DelegateCommand<MenuItemDto>(Navigate);
            ExecuteCommand = new DelegateCommand<string>(Execute);
            
            
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="option"></param>
        private void Execute(string option)
        {
            switch (option) 
            {
                case "Back"://返回上一步
                    Back();
                    break;
                case "Forward"://返回下一步
                    Forward();
                    break;
                case "LoginOut"://退出
                    LoginOut();
                    break;
                case "UserCenter"://个人中心
                    ShowUserCenter();
                    break;
            }
        }

        private async void ShowUserCenter()
        {
            var result = await dialogHostService.ShowDialog("UserCenterView",null);
            if (result.Result == Prism.Services.Dialogs.ButtonResult.OK)
            {
                CurrentUser = StaticBase.CurrentUser;
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void LoginOut()
        {
            var result = await dialogHostService.ShowMessageBox("温馨提示","你确定要退出吗？");
            if (result == Prism.Services.Dialogs.ButtonResult.OK)
            {
                CurrentUser = null;
                if (StaticBase.CurrentUser != null)
                {
                    StaticBase.CurrentUser = null;
                }
                StaticBase.DeleteToken();
                App.LoginOut(dialogHostService);
            }
        }

        /// <summary>
        /// 返回下一步
        /// </summary>
        private void Forward()
        {
            if (journal != null && journal.CanGoForward)
                journal?.GoForward();
        }

        /// <summary>
        /// 返回上一步
        /// </summary>
        private void Back()
        {
            if (journal != null && journal.CanGoBack)
                 journal?.GoBack();
        }

        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="item"></param>
        private void Navigate(MenuItemDto item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.MenuNameSpace))
                return;
            regionManager.Regions[StaticBase.MenuNavigateName].RequestNavigate(item.MenuNameSpace,callBack =>
            {
                journal = callBack.Context.NavigationService.Journal;
            }); 
        }

        /// <summary>
        /// 创建左边菜单数据
        /// </summary>
        public void CreateLeftList()
        {
            MenuBars.Add(new MenuItemDto()
            {
                Id = 1,
                Title = "首页",
                IconName = "Home",
                MenuNameSpace = "Home"
            });
            MenuBars.Add(new MenuItemDto()
            {
                Id = 2,
                Title = "待办事项",
                IconName = "BookEdit",
                MenuNameSpace = "ToDo"
            });
            MenuBars.Add(new MenuItemDto()
            {
                Id = 3,
                Title = "备忘录",
                IconName = "Notebook",
                MenuNameSpace = "Memo"
            });
            MenuBars.Add(new MenuItemDto()
            {
                Id = 4,
                Title = "设置",
                IconName = "Cog",
                MenuNameSpace = "Setting"
            });
        }
        /// <summary>
        /// 执行初始化配置
        /// </summary>
        public void Configure()
        {
            CreateLeftList();
            if (StaticBase.CurrentUser != null)
            {
                IsNeedLogin = false;
                UserName = StaticBase.CurrentUser.UserName;
            }
            CurrentMenu = MenuBars[0];
            regionManager.Regions[StaticBase.MenuNavigateName].RequestNavigate("Home");
        }

    }
}
