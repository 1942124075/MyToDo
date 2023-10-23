using MyToDo.Library.Modes;

namespace MyToDo.Library.Entity
{
    public class Memo : EntityBase
    {
        private string title;
        private string content;
        private int userId;
        public User? User { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
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
        /// 内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
