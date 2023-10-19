using MyToDo.Common.Extensions;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace MyToDo.ViewModels
{
    /// <summary>
    /// 导航ViewModel
    /// </summary>
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator aggregator;
        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="aggregator"></param>
        public NavigationViewModel(IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
        }
        /// <summary>
        /// 是否允许导航
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 离开当前窗口时触发
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
        /// <summary>
        /// 导航到当前窗口时触发
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
        /// <summary>
        /// 设置等待窗口
        /// </summary>
        /// <param name="isLoading"></param>
        public void SetLoading(bool isLoading)
        {
            aggregator.Publish(new Common.Events.UpdateModel(isLoading));
        }
    }
}
