using System.Collections.Generic;
using Interview.Data;
using Microsoft.EntityFrameworkCore;

public class BookingRepository : IBookingRepository
{
    private readonly InterviewDbContext _context;

    public BookingRepository(InterviewDbContext context)
    {
        _context = context;
    }

    public List<Booking> GetBookedAppointments()
    {
        List<Booking> bookedAppointments = _context.Bookings.Include(b => b.Appointment).ToList();
        return bookedAppointments;
    }
}