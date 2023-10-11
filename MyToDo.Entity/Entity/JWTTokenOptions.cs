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
        public string? Audience { get; set; }
        /// <summary>
        /// 加密的KEY
        /// </summary>
        public string? SecurityKey { get; set; }
        public string? Issuer { get; set; }
    }
}
