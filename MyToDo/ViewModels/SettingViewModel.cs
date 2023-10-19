using ImTools;
using MyToDo.Library.Modes;
using MyToDo.StaticInfo;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MyToDo.ViewModels
{
    public class SettingViewModel : NavigationViewModel
    {
        private readonly IRegionManager regionManager;

        public SettingViewModel(IRegionManager regionManager,IEventAggregator eventAggregator) : base(eventAggregator)
        {
            SettingListItems = new ObservableCollection<MenuItemDto>();
            SettingChangeItemCommand = new DelegateCommand<MenuItemDto>(SettingChangeItem);
            this.regionManager = regionManager;
        }

        private void SettingChangeItem(MenuItemDto listItem)
        {
            if (regionManager == null || string.IsNullOrWhiteSpace(listItem.MenuNameSpace))
                return;
            regionManager.Regions[StaticBase.SettingNavigateName].RequestNavigate(listItem.MenuNameSpace);
        }

        public ObservableCollection<MenuItemDto> SettingListItems { get; set; }

        public DelegateCommand<MenuItemDto> SettingChangeItemCommand {  get; set; }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            CreateSettingList();
            regionManager.Regions[StaticBase.SettingNavigateName].RequestNavigate(SettingListItems[0].MenuNameSpace);
        }

        void CreateSettingList()
        {
            SettingListItems.Add(new MenuItemDto()
            {
                Id = 1,
                Title = "个性化",
                IconName = "Palette",
                MenuNameSpace = "Design"
            });
            SettingListItems.Add(new MenuItemDto()
            {
                Id = 2,
                Title = "系统设置",
                IconName = "Cog",
                MenuNameSpace = "SystemSetting"
            });
            SettingListItems.Add(new MenuItemDto()
            {
                Id = 1,
                Title = "关于更多",
                IconName = "InformationVariantCircle",
                MenuNameSpace = "AboutMore"
            });
        }
    }
}
