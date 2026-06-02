namespace Reservations.Tests
{
    using Application.Reservations.Commands.UpdateReservation;
    using Application.Reservations.Common;
    using Domin.Entities;
    using Domin.interfaces;
    using FluentAssertions;
    using Moq;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateReservationCommandHandlerTests
    {
        private readonly Mock<IGenericRepository<Reservation>> _reservationRepositoryMock;
        private readonly UpdateReservationCommandHandler _handler;

        public UpdateReservationCommandHandlerTests()
        {
            _reservationRepositoryMock = new Mock<IGenericRepository<Reservation>>();
            _handler = new UpdateReservationCommandHandler(_reservationRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateReservation_WhenReservationExists()
        {
            // Arrange
            var reservationId = 1;
            var command = new UpdateReservationCommand
            {
                Id = reservationId,
                CustomerName = "Updated Customer",
                ReservationDate = DateTime.UtcNow.AddDays(1),
                Notes = "Updated notes"
            };

            var existingReservation = new Reservation
            {
                Id = reservationId,
                CustomerName = "Old Customer",
                ReservationDate = DateTime.UtcNow,
                Notes = "Old notes"
            };

            _reservationRepositoryMock.Setup(repo => repo.GetByIdAsync(reservationId))
                                      .ReturnsAsync(existingReservation);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _reservationRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Reservation>(r =>
                r.Id == reservationId &&
                r.CustomerName == "Updated Customer" &&
                r.ReservationDate == command.ReservationDate &&
                r.Notes == "Updated notes"
            )), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenReservationDoesNotExist()
        {
            // Arrange
            var command = new UpdateReservationCommand
            {
                Id = 999, // Non-existent reservation ID
                CustomerName = "Non-Existent",
                ReservationDate = DateTime.UtcNow.AddDays(1),
                Notes = "Some notes"
            };

            _reservationRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id))
                                      .ReturnsAsync((Reservation)null);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                     .WithMessage($"*Reservation*{command.Id}*");
        }
    }

}
