using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAPISecSess.Models
{
    public class GuidedTour
    {
        [Key]
        public int Id_GuidedTour { get; set; }
        [Required]
        public string GuidedTourName { get; set; }
        [Required]
        public double Distance { get; set; }

        //Foreign key for Transport
        public int Id_Transport { get; set; }

        public  Transport Transport { get; set; }

        //Foreign key for PlaceToEat
        public int? Id_PlaceToEat { get; set; }

        public  PlaceToEat PlaceToEat { get; set; }


        //Foreign key for Category
        public int Id_Category { get; set; }

        public  Category Category { get; set; }


        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<PlaceWithOrder> PlaceWithOrder { get; set; }
    }
}
