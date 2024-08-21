using Application.Reservations.Common;
using Domin.Entities;
using Domin.interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Commands.UpdateReservation
{
   public class UpdateReservationCommand : IRequest
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
    {
        private readonly IGenericRepository<Reservation> _reservationRepository;

        public UpdateReservationCommandHandler(IGenericRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
        {
            public UpdateReservationCommandValidator()
            {
                RuleFor(x => x.CustomerName)
                    .NotEmpty().WithMessage("Customer Name is required.")
                    .MaximumLength(100).WithMessage("Customer Name must not exceed 100 characters.");

                RuleFor(x => x.ReservationDate)
                    .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Reservation Date cannot be in the past.");

                RuleFor(x => x.Id)
                    .GreaterThan(0).WithMessage("Reservation ID must be greater than 0.");
            }
        }
        public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.Id);

            if (reservation == null)
            {
                throw new NotFoundException(nameof(Reservation), request.Id);
            }

            reservation.CustomerName = request.CustomerName;
            reservation.ReservationDate = request.ReservationDate;
            reservation.Notes = request.Notes;

            await _reservationRepository.UpdateAsync(reservation);


        }

    }
}
