using Microsoft.AspNetCore.Mvc;
using TravelAgency.Models;
using TravelAgency.Services;
using System.Threading.Tasks;

namespace TravelAgency.Controllers
{
    public class TripsController : Controller
    {
        private readonly IDataSource _dataSource;

        public TripsController(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<IActionResult> Index()
        {
            var trips = await _dataSource.GetTripsAsync();
            return View(trips); 
        }

        // GET: /Trips/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var trip = await _dataSource.GetTripByIdAsync(id);
            if (trip == null)
            {
                return NotFound(); 
            }
            return View(trip); 
        }

        // GET: /Trips/Create
        public IActionResult Create()
        {
            return View(); 
        }

        // POST: /Trips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.Id = 0;
                await _dataSource.AddTripAsync(trip); 
                return RedirectToAction(nameof(Index)); 
            }
            return View(trip); 
        }
    }
}
