using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using TravelAgency.Models;
using TravelAgency.Services.Interfaces;

namespace TravelAgency.Services
{
    public class FirestoreDataSource : IDataSource
    {
        private readonly FirestoreDb _db;
        private const string CollectionName = "trips";

        public FirestoreDataSource(string projectId, string credentialsPath)
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);

            _db = FirestoreDb.Create(projectId);
        }

       public async Task<List<Trip>> GetTripsAsync()
        {
            var trips = new List<Trip>();
            var snapshot = await _db.Collection(CollectionName).GetSnapshotAsync();

            foreach (var doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    var trip = doc.ConvertTo<Trip>();
                    trips.Add(trip);
                }
            }

            return trips;
        }
        public async Task<Trip> GetTripByIdAsync(int id)
        {
            var snapshot = await _db.Collection(CollectionName)
                .WhereEqualTo("Id", id)
                .GetSnapshotAsync();

            var document = snapshot.Documents.FirstOrDefault();
            return document?.ConvertTo<Trip>();
        }


        public async Task AddTripAsync(Trip trip)
        {
            var docRef = _db.Collection(CollectionName).Document(trip.Id.ToString());
            await docRef.SetAsync(trip);
        }
    }
}
