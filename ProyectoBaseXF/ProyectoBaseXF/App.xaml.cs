using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Plugin.Popups;
using ProyectoBaseXF.Infrastructure.Services;
using ProyectoBaseXF.Infrastructure.ViewModels;
using ProyectoBaseXF.Infrastructure.Views;
using ProyectoBaseXF.Service;
using ProyectoBaseXF.ViewModels;
using ProyectoBaseXF.Views;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace ProyectoBaseXF
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            INavigationResult Result = null;

            Result = await NavigationService.NavigateAsync("/MainPage/Nav/BottomNavigationBar?selectedTab=TAB1");
            if (!Result.Success)
            {
                Debugger.Break();
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            
            //Services
            containerRegistry.RegisterSingleton<ApiService>();
            containerRegistry.RegisterSingleton<UserService>();
            containerRegistry.RegisterSingleton<LoaderService>();

            //Pages
            
            containerRegistry.RegisterForNavigation<LoaderPopup, LoaderPopupViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();

            //Example
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>("MainPage");
            containerRegistry.RegisterForNavigation<NavigationPage>("Nav");
            containerRegistry.RegisterForNavigation<BottomNavigationBar>("BottomNavigationBar");
            containerRegistry.RegisterForNavigation<TAB1, TAB1PageViewModel>("TAB1");
            containerRegistry.RegisterForNavigation<TAB2, TAB2PageViewModel>("TAB2");
            containerRegistry.RegisterForNavigation<TAB3, TAB3PageViewModel>("TAB3");
            containerRegistry.RegisterForNavigation<DemoPage, DemoPageViewModel>("DemoPage");
        }


        protected async override void OnStart()
        {
            // Handle when your app resumes
            await HandleLifeCycle();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected async override void OnResume()
        {
            // Handle when your app resumes
            await HandleLifeCycle();
        }

        private async Task HandleLifeCycle()
        {
            var userService = Container.Resolve<UserService>();
            await userService.HandleOnResume();
        }

    }
}
