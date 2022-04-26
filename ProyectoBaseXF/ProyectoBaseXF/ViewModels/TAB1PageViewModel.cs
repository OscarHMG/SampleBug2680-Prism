using Prism.Navigation;
using Prism.Services;
using ProyectoBaseXF.Infrastructure.Services;
using ProyectoBaseXF.Infrastructure.ViewModels;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using static ProyectoBaseXF.Infrastructure.ApiModels.Models;

namespace ProyectoBaseXF.ViewModels
{
    public class TAB1PageViewModel: ViewModelBase
    {
        [Reactive] public ObservableCollection<Item> ListItems { get; set; } = new ObservableCollection<Item>();

        public ICommand ItemSelectedCommand { get; set; }

        public ICommand DeleteItemCommand { get; set; }

        public TAB1PageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, LoaderService loaderService): base(navigationService, pageDialogService, loaderService)
        {
            PopulateItems();

            ItemSelectedCommand = new Command<Item>(async(ItemSelected)=> 
            {
                NavigationParameters Params = new NavigationParameters();
                Params.Add("ItemSelected", ItemSelected);
                var result = await navigationService.NavigateAsync("DemoPage", Params);
                if (!result.Success)
                {
                    Debugger.Break();
                }
            });

            DeleteItemCommand = new Command<Item>(async(ItemSelected) => {
                await pageDialogService.DisplayAlertAsync("Item deleted", "CLICK IN ITEM", "OK");
            });
        }


        public void PopulateItems()
        {
            Item Item1 = new Item();
            Item1.ID = 1;
            Item1.Name = "Xamarin";

            Item Item2 = new Item();
            Item2.ID = 2;
            Item2.Name = "Prism";

            Item Item3 = new Item();
            Item3.ID = 3;
            Item3.Name = "ReactiveUI";

            ListItems = new ObservableCollection<Item>();
            ListItems.Add(Item1);
            ListItems.Add(Item2);
            ListItems.Add(Item3);
        }
    }
}
