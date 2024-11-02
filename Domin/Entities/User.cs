using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entities
{
    public class User : IdentityUser<int>
    {

      
        public ICollection<Reservation> Reservations { get; set; }
    }
}
