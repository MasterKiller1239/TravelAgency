using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    [FirestoreData]
    public class Trip
    {
        [Key]
        [FirestoreProperty("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [FirestoreProperty("Name")]
        public string Name { get; set; } = "";

        [StringLength(500)]
        [FirestoreProperty("Description")]
        public string? Description { get; set; } = "";

        [Required]
        [FirestoreProperty("Price")]
        public int Price { get; set; }
    }
}