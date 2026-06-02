namespace Reservations.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Reservations.Commands.CreateReservation;
    using Domin.Entities;
    using Domin.interfaces;
    using Moq;
    using Xunit;
    using FluentAssertions;
    using Infrastructure.Data;

    public class CreateReservationCommandHandlerTests
    {
        private readonly Mock<IGenericRepository<Reservation>> _reservationRepositoryMock;
        private readonly CreateReservationCommandHandler _handler;
        private readonly CreateReservationCommandValidator _validator;


        public CreateReservationCommandHandlerTests()
        {
            _reservationRepositoryMock = new Mock<IGenericRepository<Reservation>>();
            _handler = new CreateReservationCommandHandler(_reservationRepositoryMock.Object);
            _validator = new CreateReservationCommandValidator(new ApplicationDbContext());  // Mock DbContext if needed

        }

        [Fact]
        public async Task Handle_ShouldCreateReservation_WhenCommandIsValid()
        {
            // Arrange
            var command = new CreateReservationCommand
            {
                CustomerName = "John Doe",
                TripId = 100,
                ReservationDate = DateTime.UtcNow,
                Notes = "Sample notes"
            };
            command.SetUserId(1);

            // Mocking the repository to return a Reservation with ID = 1
            _reservationRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Reservation>()))
                          .Returns((Reservation reservation) => reservation.Id=1);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _reservationRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Reservation>(r =>
                r.ReservedById == 1 &&
                r.CustomerName == "John Doe" &&
                r.TripId == 100 &&
                r.ReservationDate == command.ReservationDate &&
                r.Notes == "Sample notes"
            )), Times.Once);

            result.Should().Be(1);  // Expecting the ID returned to be 1
        }

        [Fact]
        public void Validate_ShouldHaveError_WhenTripIdIsInvalid()
        {
            // Arrange
            var command = new CreateReservationCommand { CustomerName = "Valid Name", TripId = -1 };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.Errors.Should().Contain(x => x.PropertyName == "TripId");
        }

        [Fact]
        public void Validate_ShouldHaveError_WhenCustomerNameIsEmpty()
        {
            // Arrange
            var command = new CreateReservationCommand { CustomerName = "", TripId = 1 };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.Errors.Should().Contain(x => x.PropertyName == "CustomerName");
        }


    }
}