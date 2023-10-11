using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyToDo.Api.Utility.Swagger
{
    /// <summary>
    /// swagger文件上传扩展
    /// </summary>
    public class FileUploadFilter : IOperationFilter
    {
        /// <summary>
        /// swagger扩展,添加文件上传按钮
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            const string FileUploadContentType = "multipart/form-data";
            if (operation.RequestBody == null || !operation.RequestBody.Content.Any(x =>
                x.Key.Equals(FileUploadContentType,StringComparison.InvariantCultureIgnoreCase)
            ))
            {
                return;
            }

            if (context.ApiDescription.ParameterDescriptions[0].Type == typeof(IFormCollection))
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Description = "文件上传",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        {
                            FileUploadContentType,new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema()
                                {
                                    Type = "object",
                                    Required = new HashSet<string>(){"file"},
                                    Properties = new Dictionary<string, OpenApiSchema>()
                                    {
                                        {"file",new OpenApiSchema()
                                        {
                                            Type = "string",
                                            Format = "binary"
                                        } }
                                    }
                                }
                            }

                        }
                        
                    }
                };

            }
        }
    }
}
