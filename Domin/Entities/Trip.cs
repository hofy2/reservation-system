using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; } // HTML content stored as a string
        public DateTime CreationDate { get; set; }

        // Navigation property to Reservations
        public ICollection<Reservation> Reservations { get; set; }
    }
}
