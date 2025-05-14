// TravelAgency.Tests/TripModelValidationTests.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TravelAgency.Models;
using Xunit;

namespace TravelAgency.Tests
{
    public class TripModelValidationTests
    {
        private IList<ValidationResult> ValidateModel(object model)
        {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [Fact]
        public void Trip_WithMissingName_ShouldBeInvalid()
        {
            var trip = new Trip
            {
                Name = "",
                Description = "Some description",
                Price = 100
            };

            var results = ValidateModel(trip);
            Assert.Contains(results, r => r.MemberNames.Contains("Name"));
        }

        [Fact]
        public void Trip_WithValidData_ShouldBeValid()
        {
            var trip = new Trip
            {
                Name = "Valid Trip",
                Description = "Description",
                Price = 500
            };

            var results = ValidateModel(trip);
            Assert.Empty(results);
        }
    }
}
