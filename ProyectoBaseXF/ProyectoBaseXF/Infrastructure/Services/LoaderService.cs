using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBaseXF.Infrastructure.Services
{
    public class LoaderService
    {
        INavigationService NavigationService { get; set; }
        public LoaderService(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public async Task Show(string text)
        {
            NavigationParameters nav = new NavigationParameters();
            nav.Add("LoadingText", text);
            await NavigationService.NavigateAsync("LoaderPopup", nav);
        }
        public async Task Hide()
        {
            await NavigationService.ClearPopupStackAsync(animated: true);
        }
        public async Task HideAndNavigate(string location)
        {
            INavigationResult result;
            if (location.StartsWith("/"))
            {
                await this.Hide();
                result = await NavigationService.NavigateAsync($"{location}");
            }
            else
            {
                result = await NavigationService.NavigateAsync($"../{location}");
            }
            if (!result.Success)
            {
                Console.WriteLine(result.Exception.Message);
            }
        }
    }
}
