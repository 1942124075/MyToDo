using MyToDo.Common.Dialogs;
using MyToDo.Common.Events;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Threading.Tasks;

namespace MyToDo.Common.Extensions
{
    /// <summary>
    /// 弹窗扩展
    /// </summary>
    public static class DialogExtension
    {
        /// <summary>
        /// 弹出确认消息
        /// </summary>
        /// <param name="service"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="hostName"></param>
        /// <returns></returns>
        public static async Task<ButtonResult> ShowMessageBox(this IDialogHostService service,string? title,string? content,string hostName = "Root")
        {
            DialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add("Title", title);
            dialogParameters.Add("Content", content);
            dialogParameters.Add("HostName", hostName);
            var result = await service.ShowDialog("MessageBoxView",parameters:dialogParameters);
            return result.Result;
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="model"></param>
        public static void Publish(this IEventAggregator aggregator,UpdateModel model)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Publish(model);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void Subscribe(this IEventAggregator aggregator,Action<UpdateModel> action)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Subscribe(action);
        }

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void RegisterMessage(this IEventAggregator aggregator, Action<MessageModel> action,string filter = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Subscribe(action, ThreadOption.PublisherThread, true, (agr) =>
            {
                return filter.Equals(agr.Filter);
            });
        }


        public static void SendMessage(this IEventAggregator aggregator,string str ,string filter = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Publish(new MessageModel() { Message = str ,Filter = filter });
        }
    }
}
