using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using SmartCityApp.Model;
using SmartCityApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.System.Threading;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;


namespace SmartCityApp.ViewModel
{
    public class MapViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private ICommand _backToMenu;
        private Route _selectedRoute;
        private int _idRoute;
        private Geopoint _position = new Geopoint(new BasicGeoposition());
        private Geopoint _mapCenter;
        private MapControl _myMap;
        private Location _nearLocation;
        private String _afficheGrid = "Collapsed";
        private String _rowDefHeight = "Auto";
        private User _currentUser;
        private DispatcherTimer posTimer;
        private DispatcherTimer routeTimer;
        private MapsLibrary mapsLibrary;
        private UIElement _markerMyPos = new Canvas();
        private int nextLocation = 0;
        private MapRouteView _routeViewDir = null;
        private RouteService _routeService;

        public MapRouteView RouteViewDir
        {
            get { return _routeViewDir; }
            set
            {
                if (_routeViewDir == value)
                {
                    return;
                }
                _routeViewDir = value;
                RaisePropertyChanged("RouteViewDir");
            }
        }
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


        public Route SelectedRoute
        {
            get { return _selectedRoute; }
            set
            {
                if (_selectedRoute == value)
                {
                    return;
                }
                _selectedRoute = value;
                RaisePropertyChanged("SelectedRoute");
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

        public MapViewModel(INavigationService navigationService)
        {
            nextLocation = 0;
            _navigationService = navigationService;
            _routeService = new RouteService();
        }


        public void AfficherMap(MapControl sender)
        {
            MyMap = sender;
            InitaliseAsync();
            DispatcherTimerSetup();
        }

        public void DispatcherTimerSetup()
        {
            posTimer = new DispatcherTimer();
            posTimer.Tick += dispatcherTimer_Tick_Pos;
            posTimer.Interval = new TimeSpan(0, 0, 30);
            posTimer.Start();

            routeTimer = new DispatcherTimer();
            routeTimer.Tick += dispatcherTimer_Tick_Route;
            routeTimer.Interval = new TimeSpan(0, 0, 30);
            routeTimer.Start();
        }

        public async void dispatcherTimer_Tick_Pos(object sender, object e)
        {
            mapsLibrary = new MapsLibrary();
            Position = await mapsLibrary.Position();

            //test point felicien rops

            /*var basic = new BasicGeoposition();
            basic.Latitude = 50.463385;
            basic.Longitude = 4.8627025000000685;
            Position = new Geopoint(basic);*/

            MyMap.Children.Remove(MarkerMyPos);

            MarkerMyPos = mapsLibrary.MarkerMyPosition();
                MyMap.Children.Add(MarkerMyPos);
                MapControl.SetLocation(MarkerMyPos, Position);
                MapControl.SetNormalizedAnchorPoint(MarkerMyPos, new Point(0.5, 0.5));

               double dist = distance(SelectedRoute.TouristPlaces[nextLocation].Latitude, SelectedRoute.TouristPlaces[nextLocation].Longitude, Position.Position.Latitude, Position.Position.Longitude);

            if (dist < 30)
            {
                NearLocation = SelectedRoute.TouristPlaces[nextLocation];
                RowDefHeight = "*";
                AfficheGrid = "Visible";

                MyMap.Routes.Remove(RouteViewDir);
                nextLocation++;

                if (nextLocation == SelectedRoute.TouristPlaces.Count())
                {
                    nextLocation = 0;
                }
            }
            else
            {
                AfficheGrid = "Collapsed";
                RowDefHeight = "Auto";
            }  

        }

        public void dispatcherTimer_Tick_Route(object sender, object e)
        {
            MyMap.Center = Position;
            MyMap.Routes.Remove(RouteViewDir);
            AfficherRoute(nextLocation);

        }


        public async Task InitaliseAsync()
        {
            MyMap.ZoomLevel = 17;
            mapsLibrary = new MapsLibrary();
            Position = await mapsLibrary.Position();
            MarkerMyPos = mapsLibrary.MarkerMyPosition();
            MyMap.Children.Add(MarkerMyPos);
            MapControl.SetLocation(MarkerMyPos, Position);
            MapControl.SetNormalizedAnchorPoint(MarkerMyPos, new Point(0.5, 0.5));

            MyMap.Center = Position;

            AfficherRoute(nextLocation);

            var  locationList = SelectedRoute.TouristPlaces;

            for (int i = 0; i < locationList.Count; i++)
            {

                BasicGeoposition pos = new BasicGeoposition();
                pos.Latitude = locationList[i].Latitude;
                pos.Longitude = locationList[i].Longitude;
                Geopoint posLoc = new Geopoint(pos);

                var mapsLibrary1 = new MapsLibrary();
                DependencyObject marker1 = mapsLibrary1.Marker(locationList[i].TouristPlaceName);
                MyMap.Children.Add(marker1);
                MapControl.SetLocation(marker1, posLoc);
                MapControl.SetNormalizedAnchorPoint(marker1, new Point(0.5, 0.5));

            }

            AfficherParours();
        }

        public async void AfficherRoute(int IdPoint )
        {
            BasicGeoposition pos = new BasicGeoposition();
            pos.Latitude = SelectedRoute.TouristPlaces[IdPoint].Latitude;
            pos.Longitude = SelectedRoute.TouristPlaces[IdPoint].Longitude;
            Geopoint posDestRoute = new Geopoint(pos);

            MapRouteFinderResult route = await MapRouteFinder.GetDrivingRouteAsync(Position,
                                posDestRoute,
                                MapRouteOptimization.Time,
                                MapRouteRestrictions.None);

            RouteViewDir = new MapRouteView(route.Route);
            RouteViewDir.RouteColor = Windows.UI.Colors.Red;

            MyMap.Routes.Add(RouteViewDir);
        }

        public async void AfficherParours()
        {
            for(int i=0; i< SelectedRoute.TouristPlaces.Count-1; i++)
            {
                BasicGeoposition pos = new BasicGeoposition();
                pos.Latitude = SelectedRoute.TouristPlaces[i].Latitude;
                pos.Longitude = SelectedRoute.TouristPlaces[i].Longitude;
                Geopoint posDepRoute = new Geopoint(pos);

                BasicGeoposition pos2 = new BasicGeoposition();
                pos2.Latitude = SelectedRoute.TouristPlaces[i+1].Latitude;
                pos2.Longitude = SelectedRoute.TouristPlaces[i+1].Longitude;
                Geopoint posDestRoute = new Geopoint(pos2);

                MapRouteFinderResult routeParcours = await MapRouteFinder.GetDrivingRouteAsync(posDepRoute,
                                    posDestRoute,
                                    MapRouteOptimization.Time,
                                    MapRouteRestrictions.None);


                var routeView = new MapRouteView(routeParcours.Route);
                routeView.RouteColor = Windows.UI.Colors.Yellow;

                MyMap.Routes.Add(routeView);
            }

            int j = SelectedRoute.TouristPlaces.Count - 1;
            BasicGeoposition derPos = new BasicGeoposition();
            derPos.Latitude = SelectedRoute.TouristPlaces[j].Latitude;
            derPos.Longitude = SelectedRoute.TouristPlaces[j].Longitude;
            Geopoint posDern = new Geopoint(derPos);

            BasicGeoposition premPos = new BasicGeoposition();
            premPos.Latitude = SelectedRoute.TouristPlaces[j].Latitude;
            premPos.Longitude = SelectedRoute.TouristPlaces[j].Longitude;
            Geopoint posPrem = new Geopoint(premPos);

            MapRouteFinderResult routeFin = await MapRouteFinder.GetDrivingRouteAsync(posDern,
                    posPrem,
                    MapRouteOptimization.Time,
                    MapRouteRestrictions.None);


            var routeViewFin = new MapRouteView(routeFin.Route);
            routeViewFin.RouteColor = Windows.UI.Colors.Yellow;

            MyMap.Routes.Add(routeViewFin);

        }

        //distance en mètre
        private double distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344 * 1000;

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
            posTimer.Stop();
            routeTimer.Stop();
            this._navigationService.GoBack();
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            _idRoute = (int)e.Parameter;
            SelectedRoute = _routeService.GetRoute(_idRoute).Result;
        }
    }
}
