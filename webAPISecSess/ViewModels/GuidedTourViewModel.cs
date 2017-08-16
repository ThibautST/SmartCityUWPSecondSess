using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webAPISecSess.Models;

namespace webAPISecSess.ViewModels
{
    public class GuidedTourViewModel
    {

        public int Id_GuidedTour { get; set; }

        public string GuidedTourName { get; set; }

        public double Distance { get; set; }

        public byte[] RowVersion { get; set; }

        public ICollection<TouristPlaceViewModel> TouristPlaces { get; set; }
    }
}