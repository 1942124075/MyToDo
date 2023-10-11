using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using MyToDo.StaticInfo;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace MyToDo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuItemDto>();
            CreateLeftList();
            this.regionManager = regionManager;
            NavigateCommand = new DelegateCommand<MenuItemDto>(Navigate);
            BackCommand = new DelegateCommand(Back);
            ForwardCommand = new DelegateCommand(Forward);
        }

        private void Forward()
        {
            if (journal != null && journal.CanGoForward)
                journal?.GoForward();
        }

        private async void Back()
        {
            if (journal != null && journal.CanGoBack)
                journal?.GoBack();
            HttpClient httpClient = new HttpClient();
            var tokenurl = "https://localhost:5001/api/User?userName=123&passWord=123";
            HttpResponseMessage tokenResponse = await httpClient.GetAsync(tokenurl);
            
            var url = "http://localhost:5178/api/BlockItem/GetSingle?id=1";
            string result = await tokenResponse.Content.ReadAsStringAsync();
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
            if (apiResponse.Status)
            {
                string token = apiResponse.Result.ToString();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                    token);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
                string result2 = await httpResponse.Content.ReadAsStringAsync();
                ApiResponse apiResponse2 = JsonConvert.DeserializeObject<ApiResponse>(result2);
            }
            


        }

        private void Navigate(MenuItemDto item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.MenuNameSpace))
                return;
            regionManager.Regions[StaticBase.MenuNavigateName].RequestNavigate(item.MenuNameSpace,callBack =>
            {
                journal = callBack.Context.NavigationService.Journal;
            }); 
        }

        public DelegateCommand<MenuItemDto> NavigateCommand { get; set; }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand ForwardCommand { get; set; }

        private ObservableCollection<MenuItemDto> menuBars;
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal journal;

        public ObservableCollection<MenuItemDto> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }


        public void CreateLeftList()
        {
            MenuBars.Add(new MenuItemDto()
            {
                Id = 1,
                Title = "首页",
                IconName = "Home",
                MenuNameSpace = "Home"
            });
            MenuBars.Add(new MenuItemDto()
            {
                Id = 2,
                Title = "待办事项",
                IconName = "BookEdit",
                MenuNameSpace = "ToDo"
            });
            MenuBars.Add(new MenuItemDto()
            {
                Id = 3,
                Title = "备忘录",
                IconName = "Notebook",
                MenuNameSpace = "Memo"
            });
            MenuBars.Add(new MenuItemDto()
            {
                Id = 4,
                Title = "设置",
                IconName = "Cog",
                MenuNameSpace = "Setting"
            });
        }
    }
}
