using Prism.Navigation;
using Prism.Services;
using ProyectoBaseXF.Infrastructure.Services;
using ProyectoBaseXF.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoBaseXF.ViewModels
{
    public class MainPageViewModel: ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, LoaderService loader): base(navigationService, pageDialogService, loader)
        {

        }
    }
}
