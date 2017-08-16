using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCityApp.ViewModel
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<InscriptionModel>();
            SimpleIoc.Default.Register<MenuViewModel>();
            SimpleIoc.Default.Register<ParcoursMenuViewModel>();
            SimpleIoc.Default.Register<MapViewModel>();
            SimpleIoc.Default.Register<MapLibreViewModel>();

            NavigationService navigationPages = new NavigationService();
            SimpleIoc.Default.Unregister<INavigationService>();
            SimpleIoc.Default.Register<INavigationService>(() => navigationPages);
            navigationPages.Configure("MainPage", typeof(MainPage));
            navigationPages.Configure("Inscription", typeof(Inscription));
            navigationPages.Configure("Menu", typeof(Menu));
            navigationPages.Configure("ParcoursMenu", typeof(ParcoursMenu));
            navigationPages.Configure("Map", typeof(Map));
            navigationPages.Configure("MapLibre", typeof(MapLibre));
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public InscriptionModel Inscription
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InscriptionModel>();
            }
        }

        public MenuViewModel Menu
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MenuViewModel>();
            }
        }

        public ParcoursMenuViewModel ParcoursMenu
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ParcoursMenuViewModel>();
            }
        }


        public MapViewModel Map
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MapViewModel>();
            }
        }

        public MapLibreViewModel MapLibre
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MapLibreViewModel>();
            }
        }
    }
}
