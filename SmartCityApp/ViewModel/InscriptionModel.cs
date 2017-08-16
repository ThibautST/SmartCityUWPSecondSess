using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using SmartCityApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace SmartCityApp.ViewModel
{
    public class InscriptionModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private ICommand _backToMain;
        private ICommand _menuPage;
        private string _name;
        private string _firstName;
        private string _pseudo;
        private string _pass;
        private string _passConfirm;
        private string _address;
        private int _phoneNumber;
        private string _email;
        private string _resultMessage;
        private User _registrationUser;
        private DateTimeOffset _dateForm;
        public DateTimeOffset DateForm
        {
            get
            {
                return _dateForm;
            }
            set
            {
                if (value != _dateForm)
                {
                    _dateForm = value;
                    RaisePropertyChanged("DateForm");
                }
            }
        }


        public User RegistrationUser
        {
            get { return _registrationUser; }
            set
            {
                if (_registrationUser == value)
                {
                    return;
                }
                _registrationUser = value;
                RaisePropertyChanged("RegistrationUser");
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

        public string PassConfirm
        {
            get { return _passConfirm; }
            set
            {
                if (value != _passConfirm)
                {
                    _passConfirm = value;
                    RaisePropertyChanged("PassConfirm");
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    RaisePropertyChanged("FirstName");
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (value != _address)
                {
                    _address = value;
                    RaisePropertyChanged("Address");
                }
            }
        }


        public int PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value != _phoneNumber)
                {
                    _phoneNumber = value;
                    RaisePropertyChanged("PhoneNumber");
                }
            }
        }

        public String Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    RaisePropertyChanged("Email");
                }
            }
        }

        public String ResultMessage
        {
            get { return _resultMessage; }
            set
            {
                if (value != _resultMessage)
                {
                    _resultMessage = value;
                    RaisePropertyChanged("ResultMessage");
                }
            }
        }

        public InscriptionModel(INavigationService navigationService)
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
            var check = checkData();
            if (check == "ok")
            {

                int Yearf = DateForm.Year;
                int Monthf = DateForm.Month;
                int Dayf = DateForm.Day;

                var registration = new User()
                {
                    LastName = Name,
                    FirstName = FirstName,
                    UserName = Pseudo,
                    Password = Pass,
                    ConfirmPassword = PassConfirm,
                    Address = Address,
                    BirthDate = new DateTime(Yearf, Monthf, Dayf),
                    PhoneNumber = PhoneNumber,
                    Email = Email
                };

                RegistrationUser = registration;
                AddUser();
                if (_resultMessage == "ok")
                {
                    _navigationService.NavigateTo("MainPage");
                }
                else
                {
                    _navigationService.NavigateTo("Inscription");
                }
            }
            else
            {
                var dialogueerror = new MessageDialog(check);
                dialogueerror.ShowAsync();
            }
        }

        public async Task AddUser()
        {
            var userService = new UserService();
            _resultMessage = userService.AddUser(RegistrationUser).Result;
            if(_resultMessage == "ok")
            {
                var dialogueOK = new MessageDialog("Inscription confirmée !");
                await dialogueOK.ShowAsync();
            }
            else
            {
                var dialogueError = new MessageDialog(_resultMessage);
                await dialogueError.ShowAsync();
            }
        }

        public string checkData()
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (Name == null || FirstName == null || Email == null || Pseudo == null || Pass == null)
            {
                return "Completez les champs obligatoires: Nom, Prenom, Pseudo, Mot de passe et Email !";
            }
            else if(Pass != PassConfirm)
            {
                return "Le mot de passe et mot de passe confirmé doivent correspondrent";
            }
            else if(Pass.Length < 8)
            {
                return "Le mot de passe doit contenir au moins 8 caractères";
            }
            else if(!hasLowerChar.IsMatch(Pass))
            {
                return "Le mot de passe doit contenir au moins une lettre minuscule";
            }
            else if(!hasUpperChar.IsMatch(Pass))
            {
                return "Le mot de passe doit contenir au moins une lettre majuscule";
            }
            else if (!hasNumber.IsMatch(Pass))
            {
                return "Le mot de passe doit contenir au moins un chiffre";
            }
            else if(!hasSymbols.IsMatch(Pass))
            {
                return "Le mot de passe doit contenir un symbole";
            }
            else if (!Email.Contains("@"))
            {
                return "L'email doit contenir une adresse valide";
            }
            else
            {
                return "ok";
            }
        }
    }
}
