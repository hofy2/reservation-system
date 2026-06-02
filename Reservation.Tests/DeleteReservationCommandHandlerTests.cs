namespace Reservations.Tests
{
using System.Threading;
using System.Threading.Tasks;
using Application.Reservations.Commands.DeleteReservation;
using Application.Reservations.Common;
using Domin.Entities;
using Domin.interfaces;
using FluentAssertions;
using Moq;
using OpenQA.Selenium.DevTools.V85.Schema;
using Xunit;


    public class DeleteReservationCommandHandlerTests
    {
        private readonly Mock<IGenericRepository< Reservation>> _reservationRepositoryMock;
        private readonly DeleteReservationCommandHandler _handler;

        public DeleteReservationCommandHandlerTests()
        {
            _reservationRepositoryMock = new Mock<IGenericRepository<Reservation>>();
            _handler = new DeleteReservationCommandHandler(_reservationRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteReservation_WhenReservationExists()
        {
            // Arrange
            var reservationId = 1;
            var reservation = new Reservation { Id = reservationId };

            _reservationRepositoryMock.Setup(repo => repo.GetByIdAsync(reservationId))
                                      .ReturnsAsync(reservation);

            _reservationRepositoryMock.Setup(repo => repo.DeleteAsync(reservationId))
                                      .Returns(Task.CompletedTask);

            var command = new DeleteReservationCommand { Id = reservationId };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _reservationRepositoryMock.Verify(repo => repo.GetByIdAsync(reservationId), Times.Once);
            _reservationRepositoryMock.Verify(repo => repo.DeleteAsync(reservationId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservationId = 999; // Assume this ID does not exist
            _reservationRepositoryMock.Setup(repo => repo.GetByIdAsync(reservationId))
                                      .ReturnsAsync((Reservation)null);

            var command = new DeleteReservationCommand { Id = reservationId };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));

            _reservationRepositoryMock.Verify(repo => repo.GetByIdAsync(reservationId), Times.Once);
            _reservationRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
