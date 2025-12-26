
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

    public DbSet<ParkingArea> ParkingArea { get; set; }
    public DbSet<ParkingEvent> ParkingEvent { get; set; }
    public DbSet<User>User{get;set;}


    // requires using Microsoft.Extensions.Configuration;
    private readonly IConfiguration _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration["DbConnectionOptions:connectionString"], o => o.UseNetTopologySuite());
    }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("postgis");
        builder.Entity<ParkingArea>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Area)
                .HasColumnName("Area")
              .HasColumnType("geometry(Polygon, 4326)")
              .IsRequired();

            entity.Property(e => e.MaxCapacity);
            entity.Property(e => e.PlacesLeft);
        });

        builder.Entity<ParkingEvent>(entity =>
   {
       entity.ToTable("ParkingEvent");
       entity.HasKey(e => e.Id);
       entity.Property(e => e.Id).HasColumnName("id");

   });

     builder.Entity<User>(entity =>
   {
       entity.ToTable("User");
       entity.HasKey(e => e.Id);
       entity.Property(e => e.Id).HasColumnName("id");

   });

    // builder.Entity<Coordinate>(entity => entity.HasNoKey());


    }




}

