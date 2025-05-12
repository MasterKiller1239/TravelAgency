using Microsoft.AspNetCore.Mvc;
using TravelAgency.Models;
using TravelAgency.Services;
using System.Threading.Tasks;

namespace TravelAgency.Controllers
{
    public class TripsController : Controller
    {
        private readonly FirestoreService _firestoreService;

        public TripsController(FirestoreService firestoreService)
        {
            _firestoreService = firestoreService;
        }

        public async Task<IActionResult> Index()
        {
            var trips = await _firestoreService.GetAllTripsAsync();
            return View(trips);
        }

        public async Task<IActionResult> Details(int id)
        {
            var trips = await _firestoreService.GetAllTripsAsync();
            var trip = trips.FirstOrDefault(t => t.Id == id);
            if (trip == null)
                return NotFound();
            return View(trip);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                await _firestoreService.AddTripAsync(trip);
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }
    }
}
