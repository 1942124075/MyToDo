using MyToDo.Library.Modes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        public HomeViewModel()
        {
            Blocks = new ObservableCollection<BlockItemDto>();
            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            CreateBlocks();
            CreateContent();
        }

        public ObservableCollection<BlockItemDto> Blocks { get; set; }
        public ObservableCollection<ToDoDto> ToDoDtos { get; set; }
        public ObservableCollection<MemoDto> MemoDtos { get; set; }

        void CreateBlocks()
        {
            Blocks.Add(new BlockItemDto()
            {
                Id = 1,
                IconName = "ClipboardTextClock",
                Title = "汇总",
                Value = "9",
                BackColor = "#0097ff"
            });
            Blocks.Add(new BlockItemDto()
            {
                Id = 2,
                IconName = "TimerCheckOutline",
                Title = "已完成",
                Value = "9",
                BackColor = "#10b135"
            });
            Blocks.Add(new BlockItemDto()
            {
                Id = 3,
                IconName = "ChartTimelineVariant",
                Title = "完成比例",
                Value = "99%",
                BackColor = "#00b4dd"
            });
            Blocks.Add(new BlockItemDto()
            {
                Id = 4,
                IconName = "Notebook",
                Title = "备忘录",
                Value = "9",
                BackColor = "#ff9f00"
            });
        }

        void CreateContent()
        {
            for (int i = 0; i < 20; i++)
            {
                ToDoDtos.Add(new ToDoDto()
                {
                    Id = i+1,
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    Status = 1,
                    Content = "我是待办内容"+ i,
                    Title = "我是待办标题" + i
                });
            }
            for (int i = 0; i < 20; i++)
            {
                MemoDtos.Add(new  MemoDto()
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
