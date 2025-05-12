using Microsoft.AspNetCore.Mvc;
using TravelAgency.Models;
using TravelAgency.Data;

namespace TravelAgency.Controllers
{
    public class TripsController : Controller
    {
        public IActionResult Index()
        {
            var trips = FakeContext.GetAll();
            return View(trips);
        }

        public IActionResult Details(int id)
        {
            var trip = FakeContext.GetById(id);
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
        public IActionResult Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                FakeContext.Add(trip);
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }
    }
}
