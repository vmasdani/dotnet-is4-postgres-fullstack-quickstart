using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    private WebApplicationBuilder? _builder;

    public ApplicationContext(WebApplicationBuilder? builder)
    {
        _builder = builder;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(_builder?.Configuration.GetConnectionString("PostgresUrl"));
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
    {
    }

    public DbSet<Record>? Records { get; set; }
    public DbSet<Event>? Events { get; set; }
    public DbSet<EventData>? EventDatas { get; set; }

}