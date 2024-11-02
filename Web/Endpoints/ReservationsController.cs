using Application.Reservations.Commands.CreateReservation;
using Application.Reservations.Commands.DeleteReservation;
using Application.Reservations.Commands.Reservationservice;
using Application.Reservations.Commands.UpdateReservation;
using Application.Reservations.Queries.GetReservationById;
using Application.Reservations.Queries.GetReservations;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IResult> GetAllReservations()
        {
            var reservations = await _mediator.Send(new GetReservationsListQuery());
            return Results.Ok(reservations);
        }

        [HttpGet("GetReservationById/{id:int}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IResult> GetReservationById(int id)
        {
            var reservation = await _mediator.Send(new GetReservationByIdQuery { Id = id });

            return reservation is not null ? Results.Ok(reservation) : Results.NotFound();
        }

        [HttpPost("CreateReservation")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<IActionResult> CreateReservation(CreateReservationCommand command)
        {
            var reservationId = await _mediator.Send(command);
           BackgroundJob.Enqueue<ReservationService>(service => service.DeleteFirstReservation());

            return Ok(reservationId); ;
        }



        [HttpPut("UpdateReservation/{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IResult> UpdateReservation(int id, UpdateReservationCommand command)
        {
            if (id != command.Id)
            {
                return Results.BadRequest("Reservation ID mismatch.");
            }

            await _mediator.Send(command);
            return Results.NoContent();
        }
        [Authorize(Roles = "Admin")]
        
        [HttpDelete("DeleteReservation/{id:int}")]
        public async Task<IResult> DeleteReservation(int id)
        {
            await _mediator.Send(new DeleteReservationCommand { Id = id });
            return Results.NoContent();
        }
    }
}
