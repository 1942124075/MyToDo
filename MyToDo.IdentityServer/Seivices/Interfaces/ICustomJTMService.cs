using MyToDo.Library.Entity;

namespace MyToDo.IdentityServer.Seivices.Interfaces
{
    public interface ICustomJTMService
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GetToken(User user);
    }
}
