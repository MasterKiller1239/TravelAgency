// TravelAgency.Tests/SqlDataSourceTests.cs
using System.Threading.Tasks;
using TravelAgency.Models;
using TravelAgency.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using TravelAgency.Data;

namespace TravelAgency.Tests
{
    public class SqlDataSourceTests
    {
        private TravelAgencyContext GetContext()
        {
            var options = new DbContextOptionsBuilder<TravelAgencyContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            return new TravelAgencyContext(options);
        }

        [Fact]
        public async Task AddTripAsync_AddsTripSuccessfully()
        {
            // Arrange
            var context = GetContext();
            var dataSource = new SqlDataSource(context);
            var trip = new Trip { Name = "Test", Description = "Test Desc", Price = 100 };

            // Act
            await dataSource.AddTripAsync(trip);
            var trips = await dataSource.GetTripsAsync();

            // Assert
            Assert.Single(trips);
            Assert.Equal("Test", trips[0].Name);
        }
    }
}
