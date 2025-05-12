using TravelAgency.Models;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgency.Data
{
    public static class FakeContext
    {
        private static List<Trip> _trips = new List<Trip>
        {
            new Trip { Id = 1, Name = "Italy - Rome", Description = "5 days of sightseeing", Price = 2500 },
            new Trip { Id = 2, Name = "Greece - Athens", Description = "A week of vacation", Price = 3200 }
        };

        public static List<Trip> GetAll() => _trips;

        public static Trip GetById(int id) => _trips.FirstOrDefault(x => x.Id == id);

        public static void Add(Trip trip)
        {
            trip.Id = _trips.Max(x => x.Id) + 1;
            _trips.Add(trip);
        }
    }
}
