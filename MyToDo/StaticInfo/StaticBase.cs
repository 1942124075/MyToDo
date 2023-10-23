using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using Prism.Mvvm;
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
        public static UserDto  CurrentUser { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public static string Token { get;set; }
        /// <summary>
        /// 令牌的路径
        /// </summary>
        private static readonly string TokenJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "User.json");

        /// <summary>
        /// 写入Json文件
        /// </summary>
        /// <param name="token"></param>
        public static void WriteToken(string token)
        {
            File.WriteAllText(TokenJsonPath, token);
        }
        /// <summary>
        /// 读取Token
        /// </summary>
        /// <returns></returns>
        public static string ReadToken()
        {
            string token = string.Empty;
            if (File.Exists(TokenJsonPath))
            {
                token = File.ReadAllText(TokenJsonPath);
            }
            return token;
        }
        /// <summary>
        /// 删除token
        /// </summary>
        public static void DeleteToken()
        {
            if (File.Exists(TokenJsonPath))
            {
                File.Delete(TokenJsonPath);
            }
        }


    }
}
