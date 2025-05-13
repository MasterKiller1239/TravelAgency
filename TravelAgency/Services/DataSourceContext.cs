using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgency.Models;

namespace TravelAgency.Services
{
    public class DataSourceContext
    {
        private readonly IDataSource _dataSource;

        public DataSourceContext(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<List<Trip>> GetTripsAsync()
        {
            var trips = await _dataSource.GetTripsAsync();
            return trips != null ? new List<Trip>(trips) : new List<Trip>();
        }

        public Task<Trip> GetTripByIdAsync(int id) => _dataSource.GetTripByIdAsync(id);

        public Task AddTripAsync(Trip trip) => _dataSource.AddTripAsync(trip);
    }
}
