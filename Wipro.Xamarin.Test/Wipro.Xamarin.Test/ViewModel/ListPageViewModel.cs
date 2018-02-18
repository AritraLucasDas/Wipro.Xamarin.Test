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
        private string _title ;
        private bool _isSorted;
        private string _sortButtonText;


        public ListPageViewModel()
        {
            _listDataService = new ListDataService();
            SetDefautValues();

        }

        public void OnScreenSizeChanged()
        {
            SetDefautValues();
            LoadDataCommand.Execute(null);
        }
        private void SetDefautValues()
        {
            _title = "Loading";
            _sortButtonText = "Sort";

        }

        ICommand _loadDataCommand;
        public ICommand LoadDataCommand =>
            _loadDataCommand ?? (_loadDataCommand = new Command(async ()=>
            {
               
                await GetListDataAsync();
            }, () => !IsBusy));

        ICommand _sortListCommand;
        public ICommand SortListCommand =>
            _sortListCommand ?? (_sortListCommand = new Command(async () =>
            {
               
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
            
            try
            {
                IsBusy = true;
                await Task.Run(() =>
                {
                    DataModel receievedData = _listDataService.GetListData();

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

                });
                
            }
            catch (Exception ex)
            {
                ShowError(ex);
                
                
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
                IsBusy = true;
                await Task.Run(() => {
                    if (!_isSorted)
                    {

                        var sortedItems = from item in Items orderby item.Title select item;
                        var t = sortedItems.ToList();
                        Items.Clear();
                        t.ForEach(Items.Add);
                        _isSorted = true;
                        SortButtontext = "Unsort";

                    }
                    else
                    {
                        Items.Clear();
                        ItemsRedundant.ForEach(Items.Add);
                        _isSorted = false;
                        SortButtontext = "Sort";

                    }
                });
                
            }
            catch (Exception ex)
            {
                
                ShowError(ex);
            }
            finally
            {
                IsBusy = false;
            }



        }

        private async void ShowError(Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
            Title = "Error";
            await Application.Current.MainPage.DisplayAlert("Fetch Error", "Unable to get data",
                "OK");
        }
    }
}
