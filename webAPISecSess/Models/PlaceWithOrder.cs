using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAPISecSess.Models
{
    public class PlaceWithOrder
    {
        [Key]
        public int Id_PlaceWithOrder { get; set; }

        [Required]
        [Index("OrdreTour", IsUnique = true, Order = 2)]
        public int OrderNumber { get; set; }


        //Foreign key for GuidedTour
        [Required]
        [Index("PlaceTour", IsUnique = true, Order = 1)]
        [Index("OrdreTour", IsUnique = true, Order = 1)]
        public int Id_GuidedTour { get; set; }

        public GuidedTour GuidedTour { get; set; }

        //Foreign key for ToursitPlace

        [Required] 
        [Index("PlaceTour", IsUnique = true, Order = 2)]
        public int Id_TouristPlace { get; set; }

        public TouristPlace TouristPlace { get; set; }
    }
}
