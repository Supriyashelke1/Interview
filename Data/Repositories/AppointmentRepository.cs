using System.Collections.Generic;
using Interview.Data;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly InterviewDbContext _context;

    public AppointmentRepository(InterviewDbContext context)
    {
        _context = context;
    }

    public List<Appointment> GetAvailableAppointments()
    {
        List<Appointment> availableAppointments = _context.Appointments.ToList();
        return availableAppointments;
    }
}