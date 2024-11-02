

namespace Application.Reservations.Commands.Reservationservice
{
    using Application.Reservations.Commands.DeleteReservation;
    using Application.Reservations.Queries.GetReservations;
    using MediatR;

    public class ReservationService
    {
        private readonly ISender _mediator;

        public ReservationService(ISender mediator)
        {
            _mediator = mediator;
        }

        public async Task DeleteFirstReservation()
        {
            var reservations = await _mediator.Send(new GetReservationsListQuery());
            var firstReservation = reservations.OrderBy(r => r.ReservationDate).FirstOrDefault();

            if (firstReservation != null)
            {
                await _mediator.Send(new DeleteReservationCommand { Id = firstReservation.Id });
            }
        }
    }

}
