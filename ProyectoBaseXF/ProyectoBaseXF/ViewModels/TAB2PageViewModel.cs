using Prism.Navigation;
using Prism.Services;
using ProyectoBaseXF.Infrastructure.Services;
using ProyectoBaseXF.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoBaseXF.ViewModels
{
    public class TAB2PageViewModel : ViewModelBase
    {
        public TAB2PageViewModel(INavigationService navigation, IPageDialogService pageDialogService, LoaderService loaderService): base (navigation, pageDialogService, loaderService)
        {

        }
    }
}
