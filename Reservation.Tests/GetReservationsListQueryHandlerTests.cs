
namespace Reservation.Tests
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Reservations.Common.DTOs;
    using Application.Reservations.Queries.GetReservations;
    using AutoMapper;
    using Domin.Entities;
    using Domin.interfaces;
    using Moq;
    using Xunit;
    using FluentAssertions;

    public class GetReservationsListQueryHandlerTests
    {
        private readonly Mock<IGenericRepository<Reservation>> _reservationRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetReservationsListQueryHandler _handler;

        public GetReservationsListQueryHandlerTests()
        {
            _reservationRepositoryMock = new Mock<IGenericRepository<Reservation>>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetReservationsListQueryHandler(_reservationRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedReservations_WhenReservationsExist()
        {
            // Arrange
            var reservations = new List<Reservation>
        {
            new Reservation { Id = 1, CustomerName = "John Doe", TripId = 100, ReservedById = 1 },
            new Reservation { Id = 2, CustomerName = "Jane Smith", TripId = 101, ReservedById = 2 }
        };

            var reservationDtos = new List<ReservationDto>
        {
            new ReservationDto { Id = 1, CustomerName = "John Doe", TripId = 100 },
            new ReservationDto { Id = 2, CustomerName = "Jane Smith", TripId = 101 }
        };

            _reservationRepositoryMock.Setup(repo => repo.GetAllAsync())
                                      .ReturnsAsync(reservations);

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ReservationDto>>(reservations))
                       .Returns(reservationDtos);

            var query = new GetReservationsListQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(reservationDtos);

            _reservationRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ReservationDto>>(reservations), Times.Once);
        }
    }

}
