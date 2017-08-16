using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAPISecSess.Models
{
    public class TouristPlace
    {
        [Key]
        public int Id_TouristPlace { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public string TouristPlaceName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public string Id_Photo { get; set; }
        [Required]
        public int Price { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<PlaceWithOrder> PlaceWithOrder { get; set; }
    }
}
