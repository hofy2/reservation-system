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

namespace Application.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteReservationCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                 .MustAsync(ReservationExists);
        }
        private async Task<bool> ReservationExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Reservations.AnyAsync(m => m.Id == id, cancellationToken);
        }
    }
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
    {
        private readonly IGenericRepository<Reservation> _reservationRepository;

        public DeleteReservationCommandHandler(IGenericRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
      
        public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.Id);


            if (reservation == null)
            {
                throw new NotFoundException(nameof(Reservation), request.Id);
            }

            await _reservationRepository.DeleteAsync(reservation.Id);

        }
    }
}
