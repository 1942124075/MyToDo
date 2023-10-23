using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MyToDo.Common.Dialogs
{
    /// <summary>
    /// 自定义弹窗
    /// </summary>
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension containerExtension;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerExtension"></param>
        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }
        /// <summary>
        /// 自定义弹窗
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <param name="hostName"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string hostName = "Root")
        {
            if (parameters == null)
                parameters = new DialogParameters();

            var content = containerExtension.Resolve<object>(name);

            if (! (content is FrameworkElement dialogContent))
            {
                throw new NullReferenceException("a dialog content must be a FrameworkElement");
            }

            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
            {
                ViewModelLocator.SetAutoWireViewModel(view,true);
            }

            if (!(dialogContent.DataContext is IDialogHostAware hostAware))
            {
                throw new NullReferenceException("a dialog viewmodel must implement the IDialogHostAware interface");
            }

            hostAware.DialogHostName = hostName;
            DialogOpenedEventHandler eventHandler = (sender, args) =>
            {
                if (hostAware is IDialogHostAware aware)
                {
                    aware.OnDialogOpened(parameters);
                }
                args.Session.UpdateContent(content);
            };
            return (IDialogResult)await DialogHost.Show(dialogContent, hostAware.DialogHostName, eventHandler);
        }
    }
}
