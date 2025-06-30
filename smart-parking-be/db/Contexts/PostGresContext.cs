
using Microsoft.EntityFrameworkCore;
using smartparking.db.parkingarea;
using NetTopologySuite.Geometries;
using smartparking.db.parkingevent;
using Npgsql.EntityFrameworkCore.PostgreSQL;

public class PostGresContext(IConfiguration configuration) : DbContext
{
    public string connectionString = "Server=localhost; User ID=root; Password=pass; Database=blog";

    // public DbSet<User> UTENTE { get; set; }
    // public DbSet<Order> ORDINE { get; set; }
    // public DbSet<DeliveryCompany> SOCIETÀ_CONSEGNA { get; set; }
    // public DbSet<Menu> MENU { get; set; }
    // public DbSet<Local> LOCALE { get; set; }
    //  public DbSet<Dish> PIATTO { get; set; }

    public  DbSet<ParkingArea> ParkingArea { get; set; }
    public  DbSet<ParkingEvent> ParkingEvent { get; set; }


    // requires using Microsoft.Extensions.Configuration;
    private readonly IConfiguration _configuration = configuration;

    //   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql(_configuration["DbConnectionOptions:connectionString"], o => o.UseNetTopologySuite());
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        connectionString = _configuration["DbConnectionOptions:connectionString"];
        // optionsBuilder.EnableSensitiveDataLogging();
        if (connectionString != null)
            optionsBuilder.UseNpgsql(connectionString);
        else
            throw new Exception($"connection string: {connectionString} is evaluated as null");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("postgis");
        
        
}
    



}

