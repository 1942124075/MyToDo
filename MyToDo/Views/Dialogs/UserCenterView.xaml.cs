using MyToDo.Common.Extensions;
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

namespace MyToDo.Views.Dialogs
{
    /// <summary>
    /// UserCenter.xaml 的交互逻辑
    /// </summary>
    public partial class UserCenterView : UserControl
    {
        public UserCenterView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            eventAggregator.RegisterMessage(mess =>
            {
                snackbar.MessageQueue?.Enqueue(mess.Message);
            }, "UserCenter");
            
        }
    }
}
