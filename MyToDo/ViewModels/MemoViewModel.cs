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
    public class MemoViewModel :BindableBase
    {
        public MemoViewModel()
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            ShowAddToDo = new DelegateCommand(ShowAddToDoView);
            CreateContents();
        }

        private void ShowAddToDoView()
        {
            IsRightDrawerOpen = true;
        }

        public ObservableCollection<MemoDto> MemoDtos { get; set; }
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
                MemoDtos.Add(new MemoDto()
                {
                    Id = i + 1,
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    Status = 1,
                    Content = "我是备忘录内容" + i,
                    Title = "我是备忘录标题" + i
                });
            }
        }
    }
}
