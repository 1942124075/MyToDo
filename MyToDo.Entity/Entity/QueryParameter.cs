using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Library.Entity
{
    public class QueryParameter
    {
        /// <summary>
        /// 页的大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页的下标
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总的页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string? Search { get; set; }
    }
}
