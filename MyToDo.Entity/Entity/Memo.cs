namespace MyToDo.Library.Entity
{
    public class Memo : EntityBase
    {
        private string title;
        private string content;


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
