using AutoMapper;
using MediatR;
using Domin.Entities;
using Domin.interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Reservations.Common.DTOs;

namespace Application.Reservations.Queries.GetReservations
{
    public class GetReservationsListQuery : IRequest<IEnumerable<ReservationDto>>
    {
    }

    public class GetReservationsListQueryHandler : IRequestHandler<GetReservationsListQuery, IEnumerable<ReservationDto>>
    {
        private readonly IGenericRepository<Reservation> _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservationsListQueryHandler(IGenericRepository<Reservation> reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDto>> Handle(GetReservationsListQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAllAsync();

            // Map Reservation entities to ReservationDto
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }
    }
}
