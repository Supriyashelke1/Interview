using System;
using System.Collections.Generic;

namespace Interview.Data;

public partial class Appointment
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public string Purpose { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
