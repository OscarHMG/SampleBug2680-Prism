using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProyectoBaseXF.Infrastructure.Services;
using ProyectoBaseXF.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoBaseXF.ViewModels
{
    public class DemoPage2ViewModel : ViewModelBase
    {
        public DemoPage2ViewModel(INavigationService navigationService, IPageDialogService pageDialogService, LoaderService loader) : base(navigationService, pageDialogService, loader)
        {
            Title = "Demo Page 2";
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            Task.Run(async()=>
            {
                await Loader.Show("Descargado");
                await Task.Delay(5000);
                await Loader.Hide();
            });
        }
    }
}
