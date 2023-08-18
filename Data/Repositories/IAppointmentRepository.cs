using System.Collections.Generic;
using Interview.Data;

public interface IAppointmentRepository
{
    List<Appointment> GetAvailableAppointments();
    
}