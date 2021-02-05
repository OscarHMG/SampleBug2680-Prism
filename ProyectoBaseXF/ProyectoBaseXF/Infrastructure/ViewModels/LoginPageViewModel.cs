using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProyectoBaseXF.Infrastructure.Services;
using ProyectoBaseXF.Infrastructure.ViewModels;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoBaseXF.Infrastructure.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        [Reactive] public string Username { get; set; }
        [Reactive] public string Password { get; set; }
        [Reactive] public string Version { get; set; }
        public ICommand LoginCommand { get; set; }
        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, LoaderService loader, UserService userService) : base(navigationService, pageDialogService, loader)
        {
            Version = $"Versión: {VersionTracking.CurrentVersion} ({VersionTracking.CurrentBuild})";

            LoginCommand = new Command(async () =>
            {
                await LoadTaskAsync(userService.HandleLogin(Username, Password), "Iniciando Sesión");
            });
        }
    }
}
