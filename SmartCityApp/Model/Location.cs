﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Model
{
    public class Location
    {
        public int Id_TouristPlace { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TouristPlaceName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public string Id_Photo { get; set; }
        public int Price { get; set; }
        public byte[] RowVersion { get; set; }
    }
}