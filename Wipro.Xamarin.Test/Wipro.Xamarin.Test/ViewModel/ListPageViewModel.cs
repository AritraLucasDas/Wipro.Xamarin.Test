using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Wipro.Xamarin.Test.Model;
using Wipro.Xamarin.Test.Service;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Wipro.Xamarin.Test.ViewModel
{
    public class ListPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Row> Items { get; } = new ObservableCollection<Row>();
        public ObservableCollection<Row> ItemsRedundant { get; } = new ObservableCollection<Row>();
        private readonly ListDataService _listDataService;
        private bool _isBusy;
        private string _title = "Loading";
        private bool _isSorted;
        private string _sortButtonText = "Sort";


        public ListPageViewModel()
        {
            _listDataService = new ListDataService();

        }

        ICommand _loadDataCommand;
        public ICommand LoadDataCommand =>
            _loadDataCommand ?? (_loadDataCommand = new Command(async () =>
            {
                IsBusy = true;
                await GetListDataAsync();
            }, () => !IsBusy));

        ICommand _sortListCommand;
        public ICommand SortListCommand =>
            _sortListCommand ?? (_sortListCommand = new Command(async () =>
            {
                IsBusy = true;
                await Sort_OnClicked();
            }, () => !IsBusy));


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public string SortButtontext
        {
            get => _sortButtonText;
            set
            {
                _sortButtonText = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private async Task GetListDataAsync()
        {
            if (IsBusy)
                return;
            try
            {
                DataModel receievedData = await _listDataService.GetListData();
                Title = receievedData.Title;
                if (Items.Count > 0)
                {
                    Items.Clear();
                }
                if (ItemsRedundant.Count > 0)
                {
                    ItemsRedundant.Clear();
                }
                receievedData.Rows.ForEach(Items.Add);
                receievedData.Rows.ForEach(ItemsRedundant.Add);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                await Application.Current.MainPage.DisplayAlert("Fetch Error", "Unable to get data",
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task Sort_OnClicked()
        {
            
            try
            {
                if (!_isSorted)
                {

                    var sortedItems = from item in Items orderby item.Title select item;
                    var t = sortedItems.ToList();
                    Items.Clear();
                    t.ForEach(Items.Add);
                    _isSorted = true;
                    _sortButtonText = "Unsort";

                }
                else
                {
                    Items.Clear();
                    ItemsRedundant.ForEach(Items.Add);
                    _isSorted = false;
                    _sortButtonText = "Sort";

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                await Application.Current.MainPage.DisplayAlert("Fetch Error", "Unable to sort data",
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }



        }
    }
}
