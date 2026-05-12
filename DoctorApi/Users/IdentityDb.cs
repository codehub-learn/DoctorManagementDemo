using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoctorApi.Users;

public class IdentityDb
    : IdentityDbContext<ApplicationUser>
{
    public IdentityDb(
        DbContextOptions<IdentityDb> options)
        : base(options)
    {
    }
}