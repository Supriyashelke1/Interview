using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Interview.Models;

namespace Interview.Controllers;

public class HomeController : Controller
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IBookingRepository _bookingRepository;

    public HomeController(IAppointmentRepository appointmentRepository, IBookingRepository bookingRepository)
    {
        _appointmentRepository = appointmentRepository;
        _bookingRepository = bookingRepository;
    }
    /*private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }*/

    public IActionResult Index()
    {
        var availableAppointments = _appointmentRepository.GetAvailableAppointments();
        var bookedAppointments = _bookingRepository.GetBookedAppointments();

        ViewBag.AvailableAppointments = availableAppointments;
        ViewBag.BookedAppointments = bookedAppointments;

        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
