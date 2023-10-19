using MyToDo.Common.Extensions;
using MyToDo.Library.Entity;
using MyToDo.Services.Interface;
using MyToDo.StaticInfo;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;

namespace MyToDo.ViewModels
{
    public class LoginViewModel : NavigationViewModel, IDialogAware
    {
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand RegisterUserCommand { get; set; }
        public DelegateCommand<string> ExecuteCommand { get; set; }
        private User myUser;
        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        private readonly IEventAggregator eventAggregator;
        private int selectLoginIndex = 0;

        /// <summary>
        /// 选择是登录还是注册
        /// </summary>
        public int SelectLoginIndex
        {
            get { return selectLoginIndex; }
            set { selectLoginIndex = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public User MyUser
        {
            get { return myUser; }
            set { myUser = value; RaisePropertyChanged(); }
        }

        public LoginViewModel(IUserService userService, ITokenService tokenService,IEventAggregator eventAggregator): base(eventAggregator)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            LoginCommand = new DelegateCommand(Login);
            RegisterUserCommand = new DelegateCommand(RegisterUser);
            MyUser = new User();
            this.userService = userService;
            this.tokenService = tokenService;
            this.eventAggregator = eventAggregator;
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="option"></param>
        private void Execute(string option)
        {
            switch (option)
            {
                case "Login":
                    Login();
                    break;
                case "Register":
                    RegisterUser();
                    break;
                case "ChangeView":
                    MyUser.UserName = string.Empty;
                    MyUser.Password = string.Empty;
                    if (SelectLoginIndex == 0)
                    {
                        SelectLoginIndex = 1;
                    }
                    else
                    {
                        SelectLoginIndex = 0;
                    }
                    break;
            }
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        private async void RegisterUser()
        {
            if (string.IsNullOrWhiteSpace(MyUser.UserName) || string.IsNullOrWhiteSpace(MyUser.Password))
            {
                eventAggregator.SendMessage("用户名和密码不能为空!","Login");
                return;
            }
            SetLoading(true);
            MyUser.CreateDate = DateTime.Now;
            MyUser.ModifyDate = DateTime.Now;
            MyUser.Age = 0;
            MyUser.Sex = "男";
            MyUser.Token = string.Empty;
            MyUser.Role = "Admin";
            var result = await userService.RegisterAsync(MyUser);
            if (result.Status)
            {
                eventAggregator.SendMessage("注册成功!", "Login");
                SelectLoginIndex = 0;
                MyUser.UserName = string.Empty;
                MyUser.Password = string.Empty;
            }else
            {
                eventAggregator.SendMessage(result.Message, "Login");
            }
            SetLoading(false);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        private async void Login()
        {
            if (MyUser != null && !string.IsNullOrWhiteSpace(MyUser.UserName) && !string.IsNullOrWhiteSpace(MyUser.Password))
            {
                SetLoading(true) ;
                var result =await userService.LoginAsync(MyUser.UserName, MyUser.Password);
                if (result.Status && result.Result != null)
                {
                    StaticBase.WriteToken(result.Result.ToString());
                    var userinfo = await tokenService.DecryptToken(result.Result.ToString());
                    if (userinfo.Status)
                    {
                        StaticBase.CurrentUset = userinfo.Result;
                        RequestClose(new DialogResult( ButtonResult.OK));
                    }
                    else
                    {
                        eventAggregator.SendMessage("登录失败!认证错误！", "Login");
                    }
                }
                else
                {
                    eventAggregator.SendMessage("登录失败!用户名或密码错误！", "Login");
                }
                SetLoading(false);
                return;
            }
            else
            {
                eventAggregator.SendMessage("用户名和密码不能为空!","Login");
            }
        }

        public string Title => "登录";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
