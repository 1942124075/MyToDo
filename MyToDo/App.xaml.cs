using DryIoc;
using MyToDo.Services;
using MyToDo.Services.Interface;
using MyToDo.ViewModels;
using MyToDo.ViewModels.SettingViewModels;
using MyToDo.Views;
using MyToDo.Views.SettingView;
using Prism.DryIoc;
using Prism.Ioc;
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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer().Register<HttpRestClient>(made:Parameters.Of.Type<string>(serviceKey:"webUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:5178/", serviceKey: "webUrl");
            containerRegistry.Register<IToDoService,ToDoService>();

            containerRegistry.RegisterForNavigation<Home,HomeViewModel>();
            containerRegistry.RegisterForNavigation<ToDo, ToDoViewModel>();
            containerRegistry.RegisterForNavigation<Memo, MemoViewModel>();
            containerRegistry.RegisterForNavigation<Setting, SettingViewModel>();
            containerRegistry.RegisterForNavigation<AboutMore,AboutMoreViewModel>();
            containerRegistry.RegisterForNavigation<SystemSetting, SystemSettingViewModel>();
            containerRegistry.RegisterForNavigation<Design, DesignViewModel>();
        }
    }
}
