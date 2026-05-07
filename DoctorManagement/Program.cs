using DoctorManagement.Models;
using Microsoft.EntityFrameworkCore;


var doctor =
    new Doctor
    {
        Name = "Dr. Smith",
        Specialization = Specialization.Cardiology,
        DateOfBirth = new DateOnly(1980, 1, 1),
        Description = "Experienced cardiologist"
    };
 
var connectionString = "Server = localhost; Database = DoctorApiDb; User Id = sa; Password = P@ssw0rd!@#$;TrustServerCertificate=True;";

// manually create the database connection and context

var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionsBuilder.UseSqlServer(connectionString);
var connection = new ApplicationDbContext(optionsBuilder.Options);




// create the new data row
connection.Doctors.Add(doctor);
connection.SaveChanges();


//read all doctors
List<Doctor> doctors = connection.Doctors.ToList();
 
doctors.ForEach(d =>
{
    Console.WriteLine($"Id: {d.Id}, Name: {d.Name}, Specialization: {d.Specialization}, DateOfBirth: {d.DateOfBirth}, Description: {d.Description}");
});

//read doctor by id
Doctor? doctorFromDb = connection.Doctors.Find(1L);

if (doctorFromDb != null)
    Console.WriteLine(doctorFromDb.Name);

Appointment appointment = new Appointment
{
    Doctor = doctorFromDb,
    // specify certain date and time for the appointment

        Date = new DateTime(2026,05,02,14,30,00),
         Description = "Regular check-up"
};
connection.Appointments.Add(appointment);
await connection.SaveChangesAsync();





connection.Dispose();
