namespace DoctorManagement.Models;

public class Doctor
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string? Description { get; set; }
    public Specialization Specialization { get; set; }
}
