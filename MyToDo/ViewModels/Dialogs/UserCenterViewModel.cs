using ImTools;
using MaterialDesignThemes.Wpf;
using MyToDo.Common.Dialogs;
using MyToDo.Common.Extensions;
using MyToDo.Library.Entity;
using MyToDo.Library.Enums;
using MyToDo.Library.Modes;
using MyToDo.Services.Interface;
using MyToDo.StaticInfo;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels.Dialogs
{
    public class UserCenterViewModel : BindableBase, IDialogHostAware
    {
        public string DialogHostName { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }

        private UserDto currentUser;
        public DelegateCommand<string> ExecuteCommand {  get; set; }

        private ObservableCollection<string> rolus;
        private readonly IUserService userService;
        private readonly IEventAggregator eventAggregator;

        public ObservableCollection<string> Rolus
        {
            get { return rolus; }
            set { rolus = value; RaisePropertyChanged(); }
        }

        public UserDto CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(); }
        }

        public UserCenterViewModel(IUserService userService,IEventAggregator eventAggregator)
        {
            Rolus = new ObservableCollection<string>();
            typeof(RoluEnum).GetEnumNames().ForEach(e =>
            {
                Rolus.Add(e);
            });
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.userService = userService;
            this.eventAggregator = eventAggregator;
        }

        private void Execute(string option)
        {
            switch (option) 
            {
                case "OK":
                    OK();
                    break;
                case "Cencel":
                    Cancel();
                    break;
            }
        }

        private async void OK()
        {
            var result = await userService.UpdateAsync(currentUser);
            if (result.Status)
            {
                StaticBase.CurrentUser.UserName = CurrentUser.UserName;
                StaticBase.CurrentUser.Sex = CurrentUser.Sex;
                StaticBase.CurrentUser.Age = CurrentUser.Age;
                StaticBase.CurrentUser.Role = CurrentUser.Role;
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK));
            }
            else
            {
                eventAggregator.SendMessage("修改失败", "UserCenter");
            }
        }

        private void Cancel()
        {
            DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.Cancel));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (StaticBase.CurrentUser != null)
            {
                CurrentUser = new UserDto()
                {
                    UserName = StaticBase.CurrentUser.UserName,
                    Sex = StaticBase.CurrentUser.Sex,
                    Age = StaticBase.CurrentUser.Age,
                    Role = StaticBase.CurrentUser.Role,
                    Id = StaticBase.CurrentUser.Id,
                    CreateDate = StaticBase.CurrentUser.CreateDate
                };
            }

        }
    }
}
