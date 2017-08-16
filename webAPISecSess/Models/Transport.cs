using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAPISecSess.Models
{
    public class Transport
    {
        [Key]
        public int Id_Transport { get; set; }

        [Required]
        [MaxLength(50)]
        public string TransportName { get; set; }

        [Required]
        public int Speed { get; set; }

        public ICollection<GuidedTour> GuidedTour { get; set; }
    }
}
