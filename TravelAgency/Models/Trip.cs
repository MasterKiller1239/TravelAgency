using Google.Cloud.Firestore;

namespace TravelAgency.Models
{
    [FirestoreData]
    public class Trip
    {
        [FirestoreProperty("Id")]
        public int Id { get; set; }
        [FirestoreProperty("Name")]
        public string Name { get; set; } = "";
        [FirestoreProperty("Description")]
        public string Description { get; set; } = "";
        [FirestoreProperty("Price")]
        public int Price { get; set; }
    }
}
