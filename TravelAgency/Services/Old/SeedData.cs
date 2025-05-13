using System.Text.Json;
using System.Threading.Tasks;
using TravelAgency.Models;

namespace TravelAgency.Services.Old
{
    public class SeedData
    {
        public static async Task InitializeAsync(FirestoreService firestoreService)
        {

            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "trips.json");
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine("trips.json file not found.");
                return;
            }

            var jsonData = await File.ReadAllTextAsync(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var trips = JsonSerializer.Deserialize<List<Trip>>(jsonData, options);

            if (trips == null || trips.Count == 0)
            {
                Console.WriteLine("No trips found in JSON.");
                return;
            }

            Console.WriteLine($"Seeding {trips.Count} trips...");

            foreach (var trip in trips)
            {
                await firestoreService.AddTripAsync(trip);
                Console.WriteLine($"Added trip: {trip.Name}");
            }

            Console.WriteLine("Seeding completed.");
        }
    }
}
