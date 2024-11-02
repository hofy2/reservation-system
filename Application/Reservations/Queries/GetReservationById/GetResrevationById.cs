using Application.Reservations.Commands.UpdateReservation;
using Application.Reservations.Common;
using Domin.Entities;
using Domin.interfaces;
using FluentValidation;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        public class GetReservationByIdQueryValidator : AbstractValidator<UpdateReservationCommand>
        {
            private readonly ApplicationDbContext _context;

            public GetReservationByIdQueryValidator(ApplicationDbContext context)
            {
                _context = context;

                RuleFor(x => x.CustomerName)
                    .NotEmpty().WithMessage("Customer Name is required.")
                    .MaximumLength(100).WithMessage("Customer Name must not exceed 100 characters.");

                RuleFor(x => x.ReservationDate)
                    .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Reservation Date cannot be in the past.");

                RuleFor(x => x.Id)
                    .MustAsync(ReservationExists)
                    .GreaterThan(0).WithMessage("Reservation ID must be greater than 0.");
            }
            private async Task<bool> ReservationExists(int id, CancellationToken cancellationToken)
            {
                return await _context.Reservations.AnyAsync(m => m.Id == id, cancellationToken);
            }
        }
        public async Task<Reservation> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.Id);

            //if (reservation == null)
            //{
            //    throw new NotFoundException(nameof(Reservation), request.Id);
            //}

            return reservation;
        }
    }
}
