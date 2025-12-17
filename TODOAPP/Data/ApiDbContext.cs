using Microsoft.EntityFrameworkCore;
using TODOAPP.Models;
namespace TODOAPP.Controllers.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }
    public virtual DbSet<ItemData> Items { get; set; }

}