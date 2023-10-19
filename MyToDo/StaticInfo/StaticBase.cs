using MyToDo.Library.Entity;
using System;
using System.IO;
using System.Windows;

namespace MyToDo.StaticInfo
{
    /// <summary>
    /// 静态信息类
    /// </summary>
    public static class StaticBase
    {
        /// <summary>
        /// 主页菜单导航名称
        /// </summary>
        public static string MenuNavigateName { get; } = "MenuNavigate";
        /// <summary>
        /// 设置页导航名称
        /// </summary>
        public static string SettingNavigateName { get; } = "SettingNavigate";
        /// <summary>
        /// 当前登录的用户
        /// </summary>
        public static User  CurrentUset { get; set; }

        /// <summary>
        /// 写入Json文件
        /// </summary>
        /// <param name="token"></param>
        public static void WriteToken(string token)
        {
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory+ "User.json"), token);
        }
        /// <summary>
        /// 读取Token
        /// </summary>
        /// <returns></returns>
        public static string ReadToken()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "User.json");
            string token = string.Empty;
            if (File.Exists(path))
            {
                token = File.ReadAllText(path);
            }
            return token;
        }


    }
}
