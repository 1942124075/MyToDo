using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Library.EntityConfig
{
    /// <summary>
    /// 日志配置类
    /// </summary>
    public class NLogConfig : IEntityTypeConfiguration<NLog>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<NLog> builder)
        {
            builder.ToTable("T_NLog");
        }
    }
}
