using Domin.Entities;
using Domin.interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Entities;
using FluentValidation;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public int TripId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Notes { get; set; }
    }
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateReservationCommandValidator(ApplicationDbContext context)
        {
            _context = context;


            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer Name is required.")
                .MaximumLength(100).WithMessage("Customer Name must not exceed 100 characters.");



            RuleFor(x => x.UserId).
            NotEmpty().WithMessage("Customer Name is required.");
            
            RuleFor(x => x.TripId)
                .GreaterThan(0).WithMessage("Trip ID must be greater than 0.");
        }

    }
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
    {
        private readonly IGenericRepository<Reservation> _reservationRepository;

        public CreateReservationCommandHandler(IGenericRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            
                var reservation = new Reservation
                {
                    ReservedById = request.UserId,
                    CustomerName = request.CustomerName,
                    TripId = request.TripId,
                    ReservationDate = request.ReservationDate,
                    CreationDate = DateTime.UtcNow,
                    Notes = request.Notes
                };

                await _reservationRepository.AddAsync(reservation);

                return reservation.Id;
            
         
        }
    }
}
