using Application.Reservations.Commands.CreateReservation;
using Application.Reservations.Commands.DeleteReservation;
using Application.Reservations.Commands.UpdateReservation;
using Application.Reservations.Queries.GetReservationById;
using Application.Reservations.Queries.GetReservations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ISender _mediator;

        public ReservationsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllReservations")]
        public async Task<IResult> GetAllReservations()
        {
            var reservations = await _mediator.Send(new GetReservationsListQuery());
            return Results.Ok(reservations);
        }

        [HttpGet("GetReservationById/{id:int}")]
        public async Task<IResult> GetReservationById(int id)
        {
            var reservation = await _mediator.Send(new GetReservationByIdQuery { Id = id });

            return reservation is not null ? Results.Ok(reservation) : Results.NotFound();
        }

        [HttpPost("CreateReservation")]
        public async Task<IResult> CreateReservation(CreateReservationCommand command)
        {
            var reservationId = await _mediator.Send(command);
            return Results.CreatedAtRoute("GetReservationById", new { id = reservationId }, reservationId);
        }

        [HttpPut("UpdateReservation/{id:int}")]
        public async Task<IResult> UpdateReservation(int id, UpdateReservationCommand command)
        {
            if (id != command.Id)
            {
                return Results.BadRequest("Reservation ID mismatch.");
            }

            await _mediator.Send(command);
            return Results.NoContent();
        }

        [HttpDelete("DeleteReservation/{id:int}")]
        public async Task<IResult> DeleteReservation(int id)
        {
            await _mediator.Send(new DeleteReservationCommand { Id = id });
            return Results.NoContent();
        }
    }
}
