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
        private int _currentOrientation;

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
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            // Set the orientation as currentation on the first run
            if (_currentOrientation == 0)
            {;
                _currentOrientation =(int) (width > height ? Orientation.Landscape : Orientation.Portrait);
            }
            else
            {
                // On 2nd run onwards check whther the screen has been rotated and handle that
                int newOrientation = (int)(width > height ? Orientation.Landscape : Orientation.Portrait);
                if (newOrientation != _currentOrientation)
                {
                    
                    _currentOrientation = newOrientation;
                    InitializeComponent();                  
                    vm.OnScreenSizeChanged();
                }
            }
            
        }
        private enum Orientation { Landscape=1, Portrait};



    }
}
