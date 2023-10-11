namespace MyToDo.Library.Entity
{
    /// <summary>
    /// 菜单项类
    /// </summary>
    public class MenuItem
    {
        private int id;
        private string title;
        private string iconName;
        private string itemNameSpace;
        private bool isEnable;
        private string itemType;

        /// <summary>
        /// 菜单项类型
        /// </summary>
        public string ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable
        {
            get { return isEnable; }
            set { isEnable = value; }
        }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string ItemNameSpace
        {
            get { return itemNameSpace; }
            set { itemNameSpace = value; }
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
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
