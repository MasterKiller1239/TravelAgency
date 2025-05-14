// TravelAgency.Tests/SqlDataSourceTests.cs
using System.Threading.Tasks;
using TravelAgency.Models;
using TravelAgency.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using TravelAgency.Data;
using Google.Cloud.Firestore;
using Moq;

namespace TravelAgency.Tests
{
    public class FirestoreDataSourceTests
    {
        [Fact]
        public void Test_Constructor_ShouldSetCredentials()
        {
            // Arrange
            var firebaseProjectId = "travelagency-6f8ba";
            var credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "Secrets", "serviceAccountKey.json");

            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);

            // Act
            var firestoreDataSource = new FirestoreDataSource(firebaseProjectId, credentialsPath);

        }
     
    }
}
