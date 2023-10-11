using AutoMapper.Configuration;
using System.Reflection;

namespace MyToDo.Api.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperConfig : MemberConfigurationExpression
    {
        /// <summary>
        /// 自动映射配置
        /// </summary>
        /// <param name="destinationMember"></param>
        /// <param name="sourceType"></param>
        public AutoMapperConfig(MemberInfo destinationMember, Type sourceType) : base(destinationMember, sourceType)
        {

        }
    }
}
