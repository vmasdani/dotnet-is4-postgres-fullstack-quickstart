using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
    {
    }

    public DbSet<Record>? Records { get; set; }
    public DbSet<Event>? Events { get; set; }
    public DbSet<EventData>? EventDatas { get; set; }

}