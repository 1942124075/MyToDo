using MyToDo.Library.BaseModes;
using Prism.Mvvm;
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
        private string token;
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token
        {
            get { return token; }
            set { token = value; }
        }
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
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginDate
        {
            get { return lastLoginDate; }
            set { lastLoginDate = (DateTime)value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
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
