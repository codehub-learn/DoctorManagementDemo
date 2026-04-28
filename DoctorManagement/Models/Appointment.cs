using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorManagement.Models;

public class Appointment
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public Doctor? Doctor { get; set; } = null;
    public string? Description { get; set; }
}
