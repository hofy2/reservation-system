namespace Reservations.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Reservations.Queries.GetReservationById;
    using Domin.Entities;
    using Domin.interfaces;
    using FluentAssertions;
    using Moq;
    using Xunit;


    public class GetReservationByIdQueryHandlerTests
    {
        private readonly Mock<IGenericRepository<Reservation>> _reservationRepositoryMock;
        private readonly GetReservationByIdQueryHandler _handler;

        public GetReservationByIdQueryHandlerTests()
        {
            _reservationRepositoryMock = new Mock<IGenericRepository<Reservation>>();
            _handler = new GetReservationByIdQueryHandler(_reservationRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnReservation_WhenReservationExists()
        {
            // Arrange
            var reservationId = 1;
            var reservation = new Reservation
            {
                Id = reservationId,
                CustomerName = "John Doe",
                TripId = 100,
                ReservedById = 1,
                ReservationDate = DateTime.UtcNow
            };

            _reservationRepositoryMock.Setup(repo => repo.GetByIdAsync(reservationId))
                                      .ReturnsAsync(reservation);

            var query = new GetReservationByIdQuery { Id = reservationId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(reservation);
            _reservationRepositoryMock.Verify(repo => repo.GetByIdAsync(reservationId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservationId = 999; // Assume this ID does not exist
            _reservationRepositoryMock.Setup(repo => repo.GetByIdAsync(reservationId))
                                      .ReturnsAsync((Reservation)null);

            var query = new GetReservationByIdQuery { Id = reservationId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
            _reservationRepositoryMock.Verify(repo => repo.GetByIdAsync(reservationId), Times.Once);
        }
    }
}
