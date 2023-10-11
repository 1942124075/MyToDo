using MyToDo.ViewModels;
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
        public MainWindow()
        {
            InitializeComponent();
            SetWindowEvent();
            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };
            //this.DataContext = new MainWindowViewModel();
        }

        void SetWindowEvent()
        {
            btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };

            btnMax.Click += (s, e) => { this.WindowState = this.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal; };

            btnClose.Click += (s, e) => { this.Close(); };
            mdZone.MouseMove += (s, e) =>
            {
                if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            //mdZone.MouseDoubleClick += (s, e) => { this.WindowState = this.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal; };
        }
    }
}
