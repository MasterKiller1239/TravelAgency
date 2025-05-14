// TravelAgency.Tests/TripsControllerTests.cs
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgency.Controllers;
using TravelAgency.Models;
using TravelAgency.Services;
using TravelAgency.Services.Interfaces;
using Xunit;

namespace TravelAgency.Tests
{
    public class TripsControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewWithTrips()
        {
            // Arrange
            var mockDataSource = new Mock<IDataSource>();
            var trips = new List<Trip>
            {
                new Trip { Id = 1, Name = "Trip 1", Price = 100 },
                new Trip { Id = 2, Name = "Trip 2", Price = 200 }
            };

            mockDataSource.Setup(ds => ds.GetTripsAsync()).ReturnsAsync(trips);
            var controller = new TripsController(mockDataSource.Object);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<List<Trip>>(result.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Create_PostValidTrip_RedirectsToIndex()
        {
            // Arrange
            var mockDataSource = new Mock<IDataSource>();
            var newTrip = new Trip { Name = "New Trip", Description = "Desc", Price = 300 };

            mockDataSource.Setup(ds => ds.AddTripAsync(newTrip)).Returns(Task.CompletedTask);
            var controller = new TripsController(mockDataSource.Object);

            // Act
            var result = await controller.Create(newTrip);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Create_ModelStateInvalid_ReturnsViewWithModel()
        {
            // Arrange
            var mockDataSource = new Mock<IDataSource>();
            var controller = new TripsController(mockDataSource.Object);
            controller.ModelState.AddModelError("Name", "Required");

            var trip = new Trip { Description = "Test", Price = 100 };

            // Act
            var result = await controller.Create(trip);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(trip, viewResult.Model);
        }
    }
}
