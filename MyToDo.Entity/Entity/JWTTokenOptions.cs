using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Library.Entity
{
    /// <summary>
    /// JWT配置类
    /// </summary>
    public class JWTTokenOptions
    {
        /// <summary>
        /// 有效期(分钟)
        /// </summary>
        public int ExpirationDate { get; set; }
        /// <summary>
        /// 使用者
        /// </summary>
        public string? Audience { get; set; }
        /// <summary>
        /// 加密的KEY
        /// </summary>
        public string? SecurityKey { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string? Issuer { get; set; }
    }
}
