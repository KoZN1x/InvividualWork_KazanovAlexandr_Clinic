using Clinic_IndividualWork_KazanovAlexandr.Context;
using Clinic_IndividualWork_KazanovAlexandr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Clinic_IndividualWork_KazanovAlexandr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContextOptions<ApplicationDbContext> _context;

        public HomeController(ILogger<HomeController> logger, DbContextOptions<ApplicationDbContext> context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            using (ApplicationDbContext db = new ApplicationDbContext(_context))
            {
                var doctors = db.Doctor.ToList();
                return View(doctors);
            }
        }

        public IActionResult Privacy()
        {
            using (ApplicationDbContext db = new ApplicationDbContext(_context))
            {
                var patients = db.Patient.ToList();
                return View(patients);
            }
        }
        public IActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(Patient patient)
        {
            using (ApplicationDbContext db = new ApplicationDbContext(_context))
            {
                db.Patient.Add(patient);
                await db.SaveChangesAsync();
                return RedirectToAction("Privacy");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}