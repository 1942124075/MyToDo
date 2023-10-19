using MaterialDesignThemes.Wpf;
using MyToDo.Common.Dialogs;
using MyToDo.Library.Modes;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;

namespace MyToDo.ViewModels.Dialogs
{
    public class AddToDoViewModel : NavigationViewModel, IDialogHostAware
    {
        public DelegateCommand CancelCommand { get; set; }
        public string Title => "添加待办";

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }

        private ToDoDto currentData;

        public ToDoDto CurrentData
        {
            get { return currentData; }
            set { currentData = value; RaisePropertyChanged(); }
        }


        public AddToDoViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
            CurrentData = new ToDoDto() { 
                Status = 0
            };
        }

        private void Cancel()
        {
            DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.Cancel));
        }

        private void Save()
        {
            DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK,new DialogParameters() { { "CurrentData", CurrentData } }));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                CurrentData = parameters.GetValue<ToDoDto>("Value");
            }
            else
            {
                CurrentData = new ToDoDto();
            }
        }
    }
}
