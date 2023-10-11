using AutoMapper.Configuration;
using System.Reflection;

namespace MyToDo.Api.Exceptions
{
    public class AutoMapperConfig : MemberConfigurationExpression
    {
        public AutoMapperConfig(MemberInfo destinationMember, Type sourceType) : base(destinationMember, sourceType)
        {

        }
    }
}
