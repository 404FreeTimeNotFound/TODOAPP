using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TODOAPP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TODOAPP.Controllers.Data;

public class ApiDbContext : IdentityDbContext<IdentityUser>
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    public virtual DbSet<ItemData> Items { get; set; }

}