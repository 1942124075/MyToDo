using MaterialDesignThemes.Wpf;
using MyToDo.Common.Dialogs;
using MyToDo.Library.Modes;
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
    public class AddMemoViewModel : BindableBase, IDialogHostAware
    {
        public DelegateCommand CancelCommand { get; set; }
        public string Title => "添加备忘录";

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        private MemoDto currentData;

        public MemoDto CurrentData
        {
            get { return currentData; }
            set { currentData = value; RaisePropertyChanged(); }
        }


        public AddMemoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.Cancel));
        }

        private void Save()
        {
            DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, new DialogParameters() { { "CurrentData", CurrentData } }));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                CurrentData = parameters.GetValue<MemoDto>("Value");
            }
            else
            {
                CurrentData = new MemoDto();
            }
        }
    }
}
