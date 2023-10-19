using DryIoc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MyToDo.Common.Dialogs;
using MyToDo.Common.Extensions;
using MyToDo.Library.Entity;
using MyToDo.Services;
using MyToDo.Services.Interface;
using MyToDo.StaticInfo;
using MyToDo.ViewModels;
using MyToDo.ViewModels.Dialogs;
using MyToDo.ViewModels.SettingViewModels;
using MyToDo.Views;
using MyToDo.Views.Dialogs;
using MyToDo.Views.SettingView;
using Prism.DryIoc;
using Prism.Events;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System.Windows;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        /// <summary>
        /// 重写初始化完成方法,调用运行时配置接口
        /// </summary>
        protected async override void OnInitialized()
        {
            if (StaticBase.CurrentUset == null)
            {
                var tokenService = Container.Resolve<ITokenService>();
                string token = StaticBase.ReadToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    LoginSystem();
                }
                else
                {
                    var userinfo = await tokenService.DecryptToken(token);
                    if (userinfo.Status)
                    {
                        StaticBase.CurrentUset = userinfo.Result;
                    }
                    else
                    {
                        LoginSystem();
                    }
                }
            }
            var runConfig = App.Current.MainWindow.DataContext as IRunConfigureService;
            if (runConfig != null)
                runConfig.Configure();
            IEventAggregator eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.SendMessage("登录成功");
            base.OnInitialized();
        }

        private void LoginSystem()
        {
            IDialogService dialogService = Container.Resolve<IDialogService>();
            dialogService.ShowDialog("LoginView", callback: call =>
            {
                if (call.Result == ButtonResult.OK)
                {
                    return;
                }
                else
                {
                    App.Current.Shutdown();
                }
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //读取配置文件
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("TokenKey.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            
            containerRegistry.Register<JWTTokenOptions>(e => configurationRoot.GetSection("Token").Get<JWTTokenOptions>()) ;


            containerRegistry.GetContainer().Register<HttpRestClient>(made:Parameters.Of.Type<string>(serviceKey:"webUrl"));
            containerRegistry.GetContainer().Register<HttpUserRestClient>(made:Parameters.Of.Type<string>(serviceKey:"webUserUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"http://8.134.142.102:8080/", serviceKey: "webUrl");
            containerRegistry.GetContainer().RegisterInstance(@"http://8.134.142.102:8090/", serviceKey: "webUserUrl");
            containerRegistry.Register<IToDoService,ToDoService>();
            containerRegistry.Register<IMemoService,MemoService>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<ITokenService, TokenService>();

            containerRegistry.RegisterForNavigation<MessageBoxView, MessageBoxViewModel>();
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<AddToDoView,AddToDoViewModel>();
            containerRegistry.RegisterForNavigation<AddMemoView, AddMemoViewModel>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();

            containerRegistry.RegisterForNavigation<Home,HomeViewModel>();
            containerRegistry.RegisterForNavigation<Views.ToDo, ToDoViewModel>();
            containerRegistry.RegisterForNavigation<Views.Memo, MemoViewModel>();
            containerRegistry.RegisterForNavigation<Setting, SettingViewModel>();
            containerRegistry.RegisterForNavigation<AboutMore,AboutMoreViewModel>();
            containerRegistry.RegisterForNavigation<SystemSetting, SystemSettingViewModel>();
            containerRegistry.RegisterForNavigation<Design, DesignViewModel>();
        }
    }
}
