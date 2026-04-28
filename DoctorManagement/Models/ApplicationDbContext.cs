using Microsoft.EntityFrameworkCore;

namespace DoctorManagement.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
