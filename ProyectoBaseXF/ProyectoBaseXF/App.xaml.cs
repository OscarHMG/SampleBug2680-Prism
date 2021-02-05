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

            await HandleLifeCycle();
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
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<DemoPage, DemoPageViewModel>();
            containerRegistry.RegisterForNavigation<LoaderPopup, LoaderPopupViewModel>();
            containerRegistry.RegisterForNavigation<DemoPage2, DemoPage2ViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();

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
