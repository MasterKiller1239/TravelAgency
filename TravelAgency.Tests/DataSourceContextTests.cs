// TravelAgency.Tests/DataSourceContextTests.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TravelAgency.Models;
using TravelAgency.Services;
using TravelAgency.Services.Interfaces;
using Xunit;

namespace TravelAgency.Tests
{
    public class DataSourceContextTests
    {
        [Fact]
        public async Task GetTripsAsync_ReturnsTrips()
        {
            // Arrange
            var mock = new Mock<IDataSource>();
            mock.Setup(ds => ds.GetTripsAsync()).ReturnsAsync(new List<Trip>
            {
                new Trip { Name = "Test 1", Price = 100 }
            });

            var context = new DataSourceContext(mock.Object);

            // Act
            var trips = await context.GetTripsAsync();

            // Assert
            Assert.Single(trips);
            Assert.Equal("Test 1", trips[0].Name);
        }

        [Fact]
        public async Task AddTripAsync_CallsUnderlyingDataSource()
        {
            // Arrange
            var mock = new Mock<IDataSource>();
            var trip = new Trip { Name = "Test", Price = 100 };
            var context = new DataSourceContext(mock.Object);

            // Act
            await context.AddTripAsync(trip);

            // Assert
            mock.Verify(ds => ds.AddTripAsync(trip), Times.Once);
        }
    }
}
