using Application.Reservations.Common;
using Domin.Entities;
using Domin.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Queries.GetReservationById
{
    public class GetReservationByIdQuery : IRequest<Reservation>
    {
        public int Id { get; set; }
    }

    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, Reservation>
    {
        private readonly IGenericRepository<Reservation> _reservationRepository;

        public GetReservationByIdQueryHandler(IGenericRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Reservation> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.Id);

            if (reservation == null)
            {
                throw new NotFoundException(nameof(Reservation), request.Id);
            }

            return reservation;
        }
    }
}
