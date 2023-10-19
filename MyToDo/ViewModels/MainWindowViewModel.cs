using MyToDo.Common.Dialogs;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using MyToDo.Services.Interface;
using MyToDo.StaticInfo;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
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
        private User currentUser;

        public User CurrentUser
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
        /// 返回上一步命令
        /// </summary>
        public DelegateCommand BackCommand { get; set; }
        /// <summary>
        /// 返回下一步命令
        /// </summary>
        public DelegateCommand ForwardCommand { get; set; }
        /// <summary>
        /// 登录系统命令
        /// </summary>
        public DelegateCommand LoginCommand { get; set; }
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
        public MainWindowViewModel(IRegionManager regionManager,IDialogHostService dialogHostService )
        {
            
            this.regionManager = regionManager;
            this.dialogHostService = dialogHostService;
            NavigateCommand = new DelegateCommand<MenuItemDto>(Navigate);
            BackCommand = new DelegateCommand(Back);
            ForwardCommand = new DelegateCommand(Forward);
            LoginCommand = new DelegateCommand(Login);
        }
        /// <summary>
        /// 登录
        /// </summary>
        private void Login()
        {
            if (!IsNeedLogin)
            {
                dialogHostService.ShowDialog("LoginView", callback: (call) =>
                {

                });
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

        public MainWindowViewModel()
        {
            
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
            MenuBars = new ObservableCollection<MenuItemDto>();
            CreateLeftList();
            if (StaticBase.CurrentUset != null)
            {
                IsNeedLogin = false;
                CurrentUser = StaticBase.CurrentUset;
            }
            CurrentMenu = MenuBars[0];
            regionManager.Regions[StaticBase.MenuNavigateName].RequestNavigate("Home");
        }
    }
}
