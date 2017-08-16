using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAPISecSess.Models
{
    public class PlaceToEat
    {
        [Key]
        public int Id_PlaceToEat { get; set; }
        [Required]
        public string PlaceToEatName { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Id_Photo { get; set; }
        [Required]
        public int Price_Min { get; set; }
        [Required]
        public int Price_Max { get; set; }

        public ICollection<GuidedTour> GuidedTour { get; set; }
    }
}
