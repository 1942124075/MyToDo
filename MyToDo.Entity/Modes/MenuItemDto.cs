using MyToDo.Library.BaseModes;
using System.ComponentModel;

namespace MyToDo.Library.Modes
{
    public class MenuItemDto : PropertyChangedBase
    {
        private int id;
        private string title;
        private string iconName;
        private string menuNameSpace;

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

        public string MenuNameSpace
        {
            get { return menuNameSpace; }
            set { menuNameSpace = value; RaisePropertyChanged(); }
        }
    }
}
