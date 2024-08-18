using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ReservedById { get; set; } // Foreign key for User
        public User ReservedBy { get; set; }  // Navigation property to User
        public string CustomerName { get; set; }

        // Foreign key to Trip
        public int TripId { get; set; }        // Foreign key for Trip
        public Trip Trip { get; set; }         // Navigation property to Trip
        public DateTime ReservationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string Notes { get; set; }
    }
}
