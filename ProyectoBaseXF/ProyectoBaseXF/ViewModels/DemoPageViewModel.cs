using Prism.Navigation;
using Prism.Services;
using ProyectoBaseXF.Infrastructure.Services;
using ProyectoBaseXF.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoBaseXF.ViewModels
{
    public class DemoPageViewModel : ViewModelBase
    {
        public ICommand ResetNavigationCommand { get; set; }

        public DemoPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, LoaderService loaderService) : base(navigationService, pageDialogService, loaderService)
        {
            ResetNavigationCommand = new Command(async()=> {
                var Result = await navigationService.NavigateAsync("/MainPage/Nav/BottomNavigationBar?selectedTab=TAB3");
                if (!Result.Success)
                {
                    Debugger.Break();
                }
            });
        }
    }
}
