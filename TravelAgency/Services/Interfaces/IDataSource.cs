using TravelAgency.Models;
namespace TravelAgency.Services
{
    public interface IDataSource
    {
        Task<IEnumerable<Trip>> GetTripsAsync();
        Task<Trip> GetTripByIdAsync(int id);
        Task AddTripAsync(Trip trip);
    }
}
