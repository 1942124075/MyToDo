using MaterialDesignThemes.Wpf;
using MyToDo.Common.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels.Dialogs
{
    public class MessageBoxViewModel : BindableBase, IDialogHostAware
    {
        public string DialogHostName { get; set; } = "Root";
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }

        private string content;
        public MessageBoxViewModel()
        {
            CancelCommand = new DelegateCommand(Cancel);
            SaveCommand = new DelegateCommand(Save);
        }

        private void Save()
        {
            DialogHost.Close(DialogHostName,new DialogResult( ButtonResult.OK));
        }

        private void Cancel()
        {
            DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.Cancel));
        }

        public string Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged(); }
        }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (CancelCommand != null)
            {
                if (parameters.ContainsKey("Title"))
                    Title = parameters.GetValue<string>("Title");
                if (parameters.ContainsKey("Content"))
                    Content = parameters.GetValue<string>("Content");
                if (parameters.ContainsKey("HostName"))
                    DialogHostName = parameters.GetValue<string>("HostName");
            }
        }
    }
}
