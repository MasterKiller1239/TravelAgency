using TravelAgency.Models;
namespace TravelAgency.Services.Interfaces
{
    public interface IDataSource
    {
        Task<List<Trip>> GetTripsAsync();
        Task<Trip> GetTripByIdAsync(int id);
        Task AddTripAsync(Trip trip);
    }
}
