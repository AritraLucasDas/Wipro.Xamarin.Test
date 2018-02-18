using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Wipro.Xamarin.Test.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wipro.Xamarin.Test.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        private ListPageViewModel vm;

        public ListPage()
        {
            InitializeComponent();
            BindingContext = vm= new ListPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.LoadDataCommand.Execute(null);
        }


        
    }
}
