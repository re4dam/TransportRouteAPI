using Microsoft.EntityFrameworkCore;
using TransportRouteApi.Models;

namespace TransportRouteApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<TransitRoute> TransitRoutes { get; set; }
}