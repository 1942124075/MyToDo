namespace MyToDo.Library.Entity
{
    public class BlockItem
    {
        private int id;
        private string value;
        private string iconName;
        private string title;
        private string backColor;

        /// <summary>
        /// 背景颜色
        /// </summary>
        public string BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        /// <summary>
        /// 图标名称
        /// </summary>
        public string IconName
        {
            get { return iconName; }
            set { iconName = value; }
        }
        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}
