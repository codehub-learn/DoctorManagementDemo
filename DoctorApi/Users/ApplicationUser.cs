using Microsoft.AspNetCore.Identity;

namespace DoctorApi.Users;

public class ApplicationUser:IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
