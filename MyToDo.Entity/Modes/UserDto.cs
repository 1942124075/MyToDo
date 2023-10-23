using MyToDo.Library.Entity;

namespace MyToDo.Library.Modes
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserDto : EntityBase
    {
        private string userName;
        private int age;
        private string sex;
        private string role;

        /// <summary>
        /// 角色
        /// </summary>
        public string Role
        {
            get { return role; }
            set { role = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return sex; }
            set { sex = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return age; }
            set { age = (int)value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }
    }
}
