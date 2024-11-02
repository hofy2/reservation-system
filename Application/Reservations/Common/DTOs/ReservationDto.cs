using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domin.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Application.Reservations.Common.DTOs
{

    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<Reservation, ReservationDto>();
        }
    }
    public class ReservationDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CustomerName { get; set; }
        public int TripId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Notes { get; set; }
    }

}
