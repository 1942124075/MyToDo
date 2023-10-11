using MyToDo.Library.Modes;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel : BindableBase
    {
        public ToDoViewModel()
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            ShowAddToDo = new DelegateCommand(ShowAddToDoView);
            CreateContents();
        }

        private void ShowAddToDoView()
        {
            IsRightDrawerOpen = true;
        }

        public ObservableCollection<ToDoDto> ToDoDtos { get; set; }
        public DelegateCommand ShowAddToDo { get; set; }
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }



        void CreateContents()
        {
            for (int i = 0; i < 20; i++)
            {
                ToDoDtos.Add(new ToDoDto()
                {
                    Id = i + 1,
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    Status = 1,
                    Content = "我是待办内容" + i,
                    Title = "我是待办标题" + i
                });
            }
        }


    }
}
