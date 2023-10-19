using MyToDo.Common.Dialogs;
using MyToDo.Common.Extensions;
using MyToDo.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyToDo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDialogHostService dialogHostService;

        public MainWindow(IEventAggregator aggregator,IDialogHostService dialogHostService)
        {
            InitializeComponent();
            SetWindowEvent();
            //注册等待窗口
            aggregator.Subscribe(arg =>
            {
                dialogHost.IsOpen = arg.IsOpen;
                if (arg.IsOpen)
                {
                    dialogHost.DialogContent = new ProgressView();
                }
            });
            //注册全局消息推送
            aggregator.RegisterMessage(arg =>
            {
                snackBar.MessageQueue?.Enqueue(arg.Message);
            },"Main");

            menuMainBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };
            this.dialogHostService = dialogHostService;
            //this.DataContext = new MainWindowViewModel();
        }

        void SetWindowEvent()
        {
            btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };

            btnMax.Click += (s, e) => { this.WindowState = this.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal; };

            btnClose.Click += async (s, e) => 
            {
                if (await dialogHostService.ShowMessageBox("温馨提示", "确定要关闭应用程序吗？") == Prism.Services.Dialogs.ButtonResult.OK)
                {
                    this.Close();
                    return;
                }
            };
            mdZone.MouseMove += (s, e) =>
            {
                if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            //mdZone.MouseDoubleClick += (s, e) => { this.WindowState = this.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal; };
        }
    }
}
