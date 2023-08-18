using System.Collections.Generic;
using Interview.Data;

public interface IBookingRepository
{
    List<Booking> GetBookedAppointments();
  
}