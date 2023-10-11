namespace MyToDo.Library.Entity
{
    public class EntityBase
    {
        private int id;
        private DateTime? createDate;
        private DateTime? modifyDate;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyDate
        {
            get { return modifyDate; }
            set { modifyDate = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
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
