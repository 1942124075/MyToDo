using MyToDo.Library.BaseModes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Library.Modes
{
    public class BlockItemDto : PropertyChangedBase
    {
        private int id;
        private string value;
        private string iconName;
        private string title;
        private string backColor;

        public string BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }
        public string IconName
        {
            get { return iconName; }
            set { iconName = value; RaisePropertyChanged(); }
        }
        public string Value
        {
            get { return value; }
            set { this.value = value; RaisePropertyChanged(); }
        }
    }
}
