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
        /// 退出登录
        /// </summary> b
        /// <param name="dialogService"></param>
        public static void LoginOut(IDialogService dialogService)
        {
            Current.MainWindow.Hide();
            dialogService.ShowDialog("LoginView", callback: call =>
            {
                if (call.Result == ButtonResult.OK)
                {
                    var runConfig = App.Current.MainWindow.DataContext as IRunConfigureService;
                    if (runConfig != null)
                        runConfig.Configure();
                    Current.MainWindow.Show();
                    return;
                }
                else
                {
                    App.Current.Shutdown();
                }
            });

        }

        /// <summary>
        /// 重写初始化完成方法,调用运行时配置接口
        /// </summary>
        protected override void OnInitialized()
        {
            IDialogService dialogService = Container.Resolve<IDialogService>();
            string token = StaticBase.ReadToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                dialogService.ShowDialog("LoginView", callback: call =>
                {
                    if (call.Result != ButtonResult.OK)
                    {
                        App.Current.Shutdown();
                    }
                });
            }
            else
            {
                var tokenService = Container.Resolve<ITokenService>();
                var userinfo = tokenService.DecryptToken(token).Result;
                if (userinfo.Status)
                {
                    StaticBase.CurrentUser = userinfo.Result;
                }
                else
                {
                    dialogService.ShowDialog("LoginView", callback: call =>
                    {
                        if (call.Result != ButtonResult.OK)
                        {
                            App.Current.Shutdown();
                        }
                    });
                }
            }
            base.OnInitialized();
            var runConfig = App.Current.MainWindow.DataContext as IRunConfigureService;
            if (runConfig != null)
                runConfig.Configure();
            
            IEventAggregator eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.SendMessage("登录成功");
            
        }

        private static void LoginSystem(IDialogService dialogService)
        {
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
            containerRegistry.RegisterForNavigation<UserCenterView, UserCenterViewModel>();
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
