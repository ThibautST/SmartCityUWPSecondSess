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
using System.ComponentModel;
using Windows.UI.Popups;

namespace SmartCityApp.ViewModel
{
    public class MainViewModel: ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<User> _users = null;
        private INavigationService _navigationService;
        private ICommand _inscriptionPage;
        private ICommand _menuPage;
        private String _pseudo;
        private String _pass;
        private String _code;
        private User _currentUser;
        

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                if (_users == value)
                {
                    return;
                }
                _users = value;
                RaisePropertyChanged("Users");
            }
        }

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


        public string Pseudo
        {
            get { return _pseudo; }
            set
            {
                if (value != _pseudo)
                {
                    _pseudo = value;
                    RaisePropertyChanged("Pseudo");
                }
            }
        }

        public string Pass
        {
            get { return _pass; }
            set
            {
                if (value != _pass)
                {
                    _pass = value;
                    RaisePropertyChanged("Pass");
                }
            }
        }

        public String Code
        {
            get { return _code; }
            set
            {
                if(value != _code)
                {
                    _code = value;
                    RaisePropertyChanged("Code");
                }
            }
        }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand InscriptionPage
        {
            get
            {
                if (this._inscriptionPage == null)
                {
                    this._inscriptionPage = new RelayCommand(() => InscriptionPageCommand());
                }
                return this._inscriptionPage;
            }
        }

        private void InscriptionPageCommand()
        {
            _navigationService.NavigateTo("Inscription");
        }

        public ICommand ToMenu
        {
            get
            {
                if (this._menuPage == null)
                {
                    this._menuPage = new RelayCommand(() => ToMenuCommand());
                }
                return this._menuPage;
            }
        }

        private void ToMenuCommand()
        {
            ConnectUser();
            if (CanExecute())
            {
                _navigationService.NavigateTo("Menu");
            }
            else
            {
                _navigationService.NavigateTo("MainPage");
            }
        }

        public async Task ConnectUser()
        {
            var userService = new UserService();
            _code = userService.ConnectionUser(Pseudo, Pass).Result;
            if (_code != "ok")
            {
                var dialogue = new MessageDialog(_code);
                dialogue.ShowAsync();
            }
        }

        private bool CanExecute()
        {
            if(_code == "ok")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
