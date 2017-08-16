using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAPISecSess.Models
{
    public class Category
    {
        [Key]
        public int Id_Category { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }

        public  ICollection<GuidedTour> GuidedTour { get; set; }
    }
}
