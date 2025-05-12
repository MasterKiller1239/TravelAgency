using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Trip name")]
        public required string Name { get; set; }

        [Display(Name = "Description")]
        public required string Description { get; set; }

        [Display(Name = "Price (PLN)")]
        [Range(0, 100000)]
        public decimal Price { get; set; }
    }
}
