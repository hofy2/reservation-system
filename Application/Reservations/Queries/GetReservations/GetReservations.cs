using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Entities;
using Domin.interfaces;

namespace Application.Reservations.Queries.GetReservations
{
    public class GetReservationsListQuery : IRequest<IEnumerable<Reservation>>
    {

    }


    public class GetReservationsListQueryHandler : IRequestHandler<GetReservationsListQuery, IEnumerable<Reservation>>
    {
        private readonly IGenericRepository<Reservation> _reservationRepository;

        public GetReservationsListQueryHandler(IGenericRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<Reservation>> Handle(GetReservationsListQuery request, CancellationToken cancellationToken)
        {
            return await _reservationRepository.GetAllAsync();
        }
    }
}
