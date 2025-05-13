using Google.Cloud.Firestore;
using TravelAgency.Models;

namespace TravelAgency.Services.Old
{
    public class FirestoreService
    {
        private readonly FirestoreDb _firestoreDb;
        private const string CollectionName = "trips";

        public FirestoreService(string projectId, string credentialsPath)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);
            _firestoreDb = FirestoreDb.Create(projectId);
        }

        public async Task<List<Trip>> GetAllTripsAsync()
        {
            var trips = new List<Trip>();
            QuerySnapshot snapshot = await _firestoreDb.Collection(CollectionName).GetSnapshotAsync();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    try
                    {
                        Trip trip = doc.ConvertTo<Trip>();
                        trip.Id = int.Parse(doc.Id);
                        trips.Add(trip);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd podczas deserializacji dokumentu {doc.Id}: {ex.Message}");
                    }
                }
            }
            return trips;
        }

        public async Task AddTripAsync(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            CollectionReference tripsRef = _firestoreDb.Collection(CollectionName);

            var trips = await GetAllTripsAsync();
            int newId = trips.Count > 0 ? trips.Max(t => t.Id) + 1 : 1;
            trip.Id = newId;

            DocumentReference docRef = tripsRef.Document(newId.ToString());
            await docRef.SetAsync(trip, SetOptions.Overwrite);
        }
    }
}