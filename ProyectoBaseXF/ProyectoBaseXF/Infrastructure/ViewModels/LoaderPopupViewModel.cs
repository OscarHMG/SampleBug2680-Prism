using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoBaseXF.Infrastructure.ViewModels
{
    public class LoaderPopupViewModel : ViewModelBase
    {
        public LoaderPopupViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            Title = parameters.GetValue<string>("LoadingText");
        }
    }
}
