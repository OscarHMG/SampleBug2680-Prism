using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProyectoBaseXF.Infrastructure.Services;
using ProyectoBaseXF.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoBaseXF.ViewModels
{
    public class DemoPageViewModel : ViewModelBase
    {
        public ICommand ShowLoader { get; set; }
        public ICommand ShowLoaderAndNavigate { get; set; }
        public ICommand ShowLoaderAndException { get; set; }
        public DemoPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, LoaderService loader) : base(navigationService, pageDialogService, loader)
        {
            Title = "Main Page";

            ShowLoader = new Command(async () =>
            {
                await LoadTaskAsync(Task.Delay(4000), "Cargando información");
            });

            ShowLoaderAndNavigate = new Command(async () =>
            {
                await LoadTaskAsync(Task.Delay(4000), "Guardando información", "DemoPage2");
            });
        }

    }
}
