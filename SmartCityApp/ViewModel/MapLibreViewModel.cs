using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using SmartCityApp.Model;
using SmartCityApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;

namespace SmartCityApp.ViewModel
{
    public class MapLibreViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private ICommand _backToMenu;
        private ICommand _place;
        private Geopoint _position = new Geopoint(new BasicGeoposition());
        private Geopoint _mapCenter;
        private List<Location> _locationsList = new List<Location>();
        private MapControl _myMap;
        private Location _nearLocation;
        private String _afficheGrid = "Collapsed";
        private String _rowDefHeight = "Auto";
        private DispatcherTimer dispatcherTimer;
        private UIElement _markerMyPos = new Canvas();
        private MapsLibrary mapsLibrary;

        public UIElement MarkerMyPos
        {
            get { return _markerMyPos; }
            set
            {
                if (_markerMyPos == value)
                {
                    return;
                }
                _markerMyPos = value;
                RaisePropertyChanged("MarkerMyPos");
            }
        }

        public String RowDefHeight
        {
            get { return _rowDefHeight; }
            set
            {
                if (_rowDefHeight == value)
                {
                    return;
                }
                _rowDefHeight = value;
                RaisePropertyChanged("RowDefHeight");
            }
        }
        public String AfficheGrid
        {
            get { return _afficheGrid; }
            set
            {
                if (_afficheGrid == value)
                {
                    return;
                }
                _afficheGrid = value;
                RaisePropertyChanged("AfficheGrid");
            }
        }
        public Location NearLocation
        {
            get { return _nearLocation; }
            set
            {
                if (_nearLocation == value)
                {
                    return;
                }
                _nearLocation = value;
                RaisePropertyChanged("NearLocation");
            }
        }
        public MapControl MyMap
        {
            get { return _myMap; }
            set
            {
                if (_myMap == value)
                {
                    return;
                }
                _myMap = value;
                RaisePropertyChanged("MyMap");
            }
        }
        public List<Location> LocationList
        {
            get { return _locationsList; }
            set
            {
                if (_locationsList == value)
                {
                    return;
                }
                _locationsList = value;
                RaisePropertyChanged("LocationList");
            }
        }
        public Geopoint MapCenter
        {
            get { return _mapCenter; }
            set
            {
                if (_mapCenter == value)
                {
                    return;
                }
                _mapCenter = value;
                RaisePropertyChanged("MapCenter");
            }
        }

        public Geopoint Position
        {
            get { return _position; }
            set
            {
                if (_position == value)
                {
                    return;
                }
                _position = value;
                RaisePropertyChanged("Position");
            }
        }

        public void AfficherMap(MapControl sender)
        {
            MyMap = sender;
            InitaliseAsync();
            DispatcherTimerSetup();
        }


        public MapLibreViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
        }

        public async void dispatcherTimer_Tick(object sender, object e)
        {
            mapsLibrary = new MapsLibrary();
            Position = await mapsLibrary.Position();

            //test point Musée Félicien Rops

            /*var basic = new BasicGeoposition();
            basic.Latitude = 50.463385;
            basic.Longitude = 4.8627025000000685;
            Position = new Geopoint(basic);*/

            //test point Citadelle

            /*var basic = new BasicGeoposition();
            basic.Latitude = 50.4588001;
            basic.Longitude = 4.86218550000001;
            Position = new Geopoint(basic);*/

            MyMap.Children.Remove(MarkerMyPos);

            MarkerMyPos = mapsLibrary.MarkerMyPosition();
            MyMap.Children.Add(MarkerMyPos);
            MapControl.SetLocation(MarkerMyPos, Position);
            MapControl.SetNormalizedAnchorPoint(MarkerMyPos, new Point(0.5, 0.5));
            MyMap.Center = Position;

            string rowH="";
            string AfficheG="";

            for (int i = 0; i < LocationList.Count; i++) { 

                double di = distance(LocationList[i].Latitude, LocationList[i].Longitude, Position.Position.Latitude, Position.Position.Longitude);
                if (di < 20)
                {
                    NearLocation = LocationList[i];
                    rowH = "*";
                    AfficheG = "Visible";
                    break;
                }
                else
                {
                    AfficheG = "Collapsed";
                    rowH = "Auto";
                }
            }

            AfficheGrid = AfficheG;
            RowDefHeight = rowH;

        }


        public async Task InitaliseAsync()
        {

            var mapsLibrary = new MapsLibrary();
            Position = await mapsLibrary.Position();

            MarkerMyPos = mapsLibrary.MarkerMyPosition();
            MyMap.Children.Add(MarkerMyPos);
            MapControl.SetLocation(MarkerMyPos, Position);
            MapControl.SetNormalizedAnchorPoint(MarkerMyPos, new Point(0.5, 0.5));
            MyMap.ZoomLevel = 17;
            MyMap.Center = Position;

            var locationService = new LocationService();
            var l = await locationService.GetAllLocations();
            LocationList = new List<Location>(l);

            for (int i=0; i< LocationList.Count;i++)
            {
                LocationList[i].Id_Photo = "ms-appx:/Images/" + LocationList[i].Id_Photo;
                BasicGeoposition pos = new BasicGeoposition();
                pos.Latitude = LocationList[i].Latitude;
                pos.Longitude = LocationList[i].Longitude;
                Geopoint posLoc = new Geopoint(pos);

                var mapsLibrary1 = new MapsLibrary();
                DependencyObject marker1 = mapsLibrary1.Marker(LocationList[i].TouristPlaceName);
                MyMap.Children.Add(marker1);
                MapControl.SetLocation(marker1, posLoc);
                MapControl.SetNormalizedAnchorPoint(marker1, new Point(0.5, 0.5));

            }



        }

        //distance en mètre
        private double distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344*1000;

            return (dist);
        }

        //decimal en radian
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //randians en décimal
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
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
            dispatcherTimer.Stop();
            this._navigationService.GoBack();
        }
    }
}
