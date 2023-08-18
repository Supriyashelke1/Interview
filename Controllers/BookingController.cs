using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Interview.Data;


namespace Interview.Controllers;
public class BookingController : Controller
{
    private readonly InterviewDbContext _context;

    public BookingController(InterviewDbContext context)
    {
        _context = context;
    }

    public IActionResult ListBooked()
    {
        List<Booking> bookings = _context.Bookings.Include(b => b.Appointment).ToList();
        return View(bookings);
    }
}