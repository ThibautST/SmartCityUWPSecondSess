using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using SmartCityApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace SmartCityApp.ViewModel
{
    public class ParcoursMenuViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private ICommand _backToMenu;
        private ICommand _map;
        private ObservableCollection<Route> _routes;

        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser == value)
                {
                    return;
                }
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        public ObservableCollection<Route>Routes
        {
            get { return _routes; }
            set
            {
                if (_routes == value)
                {
                    return;
                }
                _routes = value;
                RaisePropertyChanged("Routes");
            }
        }

        public ParcoursMenuViewModel(INavigationService navigationService)
        {
            InitaliseAsync();
            _navigationService = navigationService;
        }

        public async Task InitaliseAsync()
        {
            var routeService = new RouteService();
            var r = await routeService.GetAllRoutes();
            Routes = new ObservableCollection<Route>(r);
        }


        public ICommand BackToMenu
        {
            get
            {
                if (this._backToMenu == null)
                {
                    this._backToMenu = new RelayCommand(() => BackToMainCommand());
                }
                return this._backToMenu;
            }
        }

        private void BackToMainCommand()
        {
            this._navigationService.GoBack();
        }

        public ICommand ToMap
        {
            get
            {
                if (this._map == null)
                {
                    this._map = new RelayCommand<int>((param) => ToMapCommand(param));
                }
                return this._map;
            }
        }

        private void ToMapCommand(int id)
        {
            if (CanExecute())
            {
                _navigationService.NavigateTo("Map", id);
            }
        }

        private bool CanExecute()
        {
            return true;
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentUser = (User)e.Parameter;
        }
    }
}
