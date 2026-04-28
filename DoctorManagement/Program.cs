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
Doctor? doctorFromDb = connection.Doctors.Find(1);

Console.WriteLine(doctorFromDb.Name);

