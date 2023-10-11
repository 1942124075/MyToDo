using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Utility.Swagger;
using MyToDo.Library.Entity;
using MyToDo.Library.Filters;

namespace MyToDo.Api.Controllers
{
    /// <summary>
    /// 文件控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings( IgnoreApi =false,GroupName =nameof(ApiVersion.V2))]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> logger;

        public FileController(ILogger<FileController> logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomResourceFilter]
        public ApiResponse UploadFile(IFormCollection keyValues)
        {
            logger.LogInformation("测试nlog");
            logger.LogDebug("测试nlog");
            logger.LogError("测试nlog");
            logger.LogTrace("测试nlog");
            logger.LogWarning("测试nlog");
            return new ApiResponse(true,"上传成功！");
        }
    }
}
