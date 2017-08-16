using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Model
{
    public class PlaceToEat
    {
        public int Id_PlaceToEat { get; set; }
        public string PlaceToEatName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Id_Photo { get; set; }
        public int Price_Min { get; set; }
        public int Price_Max { get; set; }
    }
}
