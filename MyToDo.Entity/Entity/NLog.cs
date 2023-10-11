using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Library.Entity
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class NLog
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 应用程序
        /// </summary>
        public string Application { get; set; }
        /// <summary>
        /// 日志日期
        /// </summary>
        public DateTime LoggedDate { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 记录人
        /// </summary>
        public string Logger { get; set; }
        /// <summary>
        /// 呼叫点
        /// </summary>
        public string CallSite { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Exception { get; set; }
    }
}
