namespace MyToDo.Library.Entity
{
    public class ApiResponse
    {
        public ApiResponse(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }
        public ApiResponse(bool status, object result)
        {
            Status = status;
            Result = result;
        }
        public ApiResponse(bool status, object result, string message)
        {
            Status = status;
            Result = result;
            Message = message;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回的结果
        /// </summary>
        public object Result { get; set; }


    }
}
