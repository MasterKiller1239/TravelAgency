using TravelAgency.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgency.Data;
using TravelAgency.Services.Interfaces;

namespace TravelAgency.Services
{
    public class SqlDataSource : IDataSource
    {
        private readonly TravelAgencyContext _context;

        public SqlDataSource(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trip>> GetTripsAsync()
        {
            return await _context.Trips.ToListAsync();
        }

        public async Task<Trip> GetTripByIdAsync(int id)
        {
            return await _context.Trips.FindAsync(id);
        }

        public async Task AddTripAsync(Trip trip)
        {
            try
            {
                if (trip == null)
                {
                    throw new ArgumentNullException(nameof(trip));
                }

                _context.Trips.Add(trip);
                _context.SaveChanges();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw new Exception("Error occurred while saving the trip to the database.", ex);
            }
        }
    }
}
