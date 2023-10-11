using MyToDo.Library.BaseModes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Library.Entity
{
    public class User : EntityBase
    {
        private string userName;
        private string passWord;
        private DateTime lastLoginDate;
        private int age;
        private string sex;
        private string role;
        /// <summary>
        /// 角色
        /// </summary>
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginDate
        {
            get { return lastLoginDate; }
            set { lastLoginDate = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

    }
}
