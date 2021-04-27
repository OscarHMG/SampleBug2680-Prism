using Prism.Navigation;
using ProyectoBaseXF.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ProyectoBaseXF.Infrastructure.Services
{
    public class UserService
    {
        private ApiService Api { get; set; }
        private INavigationService NavigationService { get; set; }
        public string Username { get; set; }
        public string Patio { get; set; }
        public string RolUsuario { get; set; }
        public UserService(INavigationService navigationService, ApiService api)
        {
            Api = api;
            NavigationService = navigationService;
        }

        public async Task StoreToken(string username, string token, DateTime expiresIn)
        {
            string expireDate = expiresIn.ToUniversalTime().ToString();

            Username = username;

            await SecureStorage.SetAsync("username", username);
            await SecureStorage.SetAsync("oauth_token", token);
            await SecureStorage.SetAsync("expiresIn", expireDate);
        }

        public async Task HandleLogin(string Username, string Password)
        {
            var respuesta = await Api.GetToken(Username.Trim(), Password.Trim());
            await StoreToken(Username.Trim(), respuesta.AccessToken, respuesta.Expires);
        }

        public async Task<bool> HasValidToken()
        {
            var token = await SecureStorage.GetAsync("oauth_token");
            if (token != null)
            {
                var expiresIn = await SecureStorage.GetAsync("expiresIn");
                var expirationDate = Convert.ToDateTime(expiresIn);

                return (DateTime.Now.CompareTo(expirationDate) < 0);
            }
            return false;
        }

        public async Task HandleOnResume()
        {
            var hasValidToken = await HasValidToken();
            INavigationResult result;
            if (hasValidToken)
            {
                result = await NavigationService.NavigateAsync("/NavigationPage/DemoPage");
                if (!result.Success)
                {
                    Console.WriteLine(result.Exception.Message);
                }
            }
            else
            {
                await CerrarSession();
            }
        }

        public async Task CerrarSession()
        {
            SecureStorage.RemoveAll();
            
            var result = await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
            if (!result.Success)
            {
                Console.WriteLine(result.Exception.Message);
            }
        }
    }
}
