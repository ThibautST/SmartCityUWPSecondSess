using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace SmartCityApp.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private ICommand _backToMain;
        private ICommand _map;
        private ICommand _mapLibre;
        private ICommand _profil;
        private ICommand _parcoursMenu;
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                var appData = Windows.Storage.ApplicationData.Current;
                var localSettings = appData.LocalSettings;
                _userName = (string)localSettings.Values["username"];
            }
        }


        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public ICommand BackToMain
        {
            get
            {
                if (this._backToMain == null)
                {
                    this._backToMain = new RelayCommand(() => BackToMainCommand());
                }
                return this._backToMain;
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
                if(this._map == null)
                {
                    this._map = new RelayCommand(() => ToMapCommand());
                }
                return this._map;
            }
        }

        private void ToMapCommand()
        {
            _navigationService.NavigateTo("Map");
        }

        public ICommand ToMapLibre
        {
            get
            {
                if (this._mapLibre == null)
                {
                    this._mapLibre = new RelayCommand(() => ToMapLibreCommand());
                }
                return this._mapLibre;
            }
        }

        private void ToMapLibreCommand()
        {
            _navigationService.NavigateTo("MapLibre");
        }
        public ICommand ToProfil
        {
            get
            {
                if(this._profil == null)
                {
                    this._profil = new RelayCommand(() => ToProfilCommand());
                }
                return this._profil;
            }
        }

        private void ToProfilCommand()
        {
            _navigationService.NavigateTo("Profil");
        }
        public ICommand ToParcoursMenu
        {
            get
            {
                if(this._parcoursMenu == null)
                {
                    this._parcoursMenu = new RelayCommand(() => ToParcoursMenuCommand());
                }
                return this._parcoursMenu;
            }
        }

        private void ToParcoursMenuCommand()
        {
            _navigationService.NavigateTo("ParcoursMenu");
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
