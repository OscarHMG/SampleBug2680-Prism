using Prism.Navigation;
using Prism.Services;
using ProyectoBaseXF.Infrastructure.Services;
using ProyectoBaseXF.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoBaseXF.ViewModels
{
    public class TAB3PageViewModel : ViewModelBase
    {

        public TAB3PageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, LoaderService loaderService): base(navigationService, pageDialogService, loaderService)
        {

        }
    }
}
