using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Library.Entity
{
    public class PageList<TEntity>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; } = 0;
        public int TotalPages { get; set; }
        public int PageCount { get; set; }
        public List<TEntity> Lists { get; set; } = new List<TEntity>();

    }
}
