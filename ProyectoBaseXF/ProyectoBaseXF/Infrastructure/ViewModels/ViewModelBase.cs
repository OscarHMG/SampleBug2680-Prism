using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProyectoBaseXF.Infrastructure.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBaseXF.Infrastructure.ViewModels
{
    public class ViewModelBase : ReactiveObject, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected IPageDialogService PageDialogService { get; private set; }
        protected LoaderService Loader { get; private set; }
        [Reactive] public string Title { get; set; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, LoaderService loader)
        {
            NavigationService = navigationService;
            PageDialogService = pageDialogService;
            Loader = loader;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        public async Task<T> LoadTaskAsync<T>(Task<T> task, string loaderText, string navigateTo = null)
        {
            bool taskFailed = false;
            try
            {
                await Loader.Show(loaderText);
                return await task;
            }
            catch (Exception e)
            {
                await PageDialogService.DisplayAlertAsync("Alerta", $"Ocurrió un error\n\n{e.Message}", "Ok");
                taskFailed = true;
                return default(T);
            }
            finally
            {
                if (!string.IsNullOrEmpty(navigateTo))
                {
                    if (!taskFailed)
                    {
                        await Loader.HideAndNavigate(navigateTo);
                    }
                    else
                    {
                        await Loader.Hide();
                    }
                }
                else
                {
                    await Loader.Hide();
                }
            }
        }

        public async Task LoadTaskAsync(Task task, string loaderText, string navigateTo = null)
        {
            bool taskFailed = false;
            try
            {
                await Loader.Show(loaderText);
                await task;
            }
            catch (Exception e)
            {
                await PageDialogService.DisplayAlertAsync("Alerta", $"Ocurrió un error\n\n{e.Message}", "Ok");
                taskFailed = true;
            }
            finally
            {
                if (!string.IsNullOrEmpty(navigateTo))
                {
                    if (!taskFailed)
                    {
                        await Loader.HideAndNavigate(navigateTo);
                    }
                    else
                    {
                        await Loader.Hide();
                    }
                }
                else
                {
                    await Loader.Hide();
                }
            }
        }

    }
}
