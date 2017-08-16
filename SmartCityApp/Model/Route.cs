using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Route
    {
        public int Id_GuidedTour { get; set; }
        public string GuidedTourName { get; set; }
        public double Distance { get; set; }
        public Transport Transport { get; set; }
        public PlaceToEat PlaceToEat { get; set; }
        public Category Category { get; set; }

        public List<Location> TouristPlaces { get; set; }
    }
}
